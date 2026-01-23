using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Yeni aidat ekleme formu
    /// </summary>
    public partial class FrmAidatEkle : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FrmAidatEkle()
        {
            InitializeComponent();
            DaireleriYukle();
            DtTarih.DateTime = DateTime.Now;
        }

        /// <summary>
        /// Daireleri yükle
        /// </summary>
        private void DaireleriYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = @"SELECT d.DaireId, CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireBilgi 
                                  FROM Daire d
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  ORDER BY k.KatNo, d.DaireNo";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var daireId = (int)reader["DaireId"];
                                var daireBilgi = reader["DaireBilgi"].ToString();
                                CmbDaire.Properties.Items.Add(new { DaireId = daireId, DaireBilgi = daireBilgi });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kaydet butonu
        /// </summary>
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (CmbDaire.SelectedIndex == -1)
            {
                XtraMessageBox.Show("Lütfen bir daire seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(TxtTutar.Text))
            {
                XtraMessageBox.Show("Lütfen tutar giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(TxtTutar.Text, out decimal tutar))
            {
                XtraMessageBox.Show("Lütfen geçerli bir tutar giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = CmbDaire.Properties.Items[CmbDaire.SelectedIndex];
            var daireId = (int)selectedItem.GetType().GetProperty("DaireId").GetValue(selectedItem);
            var tarih = DtTarih.DateTime;
            var yil = tarih.Year;
            var ay = tarih.Month;

            var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = @"INSERT INTO Aidat (DaireId, Yil, Ay, Tutar, Tarih, OdendiMi)
                                  VALUES (@DaireId, @Yil, @Ay, @Tutar, @Tarih, 0)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DaireId", daireId);
                        command.Parameters.AddWithValue("@Yil", yil);
                        command.Parameters.AddWithValue("@Ay", ay);
                        command.Parameters.AddWithValue("@Tutar", tutar);
                        command.Parameters.AddWithValue("@Tarih", tarih);

                        command.ExecuteNonQuery();
                    }
                }

                XtraMessageBox.Show("Aidat başarıyla eklendi!", "Başarılı",
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
