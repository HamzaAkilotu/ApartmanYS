using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ApartmanYS.Forms.Services;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Admin Panel - Modern ve göz alıcı tasarım
    /// </summary>
    public partial class FrmAdminPanel : XtraForm
    {
        private string _adSoyad;
        private string _kullaniciAdi;
        private int _userId;
        private GridControl _gridDuyurular;
        private GridView _viewDuyurular;
        private GridControl _gridKullanicilar;
        private GridView _viewKullanicilar;
        private GridControl _gridAidatGider;
        private GridView _viewAidatGider;
        private GridControl _gridKapici;
        private GridView _viewKapici;
        private GridControl _gridToplanti;
        private GridView _viewToplanti;
        private GridControl _gridSikayetler;
        private GridView _viewSikayetler;

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmAdminPanel(int userId, string adSoyad, string kullaniciAdi = "")
        {
            InitializeComponent();
            _userId = userId;
            _adSoyad = adSoyad;
            _kullaniciAdi = kullaniciAdi;
            LblWelcome.Text = $"Admin Paneli - {adSoyad}";
            
            // Admin.png resmini yükle
            try
            {
                string[] possiblePaths = {
                    Application.StartupPath + @"\images\admin.png",
                    Application.StartupPath + @"\admin.png",
                    Application.StartupPath + @"\..\images\admin.png",
                    Application.StartupPath + @"\Resources\admin.png",
                    Application.StartupPath + @"\..\..\images\admin.png",
                    System.IO.Path.Combine(Application.StartupPath, "images", "admin.png"),
                    System.IO.Path.Combine(Application.StartupPath, "admin.png"),
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "images", "admin.png"),
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "admin.png")
                };
                
                bool resimYuklendi = false;
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        PicAdmin.Image = Image.FromFile(path);
                        resimYuklendi = true;
                        break;
                    }
                }
                
                // Eğer resim bulunamadıysa, basit bir ikon oluştur
                if (!resimYuklendi)
                {
                    var bmp = new Bitmap(40, 40);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0, 150, 136)), 0, 0, 40, 40);
                        g.DrawString("👤", new Font("Segoe UI", 24), Brushes.White, new PointF(5, 5));
                    }
                    PicAdmin.Image = bmp;
                }
            }
            catch
            {
                // Hata durumunda varsayılan ikon
                var bmp = new Bitmap(40, 40);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 150, 136)), 0, 0, 40, 40);
                    g.DrawString("👤", new Font("Segoe UI", 24), Brushes.White, new PointF(5, 5));
                }
                PicAdmin.Image = bmp;
            }
            
            // İlk açılışta daire yönetimini göster
            MenuButton_Click(BtnDaireYonetim, null);
        }

        /// <summary>
        /// Menü butonları tıklandığında
        /// </summary>
        private void MenuButton_Click(object sender, EventArgs e)
        {
            // Tüm butonları eski haline getir
            ResetButtons();

            var btn = sender as SimpleButton;
            if (btn != null)
            {
                // Seçili butonu vurgula
                btn.Appearance.BackColor = System.Drawing.Color.FromArgb(38, 50, 56);
                btn.Appearance.ForeColor = System.Drawing.Color.White;
                btn.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);

                // Seçili menüyü göster
                string menuAdi = btn.Text.Replace("🏠", "").Replace("💰", "").Replace("📢", "").Replace("👥", "").Replace("👨‍💼", "").Replace("🏊", "").Replace("🚗", "").Replace("📝", "").Replace("⚡", "").Replace("🏛️", "").Replace("🤖", "").Replace("  ", " ").Trim();
                LblSeciliMenu.Text = menuAdi;
                
                // İçerik alanını temizle
                PnlMain.Controls.Clear();
                
                // Menüye göre içerik yükle
                if (menuAdi == "Daire Yönetimi")
                {
                    DaireYonetimGoster();
                }
                else if (menuAdi == "Aidat/Gider")
                {
                    AidatGiderGoster();
                }
                else if (menuAdi == "Duyurular")
                {
                    DuyurularGoster();
                }
                else if (menuAdi == "Kullanıcılar")
                {
                    KullanicilarGoster();
                }
                else if (menuAdi == "Kapıcı")
                {
                    KapiciGoster();
                }
                else if (menuAdi == "Havuz")
                {
                    HavuzGoster();
                }
                else if (menuAdi == "Otopark")
                {
                    OtoparkGoster();
                }
                else if (menuAdi == "Şikayetler")
                {
                    SikayetlerGoster();
                }
                else if (menuAdi == "Enerji Takip")
                {
                    EnerjiTakipGoster();
                }
                else if (menuAdi == "Toplantı Yönetimi")
                {
                    ToplantiYonetimiGoster();
                }
                else if (menuAdi == "AI Tahminler")
                {
                    AITahminlerGoster();
                }
            }
        }

        /// <summary>
        /// Daire yönetimi ekranını gösterir - İnteraktif 3D Apartman Haritası
        /// </summary>
        private void DaireYonetimGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "🏢 Dijital Apartman Haritası",
                Location = new Point(30, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Kat seçimi için combo box
            var lblKatSec = new LabelControl
            {
                Text = "Kat Seç:",
                Location = new Point(550, 25),
                Size = new Size(80, 25),
                Appearance =
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            
            var cmbKat = new ComboBoxEdit
            {
                Location = new Point(640, 22),
                Size = new Size(120, 30)
            };
            cmbKat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbKat.Properties.Items.Add("Tümü");
            
            // Scroll container for interactive apartment map - Daha geniş ve temiz
            var scrollContainer = new PanelControl
            {
                Location = new Point(30, 80),
                Size = new Size(1130, 530),
                AutoScroll = true,
                Padding = new Padding(10),
                Appearance = { BackColor = Color.FromArgb(250, 251, 252) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(lblKatSec);
            PnlMain.Controls.Add(cmbKat);
            PnlMain.Controls.Add(scrollContainer);
            
            // Daireleri yükle ve interaktif haritayı oluştur
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Önce katları yükle
                    var katQuery = "SELECT DISTINCT k.KatId, k.KatNo FROM Kat k INNER JOIN Daire d ON k.KatId = d.KatId ORDER BY k.KatNo DESC";
                    using (var katCmd = new SqlCommand(katQuery, connection))
                    {
                        using (var katReader = katCmd.ExecuteReader())
                        {
                            while (katReader.Read())
                            {
                                cmbKat.Properties.Items.Add($"Kat {katReader["KatNo"]}");
                            }
                        }
                    }
                    cmbKat.SelectedIndex = 0; // Tümü seçili
                    
                    // Daireleri yükle
                    var query = @"SELECT d.DaireId, 
                                  d.DaireNo,
                                  k.KatNo,
                                  k.KatId,
                                  ISNULL(ds.AdSoyad, 'Boş') AS Sahip,
                                  ISNULL(ds.Telefon, '-') AS Telefon,
                                  (SELECT COUNT(*) FROM Aidat a WHERE a.DaireId = d.DaireId AND a.OdendiMi = 0 AND a.Tarih < GETDATE()) AS GecikmisAidatSayisi,
                                  (SELECT COUNT(*) FROM Aidat a WHERE a.DaireId = d.DaireId AND a.OdendiMi = 0) AS BekleyenAidatSayisi,
                                  (SELECT COUNT(*) FROM Arac a WHERE a.DaireId = d.DaireId AND a.Aktif = 1) AS AracSayisi,
                                  (SELECT COUNT(*) FROM Sikayet s WHERE s.DaireId = d.DaireId AND s.Aktif = 1) AS SikayetSayisi
                                  FROM Daire d
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  LEFT JOIN DaireSahibi ds ON d.DaireId = ds.DaireId AND ds.Aktif = 1
                                  ORDER BY k.KatNo DESC, d.DaireNo";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Daireler");
                        
                        // İnteraktif apartman haritasını oluştur
                        InteraktifApartmanHaritasiOlustur(scrollContainer, ds.Tables["Daireler"], cmbKat);
                        
                        // Kat seçimi değiştiğinde filtreleme
                        cmbKat.SelectedIndexChanged += (s, e) => 
                        {
                            InteraktifApartmanHaritasiOlustur(scrollContainer, ds.Tables["Daireler"], cmbKat);
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// İnteraktif apartman haritasını oluştur - Kat kat daire kartları
        /// </summary>
        private void InteraktifApartmanHaritasiOlustur(PanelControl container, DataTable daireler, ComboBoxEdit cmbKat)
        {
            // Mevcut kontrolleri temizle
            container.Controls.Clear();
            
            // Seçili kat filtresi
            string seciliKat = cmbKat.SelectedItem?.ToString() ?? "Tümü";
            bool tumunuGoster = seciliKat == "Tümü";
            
            // Katlara göre grupla
            var katlar = new Dictionary<int, List<DataRow>>();
            
            foreach (DataRow row in daireler.Rows)
            {
                int katId = Convert.ToInt32(row["KatId"]);
                int katNo = Convert.ToInt32(row["KatNo"]);
                
                if (tumunuGoster || seciliKat == $"Kat {katNo}")
                {
                    if (!katlar.ContainsKey(katId))
                    {
                        katlar[katId] = new List<DataRow>();
                    }
                    katlar[katId].Add(row);
                }
            }
            
            int yPozisyon = 20;
            int kartGenislik = 180;
            int kartYukseklik = 160;
            int kartlarArasiBosluk = 15;
            int katlarArasiBosluk = 40;
            
            // Her kat için panel ve daireleri göster
            foreach (var kat in katlar.OrderByDescending(k => 
            {
                if (k.Value.Count > 0)
                    return Convert.ToInt32(k.Value[0]["KatNo"]);
                return 0;
            }))
            {
                int katNo = 0;
                if (kat.Value.Count > 0)
                    katNo = Convert.ToInt32(kat.Value[0]["KatNo"]);
                
                // Kat başlığı paneli - Daha modern, border yok
                var katBaslik = new PanelControl
                {
                    Location = new Point(20, yPozisyon),
                    Size = new Size(1080, 40),
                    Appearance = 
                    {
                        BackColor = Color.FromArgb(33, 37, 41)
                    },
                    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                };
                
                var lblKatBaslik = new LabelControl
                {
                    Text = $"🏢 Kat {katNo}",
                    Location = new Point(15, 8),
                    Size = new Size(200, 25),
                    Appearance =
                    {
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        ForeColor = Color.White,
                        TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                    }
                };
                
                katBaslik.Controls.Add(lblKatBaslik);
                container.Controls.Add(katBaslik);
                yPozisyon += 45;
                
                // Daire kartları
                int xPozisyon = 20;
                foreach (var daire in katlar[kat.Key].OrderBy(d => Convert.ToInt32(d["DaireNo"])))
                {
                    int daireId = Convert.ToInt32(daire["DaireId"]);
                    int daireNo = Convert.ToInt32(daire["DaireNo"]);
                    string sahip = daire["Sahip"].ToString();
                    int gecikmisAidat = Convert.ToInt32(daire["GecikmisAidatSayisi"]);
                    int bekleyenAidat = Convert.ToInt32(daire["BekleyenAidatSayisi"]);
                    int aracSayisi = Convert.ToInt32(daire["AracSayisi"]);
                    int sikayetSayisi = Convert.ToInt32(daire["SikayetSayisi"]);
                    
                    // Daire durum rengini belirle
                    Color durumRengi = Color.FromArgb(0, 168, 80); // Yeşil (normal)
                    if (gecikmisAidat > 0)
                        durumRengi = Color.FromArgb(220, 20, 60); // Kırmızı (gecikmiş aidat)
                    else if (bekleyenAidat > 0)
                        durumRengi = Color.FromArgb(255, 193, 7); // Sarı (bekleyen aidat)
                    else if (string.IsNullOrEmpty(sahip) || sahip == "Boş")
                        durumRengi = Color.FromArgb(127, 140, 141); // Gri (boş)
                    
                    // Daire kartı - Shadow efekti, border yok
                    var daireKart = new PanelControl
                    {
                        Location = new Point(xPozisyon, yPozisyon),
                        Size = new Size(kartGenislik, kartYukseklik),
                        Appearance =
                        {
                            BackColor = Color.White
                        },
                        BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                        Cursor = Cursors.Hand
                    };
                    
                    // Daire kartı üst şeridi (durum rengi)
                    var kartUstSerit = new PanelControl
                    {
                        Dock = DockStyle.Top,
                        Height = 5,
                        Appearance = { BackColor = durumRengi }
                    };
                    daireKart.Controls.Add(kartUstSerit);
                    
                    // Daire numarası
                    var lblDaireNo = new LabelControl
                    {
                        Text = $"Daire {daireNo}",
                        Location = new Point(10, 15),
                        Size = new Size(160, 25),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                            ForeColor = Color.FromArgb(24, 38, 64),
                            TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center }
                        }
                    };
                    
                    // Sahip bilgisi
                    var lblSahip = new LabelControl
                    {
                        Text = sahip,
                        Location = new Point(10, 45),
                        Size = new Size(160, 20),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 10F),
                            ForeColor = Color.FromArgb(127, 140, 141),
                            TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center }
                        }
                    };
                    
                    // İstatistikler
                    var lblIstatistikler = new LabelControl
                    {
                        Text = $"💰 {bekleyenAidat} | 🚗 {aracSayisi} | 📝 {sikayetSayisi}",
                        Location = new Point(10, 70),
                        Size = new Size(160, 35),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 9F),
                            ForeColor = Color.FromArgb(127, 140, 141),
                            TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center }
                        }
                    };
                    
                    // Durum göstergesi
                    string durumMetni = "";
                    if (gecikmisAidat > 0)
                        durumMetni = "⚠️ Gecikmiş";
                    else if (bekleyenAidat > 0)
                        durumMetni = "⏳ Bekliyor";
                    else if (sahip == "Boş")
                        durumMetni = "🏠 Boş";
                    else
                        durumMetni = "✅ Normal";
                    
                    var lblDurum = new LabelControl
                    {
                        Text = durumMetni,
                        Location = new Point(10, 110),
                        Size = new Size(160, 25),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                            ForeColor = durumRengi,
                            TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center }
                        }
                    };
                    
                    daireKart.Controls.AddRange(new Control[] { lblDaireNo, lblSahip, lblIstatistikler, lblDurum });
                    
                    // Daire kartına tıklandığında detay göster
                    daireKart.Click += (s, e) => DaireDetayGoster(daireId, katNo, daireNo, sahip, daire);
                    lblDaireNo.Click += (s, e) => DaireDetayGoster(daireId, katNo, daireNo, sahip, daire);
                    lblSahip.Click += (s, e) => DaireDetayGoster(daireId, katNo, daireNo, sahip, daire);
                    lblIstatistikler.Click += (s, e) => DaireDetayGoster(daireId, katNo, daireNo, sahip, daire);
                    lblDurum.Click += (s, e) => DaireDetayGoster(daireId, katNo, daireNo, sahip, daire);
                    
                    container.Controls.Add(daireKart);
                    
                    // Yatay boşluk
                    xPozisyon += kartGenislik + kartlarArasiBosluk;
                    
                    // Satır sonu kontrolü (her satırda 5 kart)
                    if (xPozisyon + kartGenislik > 1100)
                    {
                        xPozisyon = 20;
                        yPozisyon += kartYukseklik + kartlarArasiBosluk;
                    }
                }
                
                // Katlar arası boşluk
                yPozisyon += kartYukseklik + katlarArasiBosluk + 20;
            }
        }
        
        /// <summary>
        /// Daire detaylarını modal olarak göster - Modern kart tasarımı
        /// </summary>
        private void DaireDetayGoster(int daireId, int katNo, int daireNo, string sahip, DataRow daireRow)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Detay formunu oluştur
                    var detayForm = new XtraForm
                    {
                        Text = $"📍 Kat {katNo} - Daire {daireNo} Detayları",
                        Size = new Size(900, 700),
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        MaximizeBox = false,
                        MinimizeBox = false,
                        Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
                    };
                    
                    // Header
                    var pnlHeader = new PanelControl
                    {
                        Dock = DockStyle.Top,
                        Height = 100,
                        Appearance = { BackColor = Color.FromArgb(240, 242, 245) }
                    };
                    
                    var lblHeaderBaslik = new LabelControl
                    {
                        Text = $"🏠 Kat {katNo} - Daire {daireNo}",
                        Location = new Point(20, 15),
                        Size = new Size(600, 35),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                            ForeColor = Color.FromArgb(24, 38, 64)
                        }
                    };
                    
                    // Telefon bilgisi header'a ekle
                    var telefon = daireRow["Telefon"].ToString();
                    var lblHeaderTelefon = new LabelControl
                    {
                        Text = $"📞 Telefon: {telefon}",
                        Location = new Point(20, 50),
                        Size = new Size(600, 25),
                        Appearance =
                        {
                            Font = new Font("Segoe UI", 12F),
                            ForeColor = Color.FromArgb(73, 80, 87)
                        }
                    };
                    
                    var btnKapat = new SimpleButton
                    {
                        Text = "✕",
                        Location = new Point(830, 15),
                        Size = new Size(40, 40),
                        Anchor = AnchorStyles.Top | AnchorStyles.Right,
                        Appearance =
                        {
                            BackColor = Color.FromArgb(220, 20, 60),
                            ForeColor = Color.White,
                            Font = new Font("Segoe UI", 14F, FontStyle.Bold)
                        }
                    };
                    btnKapat.Appearance.Options.UseBackColor = true;
                    btnKapat.Click += (s, e) => detayForm.Close();
                    
                    pnlHeader.Controls.Add(lblHeaderBaslik);
                    pnlHeader.Controls.Add(lblHeaderTelefon);
                    pnlHeader.Controls.Add(btnKapat);
                    detayForm.Controls.Add(pnlHeader);
                    
                    // İçerik paneli (scrollable)
                    var scrollPanel = new PanelControl
                    {
                        Dock = DockStyle.Fill,
                        AutoScroll = true,
                        Padding = new Padding(0),
                        Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
                    };
                    
                    int yPos = 20;
                    
                    // Oturan Bilgisi Kartı - Artık sadece ad soyad göster, telefon header'da
                    var kartOturan = KartOlustur("👤 Oturan Bilgisi", yPos, scrollPanel);
                    kartOturan.Height = 80;
                    var lblOturanAd = new LabelControl
                    {
                        Text = $"Ad Soyad: {sahip}",
                        Location = new Point(15, 45),
                        Size = new Size(800, 25),
                        Appearance = { Font = new Font("Segoe UI", 11F), ForeColor = Color.FromArgb(24, 38, 64) }
                    };
                    kartOturan.Controls.Add(lblOturanAd);
                    yPos += 100;
                    
                    // Aidat Durumu Kartı
                    var kartAidat = KartOlustur("💰 Aidat Durumu", yPos, scrollPanel);
                    var gecikmisAidat = Convert.ToInt32(daireRow["GecikmisAidatSayisi"]);
                    var bekleyenAidat = Convert.ToInt32(daireRow["BekleyenAidatSayisi"]);
                    
                    var queryAidat = @"SELECT TOP 10 Tutar, Tarih, OdendiMi 
                                      FROM Aidat 
                                      WHERE DaireId = @DaireId 
                                      ORDER BY Tarih DESC";
                    
                    int aidatYPos = 45;
                    int aidatSayisi = 0;
                    
                    using (var cmdAidat = new SqlCommand(queryAidat, connection))
                    {
                        cmdAidat.Parameters.AddWithValue("@DaireId", daireId);
                        using (var reader = cmdAidat.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tutar = Convert.ToDecimal(reader["Tutar"]);
                                var tarih = Convert.ToDateTime(reader["Tarih"]);
                                var odendi = Convert.ToBoolean(reader["OdendiMi"]);
                                
                                var lblAidat = new LabelControl
                                {
                                    Text = $"{tarih:dd.MM.yyyy} - {tutar:N2} TL - {(odendi ? "✅ Ödendi" : "⏳ Beklemede")}",
                                    Location = new Point(15, aidatYPos),
                                    Size = new Size(800, 25),
                                    Appearance = 
                                    { 
                                        Font = new Font("Segoe UI", 10F),
                                        ForeColor = odendi ? Color.FromArgb(0, 168, 80) : Color.FromArgb(255, 193, 7)
                                    }
                                };
                                kartAidat.Controls.Add(lblAidat);
                                aidatYPos += 28;
                                aidatSayisi++;
                            }
                        }
                    }
                    
                    if (gecikmisAidat > 0)
                    {
                        var lblGecikmis = new LabelControl
                        {
                            Text = $"⚠️ {gecikmisAidat} adet gecikmiş aidat var!",
                            Location = new Point(15, aidatYPos + 5),
                            Size = new Size(400, 25),
                            Appearance = 
                            { 
                                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                                ForeColor = Color.FromArgb(220, 20, 60)
                            }
                        };
                        kartAidat.Controls.Add(lblGecikmis);
                        aidatYPos += 35;
                    }
                    
                    if (aidatSayisi == 0)
                    {
                        var lblAidatYok = new LabelControl
                        {
                            Text = "Aidat kaydı yok",
                            Location = new Point(15, aidatYPos),
                            Size = new Size(400, 25),
                            Appearance = 
                            { 
                                Font = new Font("Segoe UI", 10F),
                                ForeColor = Color.FromArgb(127, 140, 141)
                            }
                        };
                        kartAidat.Controls.Add(lblAidatYok);
                        aidatYPos += 35;
                    }
                    
                    kartAidat.Height = Math.Max(150, aidatYPos + 10);
                    yPos += kartAidat.Height + 20;
                    
                    // Araç Bilgisi Kartı
                    var kartArac = KartOlustur("🚗 Araç Bilgisi", yPos, scrollPanel);
                    var aracSayisi = Convert.ToInt32(daireRow["AracSayisi"]);
                    
                    var queryArac = @"SELECT a.Plaka, a.OlusturmaTarihi, ISNULL(ds.OtoparkAlani, 'Belirtilmemiş') AS ParkYeri
                                     FROM Arac a
                                     LEFT JOIN DaireSahibi ds ON a.DaireId = ds.DaireId AND ds.Aktif = 1
                                     WHERE a.DaireId = @DaireId AND a.Aktif = 1
                                     ORDER BY a.OlusturmaTarihi DESC";
                    
                    int aracYPos = 45;
                    int aracKayitSayisi = 0;
                    
                    using (var cmdArac = new SqlCommand(queryArac, connection))
                    {
                        cmdArac.Parameters.AddWithValue("@DaireId", daireId);
                        using (var reader = cmdArac.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var plaka = reader["Plaka"].ToString();
                                    var tarih = reader["OlusturmaTarihi"] != DBNull.Value ? Convert.ToDateTime(reader["OlusturmaTarihi"]) : DateTime.MinValue;
                                    var parkYeri = reader["ParkYeri"].ToString();
                                    
                                    var lblPlaka = new LabelControl
                                    {
                                        Text = tarih != DateTime.MinValue ? $"🚗 {plaka} - Park Yeri: {parkYeri} - Kayıt: {tarih:dd.MM.yyyy}" : $"🚗 {plaka} - Park Yeri: {parkYeri}",
                                        Location = new Point(15, aracYPos),
                                        Size = new Size(800, 25),
                                        Appearance = { Font = new Font("Segoe UI", 11F) }
                                    };
                                    kartArac.Controls.Add(lblPlaka);
                                    aracYPos += 30;
                                    aracKayitSayisi++;
                                }
                            }
                            else
                            {
                                var lblAracYok = new LabelControl
                                {
                                    Text = "Kayıtlı araç yok",
                                    Location = new Point(15, aracYPos),
                                    Size = new Size(400, 25),
                                    Appearance = 
                                    { 
                                        Font = new Font("Segoe UI", 10F),
                                        ForeColor = Color.FromArgb(127, 140, 141)
                                    }
                                };
                                kartArac.Controls.Add(lblAracYok);
                                aracYPos += 35;
                            }
                        }
                    }
                    
                    kartArac.Height = Math.Max(100, aracYPos + 10);
                    yPos += kartArac.Height + 20;
                    
                    // Şikayet Geçmişi Kartı
                    var kartSikayet = KartOlustur("📝 Şikayet Geçmişi", yPos, scrollPanel);
                    var sikayetSayisi = Convert.ToInt32(daireRow["SikayetSayisi"]);
                    
                    var querySikayet = @"SELECT TOP 5 Baslik, OlusturmaTarihi, Durum, Icerik 
                                        FROM Sikayet 
                                        WHERE DaireId = @DaireId AND Aktif = 1
                                        ORDER BY OlusturmaTarihi DESC";
                    
                    using (var cmdSikayet = new SqlCommand(querySikayet, connection))
                    {
                        cmdSikayet.Parameters.AddWithValue("@DaireId", daireId);
                        using (var reader = cmdSikayet.ExecuteReader())
                        {
                            int sikayetYPos = 30;
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var baslik = reader["Baslik"].ToString();
                                    var tarih = Convert.ToDateTime(reader["OlusturmaTarihi"]);
                                    var durum = reader["Durum"].ToString();
                                    var icerik = reader["Icerik"] != DBNull.Value ? reader["Icerik"].ToString() : "";
                                    
                                    var lblSikayet = new LabelControl
                                    {
                                        Text = $"{tarih:dd.MM.yyyy} - {baslik} - {durum}",
                                        Location = new Point(15, sikayetYPos),
                                        Size = new Size(800, 35), // Yükseklik artırıldı: 30 -> 35
                                        Appearance = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) }
                                    };
                                    kartSikayet.Controls.Add(lblSikayet);
                                    sikayetYPos += 50; // Boşluk artırıldı: 45 -> 50
                                    
                                    if (!string.IsNullOrEmpty(icerik))
                                    {
                                        var lblIcerik = new LabelControl
                                        {
                                            Text = $"└─ {icerik.Substring(0, Math.Min(60, icerik.Length))}{(icerik.Length > 60 ? "..." : "")}",
                                            Location = new Point(30, sikayetYPos), // Sol boşluk daha da artırıldı: 25 -> 30
                                            Size = new Size(800, 40), // Yükseklik artırıldı: 35 -> 40
                                            Appearance = 
                                            { 
                                                Font = new Font("Segoe UI", 10F),
                                                ForeColor = Color.FromArgb(127, 140, 141),
                                                TextOptions = { WordWrap = DevExpress.Utils.WordWrap.Wrap }
                                            }
                                        };
                                        lblIcerik.Appearance.Options.UseTextOptions = true;
                                        kartSikayet.Controls.Add(lblIcerik);
                                        sikayetYPos += 55; // Boşluk artırıldı: 50 -> 55
                                    }
                                    else
                                    {
                                        sikayetYPos += 15; // İçerik yoksa ekstra boşluk
                                    }
                                    sikayetYPos += 25; // Şikayetler arası boşluk artırıldı: 20 -> 25
                                }
                            }
                            else
                            {
                                var lblSikayetYok = new LabelControl
                                {
                                    Text = "Şikayet kaydı yok",
                                    Location = new Point(15, sikayetYPos),
                                    Size = new Size(400, 25),
                                    Appearance = 
                                    { 
                                        Font = new Font("Segoe UI", 10F),
                                        ForeColor = Color.FromArgb(127, 140, 141)
                                    }
                                };
                                kartSikayet.Controls.Add(lblSikayetYok);
                                sikayetYPos += 30;
                            }
                        }
                    }
                    
                    // Kart yüksekliğini ayarla
                    kartSikayet.Height = Math.Max(150, (kartSikayet.Controls.Count - 1) * 35 + 60);
                    
                    yPos += kartSikayet.Height + 20;
                    
                    // Enerji Bilgisi Kartı
                    var kartEnerji = KartOlustur("⚡   Enerji Tüketimi", yPos, scrollPanel);
                    
                    // EnerjiTuketim tablosunu kontrol et
                    var tabloKontrolQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'EnerjiTuketim'";
                    bool tabloVar = false;
                    using (var tabloCmd = new SqlCommand(tabloKontrolQuery, connection))
                    {
                        tabloVar = Convert.ToInt32(tabloCmd.ExecuteScalar()) > 0;
                    }
                    
                    int enerjiYPos = 30;
                    if (tabloVar)
                    {
                        var queryEnerji = @"SELECT TOP 5 TuketimTipi, TuketimMiktari, Birim, Tarih 
                                            FROM EnerjiTuketim 
                                            WHERE DaireId = @DaireId AND Aktif = 1
                                            ORDER BY Tarih DESC";
                        
                        using (var cmdEnerji = new SqlCommand(queryEnerji, connection))
                        {
                            cmdEnerji.Parameters.AddWithValue("@DaireId", daireId);
                            using (var reader = cmdEnerji.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var tip = reader["TuketimTipi"].ToString();
                                        var miktar = Convert.ToDecimal(reader["TuketimMiktari"]);
                                        var birim = reader["Birim"].ToString();
                                        var tarih = Convert.ToDateTime(reader["Tarih"]);
                                        
                                        var lblEnerji = new LabelControl
                                        {
                                            Text = $"{tarih:dd.MM.yyyy} - {tip}: {miktar:N2} {birim}",
                                            Location = new Point(15, enerjiYPos),
                                            Size = new Size(800, 25),
                                            Appearance = { Font = new Font("Segoe UI", 10F) }
                                        };
                                        kartEnerji.Controls.Add(lblEnerji);
                                        enerjiYPos += 30;
                                    }
                                }
                                else
                                {
                                    var lblEnerjiYok = new LabelControl
                                    {
                                        Text = "Enerji tüketim kaydı yok",
                                        Location = new Point(15, enerjiYPos),
                                        Size = new Size(400, 25),
                                        Appearance = 
                                        { 
                                            Font = new Font("Segoe UI", 10F),
                                            ForeColor = Color.FromArgb(127, 140, 141)
                                        }
                                    };
                                    kartEnerji.Controls.Add(lblEnerjiYok);
                                    enerjiYPos += 30;
                                }
                            }
                        }
                    }
                    else
                    {
                        var lblTabloYok = new LabelControl
                        {
                            Text = "EnerjiTuketim tablosu bulunamadı",
                            Location = new Point(15, enerjiYPos),
                            Size = new Size(400, 25),
                            Appearance = 
                            { 
                                Font = new Font("Segoe UI", 10F),
                                ForeColor = Color.FromArgb(127, 140, 141)
                            }
                        };
                        kartEnerji.Controls.Add(lblTabloYok);
                        enerjiYPos += 30;
                    }
                    
                    // Kart yüksekliğini ayarla
                    kartEnerji.Height = Math.Max(100, enerjiYPos + 30);
                    
                    yPos += kartEnerji.Height + 20;
                    
                    // Düzenle Butonu (Header'a ekle) - Sola yasla
                    var btnDuzenle = new SimpleButton
                    {
                        Text = "✏️ Düzenle",
                        Location = new Point(400, 30),
                        Size = new Size(120, 40),
                        Appearance =
                        {
                            BackColor = Color.FromArgb(0, 150, 136),
                            ForeColor = Color.White,
                            Font = new Font("Segoe UI", 11F, FontStyle.Bold)
                        }
                    };
                    btnDuzenle.Appearance.Options.UseBackColor = true;
                    btnDuzenle.Appearance.Options.UseForeColor = true;
                    btnDuzenle.Click += (s, e) => 
                    {
                        // Daire düzenleme formu aç
                        DaireDuzenleFormAc(detayForm, connection, daireId, katNo, daireNo, sahip, telefon);
                    };
                    
                    pnlHeader.Controls.Add(btnDuzenle);
                    
                    detayForm.Controls.Add(scrollPanel);
                    detayForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Modern kart oluştur - Yardımcı metod
        /// </summary>
        private PanelControl KartOlustur(string baslik, int yPozisyon, PanelControl container)
        {
            var kart = new PanelControl
            {
                Location = new Point(0, yPozisyon),
                Size = new Size(860, 0), // Height dinamik ayarlanacak, genişlik artırıldı
                Appearance = 
                {
                    BackColor = Color.White,
                    BorderColor = Color.FromArgb(220, 224, 230)
                },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
                Padding = new Padding(0) // Padding kaldırıldı - hizalama düzeltmesi
            };
            
            var lblBaslik = new LabelControl
            {
                Text = baslik,
                Location = new Point(15, 10),
                Size = new Size(400, 30),
                Appearance =
                {
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            
            kart.Controls.Add(lblBaslik);
            container.Controls.Add(kart);
            
            return kart;
        }
        
        /// <summary>
        /// Daire düzenleme formu aç
        /// </summary>
        private void DaireDuzenleFormAc(XtraForm parentForm, SqlConnection connection, int daireId, int katNo, int daireNo, string sahip, string telefon)
        {
            try
            {
                // Daire sahibi bilgilerini al
                string adSoyad = sahip;
                string tel = telefon;
                int sahipId = 0;
                
                var sahipQuery = @"SELECT SahipId, AdSoyad, Telefon 
                                   FROM DaireSahibi 
                                   WHERE DaireId = @DaireId AND Aktif = 1";
                
                using (var cmdSahip = new SqlCommand(sahipQuery, connection))
                {
                    cmdSahip.Parameters.AddWithValue("@DaireId", daireId);
                    using (var reader = cmdSahip.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sahipId = Convert.ToInt32(reader["SahipId"]);
                            adSoyad = reader["AdSoyad"] != DBNull.Value ? reader["AdSoyad"].ToString() : "";
                            tel = reader["Telefon"] != DBNull.Value ? reader["Telefon"].ToString() : "";
                        }
                    }
                }
                
                // Düzenleme formu oluştur
                var duzenleForm = new XtraForm
                {
                    Text = $"✏️ Daire Düzenle - Kat {katNo} - Daire {daireNo}",
                    Size = new Size(500, 350),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
                };
                
                int yPos = 20;
                
                // Ad Soyad
                var lblAdSoyad = new LabelControl
                {
                    Text = "Ad Soyad:",
                    Location = new Point(20, yPos),
                    Size = new Size(100, 25),
                    Appearance = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) }
                };
                
                var txtAdSoyad = new TextEdit
                {
                    Location = new Point(130, yPos - 2),
                    Size = new Size(320, 25),
                    Text = adSoyad
                };
                txtAdSoyad.Properties.Appearance.Font = new Font("Segoe UI", 11F);
                txtAdSoyad.Properties.MaxLength = 100;
                
                yPos += 45;
                
                // Telefon
                var lblTelefon = new LabelControl
                {
                    Text = "Telefon:",
                    Location = new Point(20, yPos),
                    Size = new Size(100, 25),
                    Appearance = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) }
                };
                
                var txtTelefon = new TextEdit
                {
                    Location = new Point(130, yPos - 2),
                    Size = new Size(320, 25),
                    Text = tel
                };
                txtTelefon.Properties.Appearance.Font = new Font("Segoe UI", 11F);
                txtTelefon.Properties.MaxLength = 20;
                
                yPos += 80;
                
                // Butonlar
                var btnKaydet = new SimpleButton
                {
                    Text = "💾 Kaydet",
                    Location = new Point(130, yPos),
                    Size = new Size(120, 40),
                    Appearance =
                    {
                        BackColor = Color.FromArgb(0, 168, 80),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold)
                    }
                };
                btnKaydet.Appearance.Options.UseBackColor = true;
                btnKaydet.Appearance.Options.UseForeColor = true;
                
                var btnIptal = new SimpleButton
                {
                    Text = "❌ İptal",
                    Location = new Point(270, yPos),
                    Size = new Size(120, 40),
                    Appearance =
                    {
                        BackColor = Color.FromArgb(220, 20, 60),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold)
                    }
                };
                btnIptal.Appearance.Options.UseBackColor = true;
                btnIptal.Appearance.Options.UseForeColor = true;
                btnIptal.Click += (s, e) => duzenleForm.Close();
                
                btnKaydet.Click += (s, e) =>
                {
                    var yeniAdSoyad = txtAdSoyad.Text.Trim();
                    var yeniTelefon = txtTelefon.Text.Trim();
                    
                    if (string.IsNullOrEmpty(yeniAdSoyad))
                    {
                        XtraMessageBox.Show("Ad Soyad boş olamaz!", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    try
                    {
                        var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                        
                        using (var newConnection = new SqlConnection(connectionString))
                        {
                            newConnection.Open();
                            
                            if (sahipId > 0)
                            {
                                // Güncelle
                                var updateQuery = @"UPDATE DaireSahibi 
                                                   SET AdSoyad = @AdSoyad, Telefon = @Telefon
                                                   WHERE SahipId = @SahipId";
                                
                                using (var cmdUpdate = new SqlCommand(updateQuery, newConnection))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@AdSoyad", yeniAdSoyad);
                                    cmdUpdate.Parameters.AddWithValue("@Telefon", string.IsNullOrEmpty(yeniTelefon) ? (object)DBNull.Value : yeniTelefon);
                                    cmdUpdate.Parameters.AddWithValue("@SahipId", sahipId);
                                    
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Yeni ekle
                                var insertQuery = @"INSERT INTO DaireSahibi (DaireId, AdSoyad, Telefon, Aktif, BaslangicTarihi)
                                                  VALUES (@DaireId, @AdSoyad, @Telefon, 1, GETDATE())";
                                
                                using (var cmdInsert = new SqlCommand(insertQuery, newConnection))
                                {
                                    cmdInsert.Parameters.AddWithValue("@DaireId", daireId);
                                    cmdInsert.Parameters.AddWithValue("@AdSoyad", yeniAdSoyad);
                                    cmdInsert.Parameters.AddWithValue("@Telefon", string.IsNullOrEmpty(yeniTelefon) ? (object)DBNull.Value : yeniTelefon);
                                    
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            
                            XtraMessageBox.Show("Daire bilgileri başarıyla güncellendi!", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            duzenleForm.Close();
                            parentForm.Close(); // Ana detay formunu kapat
                            
                            // Daire yönetim ekranını yenile
                            DaireYonetimGoster();
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                
                duzenleForm.Controls.AddRange(new Control[] 
                { 
                    lblAdSoyad, txtAdSoyad, 
                    lblTelefon, txtTelefon, 
                    btnKaydet, btnIptal 
                });
                
                duzenleForm.ShowDialog(parentForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aidat/Gider ekranını gösterir
        /// </summary>
        private void AidatGiderGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "💰 Aidat/Gider Yönetimi",
                Location = new Point(30, 20),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Ekle butonu
            var btnEkle = new SimpleButton
            {
                Text = "+ Yeni Aidat Ekle",
                Location = new Point(30, 70),
                Size = new Size(180, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnEkle.Appearance.Options.UseBackColor = true;
            btnEkle.Appearance.Options.UseForeColor = true;
            btnEkle.Click += BtnYeniAidat_Click;
            
            // Ödeme İşaretle butonu
            var btnOdeme = new SimpleButton
            {
                Text = "✓ Ödeme İşaretle",
                Location = new Point(220, 70),
                Size = new Size(180, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 150, 255),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnOdeme.Appearance.Options.UseBackColor = true;
            btnOdeme.Appearance.Options.UseForeColor = true;
            btnOdeme.Click += BtnOdemeIsaretle_Click;
            
            // Sil butonu
            var btnSil = new SimpleButton
            {
                Text = "🗑️ Aidat Sil",
                Location = new Point(410, 70),
                Size = new Size(180, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnSil.Appearance.Options.UseBackColor = true;
            btnSil.Appearance.Options.UseForeColor = true;
            btnSil.Click += BtnAidatSil_Click;
            
            // Grid
            _gridAidatGider = new GridControl
            {
                Location = new Point(30, 120),
                Size = new Size(920, 480)
            };
            
            _viewAidatGider = new GridView();
            _gridAidatGider.MainView = _viewAidatGider;
            _gridAidatGider.ViewCollection.Add(_viewAidatGider);
            _viewAidatGider.OptionsView.ShowGroupPanel = false;
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(btnEkle);
            PnlMain.Controls.Add(btnOdeme);
            PnlMain.Controls.Add(btnSil);
            PnlMain.Controls.Add(_gridAidatGider);
            
            // Veriyi grid oluşturulduktan sonra yükle
            AidatGiderleriYukle();
            
            // Kolonları veriden sonra ayarla - Refresh çağrısından sonra
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            if (_viewAidatGider.Columns["DaireNo"] != null)
            {
                _viewAidatGider.Columns["DaireNo"].Caption = "Daire No";
                _viewAidatGider.Columns["DaireNo"].Width = 120;
            }
            if (_viewAidatGider.Columns["Tutar"] != null)
            {
                _viewAidatGider.Columns["Tutar"].Caption = "Tutar";
                _viewAidatGider.Columns["Tutar"].Width = 150;
            }
            if (_viewAidatGider.Columns["Tarih"] != null)
            {
                _viewAidatGider.Columns["Tarih"].Caption = "Tarih";
                _viewAidatGider.Columns["Tarih"].Width = 150;
            }
            if (_viewAidatGider.Columns["Durum"] != null)
            {
                _viewAidatGider.Columns["Durum"].Caption = "Durum";
                _viewAidatGider.Columns["Durum"].Width = 120;
            }
            if (_viewAidatGider.Columns["OdendiMi"] != null)
            {
                _viewAidatGider.Columns["OdendiMi"].Visible = false;
            }
            if (_viewAidatGider.Columns["AidatId"] != null)
            {
                _viewAidatGider.Columns["AidatId"].Visible = false;
            }
        }
        
        /// <summary>
        /// Aidat/Gider'leri yükler
        /// </summary>
        private void AidatGiderleriYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT a.AidatId, CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireNo, a.Tutar, a.Tarih, 
                                         CASE WHEN a.OdendiMi = 1 THEN 'Ödendi' ELSE 'Beklemede' END AS Durum, a.OdendiMi
                                  FROM Aidat a
                                  INNER JOIN Daire d ON a.DaireId = d.DaireId
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  ORDER BY a.Tarih DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "AidatGider");
                        _gridAidatGider.DataSource = ds.Tables["AidatGider"];
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Yeni aidat ekle
        /// </summary>
        private void BtnYeniAidat_Click(object sender, EventArgs e)
        {
            var frm = new FrmAidatEkle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AidatGiderleriYukle();
            }
        }
        
        /// <summary>
        /// Ödeme işaretle
        /// </summary>
        private void BtnOdemeIsaretle_Click(object sender, EventArgs e)
        {
            if (_viewAidatGider.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen ödeme işaretlemek istediğiniz aidatı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var aidatId = (int)_viewAidatGider.GetFocusedRowCellValue("AidatId");
            
            if (XtraMessageBox.Show("Bu aidatı ödendi olarak işaretlemek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        var query = "UPDATE Aidat SET OdendiMi = 1 WHERE AidatId = @AidatId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AidatId", aidatId);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    XtraMessageBox.Show("Ödeme başarıyla işaretlendi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    AidatGiderleriYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Aidat sil
        /// </summary>
        private void BtnAidatSil_Click(object sender, EventArgs e)
        {
            if (_viewAidatGider.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz aidatı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var aidatId = (int)_viewAidatGider.GetFocusedRowCellValue("AidatId");

            if (XtraMessageBox.Show("Bu aidatı silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        var query = "DELETE FROM Aidat WHERE AidatId = @AidatId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AidatId", aidatId);
                            command.ExecuteNonQuery();
                        }
                    }

                    XtraMessageBox.Show("Aidat başarıyla silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    AidatGiderleriYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Duyurular ekranını gösterir
        /// </summary>
        private void DuyurularGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "📢 Duyuru Yönetimi",
                Location = new Point(30, 20),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Ekle butonu
            var btnEkle = new SimpleButton
            {
                Text = "+ Yeni Duyuru Ekle",
                Location = new Point(30, 70),
                Size = new Size(180, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnEkle.Appearance.Options.UseBackColor = true;
            btnEkle.Appearance.Options.UseForeColor = true;
            btnEkle.Click += BtnYeniDuyuru_Click;
            
            // Sil butonu
            var btnSil = new SimpleButton
            {
                Text = "🗑 Sil",
                Location = new Point(220, 70),
                Size = new Size(120, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(220, 20, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnSil.Appearance.Options.UseBackColor = true;
            btnSil.Appearance.Options.UseForeColor = true;
            btnSil.Click += BtnDuyuruSil_Click;
            
            // Grid
            _gridDuyurular = new GridControl
            {
                Location = new Point(30, 120),
                Size = new Size(920, 480)
            };
            
            _viewDuyurular = new GridView();
            _gridDuyurular.MainView = _viewDuyurular;
            _gridDuyurular.ViewCollection.Add(_viewDuyurular);
            
            // Kolonların otomatik oluşturulmasını kapat
            _viewDuyurular.OptionsCustomization.AllowQuickHideColumns = false;
            _viewDuyurular.OptionsView.ShowGroupPanel = false;
            
            // Veriyi önce yükle, sonra kolonları ayarla
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(btnEkle);
            PnlMain.Controls.Add(btnSil);
            PnlMain.Controls.Add(_gridDuyurular);
            
            // Veriyi grid oluşturulduktan sonra yükle
            DuyurulariYukle();
        }
        
        /// <summary>
        /// Duyuruları yükler
        /// </summary>
        private void DuyurulariYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Sadece gerekli kolonları çek
                    var query = "SELECT DuyuruId, Baslik, Icerik, OlusturmaTarihi, GecerlilikBitis FROM Duyuru ORDER BY OlusturmaTarihi DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Duyurular");
                        
                        // DataSource'u ayarla
                        _gridDuyurular.DataSource = ds.Tables["Duyurular"];
                        
                        // Kolonları düzenle
                        if (_viewDuyurular.Columns.Count > 0)
                        {
                            foreach (DevExpress.XtraGrid.Columns.GridColumn col in _viewDuyurular.Columns)
                            {
                                // DuyuruId'yi gizle
                                if (col.FieldName == "DuyuruId")
                                {
                                    col.Visible = false;
                                }
                                // Kolon başlıklarını ayarla
                                else if (col.FieldName == "Baslik")
                                    col.Caption = "Başlık";
                                else if (col.FieldName == "Icerik")
                                    col.Caption = "İçerik";
                                else if (col.FieldName == "OlusturmaTarihi")
                                    col.Caption = "Oluşturma Tarihi";
                                else if (col.FieldName == "GecerlilikBitis")
                                    col.Caption = "Geçerlilik Bitiş";
                            }
                        }
                        
                        if (ds.Tables["Duyurular"].Rows.Count == 0)
                        {
                            XtraMessageBox.Show("Veritabanında hiç duyuru bulunamadı.", "Bilgi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Yeni duyuru ekle
        /// </summary>
        private void BtnYeniDuyuru_Click(object sender, EventArgs e)
        {
            var frm = new FrmDuyuruEkle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DuyurulariYukle();
            }
        }
        
        /// <summary>
        /// Duyuru sil
        /// </summary>
        private void BtnDuyuruSil_Click(object sender, EventArgs e)
        {
            if (_viewDuyurular.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz duyuruyu seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var duyuruId = (int)_viewDuyurular.GetFocusedRowCellValue("DuyuruId");
            
                                if (XtraMessageBox.Show("Bu duyuruyu silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // Soft delete yerine gerçek silme yap
                        var query = "DELETE FROM Duyuru WHERE DuyuruId = @DuyuruId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@DuyuruId", duyuruId);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    XtraMessageBox.Show("Duyuru başarıyla silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DuyurulariYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Kullanıcılar ekranını gösterir
        /// </summary>
        private void KullanicilarGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "👥 Kullanıcı Yönetimi",
                Location = new Point(30, 20),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Ekle butonu
            var btnEkle = new SimpleButton
            {
                Text = "+ Yeni Kullanıcı Ekle",
                Location = new Point(30, 70),
                Size = new Size(180, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnEkle.Appearance.Options.UseBackColor = true;
            btnEkle.Appearance.Options.UseForeColor = true;
            btnEkle.Click += BtnYeniKullanici_Click;
            
            // Sil butonu
            var btnSil = new SimpleButton
            {
                Text = "🗑 Sil",
                Location = new Point(220, 70),
                Size = new Size(120, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(220, 20, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnSil.Appearance.Options.UseBackColor = true;
            btnSil.Appearance.Options.UseForeColor = true;
            btnSil.Click += BtnKullaniciSil_Click;
            
            // Grid
            _gridKullanicilar = new GridControl
            {
                Location = new Point(30, 120),
                Size = new Size(920, 480)
            };
            
            _viewKullanicilar = new GridView();
            _gridKullanicilar.MainView = _viewKullanicilar;
            _gridKullanicilar.ViewCollection.Add(_viewKullanicilar);
            
            // Kolonların otomatik oluşturulmasını kapat
            _viewKullanicilar.OptionsCustomization.AllowQuickHideColumns = false;
            _viewKullanicilar.OptionsView.ShowGroupPanel = false;
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(btnEkle);
            PnlMain.Controls.Add(btnSil);
            PnlMain.Controls.Add(_gridKullanicilar);
            
            // Veriyi grid oluşturulduktan sonra yükle
            KullanicilariYukle();
        }
        
        /// <summary>
        /// Kullanıcıları yükler
        /// </summary>
        private void KullanicilariYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Sadece gerekli kolonları çek
                    var query = "SELECT KullaniciId, AdSoyad, KullaniciAdi, KullaniciTipi FROM Kullanici ORDER BY AdSoyad";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Kullanicilar");
                        
                        // DataSource'u ayarla
                        _gridKullanicilar.DataSource = ds.Tables["Kullanicilar"];
                        
                        // Kolonları düzenle
                        if (_viewKullanicilar.Columns.Count > 0)
                        {
                            foreach (DevExpress.XtraGrid.Columns.GridColumn col in _viewKullanicilar.Columns)
                            {
                                // KullaniciId'yi gizle
                                if (col.FieldName == "KullaniciId")
                                {
                                    col.Visible = false;
                                }
                                // Kolon başlıklarını ayarla
                                else if (col.FieldName == "AdSoyad")
                                    col.Caption = "Ad Soyad";
                                else if (col.FieldName == "KullaniciAdi")
                                    col.Caption = "Kullanıcı Adı";
                                else if (col.FieldName == "KullaniciTipi")
                                    col.Caption = "Tip";
                            }
                        }
                        
                        if (ds.Tables["Kullanicilar"].Rows.Count == 0)
                        {
                            XtraMessageBox.Show("Veritabanında hiç kullanıcı bulunamadı.", "Bilgi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Yeni kullanıcı ekle
        /// </summary>
        private void BtnYeniKullanici_Click(object sender, EventArgs e)
        {
            var frm = new FrmKullaniciEkle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                KullanicilariYukle();
            }
        }
        
        /// <summary>
        /// Kullanıcı sil
        /// </summary>
        private void BtnKullaniciSil_Click(object sender, EventArgs e)
        {
            if (_viewKullanicilar.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var kullaniciId = (int)_viewKullanicilar.GetFocusedRowCellValue("KullaniciId");
            
                                if (XtraMessageBox.Show("Bu kullanıcıyı silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // Soft delete yerine gerçek silme yap
                        var query = "DELETE FROM Kullanici WHERE KullaniciId = @KullaniciId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    XtraMessageBox.Show("Kullanıcı başarıyla silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    KullanicilariYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Tüm butonları sıfırla
        /// </summary>
        private void ResetButtons()
        {
            BtnDaireYonetim.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnDaireYonetim.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnDaireYonetim.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnAidatGider.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnAidatGider.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnAidatGider.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnDuyurular.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnDuyurular.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnDuyurular.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnKullaniciYonetim.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnKullaniciYonetim.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnKullaniciYonetim.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnKapici.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnKapici.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnKapici.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnHavuz.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnHavuz.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnHavuz.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnOtopark.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnOtopark.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnOtopark.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnSikayetler.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnSikayetler.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnSikayetler.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnEnerjiTakip.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnEnerjiTakip.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnEnerjiTakip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnToplantiYonetimi.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnToplantiYonetimi.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnToplantiYonetimi.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);

            BtnAITahminler.Appearance.BackColor = System.Drawing.Color.Transparent;
            BtnAITahminler.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            BtnAITahminler.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
        }

        /// <summary>
        /// Kapıcı bilgilerini göster
        /// </summary>
        private void KapiciGoster()
        {
            var lblBaslik = new LabelControl
            {
                Text = "👨‍💼 Kapıcı Yönetimi",
                Location = new Point(30, 20),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            PnlMain.Controls.Add(lblBaslik);

            // Grid
            _gridKapici = new GridControl
            {
                Location = new Point(30, 80),
                Size = new Size(700, 400)
            };
            _viewKapici = new GridView(_gridKapici);
            _viewKapici.OptionsView.ShowGroupPanel = false;

            // Butonlar
            var btnEkle = new SimpleButton
            {
                Text = "+ Yeni Kapıcı Ekle",
                Location = new Point(750, 80),
                Size = new Size(200, 45),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnEkle.Appearance.Options.UseBackColor = true;
            btnEkle.Appearance.Options.UseForeColor = true;
            btnEkle.Click += BtnKapiciEkle_Click;

            var btnSil = new SimpleButton
            {
                Text = "Kapıcı Sil",
                Location = new Point(750, 140),
                Size = new Size(200, 45),
                Appearance =
                {
                    BackColor = Color.FromArgb(220, 20, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnSil.Appearance.Options.UseBackColor = true;
            btnSil.Appearance.Options.UseForeColor = true;
            btnSil.Click += BtnKapiciSil_Click;

            KapiciYukle();

            PnlMain.Controls.Add(_gridKapici);
            PnlMain.Controls.Add(btnEkle);
            PnlMain.Controls.Add(btnSil);
        }

        /// <summary>
        /// Kapıcı listesini yükle
        /// </summary>
        private void KapiciYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT KapiciId, AdSoyad, Telefon, Maas FROM Kapici ORDER BY KapiciId DESC";
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Kapici");
                        _gridKapici.DataSource = ds.Tables["Kapici"];
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kapıcı ekle
        /// </summary>
        private void BtnKapiciEkle_Click(object sender, EventArgs e)
        {
            var ad = XtraInputBox.Show("Kapıcı Ad Soyad:", "Kapıcı Ekle", "");
            if (string.IsNullOrWhiteSpace(ad)) return;

            var telefon = XtraInputBox.Show("Telefon:", "Kapıcı Ekle", "");
            var maasStr = XtraInputBox.Show("Maaş:", "Kapıcı Ekle", "");

            if (decimal.TryParse(maasStr, out decimal maas))
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var query = "INSERT INTO Kapici (AdSoyad, Telefon, Maas) VALUES (@AdSoyad, @Telefon, @Maas)";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AdSoyad", ad);
                            command.Parameters.AddWithValue("@Telefon", telefon ?? "");
                            command.Parameters.AddWithValue("@Maas", maas);
                            command.ExecuteNonQuery();
                        }
                    }

                    XtraMessageBox.Show("Kapıcı başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KapiciYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Kapıcı sil
        /// </summary>
        private void BtnKapiciSil_Click(object sender, EventArgs e)
        {
            if (_viewKapici.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz kapıcıyı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kapiciId = (int)_viewKapici.GetFocusedRowCellValue("KapiciId");

            if (XtraMessageBox.Show("Bu kapıcıyı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var query = "DELETE FROM Kapici WHERE KapiciId = @KapiciId";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@KapiciId", kapiciId);
                            command.ExecuteNonQuery();
                        }
                    }

                    XtraMessageBox.Show("Kapıcı başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KapiciYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Havuz bilgilerini göster
        /// </summary>
        private void HavuzGoster()
        {
            var lblBaslik = new LabelControl
            {
                Text = "🏊 Havuz Durumu ve Bakım Yönetimi",
                Location = new Point(30, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            PnlMain.Controls.Add(lblBaslik);

            // Havuz resmi
            var picHavuz = new PictureEdit
            {
                Location = new Point(30, 80),
                Size = new Size(300, 200)
            };
            picHavuz.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picHavuz.Properties.ShowMenu = false;
            picHavuz.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            
            try
            {
                string[] possiblePaths = {
                    Application.StartupPath + @"\images\havuz.jpg",
                    Application.StartupPath + @"\havuz.jpg",
                    Application.StartupPath + @"\..\images\havuz.jpg",
                    Application.StartupPath + @"\Resources\havuz.jpg",
                    System.IO.Path.Combine(Application.StartupPath, "images", "havuz.jpg"),
                    System.IO.Path.Combine(Application.StartupPath, "havuz.jpg")
                };
                
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        picHavuz.Image = Image.FromFile(path);
                        break;
                    }
                }
            }
            catch
            {
                // Resim yüklenemezse devam et
            }

            var lblInfo = new LabelControl
            {
                Location = new Point(350, 80),
                Size = new Size(400, 200),
                Appearance = { Font = new Font("Segoe UI", 12F) }
            };

            var btnBakim = new SimpleButton
            {
                Text = "🔧 Bakım Yüzdesi Güncelle",
                Location = new Point(350, 290),
                Size = new Size(250, 50),
                Appearance =
                {
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnBakim.Appearance.Options.UseBackColor = true;
            btnBakim.Appearance.Options.UseForeColor = true;
            btnBakim.Click += BtnHavuzBakim_Click;

            var btnDoluluk = new SimpleButton
            {
                Text = "👥 Doluluk Oranı Güncelle",
                Location = new Point(620, 290),
                Size = new Size(250, 50),
                Appearance =
                {
                    BackColor = Color.FromArgb(155, 89, 182),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            btnDoluluk.Appearance.Options.UseBackColor = true;
            btnDoluluk.Appearance.Options.UseForeColor = true;
            btnDoluluk.Click += BtnHavuzDoluluk_Click;

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT TOP 1 HavuzId, BakimYuzdesi, SonBakimTarihi, DolulukOrani FROM Havuz ORDER BY HavuzId DESC";
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Havuz");
                        var dt = ds.Tables["Havuz"];

                        if (dt.Rows.Count > 0)
                        {
                            var bakimYuzde = Convert.ToInt32(dt.Rows[0]["BakimYuzdesi"]);
                            var dolulukOrani = dt.Rows[0]["DolulukOrani"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["DolulukOrani"]) : 0;
                            lblInfo.Text = $"Bakım Yüzdesi: %{bakimYuzde}\n\n" +
                                          $"Doluluk Oranı: {dolulukOrani} kişi\n\n" +
                                          $"Son Bakım Tarihi: {dt.Rows[0]["SonBakimTarihi"]}";
                        }
                        else
                        {
                            lblInfo.Text = "Havuz bilgisi bulunamadı. Lütfen bakım bilgisi ekleyin.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            PnlMain.Controls.Add(picHavuz);
            PnlMain.Controls.Add(lblInfo);
            PnlMain.Controls.Add(btnBakim);
            PnlMain.Controls.Add(btnDoluluk);
        }

        /// <summary>
        /// Havuz bakım yüzdesi güncelle
        /// </summary>
        private void BtnHavuzBakim_Click(object sender, EventArgs e)
        {
            var yuzdeStr = XtraInputBox.Show("Bakım Yüzdesi (%):", "Bakım Güncelle", "100");
            if (int.TryParse(yuzdeStr, out int yuzde))
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var query = @"IF EXISTS (SELECT 1 FROM Havuz) 
                                    UPDATE Havuz SET BakimYuzdesi = @Yuzde, SonBakimTarihi = GETDATE()
                                    ELSE
                                    INSERT INTO Havuz (BakimYuzdesi, SonBakimTarihi, DolulukOrani) VALUES (@Yuzde, GETDATE(), 0)";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Yuzde", yuzde);
                            command.ExecuteNonQuery();
                        }
                    }

                    XtraMessageBox.Show("Bakım bilgisi güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HavuzGoster();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Havuz doluluk oranı güncelle
        /// </summary>
        private void BtnHavuzDoluluk_Click(object sender, EventArgs e)
        {
            var dolulukStr = XtraInputBox.Show("Doluluk Oranı (kişi sayısı):", "Doluluk Güncelle", "0");
            if (int.TryParse(dolulukStr, out int doluluk))
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var query = @"IF EXISTS (SELECT 1 FROM Havuz) 
                                    UPDATE Havuz SET DolulukOrani = @Doluluk
                                    ELSE
                                    INSERT INTO Havuz (BakimYuzdesi, DolulukOrani) VALUES (100, @Doluluk)";
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Doluluk", doluluk);
                            command.ExecuteNonQuery();
                        }
                    }

                    XtraMessageBox.Show("Doluluk oranı güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HavuzGoster();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Otopark bilgilerini göster
        /// </summary>
        private void OtoparkGoster()
        {
            var lblBaslik = new LabelControl
            {
                Text = "🚗 Otopark - Sakinlerin Arabaları",
                Location = new Point(30, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            PnlMain.Controls.Add(lblBaslik);

            var lblDurum = new LabelControl
            {
                Location = new Point(600, 20),
                Size = new Size(300, 40),
                Appearance = { Font = new Font("Segoe UI", 14F, FontStyle.Bold) }
            };
            PnlMain.Controls.Add(lblDurum);

            // Otopark resmi
            var picOtopark = new PictureEdit
            {
                Location = new Point(30, 80),
                Size = new Size(300, 200)
            };
            picOtopark.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picOtopark.Properties.ShowMenu = false;
            picOtopark.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            
            try
            {
                string[] possiblePaths = {
                    Application.StartupPath + @"\images\otopark.jpg",
                    Application.StartupPath + @"\otopark.jpg",
                    Application.StartupPath + @"\..\images\otopark.jpg",
                    Application.StartupPath + @"\Resources\otopark.jpg",
                    System.IO.Path.Combine(Application.StartupPath, "images", "otopark.jpg"),
                    System.IO.Path.Combine(Application.StartupPath, "otopark.jpg")
                };
                
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        picOtopark.Image = Image.FromFile(path);
                        break;
                    }
                }
            }
            catch
            {
                // Resim yüklenemezse devam et
            }

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"SELECT a.Plaka, CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) as DaireBilgi
                                  FROM Arac a
                                  INNER JOIN Daire d ON a.DaireId = d.DaireId
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  WHERE a.Aktif = 1";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Otopark");
                        
                        var aracSayisi = ds.Tables["Otopark"].Rows.Count;
                        var kapasite = 20;
                        var dolulukYuzde = (aracSayisi * 100) / kapasite;
                        
                        lblDurum.Text = $"Dolu: {aracSayisi}/{kapasite} araç\nDoluluk: %{dolulukYuzde}";
                        lblDurum.Appearance.ForeColor = dolulukYuzde > 80 ? Color.FromArgb(220, 20, 60) : Color.FromArgb(0, 168, 80);
                        
                        var grid = new GridControl
                        {
                            Location = new Point(350, 80),
                            Size = new Size(750, 400),
                            DataSource = ds.Tables["Otopark"]
                        };
                        
                        var view = new GridView(grid);
                        view.OptionsView.ShowGroupPanel = false;
                        
                        PnlMain.Controls.Add(picOtopark);
                        PnlMain.Controls.Add(grid);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Şikayetler sayfasını göster
        /// </summary>
        private void SikayetlerGoster()
        {
            var lblBaslik = new LabelControl
            {
                Text = "📝 Şikayetler",
                Location = new Point(30, 20),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            
            // Sil butonu
            var btnSikayetSil = new SimpleButton
            {
                Text = "🗑️ Şikayet Sil",
                Location = new Point(860, 20),
                Size = new Size(150, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    Options =
                    {
                        UseBackColor = true,
                        UseForeColor = true
                    }
                }
            };
            btnSikayetSil.Click += BtnSikayetSil_Click;
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(btnSikayetSil);

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"SELECT s.SikayetId, s.Baslik, s.Icerik, s.OlusturmaTarihi, s.Durum,
                                  CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) as DaireBilgi
                                  FROM Sikayet s
                                  INNER JOIN Daire d ON s.DaireId = d.DaireId
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  WHERE s.Aktif = 1
                                  ORDER BY s.OlusturmaTarihi DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Sikayetler");
                        
                        _gridSikayetler = new GridControl
                        {
                            Location = new Point(30, 80),
                            Size = new Size(900, 400),
                            DataSource = ds.Tables["Sikayetler"]
                        };
                        
                        _viewSikayetler = new GridView(_gridSikayetler);
                        _gridSikayetler.MainView = _viewSikayetler;
                        _gridSikayetler.ViewCollection.Add(_viewSikayetler);
                        _viewSikayetler.OptionsView.ShowGroupPanel = false;
                        
                        PnlMain.Controls.Add(_gridSikayetler);
                        
                        // Kolon genişlikleri DataSource'tan sonra ayarlanmalı
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                        if (_viewSikayetler.Columns["SikayetId"] != null) _viewSikayetler.Columns["SikayetId"].Visible = false;
                        if (_viewSikayetler.Columns["Baslik"] != null) _viewSikayetler.Columns["Baslik"].Width = 200;
                        if (_viewSikayetler.Columns["Icerik"] != null) _viewSikayetler.Columns["Icerik"].Width = 300;
                        if (_viewSikayetler.Columns["DaireBilgi"] != null) _viewSikayetler.Columns["DaireBilgi"].Width = 150;
                        if (_viewSikayetler.Columns["OlusturmaTarihi"] != null) _viewSikayetler.Columns["OlusturmaTarihi"].Width = 150;
                        if (_viewSikayetler.Columns["Durum"] != null) _viewSikayetler.Columns["Durum"].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Şikayet sil
        /// </summary>
        private void BtnSikayetSil_Click(object sender, EventArgs e)
        {
            if (_viewSikayetler == null || _viewSikayetler.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz şikayeti seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sikayetId = (int)_viewSikayetler.GetFocusedRowCellValue("SikayetId");

            if (XtraMessageBox.Show("Bu şikayeti silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        var query = "UPDATE Sikayet SET Aktif = 0 WHERE SikayetId = @SikayetId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SikayetId", sikayetId);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    XtraMessageBox.Show("Şikayet başarıyla silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    SikayetlerGoster();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Enerji Takip ekranını gösterir
        /// </summary>
        private void EnerjiTakipGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "⚡   Enerji Takip ve Analiz",
                Location = new Point(30, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Daire seçimi
            var lblDaireSec = new LabelControl
            {
                Text = "Daire Seç:",
                Location = new Point(30, 80),
                Size = new Size(100, 25),
                Appearance =
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };
            
            var cmbDaire = new ComboBoxEdit
            {
                Location = new Point(140, 77),
                Size = new Size(250, 30)
            };
            cmbDaire.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbDaire.Properties.Items.Add("Tümü");
            
            // Grid
            var gridEnerji = new GridControl
            {
                Location = new Point(30, 130),
                Size = new Size(920, 480)
            };
            
            var viewEnerji = new GridView();
            gridEnerji.MainView = viewEnerji;
            gridEnerji.ViewCollection.Add(viewEnerji);
            viewEnerji.OptionsView.ShowGroupPanel = false;
            viewEnerji.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            
            // İstatistik paneli
            var pnlIstatistik = new PanelControl
            {
                Location = new Point(410, 77),
                Size = new Size(540, 40),
                Appearance = { BackColor = Color.FromArgb(245, 247, 250) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            };
            
            var lblIstatistik = new LabelControl
            {
                Text = "📊 Toplam Enerji Tüketimi: Hesaplanıyor...",
                Location = new Point(15, 10),
                Size = new Size(510, 25),
                Appearance =
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 168, 80),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            pnlIstatistik.Controls.Add(lblIstatistik);
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(lblDaireSec);
            PnlMain.Controls.Add(cmbDaire);
            PnlMain.Controls.Add(pnlIstatistik);
            PnlMain.Controls.Add(gridEnerji);
            
            // Daireleri yükle
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Önce EnerjiTuketim tablosunu kontrol et
                    var tabloKontrolQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'EnerjiTuketim'";
                    using (var tabloCmd = new SqlCommand(tabloKontrolQuery, connection))
                    {
                        var tabloVar = Convert.ToInt32(tabloCmd.ExecuteScalar()) > 0;
                        
                        if (!tabloVar)
                        {
                            lblIstatistik.Text = "⚠️ EnerjiTuketim tablosu bulunamadı. Lütfen SQL scriptini çalıştırın.";
                            lblIstatistik.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
                            return;
                        }
                    }
                    
                    // Daireleri yükle
                    var daireQuery = @"SELECT d.DaireId, 
                                      CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireBilgi 
                                      FROM Daire d
                                      INNER JOIN Kat k ON d.KatId = k.KatId
                                      ORDER BY k.KatNo DESC, d.DaireNo";
                    
                    using (var daireCmd = new SqlCommand(daireQuery, connection))
                    {
                        using (var reader = daireCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cmbDaire.Properties.Items.Add(reader["DaireBilgi"].ToString());
                            }
                        }
                    }
                    cmbDaire.SelectedIndex = 0; // Tümü seçili
                    
                    // Enerji verilerini yükle
                    EnerjiVerileriniYukle(connection, gridEnerji, viewEnerji, lblIstatistik, cmbDaire);
                    
                    // Daire seçimi değiştiğinde filtreleme
                    cmbDaire.SelectedIndexChanged += (s, e) => 
                    {
                        using (var newConnection = new SqlConnection(connectionString))
                        {
                            newConnection.Open();
                            EnerjiVerileriniYukle(newConnection, gridEnerji, viewEnerji, lblIstatistik, cmbDaire);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Enerji verilerini yükle ve göster
        /// </summary>
        private void EnerjiVerileriniYukle(SqlConnection connection, GridControl grid, GridView view, LabelControl lblIstatistik, ComboBoxEdit cmbDaire)
        {
            try
            {
                // Seçili daire filtresi
                string seciliDaire = cmbDaire.SelectedItem?.ToString() ?? "Tümü";
                bool tumunuGoster = seciliDaire == "Tümü";
                
                // Daire ID'yi al
                int daireId = 0;
                if (!tumunuGoster)
                {
                    var daireQuery = @"SELECT d.DaireId 
                                      FROM Daire d
                                      INNER JOIN Kat k ON d.KatId = k.KatId
                                      WHERE CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) = @DaireBilgi";
                    
                    using (var daireCmd = new SqlCommand(daireQuery, connection))
                    {
                        daireCmd.Parameters.AddWithValue("@DaireBilgi", seciliDaire);
                        var result = daireCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            daireId = Convert.ToInt32(result);
                    }
                }
                
                // Enerji tüketim verilerini yükle
                var query = @"SELECT e.EnerjiTuketimId,
                                  CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireBilgi,
                                  e.TuketimTipi,
                                  e.TuketimMiktari,
                                  e.Birim,
                                  e.Tarih,
                                  e.Yil,
                                  e.Ay
                                  FROM EnerjiTuketim e
                                  INNER JOIN Daire d ON e.DaireId = d.DaireId
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  WHERE e.Aktif = 1";
                
                if (!tumunuGoster && daireId > 0)
                {
                    query += " AND e.DaireId = @DaireId";
                }
                
                query += " ORDER BY e.Yil DESC, e.Ay DESC, e.Tarih DESC";
                
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    if (!tumunuGoster && daireId > 0)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@DaireId", daireId);
                    }
                    
                    var ds = new DataSet();
                    adapter.Fill(ds, "EnerjiTuketim");
                    
                    grid.DataSource = ds.Tables["EnerjiTuketim"];
                    
                    // Kolon ayarları
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    
                    if (view.Columns.Count > 0)
                    {
                        if (view.Columns["EnerjiTuketimId"] != null)
                            view.Columns["EnerjiTuketimId"].Visible = false;
                        if (view.Columns["DaireBilgi"] != null)
                        {
                            view.Columns["DaireBilgi"].Caption = "📍 Daire";
                            view.Columns["DaireBilgi"].Width = 200;
                        }
                        if (view.Columns["TuketimTipi"] != null)
                        {
                            view.Columns["TuketimTipi"].Caption = "⚡ Tüketim Tipi";
                            view.Columns["TuketimTipi"].Width = 150;
                        }
                        if (view.Columns["TuketimMiktari"] != null)
                        {
                            view.Columns["TuketimMiktari"].Caption = "📊 Miktar";
                            view.Columns["TuketimMiktari"].Width = 150;
                            view.Columns["TuketimMiktari"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            view.Columns["TuketimMiktari"].DisplayFormat.FormatString = "N2";
                        }
                        if (view.Columns["Birim"] != null)
                        {
                            view.Columns["Birim"].Caption = "📏 Birim";
                            view.Columns["Birim"].Width = 100;
                        }
                        if (view.Columns["Tarih"] != null)
                        {
                            view.Columns["Tarih"].Caption = "📅 Tarih";
                            view.Columns["Tarih"].Width = 150;
                            view.Columns["Tarih"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            view.Columns["Tarih"].DisplayFormat.FormatString = "dd.MM.yyyy";
                        }
                        if (view.Columns["Yil"] != null)
                            view.Columns["Yil"].Visible = false;
                        if (view.Columns["Ay"] != null)
                            view.Columns["Ay"].Visible = false;
                    }
                    
                    // İstatistik hesapla
                    decimal toplamTuketim = 0;
                    foreach (DataRow row in ds.Tables["EnerjiTuketim"].Rows)
                    {
                        toplamTuketim += Convert.ToDecimal(row["TuketimMiktari"]);
                    }
                    
                    lblIstatistik.Text = $"📊 Toplam Enerji Tüketimi: {toplamTuketim:N2} (Toplam {ds.Tables["EnerjiTuketim"].Rows.Count} kayıt)";
                    lblIstatistik.Appearance.ForeColor = Color.FromArgb(0, 168, 80);
                }
            }
            catch (Exception ex)
            {
                lblIstatistik.Text = $"⚠️ Hata: {ex.Message}";
                lblIstatistik.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
            }
        }

        /// <summary>
        /// Toplantı Yönetimi ekranını gösterir
        /// </summary>
        private void ToplantiYonetimiGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "🏛️ Toplantı Kararları",
                Location = new Point(30, 20),
                Size = new Size(600, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Yeni Toplantı Ekle butonu
            var btnYeniToplanti = new SimpleButton
            {
                Text = "➕ Yeni Toplantı Ekle",
                Location = new Point(650, 20),
                Size = new Size(200, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Options =
                    {
                        UseBackColor = true,
                        UseForeColor = true
                    }
                }
            };
            btnYeniToplanti.Click += BtnYeniToplantiEkle_Click;
            
            // Sil butonu
            var btnToplantiSil = new SimpleButton
            {
                Text = "🗑️ Toplantı Sil",
                Location = new Point(860, 20),
                Size = new Size(150, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    Options =
                    {
                        UseBackColor = true,
                        UseForeColor = true
                    }
                }
            };
            btnToplantiSil.Click += BtnToplantiSil_Click;
            
            // Grid
            _gridToplanti = new GridControl
            {
                Location = new Point(30, 80),
                Size = new Size(1120, 500)
            };
            
            _viewToplanti = new GridView();
            _gridToplanti.MainView = _viewToplanti;
            _gridToplanti.ViewCollection.Add(_viewToplanti);
            
            _viewToplanti.OptionsCustomization.AllowQuickHideColumns = false;
            _viewToplanti.OptionsView.ShowGroupPanel = false;
            
            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(btnYeniToplanti);
            PnlMain.Controls.Add(btnToplantiSil);
            PnlMain.Controls.Add(_gridToplanti);
            
            ToplantilariYukle();
        }
        
        /// <summary>
        /// Toplantıları yükler
        /// </summary>
        private void ToplantilariYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT TOP 50 
                                  ToplantiId, 
                                  ToplantiTarihi, 
                                  ToplantiKonusu,
                                  LEFT(ToplantiKararlari, 100) AS KararlarOzet,
                                  KatilanSayisi,
                                  OlusturmaTarihi
                                  FROM Toplanti 
                                  WHERE Aktif = 1 
                                  ORDER BY ToplantiTarihi DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Toplantilar");
                        
                        _gridToplanti.DataSource = ds.Tables["Toplantilar"];
                        
                        if (_viewToplanti.Columns.Count > 0)
                        {
                            _viewToplanti.Columns["ToplantiId"].Visible = false;
                            _viewToplanti.Columns["ToplantiTarihi"].Caption = "📅 Toplantı Tarihi";
                            _viewToplanti.Columns["ToplantiKonusu"].Caption = "📋 Konu";
                            _viewToplanti.Columns["KararlarOzet"].Caption = "📝 Kararlar (Özet)";
                            _viewToplanti.Columns["KatilanSayisi"].Caption = "👥 Katılımcı";
                            _viewToplanti.Columns["OlusturmaTarihi"].Caption = "🕐 Oluşturulma";
                            
                            _viewToplanti.Columns["ToplantiTarihi"].Width = 150;
                            _viewToplanti.Columns["ToplantiKonusu"].Width = 250;
                            _viewToplanti.Columns["KararlarOzet"].Width = 400;
                            _viewToplanti.Columns["KatilanSayisi"].Width = 100;
                            _viewToplanti.Columns["OlusturmaTarihi"].Width = 150;
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
        /// Yeni toplantı ekle
        /// </summary>
        private void BtnYeniToplantiEkle_Click(object sender, EventArgs e)
        {
            var frm = new FrmToplantiEkle(_userId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ToplantilariYukle();
            }
        }

        /// <summary>
        /// Toplantı sil
        /// </summary>
        private void BtnToplantiSil_Click(object sender, EventArgs e)
        {
            if (_viewToplanti.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz toplantıyı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var toplantiId = (int)_viewToplanti.GetFocusedRowCellValue("ToplantiId");

            if (XtraMessageBox.Show("Bu toplantıyı silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        var query = "UPDATE Toplanti SET Aktif = 0 WHERE ToplantiId = @ToplantiId";
                        
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ToplantiId", toplantiId);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    XtraMessageBox.Show("Toplantı başarıyla silindi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ToplantilariYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// AI Tahminler ekranını gösterir
        /// </summary>
        private void AITahminlerGoster()
        {
            // Başlık
            var lblBaslik = new LabelControl
            {
                Text = "🤖 AI Daire Analizi",
                Location = new Point(30, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };

            // Daire seçimi
            var lblDaireSec = new LabelControl
            {
                Text = "Daire Seç:",
                Location = new Point(30, 80),
                Size = new Size(100, 25),
                Appearance =
                {
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(24, 38, 64)
                }
            };

            var cmbDaire = new ComboBoxEdit
            {
                Location = new Point(140, 78),
                Size = new Size(300, 30),
                Properties = { TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor }
            };

            // Daireleri yükle
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"SELECT d.DaireId, CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo, ' (', ISNULL(ds.AdSoyad, 'Sahip Yok'), ')') AS DaireBilgi 
                                  FROM Daire d
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  LEFT JOIN DaireSahibi ds ON d.DaireId = ds.DaireId AND ds.Aktif = 1
                                  ORDER BY k.KatNo, d.DaireNo";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var daireId = (int)reader["DaireId"];
                                var daireBilgi = reader["DaireBilgi"].ToString();
                                cmbDaire.Properties.Items.Add(new { DaireId = daireId, DaireBilgi = daireBilgi });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Daireler yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Analiz butonu
            var btnAnaliz = new SimpleButton
            {
                Text = "🔍 Analiz Yap",
                Location = new Point(460, 78),
                Size = new Size(150, 30),
                Appearance =
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BackColor = Color.FromArgb(156, 39, 176),
                    ForeColor = Color.White,
                    Options = { UseBackColor = true, UseForeColor = true }
                }
            };

            // Sonuç alanı
            var memoSonuc = new MemoEdit
            {
                Location = new Point(30, 130),
                Size = new Size(1120, 450)
            };
            memoSonuc.Properties.ReadOnly = true;
            memoSonuc.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            memoSonuc.Properties.Appearance.Font = new Font("Segoe UI", 10F);
            memoSonuc.Properties.Appearance.ForeColor = Color.FromArgb(33, 33, 33);

            // Yükleme göstergesi
            var lblYukleniyor = new LabelControl
            {
                Text = "⏳ Analiz yapılıyor, lütfen bekleyin...",
                Location = new Point(30, 590),
                Size = new Size(500, 25),
                Visible = false,
                Appearance =
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(156, 39, 176)
                }
            };

            // Analiz butonu click event
            btnAnaliz.Click += async (s, e) =>
            {
                if (cmbDaire.SelectedItem == null)
                {
                    XtraMessageBox.Show("Lütfen bir daire seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Anonymous type'tan DaireId'yi al
                int daireId = 0;
                var selectedItem = cmbDaire.SelectedItem;
                if (selectedItem != null)
                {
                    var itemType = selectedItem.GetType();
                    var daireIdProperty = itemType.GetProperty("DaireId");
                    if (daireIdProperty != null)
                    {
                        daireId = (int)daireIdProperty.GetValue(selectedItem);
                    }
                }
                
                if (daireId == 0)
                {
                    XtraMessageBox.Show("Daire seçimi geçersiz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnAnaliz.Enabled = false;
                lblYukleniyor.Visible = true;
                memoSonuc.Text = "";

                try
                {
                    // Daire bilgilerini al
                    string daireBilgisi = "";
                    string odemeGecmisi = "";
                    string mevcutBorclar = "";

                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Daire bilgisi
                        var daireQuery = @"SELECT CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireNo, 
                                          ISNULL(ds.AdSoyad, 'Sahip Bilgisi Yok') AS AdSoyad
                                          FROM Daire d
                                          INNER JOIN Kat k ON d.KatId = k.KatId
                                          LEFT JOIN DaireSahibi ds ON d.DaireId = ds.DaireId AND ds.Aktif = 1
                                          WHERE d.DaireId = @DaireId";
                        
                        using (var cmd = new SqlCommand(daireQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@DaireId", daireId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    daireBilgisi = $"{reader["DaireNo"]} - {reader["AdSoyad"]}";
                                }
                            }
                        }

                        // Ödeme geçmişi (son 12 ay)
                        var gecmisQuery = @"SELECT 
                                            COUNT(CASE WHEN OdendiMi = 1 THEN 1 END) AS OdemeSayisi,
                                            COUNT(*) AS ToplamAidat,
                                            AVG(CASE WHEN OdendiMi = 1 THEN DATEDIFF(day, Tarih, GETDATE()) END) AS OrtalamaOdemeGunu
                                            FROM Aidat
                                            WHERE DaireId = @DaireId 
                                            AND Tarih >= DATEADD(month, -12, GETDATE())";
                        
                        using (var cmd = new SqlCommand(gecmisQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@DaireId", daireId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int odemeSayisi = reader["OdemeSayisi"] != DBNull.Value ? Convert.ToInt32(reader["OdemeSayisi"]) : 0;
                                    int toplamAidat = reader["ToplamAidat"] != DBNull.Value ? Convert.ToInt32(reader["ToplamAidat"]) : 0;
                                    double ortalamaGun = reader["OrtalamaOdemeGunu"] != DBNull.Value ? Convert.ToDouble(reader["OrtalamaOdemeGunu"]) : 0;
                                    double odemeOrani = toplamAidat > 0 ? (odemeSayisi * 100.0 / toplamAidat) : 0;
                                    
                                    odemeGecmisi = $"Son 12 ayda {odemeSayisi}/{toplamAidat} aidat ödendi (%{odemeOrani:F1}). Ortalama ödeme süresi: {ortalamaGun:F0} gün.";
                                }
                            }
                        }

                        // Mevcut borçlar
                        var borcQuery = @"SELECT 
                                          COUNT(*) AS GecikmisSayisi,
                                          SUM(Tutar) AS ToplamBorc,
                                          MAX(Tarih) AS EnEskiBorcTarihi
                                          FROM Aidat
                                          WHERE DaireId = @DaireId 
                                          AND OdendiMi = 0
                                          AND Tarih < GETDATE()";
                        
                        using (var cmd = new SqlCommand(borcQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@DaireId", daireId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int gecikmisSayisi = reader["GecikmisSayisi"] != DBNull.Value ? Convert.ToInt32(reader["GecikmisSayisi"]) : 0;
                                    decimal toplamBorc = reader["ToplamBorc"] != DBNull.Value ? Convert.ToDecimal(reader["ToplamBorc"]) : 0;
                                    DateTime? enEskiTarih = reader["EnEskiBorcTarihi"] != DBNull.Value ? Convert.ToDateTime(reader["EnEskiBorcTarihi"]) : (DateTime?)null;
                                    
                                    if (gecikmisSayisi > 0)
                                    {
                                        mevcutBorclar = $"{gecikmisSayisi} adet gecikmiş aidat. Toplam borç: {toplamBorc:N2} TL. En eski borç: {enEskiTarih?.ToString("dd.MM.yyyy") ?? "Bilinmiyor"}";
                                    }
                                    else
                                    {
                                        mevcutBorclar = "Gecikmiş borç bulunmuyor.";
                                    }
                                }
                            }
                        }
                    }

                    // Gemini API ile analiz yap
                    var geminiService = new GeminiService();
                    string analizSonucu = await geminiService.AnalyzeDebtPredictionAsync(daireBilgisi, odemeGecmisi, mevcutBorclar);
                    
                    // Düzenli formatlanmış sonuç
                    memoSonuc.Text = "═══════════════════════════════════════════════════════════\r\n" +
                                     "                    📊 DAİRE ANALİZİ\r\n" +
                                     "═══════════════════════════════════════════════════════════\r\n\r\n" +
                                     $"🏠 DAİRE BİLGİSİ\r\n" +
                                     $"   {daireBilgisi}\r\n\r\n" +
                                     $"📈 ÖDEME GEÇMİŞİ\r\n" +
                                     $"   {odemeGecmisi}\r\n\r\n" +
                                     $"💰 MEVCUT BORÇLAR\r\n" +
                                     $"   {mevcutBorclar}\r\n\r\n" +
                                     "═══════════════════════════════════════════════════════════\r\n" +
                                     "                    🤖 AI ANALİZ SONUCU\r\n" +
                                     "═══════════════════════════════════════════════════════════\r\n\r\n" +
                                     $"{analizSonucu}";
                }
                catch (Exception ex)
                {
                    memoSonuc.Text = $"❌ Hata oluştu: {ex.Message}";
                }
                finally
                {
                    btnAnaliz.Enabled = true;
                    lblYukleniyor.Visible = false;
                }
            };

            PnlMain.Controls.Add(lblBaslik);
            PnlMain.Controls.Add(lblDaireSec);
            PnlMain.Controls.Add(cmbDaire);
            PnlMain.Controls.Add(btnAnaliz);
            PnlMain.Controls.Add(memoSonuc);
            PnlMain.Controls.Add(lblYukleniyor);
        }

        /// <summary>
        /// Çıkış butonu
        /// </summary>
        private void BtnCikis_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
