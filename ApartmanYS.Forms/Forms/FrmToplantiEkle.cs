using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Yeni toplantı ekleme formu
    /// </summary>
    public partial class FrmToplantiEkle : DevExpress.XtraEditors.XtraForm
    {
        private int _userId;

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmToplantiEkle(int userId)
        {
            InitializeComponent();
            _userId = userId;
            DtToplantiTarihi.DateTime = DateTime.Now;
        }

        /// <summary>
        /// Kaydet butonu
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            var konusu = TxtToplantiKonusu.Text.Trim();
            var icerik = TxtToplantiIcerik.Text.Trim();
            var kararlari = TxtToplantiKararlari.Text.Trim();
            var tarih = DtToplantiTarihi.DateTime;
            var katilanSayisi = Convert.ToInt32(SpinKatilanSayisi.Value);

            if (string.IsNullOrEmpty(konusu))
            {
                XtraMessageBox.Show("Lütfen toplantı konusunu giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(kararlari))
            {
                XtraMessageBox.Show("Lütfen toplantı kararlarını giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (katilanSayisi < 0)
            {
                XtraMessageBox.Show("Katılan sayısı 0'dan küçük olamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = @"INSERT INTO Toplanti (ToplantiTarihi, ToplantiKonusu, ToplantiIcerik, ToplantiKararlari, KatilanSayisi, OlusturmaTarihi, OlusturanKullaniciId, Aktif)
                                  VALUES (@ToplantiTarihi, @ToplantiKonusu, @ToplantiIcerik, @ToplantiKararlari, @KatilanSayisi, GETDATE(), @OlusturanKullaniciId, 1)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ToplantiTarihi", tarih);
                        command.Parameters.AddWithValue("@ToplantiKonusu", konusu);
                        command.Parameters.AddWithValue("@ToplantiIcerik", string.IsNullOrEmpty(icerik) ? (object)DBNull.Value : icerik);
                        command.Parameters.AddWithValue("@ToplantiKararlari", kararlari);
                        command.Parameters.AddWithValue("@KatilanSayisi", katilanSayisi);
                        command.Parameters.AddWithValue("@OlusturanKullaniciId", _userId);

                        command.ExecuteNonQuery();
                    }
                }

                XtraMessageBox.Show("Toplantı başarıyla eklendi!", "Başarılı",
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

