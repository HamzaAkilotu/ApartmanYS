using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Yeni duyuru ekleme formu
    /// </summary>
    public partial class FrmDuyuruEkle : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmDuyuruEkle()
        {
            InitializeComponent();
            // Varsayılan olarak bir ay sonrası tarih
            DtBitisTarihi.DateTime = DateTime.Now.AddMonths(1);
        }

        /// <summary>
        /// Kaydet butonu
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            var baslik = TxtDuyuruBaslik.Text.Trim();
            var icerik = TxtDuyuruIcerik.Text.Trim();
            var bitisTarihi = DtBitisTarihi.DateTime;

            if (string.IsNullOrEmpty(baslik) || string.IsNullOrEmpty(icerik))
            {
                XtraMessageBox.Show("Lütfen başlık ve içerik alanlarını doldurunuz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bitisTarihi < DateTime.Now.Date)
            {
                XtraMessageBox.Show("Bitiş tarihi bugünden önce olamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = @"INSERT INTO Duyuru (Baslik, Icerik, OlusturmaTarihi, GecerlilikBitis, Aktif)
                                  VALUES (@Baslik, @Icerik, GETDATE(), @BitisTarihi, 1)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Baslik", baslik);
                        command.Parameters.AddWithValue("@Icerik", icerik);
                        command.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);

                        command.ExecuteNonQuery();
                    }
                }

                XtraMessageBox.Show("Duyuru başarıyla eklendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
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
