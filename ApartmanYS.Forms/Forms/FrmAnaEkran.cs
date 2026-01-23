using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ApartmanYS.Forms.Services;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Kullanıcı Ana Ekran - Modern ve şık tasarım
    /// </summary>
    public partial class FrmAnaEkran : XtraForm
    {
        private int _userId;
        private string _adSoyad;
        private string _kullaniciAdi;
        private int _daireId;
        
        // Sekme kontrolleri
        private PanelControl _pnlHeader;
        private PanelControl _pnlContent;
        private SimpleButton _btnSikayet;
        private SimpleButton _btnOtopark;
        private SimpleButton _btnAidatlar;
        private SimpleButton _btnDuyurular;
        private SimpleButton _btnEnerji;
        private SimpleButton _btnToplanti;
        private SimpleButton _btnHavuz;
        
        // İçerik kontrolleri
        private PanelControl _pnlSikayet;
        private PanelControl _pnlOtopark;
        private PanelControl _pnlAidatlar;
        private PanelControl _pnlDuyurular;
        private PanelControl _pnlToplanti;
        private PanelControl _pnlHavuz;
        
        // Toplantı kontrolleri
        private GridControl _gridToplanti;
        private GridView _viewToplanti;
        
        // Şikayet kontrolleri
        private TextEdit _txtSikayetBaslik;
        private MemoEdit _memoSikayetIcerik;
        private SimpleButton _btnSikayetGonder;
        
        // Otopark kontrolleri
        private TextEdit _txtPlaka;
        private SimpleButton _btnAracEkle;
        private SimpleButton _btnAracSil;
        private GridControl _gridAraclar;
        private GridView _viewAraclar;
        private LabelControl _lblAracDurum;
        
        // Aidatlar kontrolleri
        private GridControl _gridAidatlar;
        private GridView _viewAidatlar;
        
        // Duyurular kontrolleri
        private GridControl _gridDuyurular;
        private GridView _viewDuyurular;
        
        // Enerji takip kontrolleri
        private PanelControl _pnlEnerji;
        private GridControl _gridEnerji;
        private GridView _viewEnerji;
        private LabelControl _lblEnerjiTasarruf;
        
        // Tema kontrolleri
        private SimpleButton _btnTema;
        private bool _geceModu = false;
        
        // AI Sohbet kontrolleri
        private PanelControl _pnlAISohbet;
        private SimpleButton _btnAISohbetToggle;
        private PanelControl _pnlSohbetMesajlar; // Scrollable panel for chat bubbles
        private System.Windows.Forms.Panel _pnlSohbetScroll; // Scroll container (Windows Forms Panel for AutoScroll)
        private TextEdit _txtAIMesaj;
        private SimpleButton _btnAIGonder;
        private bool _aiSohbetAcik = false;
        private List<string> _aiSohbetGecmisi = new List<string>();
        private GeminiService _geminiService;
        private int _sohbetYukseklik = 10; // Mesajların yüksekliğini takip et

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmAnaEkran(int userId, string adSoyad, string kullaniciTipi, string kullaniciAdi)
        {
            InitializeComponent();
            _userId = userId;
            _adSoyad = adSoyad;
            _kullaniciAdi = kullaniciAdi;
            
            // Kullanıcının DaireId'sini bul
            DaireIdBul();
            
            // Form tasarımı
            this.Text = $"Hoş Geldiniz - {adSoyad}";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 247, 250);
            
            // Arayüzü oluştur
            ArayuzOlustur();
            
            // Gecikmiş aidat bildirimi kontrol et
            GecikmisAidatBildirimiKontrol();
            
            // İlk sekme olarak Duyurular'ı göster
            SekmeDegistir(_btnDuyurular);
        }

        /// <summary>
        /// Kullanıcının DaireId'sini bul - DaireSahibi tablosundan AdSoyad ile eşleştirme yapılır
        /// </summary>
        private void DaireIdBul()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Kullanici tablosundan AdSoyad'ı al
                    var kullaniciQuery = @"SELECT AdSoyad FROM Kullanici WHERE KullaniciId = @UserId";
                    string kullaniciAdSoyad = null;
                    
                    using (var kullaniciCmd = new SqlCommand(kullaniciQuery, connection))
                    {
                        kullaniciCmd.Parameters.AddWithValue("@UserId", _userId);
                        var kullaniciResult = kullaniciCmd.ExecuteScalar();
                        
                        if (kullaniciResult != null && kullaniciResult != DBNull.Value)
                        {
                            kullaniciAdSoyad = kullaniciResult.ToString();
                        }
                    }
                    
                    // AdSoyad ile DaireSahibi tablosundan DaireId bul
                    if (!string.IsNullOrEmpty(kullaniciAdSoyad))
                    {
                        var daireQuery = @"SELECT TOP 1 d.DaireId
                                          FROM Daire d
                                          INNER JOIN DaireSahibi ds ON d.DaireId = ds.DaireId
                                          WHERE ds.AdSoyad = @AdSoyad AND ds.Aktif = 1";
                        
                        using (var daireCmd = new SqlCommand(daireQuery, connection))
                        {
                            daireCmd.Parameters.AddWithValue("@AdSoyad", kullaniciAdSoyad);
                            var daireResult = daireCmd.ExecuteScalar();
                            
                            if (daireResult != null && daireResult != DBNull.Value)
                            {
                                _daireId = Convert.ToInt32(daireResult);
                                return;
                            }
                        }
                    }
                    
                    // Eğer hala bulunamadıysa, constructor'dan gelen _adSoyad ile dene
                    if (_daireId == 0 && !string.IsNullOrEmpty(_adSoyad))
                    {
                        var daireQuery2 = @"SELECT TOP 1 d.DaireId
                                           FROM Daire d
                                           INNER JOIN DaireSahibi ds ON d.DaireId = ds.DaireId
                                           WHERE ds.AdSoyad = @AdSoyad AND ds.Aktif = 1";
                        
                        using (var daireCmd2 = new SqlCommand(daireQuery2, connection))
                        {
                            daireCmd2.Parameters.AddWithValue("@AdSoyad", _adSoyad);
                            var daireResult2 = daireCmd2.ExecuteScalar();
                            
                            if (daireResult2 != null && daireResult2 != DBNull.Value)
                            {
                                _daireId = Convert.ToInt32(daireResult2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Sessizce log yap, kullanıcıya sadece şikayet ve araç işlemleri çalışmayabilir uyarısı ver
                System.Diagnostics.Debug.WriteLine($"DaireId bulunamadı: {ex.Message}");
            }
        }

        /// <summary>
        /// Arayüzü oluştur
        /// </summary>
        private void ArayuzOlustur()
        {
            // Modern Header panel - Daha modern ve şık tasarım
            _pnlHeader = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 130,
                Appearance = { BackColor = Color.FromArgb(33, 37, 41) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };
            
            // Modern gradient efekti için bir panel ekle - Daha belirgin
            var pnlGradient = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 4,
                Appearance = { BackColor = Color.FromArgb(0, 150, 136) }
            };
            
            // Apartman resmi (simge) - Modern ve düzgün konumlu
            var picApartman = new PictureEdit
            {
                Location = new Point(40, 30),
                Size = new Size(50, 50)
            };
            picApartman.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picApartman.Properties.ShowMenu = false;
            picApartman.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            // Resmi yükle - farklı konumlardan dene
            try
            {
                string[] possiblePaths = {
                    Application.StartupPath + @"\images\apartman.jpg",
                    Application.StartupPath + @"\apartman.jpg",
                    Application.StartupPath + @"\..\images\apartman.jpg",
                    Application.StartupPath + @"\Resources\apartman.jpg",
                    Application.StartupPath + @"\..\..\images\apartman.jpg",
                    System.IO.Path.Combine(Application.StartupPath, "images", "apartman.jpg"),
                    System.IO.Path.Combine(Application.StartupPath, "apartman.jpg"),
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "images", "apartman.jpg"),
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "apartman.jpg")
                };
                
                bool resimYuklendi = false;
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        picApartman.Image = Image.FromFile(path);
                        resimYuklendi = true;
                        break;
                    }
                }
                
                // Eğer resim bulunamadıysa, basit bir ikon oluştur
                if (!resimYuklendi)
                {
                    // Varsayılan bir ikon oluştur (apartman simgesi için)
                    var bmp = new Bitmap(60, 60);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0, 168, 80)), 0, 0, 60, 60);
                        g.DrawString("🏠", new Font("Segoe UI", 32), Brushes.White, new PointF(10, 8));
                    }
                    picApartman.Image = bmp;
                }
            }
            catch
            {
                // Hata durumunda varsayılan ikon
                var bmp = new Bitmap(60, 60);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 168, 80)), 0, 0, 60, 60);
                    g.DrawString("🏠", new Font("Segoe UI", 32), Brushes.White, new PointF(10, 8));
                }
                picApartman.Image = bmp;
            }
            
            var lblBaslik = new LabelControl
            {
                Text = $"Hoş Geldiniz, {_adSoyad}",
                Location = new Point(100, 32),
                Size = new Size(700, 50),
                Appearance =
                {
                    Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Daire bilgisi göster (eğer varsa) - Modern tasarım
            var lblDaireBilgi = new LabelControl
            {
                Text = _daireId > 0 ? "Daire Bilgisi Yükleniyor..." : "",
                Location = new Point(100, 88),
                Size = new Size(600, 28),
                Appearance =
                {
                    Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(220, 220, 220),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near }
                }
            };
            
            // Daire bilgisini yükle
            if (_daireId > 0)
            {
                DaireBilgisiniYukle(lblDaireBilgi);
            }
            
            var btnCikis = new SimpleButton
            {
                Text = "🚪 Çıkış",
                Location = new Point(1070, 35),
                Size = new Size(120, 45),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Appearance =
                {
                    BackColor = Color.FromArgb(220, 20, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    BorderColor = Color.FromArgb(180, 10, 40)
                }
            };
            btnCikis.Appearance.Options.UseBackColor = true;
            btnCikis.Appearance.Options.UseForeColor = true;
            btnCikis.Appearance.Options.UseBorderColor = true;
            btnCikis.AppearanceHovered.BackColor = Color.FromArgb(200, 10, 50);
            btnCikis.AppearanceHovered.Options.UseBackColor = true;
            btnCikis.Click += BtnCikis_Click;
            
            // Tema/Gece modu butonu
            _btnTema = new SimpleButton
            {
                Text = "🌙 Gece Modu",
                Location = new Point(930, 35),
                Size = new Size(120, 45),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Appearance =
                {
                    BackColor = Color.FromArgb(52, 58, 64),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    BorderColor = Color.FromArgb(40, 44, 48)
                }
            };
            _btnTema.Appearance.Options.UseBackColor = true;
            _btnTema.Appearance.Options.UseForeColor = true;
            _btnTema.Appearance.Options.UseBorderColor = true;
            _btnTema.AppearanceHovered.BackColor = Color.FromArgb(62, 68, 74);
            _btnTema.AppearanceHovered.Options.UseBackColor = true;
            _btnTema.Click += BtnTemaDegistir_Click;
            
            _pnlHeader.Controls.Add(picApartman);
            _pnlHeader.Controls.Add(lblBaslik);
            _pnlHeader.Controls.Add(lblDaireBilgi);
            _pnlHeader.Controls.Add(_btnTema);
            _pnlHeader.Controls.Add(btnCikis);
            
            // Modern sekme butonları paneli - Daha şık tasarım
            var pnlSekmeler = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 80,
                Appearance = { BackColor = Color.FromArgb(248, 249, 250) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };
            
            // Modern shadow efekti için alt çizgi
            var pnlSekmeCizgi = new PanelControl
            {
                Dock = DockStyle.Bottom,
                Height = 3,
                Appearance = { BackColor = Color.FromArgb(220, 224, 230) }
            };
            pnlSekmeler.Controls.Add(pnlSekmeCizgi);
            
            // Modern sekme butonları - hover efektleri ile
            _btnSikayet = new SimpleButton
            {
                Text = "📝 Şikayetler",
                Location = new Point(40, 18),
                Size = new Size(180, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnSikayet.Appearance.Options.UseBackColor = true;
            _btnSikayet.Appearance.Options.UseForeColor = true;
            _btnSikayet.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnSikayet.AppearanceHovered.Options.UseBackColor = true;
            _btnSikayet.Click += (s, e) => SekmeDegistir(_btnSikayet);
            
            _btnOtopark = new SimpleButton
            {
                Text = "🚗 Otopark",
                Location = new Point(240, 18),
                Size = new Size(180, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnOtopark.Appearance.Options.UseBackColor = true;
            _btnOtopark.Appearance.Options.UseForeColor = true;
            _btnOtopark.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnOtopark.AppearanceHovered.Options.UseBackColor = true;
            _btnOtopark.Click += (s, e) => SekmeDegistir(_btnOtopark);
            
            _btnAidatlar = new SimpleButton
            {
                Text = "💰 Aidatlarım",
                Location = new Point(440, 18),
                Size = new Size(180, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnAidatlar.Appearance.Options.UseBackColor = true;
            _btnAidatlar.Appearance.Options.UseForeColor = true;
            _btnAidatlar.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnAidatlar.AppearanceHovered.Options.UseBackColor = true;
            _btnAidatlar.Click += (s, e) => SekmeDegistir(_btnAidatlar);
            
            _btnDuyurular = new SimpleButton
            {
                Text = "📢 Duyurular",
                Location = new Point(640, 18),
                Size = new Size(180, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnDuyurular.Appearance.Options.UseBackColor = true;
            _btnDuyurular.Appearance.Options.UseForeColor = true;
            _btnDuyurular.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnDuyurular.AppearanceHovered.Options.UseBackColor = true;
            _btnDuyurular.Click += (s, e) => SekmeDegistir(_btnDuyurular);
            
            // Enerji Takip sekmesi
            _btnEnerji = new SimpleButton
            {
                Text = "⚡  Enerji",
                Location = new Point(840, 18),
                Size = new Size(160, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnEnerji.Appearance.Options.UseBackColor = true;
            _btnEnerji.Appearance.Options.UseForeColor = true;
            _btnEnerji.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnEnerji.AppearanceHovered.Options.UseBackColor = true;
            _btnEnerji.Click += (s, e) => SekmeDegistir(_btnEnerji);
            
            // Havuz sekmesi
            _btnHavuz = new SimpleButton
            {
                Text = "🏊 Havuz",
                Location = new Point(1020, 18),
                Size = new Size(160, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnHavuz.Appearance.Options.UseBackColor = true;
            _btnHavuz.Appearance.Options.UseForeColor = true;
            _btnHavuz.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnHavuz.AppearanceHovered.Options.UseBackColor = true;
            _btnHavuz.Click += (s, e) => SekmeDegistir(_btnHavuz);
            
            // Toplantı sekmesi
            _btnToplanti = new SimpleButton
            {
                Text = "🏛️ Toplantılar",
                Location = new Point(1200, 18),
                Size = new Size(160, 42),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Font = new Font("Segoe UI", 11F)
                }
            };
            _btnToplanti.Appearance.Options.UseBackColor = true;
            _btnToplanti.Appearance.Options.UseForeColor = true;
            _btnToplanti.AppearanceHovered.BackColor = Color.FromArgb(245, 247, 250);
            _btnToplanti.AppearanceHovered.Options.UseBackColor = true;
            _btnToplanti.Click += (s, e) => SekmeDegistir(_btnToplanti);
            
            pnlSekmeler.Controls.AddRange(new Control[] { _btnSikayet, _btnOtopark, _btnAidatlar, _btnDuyurular, _btnEnerji, _btnHavuz, _btnToplanti });
            
            // Modern içerik paneli - Daha açık ve temiz
            _pnlContent = new PanelControl
            {
                Dock = DockStyle.Fill,
                Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
            };
            
            // Sekme içeriklerini oluştur
            SikayetSekmesiOlustur();
            OtoparkSekmesiOlustur();
            AidatlarSekmesiOlustur();
            DuyurularSekmesiOlustur();
            EnerjiSekmesiOlustur();
            HavuzSekmesiOlustur();
            ToplantiSekmesiOlustur();
            
            // AI Sohbet widget'ını oluştur
            AISohbetWidgetOlustur();
            
            this.Controls.Add(_pnlContent);
            this.Controls.Add(pnlSekmeler);
            this.Controls.Add(pnlGradient);
            this.Controls.Add(_pnlHeader);
            
            // Gemini servisini başlat
            _geminiService = new GeminiService();
        }
        
        /// <summary>
        /// Daire bilgisini yükle ve göster
        /// </summary>
        private void DaireBilgisiniYukle(LabelControl lblDaireBilgi)
        {
            if (_daireId == 0)
            {
                lblDaireBilgi.Text = "";
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT CONCAT('Kat ', k.KatNo, ' - Daire ', d.DaireNo) AS DaireBilgi
                                  FROM Daire d
                                  INNER JOIN Kat k ON d.KatId = k.KatId
                                  WHERE d.DaireId = @DaireId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DaireId", _daireId);
                        var result = command.ExecuteScalar();
                        
                        if (result != null && result != DBNull.Value)
                        {
                            lblDaireBilgi.Text = $"📍 {result}";
                        }
                        else
                        {
                            lblDaireBilgi.Text = "";
                        }
                    }
                }
            }
            catch
            {
                lblDaireBilgi.Text = "";
            }
        }

        /// <summary>
        /// Sekme değiştir
        /// </summary>
        private void SekmeDegistir(SimpleButton seciliBtn)
        {
            // Tüm butonları sıfırla
            ResetSekmeButonlari();
            
            // Seçili butonu modern vurgu ile vurgula
            seciliBtn.Appearance.BackColor = Color.FromArgb(38, 50, 56);
            seciliBtn.Appearance.ForeColor = Color.White;
            seciliBtn.Appearance.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            seciliBtn.Appearance.BorderColor = Color.FromArgb(0, 168, 80);
            seciliBtn.Appearance.Options.UseBackColor = true;
            seciliBtn.Appearance.Options.UseForeColor = true;
            seciliBtn.Appearance.Options.UseBorderColor = true;
            
            // Tüm sekmeleri gizle
            _pnlSikayet.Visible = false;
            _pnlOtopark.Visible = false;
            _pnlAidatlar.Visible = false;
            _pnlDuyurular.Visible = false;
            if (_pnlEnerji != null)
                _pnlEnerji.Visible = false;
            if (_pnlHavuz != null)
                _pnlHavuz.Visible = false;
            if (_pnlToplanti != null)
                _pnlToplanti.Visible = false;
            
            // Seçili sekmeyi göster
            if (seciliBtn == _btnSikayet)
            {
                _pnlSikayet.Visible = true;
            }
            else if (seciliBtn == _btnOtopark)
            {
                _pnlOtopark.Visible = true;
                AraclariYukle();
            }
            else if (seciliBtn == _btnAidatlar)
            {
                _pnlAidatlar.Visible = true;
                AidatlariYukle();
            }
            else if (seciliBtn == _btnDuyurular)
            {
                _pnlDuyurular.Visible = true;
                DuyurulariYukle();
            }
            else if (seciliBtn == _btnEnerji)
            {
                if (_pnlEnerji != null)
                {
                    _pnlEnerji.Visible = true;
                    EnerjiVerileriniYukle();
                }
            }
            else if (seciliBtn == _btnHavuz)
            {
                if (_pnlHavuz != null)
                {
                    _pnlHavuz.Visible = true;
                    HavuzBilgileriniYukle();
                }
            }
            else if (seciliBtn == _btnToplanti)
            {
                if (_pnlToplanti != null)
                {
                    _pnlToplanti.Visible = true;
                    ToplantilariYukle();
                }
            }
        }

        /// <summary>
        /// Tüm sekme butonlarını sıfırla - Modern tasarım
        /// </summary>
        private void ResetSekmeButonlari()
        {
            _btnSikayet.Appearance.BackColor = Color.Transparent;
            _btnSikayet.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnSikayet.Appearance.Font = new Font("Segoe UI", 11F);
            _btnSikayet.Appearance.BorderColor = Color.Transparent;
            
            _btnOtopark.Appearance.BackColor = Color.Transparent;
            _btnOtopark.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnOtopark.Appearance.Font = new Font("Segoe UI", 11F);
            _btnOtopark.Appearance.BorderColor = Color.Transparent;
            
            _btnAidatlar.Appearance.BackColor = Color.Transparent;
            _btnAidatlar.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnAidatlar.Appearance.Font = new Font("Segoe UI", 11F);
            _btnAidatlar.Appearance.BorderColor = Color.Transparent;
            
            _btnDuyurular.Appearance.BackColor = Color.Transparent;
            _btnDuyurular.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnDuyurular.Appearance.Font = new Font("Segoe UI", 11F);
            _btnDuyurular.Appearance.BorderColor = Color.Transparent;
            
            _btnEnerji.Appearance.BackColor = Color.Transparent;
            _btnEnerji.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnEnerji.Appearance.Font = new Font("Segoe UI", 11F);
            _btnEnerji.Appearance.BorderColor = Color.Transparent;
            
            _btnHavuz.Appearance.BackColor = Color.Transparent;
            _btnHavuz.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnHavuz.Appearance.Font = new Font("Segoe UI", 11F);
            _btnHavuz.Appearance.BorderColor = Color.Transparent;
            
            _btnToplanti.Appearance.BackColor = Color.Transparent;
            _btnToplanti.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
            _btnToplanti.Appearance.Font = new Font("Segoe UI", 11F);
            _btnToplanti.Appearance.BorderColor = Color.Transparent;
        }

        /// <summary>
        /// Şikayet sekmesini oluştur
        /// </summary>
        private void SikayetSekmesiOlustur()
        {
            _pnlSikayet = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                Appearance = { BackColor = Color.FromArgb(245, 247, 250) }
            };
            
            // Modern kart tasarımı
            var pnlSikayetKart = new PanelControl
            {
                Location = new Point(0, 0),
                Size = new Size(650, 450),
                Appearance = 
                {
                    BackColor = Color.White,
                    BorderColor = Color.FromArgb(230, 230, 230)
                },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            };
            
            var lblBaslik = new LabelControl
            {
                Text = "📝 Şikayet Oluştur",
                Location = new Point(20, 20),
                Size = new Size(500, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(220, 20, 60)
                }
            };
            
            var lblBaslik2 = new LabelControl
            {
                Text = "Başlık:",
                Location = new Point(20, 80),
                Size = new Size(100, 25),
                Appearance = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) }
            };
            
            _txtSikayetBaslik = new TextEdit
            {
                Location = new Point(20, 110),
                Size = new Size(590, 35)
            };
            _txtSikayetBaslik.Properties.Appearance.Font = new Font("Segoe UI", 11F);
            _txtSikayetBaslik.Properties.Appearance.Options.UseFont = true;
            _txtSikayetBaslik.Properties.MaxLength = 200;
            
            var lblIcerik = new LabelControl
            {
                Text = "İçerik:",
                Location = new Point(20, 160),
                Size = new Size(100, 25),
                Appearance = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) }
            };
            
            _memoSikayetIcerik = new MemoEdit
            {
                Location = new Point(20, 190),
                Size = new Size(590, 180)
            };
            _memoSikayetIcerik.Properties.Appearance.Font = new Font("Segoe UI", 11F);
            _memoSikayetIcerik.Properties.Appearance.Options.UseFont = true;
            _memoSikayetIcerik.Properties.MaxLength = 2000;
            
            _btnSikayetGonder = new SimpleButton
            {
                Text = "✅ Şikayeti Gönder",
                Location = new Point(20, 385),
                Size = new Size(220, 45),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    BorderColor = Color.FromArgb(0, 140, 70)
                }
            };
            _btnSikayetGonder.Appearance.Options.UseBackColor = true;
            _btnSikayetGonder.Appearance.Options.UseForeColor = true;
            _btnSikayetGonder.Appearance.Options.UseBorderColor = true;
            _btnSikayetGonder.AppearanceHovered.BackColor = Color.FromArgb(0, 150, 70);
            _btnSikayetGonder.AppearanceHovered.Options.UseBackColor = true;
            _btnSikayetGonder.Click += BtnSikayetGonder_Click;
            
            // Kontrolleri karta ekle
            pnlSikayetKart.Controls.AddRange(new Control[] 
            { 
                lblBaslik, lblBaslik2, _txtSikayetBaslik, lblIcerik, _memoSikayetIcerik, _btnSikayetGonder 
            });
            
            // Kartı panele ekle
            _pnlSikayet.Controls.Add(pnlSikayetKart);
            
            _pnlContent.Controls.Add(_pnlSikayet);
        }

        /// <summary>
        /// Şikayet gönder - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void BtnSikayetGonder_Click(object sender, EventArgs e)
        {
            // Validasyon kontrolleri
            if (_daireId == 0)
            {
                XtraMessageBox.Show("Daire bilginiz bulunamadı. Lütfen yöneticiye başvurun.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var baslik = _txtSikayetBaslik.Text.Trim();
            var icerik = _memoSikayetIcerik.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(baslik))
            {
                XtraMessageBox.Show("Lütfen şikayet başlığı giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtSikayetBaslik.Focus();
                return;
            }
            
            if (baslik.Length > 200)
            {
                XtraMessageBox.Show("Başlık en fazla 200 karakter olabilir!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(icerik))
            {
                XtraMessageBox.Show("Lütfen şikayet içeriğini giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _memoSikayetIcerik.Focus();
                return;
            }
            
            if (icerik.Length > 2000)
            {
                XtraMessageBox.Show("İçerik en fazla 2000 karakter olabilir!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Optimize edilmiş sorgu - transaction ile
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var query = @"INSERT INTO Sikayet (DaireId, Baslik, Icerik, OlusturmaTarihi, Durum, Aktif)
                                          VALUES (@DaireId, @Baslik, @Icerik, GETDATE(), 'Beklemede', 1)";
                            
                            using (var command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@DaireId", _daireId);
                                command.Parameters.AddWithValue("@Baslik", baslik);
                                command.Parameters.AddWithValue("@Icerik", icerik);
                                
                                var affectedRows = command.ExecuteNonQuery();
                                
                                if (affectedRows > 0)
                                {
                                    // Admin'e bildirim ekle
                                    try
                                    {
                                        var bildirimQuery = @"INSERT INTO Bildirim (KullaniciId, DaireId, BildirimTipi, Baslik, Icerik, Okundu, Aktif)
                                                             VALUES (NULL, @DaireId, 'Sikayet', @Baslik, @Icerik, 0, 1)";
                                        
                                        using (var bildirimCmd = new SqlCommand(bildirimQuery, connection, transaction))
                                        {
                                            bildirimCmd.Parameters.AddWithValue("@DaireId", _daireId);
                                            bildirimCmd.Parameters.AddWithValue("@Baslik", $"Yeni Şikayet: {baslik}");
                                            bildirimCmd.Parameters.AddWithValue("@Icerik", icerik);
                                            bildirimCmd.ExecuteNonQuery();
                                        }
                                    }
                                    catch
                                    {
                                        // Bildirim eklenemezse devam et
                                    }
                                    
                                    transaction.Commit();
                                    
                                    XtraMessageBox.Show("Şikayetiniz başarıyla gönderildi! Admin panelinde görüntülenecektir.", "Başarılı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    // Form temizle
                                    _txtSikayetBaslik.Text = "";
                                    _memoSikayetIcerik.Text = "";
                                }
                                else
                                {
                                    transaction.Rollback();
                                    XtraMessageBox.Show("Şikayet kaydedilemedi. Lütfen tekrar deneyin.", "Hata",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
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
        /// Otopark sekmesini oluştur
        /// </summary>
        private void OtoparkSekmesiOlustur()
        {
            _pnlOtopark = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                Appearance = { BackColor = Color.FromArgb(245, 247, 250) }
            };
            
            var lblBaslik = new LabelControl
            {
                Text = "🚗 Otopark Yönetimi",
                Location = new Point(0, 0),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(127, 140, 141)
                }
            };
            
            var lblPlaka = new LabelControl
            {
                Text = "Plaka:",
                Location = new Point(0, 60),
                Size = new Size(100, 25),
                Appearance = { Font = new Font("Segoe UI", 11F) }
            };
            
            _txtPlaka = new TextEdit
            {
                Location = new Point(0, 90),
                Size = new Size(300, 35)
            };
            _txtPlaka.Properties.Appearance.Font = new Font("Segoe UI", 11F);
            _txtPlaka.Properties.Appearance.Options.UseFont = true;
            _txtPlaka.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    BtnAracEkle_Click(null, null);
                }
            };
            
            _btnAracEkle = new SimpleButton
            {
                Text = "➕ Araç Ekle",
                Location = new Point(320, 90),
                Size = new Size(150, 38),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    BorderColor = Color.FromArgb(0, 140, 70)
                }
            };
            _btnAracEkle.Appearance.Options.UseBackColor = true;
            _btnAracEkle.Appearance.Options.UseForeColor = true;
            _btnAracEkle.Appearance.Options.UseBorderColor = true;
            _btnAracEkle.AppearanceHovered.BackColor = Color.FromArgb(0, 150, 70);
            _btnAracEkle.AppearanceHovered.Options.UseBackColor = true;
            _btnAracEkle.Click += BtnAracEkle_Click;
            
            _btnAracSil = new SimpleButton
            {
                Text = "🗑️ Araç Sil",
                Location = new Point(480, 90),
                Size = new Size(150, 38),
                Appearance =
                {
                    BackColor = Color.FromArgb(220, 20, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    BorderColor = Color.FromArgb(180, 10, 40)
                }
            };
            _btnAracSil.Appearance.Options.UseBackColor = true;
            _btnAracSil.Appearance.Options.UseForeColor = true;
            _btnAracSil.Appearance.Options.UseBorderColor = true;
            _btnAracSil.AppearanceHovered.BackColor = Color.FromArgb(200, 10, 50);
            _btnAracSil.AppearanceHovered.Options.UseBackColor = true;
            _btnAracSil.Click += BtnAracSil_Click;
            
            // Otopark resmi
            var picOtopark = new PictureEdit
            {
                Location = new Point(650, 0),
                Size = new Size(280, 160)
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

            _lblAracDurum = new LabelControl
            {
                Location = new Point(0, 140),
                Size = new Size(600, 30),
                Appearance =
                {
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(38, 50, 56)
                }
            };
            
            _gridAraclar = new GridControl
            {
                Location = new Point(0, 180),
                Size = new Size(900, 400)
            };
            
            _viewAraclar = new GridView();
            _gridAraclar.MainView = _viewAraclar;
            _gridAraclar.ViewCollection.Add(_viewAraclar);
            _viewAraclar.OptionsView.ShowGroupPanel = false;
            _viewAraclar.OptionsView.ShowIndicator = false;
            
            _pnlOtopark.Controls.AddRange(new Control[] 
            { 
                lblBaslik, lblPlaka, _txtPlaka, _btnAracEkle, _btnAracSil, picOtopark, _lblAracDurum, _gridAraclar 
            });
            
            _pnlContent.Controls.Add(_pnlOtopark);
        }

        /// <summary>
        /// Araç ekle - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void BtnAracEkle_Click(object sender, EventArgs e)
        {
            // Validasyon kontrolleri
            if (_daireId == 0)
            {
                XtraMessageBox.Show("Daire bilginiz bulunamadı. Lütfen yöneticiye başvurun.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var plakaGirilen = _txtPlaka.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(plakaGirilen))
            {
                XtraMessageBox.Show("Lütfen plaka giriniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPlaka.Focus();
                return;
            }
            
            // Plaka format kontrolü
            var plaka = plakaGirilen.ToUpper().Replace(" ", "");
            
            if (plaka.Length < 5 || plaka.Length > 15)
            {
                XtraMessageBox.Show("Plaka 5-15 karakter arasında olmalıdır!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Plaka zaten var mı kontrol et - tüm aktif araçlar içinde (aynı daire değil)
                    var checkQuery = @"SELECT COUNT(*) FROM Arac 
                                       WHERE Plaka = @Plaka AND Aktif = 1";
                    
                    int mevcutAracSayisi = 0;
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Plaka", plaka);
                        mevcutAracSayisi = (int)checkCmd.ExecuteScalar();
                    }
                    
                    if (mevcutAracSayisi > 0)
                    {
                        // Aynı daireye mi ait kontrol et
                        var ayniDaireQuery = @"SELECT COUNT(*) FROM Arac 
                                               WHERE DaireId = @DaireId AND Plaka = @Plaka AND Aktif = 1";
                        
                        using (var ayniDaireCmd = new SqlCommand(ayniDaireQuery, connection))
                        {
                            ayniDaireCmd.Parameters.AddWithValue("@DaireId", _daireId);
                            ayniDaireCmd.Parameters.AddWithValue("@Plaka", plaka);
                            var ayniDaireCount = (int)ayniDaireCmd.ExecuteScalar();
                            
                            if (ayniDaireCount > 0)
                            {
                                XtraMessageBox.Show("Bu plaka zaten kayıtlı!", "Uyarı",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    
                    // Yeni araç ekle - transaction ile
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var insertQuery = @"INSERT INTO Arac (DaireId, Plaka, OlusturmaTarihi, Aktif)
                                                 VALUES (@DaireId, @Plaka, GETDATE(), 1)";
                            
                            using (var command = new SqlCommand(insertQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@DaireId", _daireId);
                                command.Parameters.AddWithValue("@Plaka", plaka);
                                
                                var affectedRows = command.ExecuteNonQuery();
                                
                                if (affectedRows > 0)
                                {
                                    transaction.Commit();
                                    
                                    XtraMessageBox.Show("Araç başarıyla eklendi! Admin panelindeki otopark listesine yansıyacaktır.", "Başarılı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    _txtPlaka.Text = "";
                                    AraclariYukle();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    XtraMessageBox.Show("Araç kaydedilemedi. Lütfen tekrar deneyin.", "Hata",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Unique constraint violation
                {
                    XtraMessageBox.Show("Bu plaka zaten başka bir dairede kayıtlı!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    XtraMessageBox.Show($"Veritabanı hatası: {sqlEx.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Araç sil - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void BtnAracSil_Click(object sender, EventArgs e)
        {
            // Validasyon kontrolleri
            if (_viewAraclar.GetFocusedRow() == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz aracı seçiniz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_daireId == 0)
            {
                XtraMessageBox.Show("Daire bilginiz bulunamadı. Lütfen yöneticiye başvurun.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var plaka = _viewAraclar.GetFocusedRowCellValue("Plaka")?.ToString();
            
            if (string.IsNullOrEmpty(plaka))
            {
                XtraMessageBox.Show("Plaka bilgisi alınamadı!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (XtraMessageBox.Show($"'{plaka}' plakalı aracı silmek istediğinize emin misiniz?", "Onay",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                    
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // Transaction ile güvenli silme
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var query = @"UPDATE Arac SET Aktif = 0 
                                              WHERE DaireId = @DaireId AND Plaka = @Plaka AND Aktif = 1";
                                
                                using (var command = new SqlCommand(query, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@DaireId", _daireId);
                                    command.Parameters.AddWithValue("@Plaka", plaka);
                                    
                                    var affectedRows = command.ExecuteNonQuery();
                                    
                                    if (affectedRows > 0)
                                    {
                                        transaction.Commit();
                                        
                                        XtraMessageBox.Show("Araç başarıyla silindi!", "Başarılı",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                        AraclariYukle();
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        XtraMessageBox.Show("Araç bulunamadı veya zaten silinmiş!", "Uyarı",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
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
        }

        /// <summary>
        /// Araçları yükle - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void AraclariYukle()
        {
            if (_daireId == 0)
            {
                _lblAracDurum.Text = "Daire bilginiz bulunamadığı için araç listesi gösterilemiyor.";
                _lblAracDurum.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
                _gridAraclar.DataSource = null;
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    _lblAracDurum.Text = "Veritabanı bağlantısı yapılamadı.";
                    _lblAracDurum.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT a.Plaka, a.OlusturmaTarihi, ISNULL(ds.OtoparkAlani, 'Belirtilmemiş') AS ParkYeri 
                                  FROM Arac a
                                  LEFT JOIN DaireSahibi ds ON a.DaireId = ds.DaireId AND ds.Aktif = 1
                                  WHERE a.DaireId = @DaireId AND a.Aktif = 1
                                  ORDER BY a.OlusturmaTarihi DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@DaireId", _daireId);
                        var ds = new DataSet();
                        adapter.Fill(ds, "Araclar");
                        
                        _gridAraclar.DataSource = ds.Tables["Araclar"];
                        
                        // Kolon ayarları
                        if (_viewAraclar.Columns["Plaka"] != null)
                        {
                            _viewAraclar.Columns["Plaka"].Caption = "Plaka";
                            _viewAraclar.Columns["Plaka"].Width = 200;
                            _viewAraclar.Columns["Plaka"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }
                        if (_viewAraclar.Columns["OlusturmaTarihi"] != null)
                        {
                            _viewAraclar.Columns["OlusturmaTarihi"].Caption = "Kayıt Tarihi";
                            _viewAraclar.Columns["OlusturmaTarihi"].Width = 150;
                            _viewAraclar.Columns["OlusturmaTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            _viewAraclar.Columns["OlusturmaTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy";
                        }
                        if (_viewAraclar.Columns["ParkYeri"] != null)
                        {
                            _viewAraclar.Columns["ParkYeri"].Caption = "Park Yeri";
                            _viewAraclar.Columns["ParkYeri"].Width = 150;
                            _viewAraclar.Columns["ParkYeri"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }
                        
                        // Modern durum bilgisi
                        var aracSayisi = ds.Tables["Araclar"].Rows.Count;
                        if (aracSayisi > 0)
                        {
                            _lblAracDurum.Text = $"✅ Otoparkta {aracSayisi} aracınız kayıtlı";
                            _lblAracDurum.Appearance.ForeColor = Color.FromArgb(0, 168, 80);
                            _lblAracDurum.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                        }
                        else
                        {
                            _lblAracDurum.Text = "ℹ️ Otoparkta henüz aracınız bulunmamaktadır";
                            _lblAracDurum.Appearance.ForeColor = Color.FromArgb(127, 140, 141);
                            _lblAracDurum.Appearance.Font = new Font("Segoe UI", 12F);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _lblAracDurum.Text = $"⚠️ Veritabanı hatası: {sqlEx.Message}";
                _lblAracDurum.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
                System.Diagnostics.Debug.WriteLine($"Araçlar yüklenirken hata: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _lblAracDurum.Text = $"⚠️ Bir hata oluştu: {ex.Message}";
                _lblAracDurum.Appearance.ForeColor = Color.FromArgb(220, 20, 60);
                System.Diagnostics.Debug.WriteLine($"Araçlar yüklenirken hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Aidatlar sekmesini oluştur
        /// </summary>
        private void AidatlarSekmesiOlustur()
        {
            _pnlAidatlar = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                Appearance = { BackColor = Color.FromArgb(245, 247, 250) }
            };
            
            var lblBaslik = new LabelControl
            {
                Text = "💰 Aidatlarım",
                Location = new Point(0, 0),
                Size = new Size(400, 40),
                Appearance =
                {
                    Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 152, 0)
                }
            };
            
            _gridAidatlar = new GridControl
            {
                Location = new Point(0, 60),
                Size = new Size(900, 500)
            };
            
            _viewAidatlar = new GridView();
            _gridAidatlar.MainView = _viewAidatlar;
            _gridAidatlar.ViewCollection.Add(_viewAidatlar);
            _viewAidatlar.OptionsView.ShowGroupPanel = false;
            
            _pnlAidatlar.Controls.AddRange(new Control[] { lblBaslik, _gridAidatlar });
            _pnlContent.Controls.Add(_pnlAidatlar);
        }

        /// <summary>
        /// Aidatları yükle - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void AidatlariYukle()
        {
            if (_daireId == 0)
            {
                _gridAidatlar.DataSource = null;
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT Tutar, Tarih, 
                                  CASE WHEN OdendiMi = 1 THEN '✅ Ödendi' ELSE '⏳ Beklemede' END AS Durum,
                                  OdendiMi
                                  FROM Aidat
                                  WHERE DaireId = @DaireId
                                  ORDER BY Tarih DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@DaireId", _daireId);
                        var ds = new DataSet();
                        adapter.Fill(ds, "Aidatlar");
                        
                        _gridAidatlar.DataSource = ds.Tables["Aidatlar"];
                        
                        // Modern kolon ayarları
                        if (_viewAidatlar.Columns["Tutar"] != null)
                        {
                            _viewAidatlar.Columns["Tutar"].Caption = "💰 Tutar (TL)";
                            _viewAidatlar.Columns["Tutar"].Width = 180;
                            _viewAidatlar.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            _viewAidatlar.Columns["Tutar"].DisplayFormat.FormatString = "N2";
                            _viewAidatlar.Columns["Tutar"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            _viewAidatlar.Columns["Tutar"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        }
                        if (_viewAidatlar.Columns["Tarih"] != null)
                        {
                            _viewAidatlar.Columns["Tarih"].Caption = "📅 Tarih";
                            _viewAidatlar.Columns["Tarih"].Width = 220;
                            _viewAidatlar.Columns["Tarih"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            _viewAidatlar.Columns["Tarih"].DisplayFormat.FormatString = "dd.MM.yyyy";
                            _viewAidatlar.Columns["Tarih"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }
                        if (_viewAidatlar.Columns["Durum"] != null)
                        {
                            _viewAidatlar.Columns["Durum"].Caption = "📊 Durum";
                            _viewAidatlar.Columns["Durum"].Width = 180;
                            _viewAidatlar.Columns["Durum"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            
                            // Durum renklendirme - Ödendi olanları yeşil yap
                            var condition = new DevExpress.XtraGrid.StyleFormatCondition();
                            condition.Column = _viewAidatlar.Columns["Durum"];
                            condition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
                            condition.Expression = "[OdendiMi] = 1";
                            condition.Appearance.ForeColor = Color.FromArgb(0, 168, 80);
                            condition.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                            _viewAidatlar.FormatConditions.Add(condition);
                        }
                        if (_viewAidatlar.Columns["OdendiMi"] != null)
                        {
                            _viewAidatlar.Columns["OdendiMi"].Visible = false;
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
        /// Duyurular sekmesini oluştur
        /// </summary>
        private void DuyurularSekmesiOlustur()
        {
            _pnlDuyurular = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
            };
            
            // Modern başlık tasarımı
            var lblBaslik = new LabelControl
            {
                Text = "📢 Duyurular",
                Location = new Point(0, 10),
                Size = new Size(500, 45),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 152, 0)
                }
            };
            
            _gridDuyurular = new GridControl
            {
                Location = new Point(0, 70),
                Size = new Size(1100, 550),
                Dock = DockStyle.Fill
            };
            _gridDuyurular.BackColor = Color.White;
            
            _viewDuyurular = new GridView();
            _gridDuyurular.MainView = _viewDuyurular;
            _gridDuyurular.ViewCollection.Add(_viewDuyurular);
            _viewDuyurular.OptionsView.ShowGroupPanel = false;
            _viewDuyurular.OptionsView.AllowCellMerge = false;
            _viewDuyurular.OptionsView.AutoCalcPreviewLineCount = true;
            _viewDuyurular.OptionsSelection.EnableAppearanceFocusedCell = false;
            _viewDuyurular.OptionsView.ShowIndicator = true;
            _viewDuyurular.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            
            _pnlDuyurular.Controls.AddRange(new Control[] { lblBaslik, _gridDuyurular });
            _pnlContent.Controls.Add(_pnlDuyurular);
        }

        /// <summary>
        /// Duyuruları yükle - Optimize edilmiş veritabanı işlemi
        /// </summary>
        private void DuyurulariYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    XtraMessageBox.Show("Veritabanı bağlantı ayarları bulunamadı!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Tüm aktif duyuruları getir - geçerlilik tarihi kontrol ediliyor
                    var query = @"SELECT Baslik, 
                                  Icerik,
                                  OlusturmaTarihi, 
                                  GecerlilikBitis
                                  FROM Duyuru
                                  WHERE Aktif = 1
                                  ORDER BY OlusturmaTarihi DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Duyurular");
                        
                        // Grid'e veri yükle - her durumda (veri olsa da olmasa da)
                        _gridDuyurular.DataSource = ds.Tables["Duyurular"];
                        
                        // Grid view'ı refresh et ve güncelle
                        _viewDuyurular.RefreshData();
                        _gridDuyurular.RefreshDataSource();
                        
                        // Kolon ayarları - veri yüklendikten sonra yapılmalı
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(250);
                        
                        // Debug: Kaç satır yüklendi kontrol et
                        var satirSayisi = ds.Tables["Duyurular"].Rows.Count;
                        System.Diagnostics.Debug.WriteLine($"Duyurular yüklendi: {satirSayisi} satır");
                        
                        // Eğer veri yoksa bilgilendirme mesajı göster (opsiyonel)
                        if (satirSayisi == 0)
                        {
                            // Boş veri durumu - grid zaten boş gösterilecek
                            System.Diagnostics.Debug.WriteLine("Duyuru bulunamadı - Grid boş gösterilecek");
                        }
                        
                        // Kolon ayarları - her durumda kontrol et
                        if (_viewDuyurular.Columns.Count > 0)
                        {
                            // Modern kolon ayarları
                            if (_viewDuyurular.Columns["Baslik"] != null)
                            {
                                _viewDuyurular.Columns["Baslik"].Caption = "📢 Başlık";
                                _viewDuyurular.Columns["Baslik"].Width = 250;
                                _viewDuyurular.Columns["Baslik"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                _viewDuyurular.Columns["Baslik"].AppearanceHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                            }
                            if (_viewDuyurular.Columns["Icerik"] != null)
                            {
                                _viewDuyurular.Columns["Icerik"].Caption = "📄 İçerik";
                                _viewDuyurular.Columns["Icerik"].Width = 600;
                                _viewDuyurular.Columns["Icerik"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                _viewDuyurular.Columns["Icerik"].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                                _viewDuyurular.Columns["Icerik"].AppearanceCell.Options.UseTextOptions = true;
                                _viewDuyurular.Columns["Icerik"].OptionsColumn.AllowEdit = false;
                                _viewDuyurular.Columns["Icerik"].OptionsColumn.FixedWidth = false;
                                // Satır yüksekliğini otomatik ayarla
                                _viewDuyurular.OptionsView.RowAutoHeight = true;
                            }
                            if (_viewDuyurular.Columns["OlusturmaTarihi"] != null)
                            {
                                _viewDuyurular.Columns["OlusturmaTarihi"].Caption = "📅 Tarih";
                                _viewDuyurular.Columns["OlusturmaTarihi"].Width = 160;
                                _viewDuyurular.Columns["OlusturmaTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                _viewDuyurular.Columns["OlusturmaTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy";
                                _viewDuyurular.Columns["OlusturmaTarihi"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            }
                            if (_viewDuyurular.Columns["GecerlilikBitis"] != null)
                            {
                                _viewDuyurular.Columns["GecerlilikBitis"].Caption = "⏰ Geçerlilik";
                                _viewDuyurular.Columns["GecerlilikBitis"].Width = 160;
                                _viewDuyurular.Columns["GecerlilikBitis"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                _viewDuyurular.Columns["GecerlilikBitis"].DisplayFormat.FormatString = "dd.MM.yyyy";
                                _viewDuyurular.Columns["GecerlilikBitis"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            }
                            
                            // Satır yüksekliği otomatik ayarlanıyor (RowAutoHeight = true)
                            // Ancak minimum yükseklik ayarla - Daha geniş satırlar
                            _viewDuyurular.RowHeight = 90;
                        }
                        
                        // Debug: Kaç satır yüklendi kontrol et
                        System.Diagnostics.Debug.WriteLine($"Duyurular yüklendi: {ds.Tables["Duyurular"].Rows.Count} satır");
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
        /// Enerji Takip sekmesini oluştur
        /// </summary>
        private void EnerjiSekmesiOlustur()
        {
            _pnlEnerji = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
            };
            
            // Modern başlık tasarımı
            var lblBaslik = new LabelControl
            {
                Text = "⚡  Enerji Takip ve Tasarruf",
                Location = new Point(0, 10),
                Size = new Size(500, 45),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 168, 80)
                }
            };
            
            // Tasarruf geri bildirimi kartı
            _lblEnerjiTasarruf = new LabelControl
            {
                Text = "Hesaplanıyor...",
                Location = new Point(0, 70),
                Size = new Size(1020, 80),
                Appearance =
                {
                    BackColor = Color.FromArgb(0, 168, 80),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center, VAlignment = DevExpress.Utils.VertAlignment.Center },
                    BorderColor = Color.FromArgb(0, 148, 70)
                },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            };
            _lblEnerjiTasarruf.Appearance.Options.UseTextOptions = true;
            _lblEnerjiTasarruf.Appearance.Options.UseBackColor = true;
            _lblEnerjiTasarruf.Appearance.Options.UseForeColor = true;
            _lblEnerjiTasarruf.Appearance.Options.UseBorderColor = true;
            
            // Grid
            _gridEnerji = new GridControl
            {
                Location = new Point(0, 170),
                Size = new Size(1020, 400),
                BackColor = Color.White
            };
            
            _viewEnerji = new GridView();
            _gridEnerji.MainView = _viewEnerji;
            _gridEnerji.ViewCollection.Add(_viewEnerji);
            _viewEnerji.OptionsView.ShowGroupPanel = false;
            _viewEnerji.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            
            _pnlEnerji.Controls.AddRange(new Control[] { lblBaslik, _lblEnerjiTasarruf, _gridEnerji });
            _pnlContent.Controls.Add(_pnlEnerji);
            
            // Enerji verilerini yükle
            EnerjiVerileriniYukle();
        }
        
        /// <summary>
        /// Enerji verilerini yükle ve tasarruf hesapla
        /// </summary>
        private void EnerjiVerileriniYukle()
        {
            if (_daireId == 0)
            {
                _lblEnerjiTasarruf.Text = "⚠️ Daire bilginiz bulunamadı.";
                _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(255, 193, 7);
                return;
            }
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    _lblEnerjiTasarruf.Text = "⚠️ Veritabanı bağlantı hatası!";
                    _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(220, 20, 60);
                    return;
                }
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Önce tabloyu kontrol et - yoksa oluşturma scripti çalıştırılmalı
                    var tabloKontrolQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'EnerjiTuketim'";
                    using (var tabloCmd = new SqlCommand(tabloKontrolQuery, connection))
                    {
                        var tabloVar = Convert.ToInt32(tabloCmd.ExecuteScalar()) > 0;
                        
                        if (!tabloVar)
                        {
                            _lblEnerjiTasarruf.Text = "📋 EnerjiTuketim tablosu bulunamadı. Lütfen 'EnerjiTakip_TabloOlustur.sql' scriptini çalıştırın.";
                            _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(52, 58, 64);
                            _gridEnerji.DataSource = null;
                            return;
                        }
                    }
                    
                    // Enerji tüketim verilerini yükle - Son 6 ay
                    var query = @"SELECT TOP 12 TuketimTipi, TuketimMiktari, Birim, Tarih, Yil, Ay
                                  FROM EnerjiTuketim
                                  WHERE DaireId = @DaireId AND Aktif = 1
                                  ORDER BY Yil DESC, Ay DESC, Tarih DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@DaireId", _daireId);
                        var ds = new DataSet();
                        adapter.Fill(ds, "EnerjiTuketim");
                        
                        _gridEnerji.DataSource = ds.Tables["EnerjiTuketim"];
                        
                        // Kolon ayarları
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                        
                        if (_viewEnerji.Columns.Count > 0)
                        {
                            if (_viewEnerji.Columns["TuketimTipi"] != null)
                            {
                                _viewEnerji.Columns["TuketimTipi"].Caption = "⚡ Tüketim Tipi";
                                _viewEnerji.Columns["TuketimTipi"].Width = 200;
                            }
                            if (_viewEnerji.Columns["TuketimMiktari"] != null)
                            {
                                _viewEnerji.Columns["TuketimMiktari"].Caption = "📊 Miktar";
                                _viewEnerji.Columns["TuketimMiktari"].Width = 150;
                                _viewEnerji.Columns["TuketimMiktari"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                _viewEnerji.Columns["TuketimMiktari"].DisplayFormat.FormatString = "N2";
                            }
                            if (_viewEnerji.Columns["Birim"] != null)
                            {
                                _viewEnerji.Columns["Birim"].Caption = "📏 Birim";
                                _viewEnerji.Columns["Birim"].Width = 100;
                            }
                            if (_viewEnerji.Columns["Tarih"] != null)
                            {
                                _viewEnerji.Columns["Tarih"].Caption = "📅 Tarih";
                                _viewEnerji.Columns["Tarih"].Width = 150;
                                _viewEnerji.Columns["Tarih"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                _viewEnerji.Columns["Tarih"].DisplayFormat.FormatString = "dd.MM.yyyy";
                            }
                            if (_viewEnerji.Columns["Yil"] != null)
                            {
                                _viewEnerji.Columns["Yil"].Visible = false;
                            }
                            if (_viewEnerji.Columns["Ay"] != null)
                            {
                                _viewEnerji.Columns["Ay"].Visible = false;
                            }
                        }
                        
                        // Tasarruf hesaplama
                        EnerjiTasarrufHesapla(connection, ds.Tables["EnerjiTuketim"]);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _lblEnerjiTasarruf.Text = $"⚠️ Veritabanı hatası: {sqlEx.Message}";
                _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(220, 20, 60);
            }
            catch (Exception ex)
            {
                _lblEnerjiTasarruf.Text = $"⚠️ Hata: {ex.Message}";
                _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(220, 20, 60);
            }
        }
        
        /// <summary>
        /// Enerji tasarruf hesapla ve geri bildirim göster
        /// </summary>
        private void EnerjiTasarrufHesapla(SqlConnection connection, DataTable enerjiData)
        {
            if (enerjiData.Rows.Count < 2)
            {
                _lblEnerjiTasarruf.Text = "📊 Henüz yeterli veri yok. En az 2 ay veri gereklidir.";
                _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(52, 58, 64);
                return;
            }
            
            try
            {
                // Bu ay ve geçen ay toplam tüketim hesapla
                var buAy = DateTime.Now;
                var gecenAy = buAy.AddMonths(-1);
                
                decimal buAyToplam = 0;
                decimal gecenAyToplam = 0;
                
                foreach (DataRow row in enerjiData.Rows)
                {
                    var tarih = Convert.ToDateTime(row["Tarih"]);
                    var miktar = Convert.ToDecimal(row["TuketimMiktari"]);
                    
                    if (tarih.Year == buAy.Year && tarih.Month == buAy.Month)
                        buAyToplam += miktar;
                    else if (tarih.Year == gecenAy.Year && tarih.Month == gecenAy.Month)
                        gecenAyToplam += miktar;
                }
                
                if (gecenAyToplam == 0)
                {
                    _lblEnerjiTasarruf.Text = "📊 Geçen ay verisi bulunamadı.";
                    _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(52, 58, 64);
                    return;
                }
                
                // Yüzde değişim hesapla
                decimal yuzdeDegisim = ((buAyToplam - gecenAyToplam) / gecenAyToplam) * 100;
                
                if (yuzdeDegisim < 0)
                {
                    // Tasarruf var
                    decimal tasarrufYuzde = Math.Abs(yuzdeDegisim);
                    _lblEnerjiTasarruf.Text = $"✅ Bu ay enerji tasarrufun geçen aya göre %{tasarrufYuzde:F1} arttı! 🌱";
                    _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(0, 168, 80);
                }
                else if (yuzdeDegisim > 0)
                {
                    // Tüketim arttı
                    _lblEnerjiTasarruf.Text = $"⚠️ Bu ay tüketiminiz geçen aya göre %{yuzdeDegisim:F1} arttı. Tasarruf önlemlerini gözden geçirin.";
                    _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(255, 193, 7);
                }
                else
                {
                    _lblEnerjiTasarruf.Text = $"📊 Bu ay tüketiminiz geçen ay ile aynı. Tasarruf önlemlerini sürdürmeye devam edin.";
                    _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(52, 58, 64);
                }
            }
            catch (Exception ex)
            {
                _lblEnerjiTasarruf.Text = $"⚠️ Tasarruf hesaplanamadı: {ex.Message}";
                _lblEnerjiTasarruf.Appearance.BackColor = Color.FromArgb(220, 20, 60);
            }
        }
        
        /// <summary>
        /// Havuz sekmesini oluştur
        /// </summary>
        private void HavuzSekmesiOlustur()
        {
            _pnlHavuz = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
            };
            
            // Modern başlık tasarımı
            var lblBaslik = new LabelControl
            {
                Text = "🏊 Havuz Bilgileri",
                Location = new Point(0, 10),
                Size = new Size(600, 45),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 152, 219)
                }
            };
            
            _pnlHavuz.Controls.Add(lblBaslik);
            
            // Havuz resmi
            var picHavuz = new PictureEdit
            {
                Location = new Point(650, 10),
                Size = new Size(340, 220)
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
            
            _pnlHavuz.Controls.Add(picHavuz);
            _pnlContent.Controls.Add(_pnlHavuz);
            
            // Havuz bilgilerini yükle
            HavuzBilgileriniYukle();
        }
        
        /// <summary>
        /// Havuz bilgilerini yükle
        /// </summary>
        private void HavuzBilgileriniYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "SELECT TOP 1 BakimYuzdesi, DolulukOrani, SonBakimTarihi FROM Havuz ORDER BY HavuzId DESC";
                    
                    using (var adapter = new SqlDataAdapter(query, connection))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds, "Havuz");
                        
                        if (ds.Tables["Havuz"].Rows.Count > 0)
                        {
                            var row = ds.Tables["Havuz"].Rows[0];
                            var bakimYuzde = Convert.ToInt32(row["BakimYuzdesi"]);
                            var dolulukOrani = row["DolulukOrani"] != DBNull.Value ? Convert.ToInt32(row["DolulukOrani"]) : 0;
                            var sonBakimTarihi = row["SonBakimTarihi"] != DBNull.Value ? Convert.ToDateTime(row["SonBakimTarihi"]) : DateTime.MinValue;
                            
                            // Bakım bilgisi kartı
                            var lblBakim = new LabelControl
                            {
                                Text = $"Bakım Durumu: %{bakimYuzde}",
                                Location = new Point(0, 80),
                                Size = new Size(600, 50),
                                Appearance =
                                {
                                    Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                                    ForeColor = bakimYuzde >= 80 ? Color.FromArgb(0, 168, 80) : Color.FromArgb(255, 193, 7),
                                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near, VAlignment = DevExpress.Utils.VertAlignment.Center },
                                    BorderColor = bakimYuzde >= 80 ? Color.FromArgb(0, 168, 80) : Color.FromArgb(255, 193, 7)
                                },
                                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                            };
                            lblBakim.Appearance.Options.UseTextOptions = true;
                            lblBakim.Appearance.Options.UseBorderColor = true;
                            lblBakim.BackColor = Color.White;
                            
                            // Doluluk bilgisi kartı
                            var lblDoluluk = new LabelControl
                            {
                                Text = $"Doluluk: {dolulukOrani} kişi",
                                Location = new Point(0, 140),
                                Size = new Size(600, 50),
                                Appearance =
                                {
                                    Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                                    ForeColor = Color.FromArgb(52, 152, 219),
                                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near, VAlignment = DevExpress.Utils.VertAlignment.Center },
                                    BorderColor = Color.FromArgb(52, 152, 219)
                                },
                                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                            };
                            lblDoluluk.Appearance.Options.UseTextOptions = true;
                            lblDoluluk.Appearance.Options.UseBorderColor = true;
                            lblDoluluk.BackColor = Color.White;
                            
                            // Son bakım tarihi kartı
                            var lblSonBakim = new LabelControl
                            {
                                Text = $"Son Bakım: {sonBakimTarihi:dd.MM.yyyy}",
                                Location = new Point(0, 200),
                                Size = new Size(600, 50),
                                Appearance =
                                {
                                    Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                                    ForeColor = Color.FromArgb(155, 89, 182),
                                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Near, VAlignment = DevExpress.Utils.VertAlignment.Center },
                                    BorderColor = Color.FromArgb(155, 89, 182)
                                },
                                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                            };
                            lblSonBakim.Appearance.Options.UseTextOptions = true;
                            lblSonBakim.Appearance.Options.UseBorderColor = true;
                            lblSonBakim.BackColor = Color.White;
                            
                            _pnlHavuz.Controls.Add(lblBakim);
                            _pnlHavuz.Controls.Add(lblDoluluk);
                            _pnlHavuz.Controls.Add(lblSonBakim);
                        }
                        else
                        {
                            var lblYok = new LabelControl
                            {
                                Text = "Havuz bilgisi bulunamadı.",
                                Location = new Point(0, 80),
                                Size = new Size(600, 50),
                                Appearance =
                                {
                                    Font = new Font("Segoe UI", 14F),
                                    ForeColor = Color.FromArgb(127, 140, 141)
                                }
                            };
                            _pnlHavuz.Controls.Add(lblYok);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var lblHata = new LabelControl
                {
                    Text = $"Hata: {ex.Message}",
                    Location = new Point(0, 80),
                    Size = new Size(600, 50),
                    Appearance =
                    {
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.FromArgb(220, 20, 60)
                    }
                };
                _pnlHavuz.Controls.Add(lblHata);
            }
        }
        
        /// <summary>
        /// Tema/Gece modu değiştir
        /// </summary>
        private void BtnTemaDegistir_Click(object sender, EventArgs e)
        {
            _geceModu = !_geceModu;
            
            if (_geceModu)
            {
                // Gece modu - Koyu tema
                _btnTema.Text = "☀️ Gündüz Modu";
                _btnTema.Appearance.BackColor = Color.FromArgb(255, 193, 7);
                _btnTema.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
                
                this.BackColor = Color.FromArgb(33, 37, 41);
                _pnlContent.Appearance.BackColor = Color.FromArgb(40, 44, 48);
                _pnlHeader.Appearance.BackColor = Color.FromArgb(25, 29, 33);
                
                // Grid renkleri
                if (_gridAidatlar != null)
                {
                    _gridAidatlar.BackColor = Color.FromArgb(45, 49, 53);
                    _viewAidatlar.Appearance.Row.BackColor = Color.FromArgb(45, 49, 53);
                    _viewAidatlar.Appearance.Row.ForeColor = Color.White;
                    _viewAidatlar.Appearance.HeaderPanel.BackColor = Color.FromArgb(33, 37, 41);
                    _viewAidatlar.Appearance.HeaderPanel.ForeColor = Color.White;
                }
                
                if (_gridDuyurular != null)
                {
                    _gridDuyurular.BackColor = Color.FromArgb(45, 49, 53);
                    _viewDuyurular.Appearance.Row.BackColor = Color.FromArgb(45, 49, 53);
                    _viewDuyurular.Appearance.Row.ForeColor = Color.White;
                    _viewDuyurular.Appearance.HeaderPanel.BackColor = Color.FromArgb(33, 37, 41);
                    _viewDuyurular.Appearance.HeaderPanel.ForeColor = Color.White;
                }
                
                if (_gridEnerji != null)
                {
                    _gridEnerji.BackColor = Color.FromArgb(45, 49, 53);
                    _viewEnerji.Appearance.Row.BackColor = Color.FromArgb(45, 49, 53);
                    _viewEnerji.Appearance.Row.ForeColor = Color.White;
                    _viewEnerji.Appearance.HeaderPanel.BackColor = Color.FromArgb(33, 37, 41);
                    _viewEnerji.Appearance.HeaderPanel.ForeColor = Color.White;
                }
            }
            else
            {
                // Gündüz modu - Açık tema
                _btnTema.Text = "🌙 Gece Modu";
                _btnTema.Appearance.BackColor = Color.FromArgb(52, 58, 64);
                _btnTema.Appearance.ForeColor = Color.White;
                
                this.BackColor = Color.FromArgb(245, 247, 250);
                _pnlContent.Appearance.BackColor = Color.FromArgb(252, 252, 252);
                _pnlHeader.Appearance.BackColor = Color.FromArgb(33, 37, 41);
                
                // Grid renkleri
                if (_gridAidatlar != null)
                {
                    _gridAidatlar.BackColor = Color.White;
                    _viewAidatlar.Appearance.Row.BackColor = Color.White;
                    _viewAidatlar.Appearance.Row.ForeColor = Color.FromArgb(33, 37, 41);
                    _viewAidatlar.Appearance.HeaderPanel.BackColor = Color.FromArgb(248, 249, 250);
                    _viewAidatlar.Appearance.HeaderPanel.ForeColor = Color.FromArgb(33, 37, 41);
                }
                
                if (_gridDuyurular != null)
                {
                    _gridDuyurular.BackColor = Color.White;
                    _viewDuyurular.Appearance.Row.BackColor = Color.White;
                    _viewDuyurular.Appearance.Row.ForeColor = Color.FromArgb(33, 37, 41);
                    _viewDuyurular.Appearance.HeaderPanel.BackColor = Color.FromArgb(248, 249, 250);
                    _viewDuyurular.Appearance.HeaderPanel.ForeColor = Color.FromArgb(33, 37, 41);
                }
                
                if (_gridEnerji != null)
                {
                    _gridEnerji.BackColor = Color.White;
                    _viewEnerji.Appearance.Row.BackColor = Color.White;
                    _viewEnerji.Appearance.Row.ForeColor = Color.FromArgb(33, 37, 41);
                    _viewEnerji.Appearance.HeaderPanel.BackColor = Color.FromArgb(248, 249, 250);
                    _viewEnerji.Appearance.HeaderPanel.ForeColor = Color.FromArgb(33, 37, 41);
                }
            }
            
            // Sekme panellerini güncelle
            if (_pnlSikayet != null)
                _pnlSikayet.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlOtopark != null)
                _pnlOtopark.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlAidatlar != null)
                _pnlAidatlar.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlDuyurular != null)
                _pnlDuyurular.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlEnerji != null)
                _pnlEnerji.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlHavuz != null)
                _pnlHavuz.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
            if (_pnlToplanti != null)
                _pnlToplanti.Appearance.BackColor = _geceModu ? Color.FromArgb(40, 44, 48) : Color.FromArgb(252, 252, 252);
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
        
        /// <summary>
        /// Gecikmiş aidat bildirimi kontrol et ve göster
        /// </summary>
        private void GecikmisAidatBildirimiKontrol()
        {
            if (_daireId == 0)
                return;
            
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                    return;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Bildirim tablosunun varlığını kontrol et
                    var tabloKontrolQuery = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'Bildirim'";
                    bool tabloVar = false;
                    using (var tabloCmd = new SqlCommand(tabloKontrolQuery, connection))
                    {
                        tabloVar = Convert.ToInt32(tabloCmd.ExecuteScalar()) > 0;
                    }
                    
                    // Gecikmiş aidat kontrolü
                    var query = @"SELECT COUNT(*) AS GecikmisSayisi,
                                  SUM(Tutar) AS ToplamTutar
                                  FROM Aidat 
                                  WHERE DaireId = @DaireId 
                                  AND OdendiMi = 0 
                                  AND Tarih < GETDATE()";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DaireId", _daireId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var gecikmisSayisi = Convert.ToInt32(reader["GecikmisSayisi"]);
                                
                                if (gecikmisSayisi > 0)
                                {
                                    var toplamTutar = reader["ToplamTutar"] != DBNull.Value ? Convert.ToDecimal(reader["ToplamTutar"]) : 0;
                                    
                                    // Bildirim tablosu varsa kaydet
                                    if (tabloVar)
                                    {
                                        try
                                        {
                                            var bildirimQuery = @"INSERT INTO Bildirim (KullaniciId, DaireId, BildirimTipi, Baslik, Icerik, Okundu, Aktif)
                                                                 VALUES (@KullaniciId, @DaireId, 'AidatGecikmesi', 
                                                                        @Baslik, @Icerik, 0, 1)";
                                            
                                            using (var bildirimCmd = new SqlCommand(bildirimQuery, connection))
                                            {
                                                bildirimCmd.Parameters.AddWithValue("@KullaniciId", _userId);
                                                bildirimCmd.Parameters.AddWithValue("@DaireId", _daireId);
                                                bildirimCmd.Parameters.AddWithValue("@Baslik", "⚠️ Gecikmiş Aidat Uyarısı");
                                                bildirimCmd.Parameters.AddWithValue("@Icerik", $"{gecikmisSayisi} adet gecikmiş aidatınız bulunmaktadır. Toplam tutar: {toplamTutar:N2} TL");
                                                bildirimCmd.ExecuteNonQuery();
                                            }
                                        }
                                        catch
                                        {
                                            // Bildirim eklenemezse devam et
                                        }
                                    }
                                    
                                    // Kullanıcıya bildirim göster
                                    var mesaj = $"⚠️ Gecikmiş Aidat Uyarısı\n\n{gecikmisSayisi} adet gecikmiş aidatınız bulunmaktadır.\nToplam tutar: {toplamTutar:N2} TL\n\nLütfen aidatlarınızı en kısa sürede ödeyiniz.";
                                    XtraMessageBox.Show(mesaj, "Bildirim",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Hata durumunda sessizce devam et
            }
        }
        
        /// <summary>
        /// Toplantı sekmesini oluştur
        /// </summary>
        private void ToplantiSekmesiOlustur()
        {
            _pnlToplanti = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                Appearance = { BackColor = Color.FromArgb(252, 252, 252) }
            };
            
            // Modern başlık
            var lblBaslik = new LabelControl
            {
                Text = "🏛️ Toplantı Kararları",
                Location = new Point(0, 10),
                Size = new Size(600, 45),
                Appearance =
                {
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(33, 37, 41)
                }
            };
            
            _pnlToplanti.Controls.Add(lblBaslik);
            
            // Grid oluştur
            _gridToplanti = new GridControl
            {
                Location = new Point(0, 70),
                Size = new Size(1020, 550)
            };
            
            _viewToplanti = new GridView();
            _gridToplanti.MainView = _viewToplanti;
            _gridToplanti.ViewCollection.Add(_viewToplanti);
            
            _viewToplanti.OptionsCustomization.AllowQuickHideColumns = false;
            _viewToplanti.OptionsView.ShowGroupPanel = false;
            _viewToplanti.OptionsView.RowAutoHeight = true;
            _viewToplanti.RowHeight = 80;
            
            _pnlToplanti.Controls.Add(_gridToplanti);
            _pnlContent.Controls.Add(_pnlToplanti);
            
            // Toplantıları yükle
            ToplantilariYukle();
        }
        
        /// <summary>
        /// Toplantıları yükle
        /// </summary>
        private void ToplantilariYukle()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ApartmanYSConnection"].ConnectionString;
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    var query = @"SELECT TOP 20 
                                  ToplantiTarihi, 
                                  ToplantiKonusu,
                                  ToplantiKararlari,
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
                            _viewToplanti.Columns["ToplantiTarihi"].Caption = "📅 Tarih";
                            _viewToplanti.Columns["ToplantiKonusu"].Caption = "📋 Konu";
                            _viewToplanti.Columns["ToplantiKararlari"].Caption = "📝 Kararlar";
                            _viewToplanti.Columns["KatilanSayisi"].Caption = "👥 Katılımcı";
                            _viewToplanti.Columns["OlusturmaTarihi"].Caption = "🕐 Oluşturma";
                            
                            _viewToplanti.Columns["ToplantiTarihi"].Width = 150;
                            _viewToplanti.Columns["ToplantiKonusu"].Width = 300;
                            _viewToplanti.Columns["ToplantiKararlari"].Width = 400;
                            _viewToplanti.Columns["KatilanSayisi"].Width = 100;
                            _viewToplanti.Columns["OlusturmaTarihi"].Width = 150;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Toplantılar yüklenirken hata: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// AI Sohbet widget'ını oluştur (sol alta)
        /// </summary>
        private void AISohbetWidgetOlustur()
        {
            // AI Sohbet paneli - Sol alta sabitlenmiş
            _pnlAISohbet = new PanelControl
            {
                Size = new Size(350, 450),
                Location = new Point(20, 0), // Sol alt için hesaplanacak
                Appearance = 
                { 
                    BackColor = Color.White,
                    BorderColor = Color.FromArgb(200, 200, 200)
                },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
                Visible = false
            };

            // Başlık paneli
            var pnlBaslik = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 50,
                Appearance = { BackColor = Color.FromArgb(156, 39, 176) }
            };

            var lblBaslik = new LabelControl
            {
                Text = "🤖 AI Asistan",
                Location = new Point(15, 15),
                Size = new Size(200, 25),
                Appearance =
                {
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.Black // Siyah yapıldı
                }
            };

            // Kapat butonu - Kırmızı yapıldı
            var btnKapat = new SimpleButton
            {
                Text = "✕",
                Location = new Point(310, 10),
                Size = new Size(30, 30),
                Appearance =
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(231, 76, 60), // Kırmızı
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold)
                }
            };
            btnKapat.AppearanceHovered.ForeColor = Color.FromArgb(192, 57, 43); // Daha koyu kırmızı hover
            btnKapat.AppearanceHovered.Options.UseForeColor = true;
            btnKapat.Appearance.Options.UseBackColor = true;
            btnKapat.Appearance.Options.UseForeColor = true;
            btnKapat.Click += (s, e) =>
            {
                _aiSohbetAcik = false;
                _pnlAISohbet.Visible = false;
                _btnAISohbetToggle.Text = "🤖 AI Asistan";
            };

            pnlBaslik.Controls.Add(lblBaslik);
            pnlBaslik.Controls.Add(btnKapat);

            // Scrollable sohbet mesaj alanı (Windows Forms Panel - AutoScroll özelliği için)
            _pnlSohbetScroll = new System.Windows.Forms.Panel
            {
                Location = new Point(10, 60),
                Size = new Size(330, 320),
                BackColor = Color.FromArgb(245, 247, 250),
                AutoScroll = true
            };

            // Mesajlar paneli (içerik)
            _pnlSohbetMesajlar = new PanelControl
            {
                Location = new Point(0, 0),
                Size = new Size(310, 100), // Genişlik scroll container'dan biraz küçük (scrollbar için)
                Appearance = { BackColor = Color.FromArgb(245, 247, 250) },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            };

            _pnlSohbetScroll.Controls.Add(_pnlSohbetMesajlar);
            
            // İlk hoş geldin mesajı
            SohbetBaloncuguEkle("AI", "Merhaba! Size nasıl yardımcı olabilirim? Apartman yönetimi, aidat ödemeleri, duyurular hakkında sorularınızı sorabilirsiniz.", false);

            // Mesaj giriş alanı
            _txtAIMesaj = new TextEdit
            {
                Location = new Point(10, 390),
                Size = new Size(250, 30),
                Properties =
                {
                    NullText = "Mesajınızı yazın..."
                }
            };
            _txtAIMesaj.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.Handled = true;
                    AISohbetMesajGonder();
                }
            };

            // Gönder butonu
            _btnAIGonder = new SimpleButton
            {
                Text = "Gönder",
                Location = new Point(270, 390),
                Size = new Size(70, 30),
                Appearance =
                {
                    BackColor = Color.FromArgb(156, 39, 176),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                }
            };
            _btnAIGonder.Appearance.Options.UseBackColor = true;
            _btnAIGonder.Appearance.Options.UseForeColor = true;
            _btnAIGonder.Click += (s, e) => AISohbetMesajGonder();

            _pnlAISohbet.Controls.Add(pnlBaslik);
            _pnlAISohbet.Controls.Add(_pnlSohbetScroll);
            _pnlAISohbet.Controls.Add(_txtAIMesaj);
            _pnlAISohbet.Controls.Add(_btnAIGonder);

            // Toggle butonu - Sol alta sabitlenmiş
            _btnAISohbetToggle = new SimpleButton
            {
                Text = "🤖 AI Asistan",
                Size = new Size(120, 40),
                Appearance =
                {
                    BackColor = Color.FromArgb(156, 39, 176),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
            _btnAISohbetToggle.Appearance.Options.UseBackColor = true;
            _btnAISohbetToggle.Appearance.Options.UseForeColor = true;
            _btnAISohbetToggle.Click += (s, e) =>
            {
                _aiSohbetAcik = !_aiSohbetAcik;
                _pnlAISohbet.Visible = _aiSohbetAcik;
            };

            // Form resize event'inde konumları güncelle
            this.Resize += (s, e) =>
            {
                _btnAISohbetToggle.Location = new Point(20, this.ClientSize.Height - 50);
                _pnlAISohbet.Location = new Point(20, this.ClientSize.Height - 500);
            };

            // İlk konumlandırma
            _btnAISohbetToggle.Location = new Point(20, this.ClientSize.Height - 50);
            _pnlAISohbet.Location = new Point(20, this.ClientSize.Height - 500);

            this.Controls.Add(_pnlAISohbet);
            this.Controls.Add(_btnAISohbetToggle);
            
            // AI sohbet panelini en üste getir
            _pnlAISohbet.BringToFront();
            _btnAISohbetToggle.BringToFront();
        }

        /// <summary>
        /// Sohbet baloncuğu ekle
        /// </summary>
        private PanelControl SohbetBaloncuguEkle(string gonderen, string mesaj, bool kullaniciMi)
        {
            // Mesaj baloncuğu paneli
            var pnlBaloncuk = new PanelControl
            {
                Location = new Point(kullaniciMi ? 80 : 10, _sohbetYukseklik),
                Size = new Size(240, 0), // Yükseklik dinamik olarak ayarlanacak
                Appearance = 
                {
                    BackColor = kullaniciMi ? Color.FromArgb(156, 39, 176) : Color.White,
                    BorderColor = kullaniciMi ? Color.FromArgb(156, 39, 176) : Color.FromArgb(200, 200, 200),
                    Options = { UseBackColor = true }
                },
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            };

            // Mesaj label'ı
            var lblMesaj = new LabelControl
            {
                Location = new Point(10, 10),
                Size = new Size(220, 0),
                Text = mesaj,
                Appearance =
                {
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = kullaniciMi ? Color.White : Color.FromArgb(33, 33, 33),
                    TextOptions = { WordWrap = DevExpress.Utils.WordWrap.Wrap }
                },
                AutoSizeMode = LabelAutoSizeMode.Vertical
            };

            // Label'ın gerçek yüksekliğini al
            using (Graphics g = _pnlSohbetMesajlar.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(mesaj, lblMesaj.Appearance.Font, 220);
                int labelHeight = (int)Math.Ceiling(textSize.Height) + 20;
                lblMesaj.Height = labelHeight;
                pnlBaloncuk.Height = labelHeight + 20;
            }

            pnlBaloncuk.Controls.Add(lblMesaj);
            _pnlSohbetMesajlar.Controls.Add(pnlBaloncuk);

            // Yüksekliği güncelle
            _sohbetYukseklik += pnlBaloncuk.Height + 10;
            _pnlSohbetMesajlar.Height = _sohbetYukseklik;

            // Scroll'u en alta kaydır
            _pnlSohbetScroll.AutoScrollPosition = new Point(0, _sohbetYukseklik);
            _pnlSohbetScroll.PerformLayout();
            
            return pnlBaloncuk;
        }

        /// <summary>
        /// AI sohbet mesajı gönder
        /// </summary>
        private async void AISohbetMesajGonder()
        {
            string mesaj = _txtAIMesaj.Text.Trim();
            if (string.IsNullOrEmpty(mesaj))
                return;

            // Kullanıcı mesajını ekle (sağda, mor baloncuk)
            _aiSohbetGecmisi.Add($"Kullanıcı: {mesaj}");
            SohbetBaloncuguEkle("Kullanıcı", mesaj, true);
            _txtAIMesaj.Text = "";
            _txtAIMesaj.Focus();

            // Yükleme göstergesi (solda, beyaz baloncuk)
            PanelControl yuklemeBaloncuk = SohbetBaloncuguEkle("AI", "Düşünüyorum...", false);
            _btnAIGonder.Enabled = false;

            try
            {
                // Sohbet geçmişini oluştur
                string sohbetGecmisi = string.Join("\n", _aiSohbetGecmisi);

                // Gemini API'ye gönder
                string yanit = await _geminiService.ChatAsync(mesaj, sohbetGecmisi);

                // Yanıtı ekle
                _aiSohbetGecmisi.Add($"Asistan: {yanit}");
                
                // Yükleme baloncuğunu kaldır
                if (yuklemeBaloncuk != null && _pnlSohbetMesajlar.Controls.Contains(yuklemeBaloncuk))
                {
                    _pnlSohbetMesajlar.Controls.Remove(yuklemeBaloncuk);
                    _sohbetYukseklik -= yuklemeBaloncuk.Height + 10;
                    _pnlSohbetMesajlar.Height = _sohbetYukseklik;
                }
                
                // AI yanıtını ekle (solda, beyaz baloncuk)
                SohbetBaloncuguEkle("AI", yanit, false);
            }
            catch (Exception ex)
            {
                // Hata baloncuğu
                if (yuklemeBaloncuk != null && _pnlSohbetMesajlar.Controls.Contains(yuklemeBaloncuk))
                {
                    _pnlSohbetMesajlar.Controls.Remove(yuklemeBaloncuk);
                    _sohbetYukseklik -= yuklemeBaloncuk.Height + 10;
                    _pnlSohbetMesajlar.Height = _sohbetYukseklik;
                }
                SohbetBaloncuguEkle("AI", $"❌ Hata: {ex.Message}", false);
            }
            finally
            {
                _btnAIGonder.Enabled = true;
            }
        }
    }
}
