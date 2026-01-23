using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Giriş ekranı
    /// </summary>
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            YukleGorsel();
        }

        /// <summary>
        /// Görseli yükle
        /// </summary>
        private void YukleGorsel()
        {
            try
            {
                var imagePath = System.IO.Path.Combine(Application.StartupPath, "images", "user.png");
                if (System.IO.File.Exists(imagePath))
                {
                    PictureEdit.Image = System.Drawing.Image.FromFile(imagePath);
                }
            }
            catch
            {
                // Görsel yüklenemezse devam et
            }
        }

        /// <summary>
        /// Giriş butonu tıklandığında
        /// </summary>
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            var kullaniciAdi = TxtKullaniciAdi.Text.Trim();
            var sifre = TxtSifre.Text.Trim();

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                XtraMessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Veritabanından kullanıcı kontrolü
            var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT KullaniciId, KullaniciAdi, AdSoyad, KullaniciTipi 
                                  FROM Kullanici 
                                  WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre AND Aktif = 1";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@Sifre", sifre);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Giriş başarılı
                                var userId = reader.GetInt32(0);
                                var dbKullaniciAdi = reader.GetString(1);
                                var kullaniciTipi = reader.GetString(3);
                                var adSoyad = reader.GetString(2);

                                // Kullanıcı tipine göre yönlendir
                                this.Hide();
                                
                                if (kullaniciTipi == "Admin")
                                {
                                    var frmAdminPanel = new FrmAdminPanel(userId, adSoyad, dbKullaniciAdi);
                                    frmAdminPanel.ShowDialog();
                                }
                                else
                                {
                                    var frmAnaEkran = new FrmAnaEkran(userId, adSoyad, kullaniciTipi, dbKullaniciAdi);
                                    frmAnaEkran.ShowDialog();
                                }
                                
                                this.Close();
                            }
                            else
                            {
                                XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Çıkış butonu tıklandığında
        /// </summary>
        private void BtnCikis_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
