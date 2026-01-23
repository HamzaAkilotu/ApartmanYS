using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Yeni kullanıcı ekleme formu
    /// </summary>
    public partial class FrmKullaniciEkle : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmKullaniciEkle()
        {
            InitializeComponent();
            CmbKullaniciTipi.SelectedIndex = 1; // Default olarak User seçili
        }

        /// <summary>
        /// Kaydet butonu
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            var adSoyad = TxtAdSoyad.Text.Trim();
            var kullaniciAdi = TxtKullaniciAdi.Text.Trim();
            var sifre = TxtSifre.Text.Trim();
            var kullaniciTipi = CmbKullaniciTipi.Text;
            var email = TxtEmail.Text.Trim();
            var telefon = TxtTelefon.Text.Trim();

            // Zorunlu alanlar kontrolü
            if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                XtraMessageBox.Show("Lütfen zorunlu alanları (Ad Soyad, Kullanıcı Adı, Şifre) doldurunuz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kullanıcı adı kontrolü (sadece kullanıcı adı unique olmalı)
                    var checkQuery = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi = @KullaniciAdi";
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        var count = (int)checkCmd.ExecuteScalar();
                        
                        if (count > 0)
                        {
                            XtraMessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Email ve telefon için kontrol yok - aynı email ve telefon da girilebilir
                    // INSERT sorgusuna Email ve Telefon ekleniyor
                    var insertQuery = @"INSERT INTO Kullanici (AdSoyad, KullaniciAdi, Sifre, KullaniciTipi, Email, Telefon, Aktif, OlusturmaTarihi)
                                       VALUES (@AdSoyad, @KullaniciAdi, @Sifre, @KullaniciTipi, @Email, @Telefon, 1, GETDATE())";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@Sifre", sifre);
                        command.Parameters.AddWithValue("@KullaniciTipi", kullaniciTipi);
                        
                        // Email ve telefon NULL olabilir
                        if (string.IsNullOrEmpty(email))
                            command.Parameters.AddWithValue("@Email", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Email", email);
                        
                        if (string.IsNullOrEmpty(telefon))
                            command.Parameters.AddWithValue("@Telefon", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Telefon", telefon);

                        var affectedRows = command.ExecuteNonQuery();
                        
                        if (affectedRows > 0)
                        {
                            XtraMessageBox.Show("Kullanıcı başarıyla eklendi!", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            XtraMessageBox.Show("Kullanıcı eklenirken bir hata oluştu!", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                XtraMessageBox.Show($"Veritabanı hatası: {sqlEx.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// İptal butonu
        /// </summary>
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
