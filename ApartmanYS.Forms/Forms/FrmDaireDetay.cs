using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Daire detay modal formu
    /// </summary>
    public partial class FrmDaireDetay : DevExpress.XtraEditors.XtraForm
    {
        private int _daireId;

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmDaireDetay(int daireId)
        {
            InitializeComponent();
            _daireId = daireId;
            DaireBilgileriniYukle();
        }

        /// <summary>
        /// Daire bilgilerini yükle
        /// </summary>
        private void DaireBilgileriniYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Daire ve sakin bilgilerini çek
                    var query = @"SELECT d.DaireNo, k.KatNo, ds.AdSoyad, ds.Telefon
                                  FROM Daire d
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  LEFT JOIN DaireSahibi ds ON d.DaireId = ds.DaireId AND ds.Aktif = 1
                                  WHERE d.DaireId = @DaireId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DaireId", _daireId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LblDaireNo.Text = $"Kat {reader["KatNo"]} - Daire {reader["DaireNo"]}";
                                LblSahip.Text = reader["AdSoyad"] != DBNull.Value ? reader["AdSoyad"].ToString() : "Boş";
                                LblTelefon.Text = reader["Telefon"] != DBNull.Value ? reader["Telefon"].ToString() : "-";
                            }
                        }
                    }

                    // Aidat bilgilerini çek
                    var aidatQuery = @"SELECT Tutar, Tarih, OdendiMi
                                      FROM Aidat
                                      WHERE DaireId = @DaireId
                                      ORDER BY Tarih DESC";

                    using (var adapter = new SqlDataAdapter(aidatQuery, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@DaireId", _daireId);
                        var ds = new DataSet();
                        adapter.Fill(ds, "Aidatlar");
                        GridAidat.DataSource = ds.Tables["Aidatlar"];
                    }

                    // Araç bilgilerini çek
                    var aracQuery = @"SELECT Plaka
                                     FROM Arac
                                     WHERE DaireId = @DaireId";

                    using (var aracAdapter = new SqlDataAdapter(aracQuery, connection))
                    {
                        aracAdapter.SelectCommand.Parameters.AddWithValue("@DaireId", _daireId);
                        var ds = new DataSet();
                        aracAdapter.Fill(ds, "Araclar");
                        GridArac.DataSource = ds.Tables["Araclar"];
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kapat butonu
        /// </summary>
        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
