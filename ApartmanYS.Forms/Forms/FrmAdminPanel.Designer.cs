namespace ApartmanYS.Forms
{
    partial class FrmAdminPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.PnlTop = new DevExpress.XtraEditors.PanelControl();
            this.LblWelcome = new DevExpress.XtraEditors.LabelControl();
            this.PicAdmin = new DevExpress.XtraEditors.PictureEdit();
            this.BtnCikis = new DevExpress.XtraEditors.SimpleButton();
            this.PnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.BtnToplantiYonetimi = new DevExpress.XtraEditors.SimpleButton();
            this.BtnKullaniciYonetim = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDuyurular = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAidatGider = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDaireYonetim = new DevExpress.XtraEditors.SimpleButton();
            this.BtnKapici = new DevExpress.XtraEditors.SimpleButton();
            this.BtnHavuz = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOtopark = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSikayetler = new DevExpress.XtraEditors.SimpleButton();
            this.BtnEnerjiTakip = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAITahminler = new DevExpress.XtraEditors.SimpleButton();
            this.PnlMain = new DevExpress.XtraEditors.PanelControl();
            this.LblSeciliMenu = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.PnlTop)).BeginInit();
            this.PnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PnlLeft)).BeginInit();
            this.PnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).BeginInit();
            this.PnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlTop
            // 
            this.PnlTop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.PnlTop.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.PnlTop.Appearance.Options.UseBackColor = true;
            this.PnlTop.Appearance.Options.UseBorderColor = true;
            this.PnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PnlTop.Controls.Add(this.LblWelcome);
            this.PnlTop.Controls.Add(this.PicAdmin);
            this.PnlTop.Controls.Add(this.BtnCikis);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(1200, 70);
            this.PnlTop.TabIndex = 0;
            // 
            // LblWelcome
            // 
            this.LblWelcome.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblWelcome.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblWelcome.Location = new System.Drawing.Point(75, 22);
            this.LblWelcome.Name = "LblWelcome";
            this.LblWelcome.Size = new System.Drawing.Size(400, 30);
            this.LblWelcome.TabIndex = 0;
            this.LblWelcome.Text = "Admin Paneli";
            // 
            // PicAdmin
            // 
            ((System.ComponentModel.ISupportInitialize)(this.PicAdmin.Properties)).BeginInit();
            this.PicAdmin.Location = new System.Drawing.Point(25, 15);
            this.PicAdmin.Name = "PicAdmin";
            this.PicAdmin.Size = new System.Drawing.Size(40, 40);
            this.PicAdmin.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.PicAdmin.Properties.ShowMenu = false;
            this.PicAdmin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ((System.ComponentModel.ISupportInitialize)(this.PicAdmin.Properties)).EndInit();
            // 
            // BtnCikis
            // 
            this.BtnCikis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCikis.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnCikis.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnCikis.Appearance.Options.UseBackColor = true;
            this.BtnCikis.Appearance.Options.UseForeColor = true;
            this.BtnCikis.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCikis.Appearance.Options.UseFont = true;
            this.BtnCikis.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.BtnCikis.AppearanceHovered.Options.UseBackColor = true;
            this.BtnCikis.Location = new System.Drawing.Point(1100, 20);
            this.BtnCikis.Name = "BtnCikis";
            this.BtnCikis.Size = new System.Drawing.Size(90, 35);
            this.BtnCikis.TabIndex = 1;
            this.BtnCikis.Text = "🚪 Çıkış";
            this.BtnCikis.Click += new System.EventHandler(this.BtnCikis_Click);
            // 
            // PnlLeft
            // 
            this.PnlLeft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.PnlLeft.Appearance.Options.UseBackColor = true;
            this.PnlLeft.Controls.Add(this.BtnAITahminler);
            this.PnlLeft.Controls.Add(this.BtnToplantiYonetimi);
            this.PnlLeft.Controls.Add(this.BtnKullaniciYonetim);
            this.PnlLeft.Controls.Add(this.BtnDuyurular);
            this.PnlLeft.Controls.Add(this.BtnAidatGider);
            this.PnlLeft.Controls.Add(this.BtnDaireYonetim);
            this.PnlLeft.Controls.Add(this.BtnKapici);
            this.PnlLeft.Controls.Add(this.BtnHavuz);
            this.PnlLeft.Controls.Add(this.BtnOtopark);
            this.PnlLeft.Controls.Add(this.BtnSikayetler);
            this.PnlLeft.Controls.Add(this.BtnEnerjiTakip);
            this.PnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlLeft.Location = new System.Drawing.Point(0, 70);
            this.PnlLeft.Name = "PnlLeft";
            this.PnlLeft.Size = new System.Drawing.Size(240, 630);
            this.PnlLeft.TabIndex = 1;
            // 
            // BtnKullaniciYonetim
            // 
            this.BtnKullaniciYonetim.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnKullaniciYonetim.Appearance.Options.UseFont = true;
            this.BtnKullaniciYonetim.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnKullaniciYonetim.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKullaniciYonetim.Appearance.Options.UseBackColor = true;
            this.BtnKullaniciYonetim.Appearance.Options.UseForeColor = true;
            this.BtnKullaniciYonetim.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.BtnKullaniciYonetim.AppearanceHovered.Options.UseBackColor = true;
            this.BtnKullaniciYonetim.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(31)))), ((int)(((byte)(162)))));
            this.BtnKullaniciYonetim.AppearancePressed.Options.UseBackColor = true;
            this.BtnKullaniciYonetim.Location = new System.Drawing.Point(0, 150);
            this.BtnKullaniciYonetim.Name = "BtnKullaniciYonetim";
            this.BtnKullaniciYonetim.Size = new System.Drawing.Size(240, 50);
            this.BtnKullaniciYonetim.TabIndex = 3;
            this.BtnKullaniciYonetim.Text = "👥  Kullanıcılar";
            this.BtnKullaniciYonetim.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnDuyurular
            // 
            this.BtnDuyurular.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnDuyurular.Appearance.Options.UseFont = true;
            this.BtnDuyurular.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnDuyurular.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnDuyurular.Appearance.Options.UseBackColor = true;
            this.BtnDuyurular.Appearance.Options.UseForeColor = true;
            this.BtnDuyurular.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.BtnDuyurular.AppearanceHovered.Options.UseBackColor = true;
            this.BtnDuyurular.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(46)))), ((int)(((byte)(6)))));
            this.BtnDuyurular.AppearancePressed.Options.UseBackColor = true;
            this.BtnDuyurular.Location = new System.Drawing.Point(0, 100);
            this.BtnDuyurular.Name = "BtnDuyurular";
            this.BtnDuyurular.Size = new System.Drawing.Size(240, 50);
            this.BtnDuyurular.TabIndex = 2;
            this.BtnDuyurular.Text = "📢  Duyurular";
            this.BtnDuyurular.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAidatGider
            // 
            this.BtnAidatGider.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnAidatGider.Appearance.Options.UseFont = true;
            this.BtnAidatGider.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnAidatGider.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnAidatGider.Appearance.Options.UseBackColor = true;
            this.BtnAidatGider.Appearance.Options.UseForeColor = true;
            this.BtnAidatGider.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.BtnAidatGider.AppearanceHovered.Options.UseBackColor = true;
            this.BtnAidatGider.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(17)))));
            this.BtnAidatGider.AppearancePressed.Options.UseBackColor = true;
            this.BtnAidatGider.Location = new System.Drawing.Point(0, 50);
            this.BtnAidatGider.Name = "BtnAidatGider";
            this.BtnAidatGider.Size = new System.Drawing.Size(240, 50);
            this.BtnAidatGider.TabIndex = 1;
            this.BtnAidatGider.Text = "💰  Aidat/Gider";
            this.BtnAidatGider.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnDaireYonetim
            // 
            this.BtnDaireYonetim.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnDaireYonetim.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnDaireYonetim.Appearance.Options.UseBackColor = true;
            this.BtnDaireYonetim.Appearance.Options.UseForeColor = true;
            this.BtnDaireYonetim.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.BtnDaireYonetim.Appearance.Options.UseFont = true;
            this.BtnDaireYonetim.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.BtnDaireYonetim.AppearanceHovered.Options.UseBackColor = true;
            this.BtnDaireYonetim.Location = new System.Drawing.Point(0, 0);
            this.BtnDaireYonetim.Name = "BtnDaireYonetim";
            this.BtnDaireYonetim.Size = new System.Drawing.Size(240, 50);
            this.BtnDaireYonetim.TabIndex = 0;
            this.BtnDaireYonetim.Text = "🏠  Daire Yönetimi";
            this.BtnDaireYonetim.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnKapici
            // 
            this.BtnKapici.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnKapici.Appearance.Options.UseFont = true;
            this.BtnKapici.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnKapici.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKapici.Appearance.Options.UseBackColor = true;
            this.BtnKapici.Appearance.Options.UseForeColor = true;
            this.BtnKapici.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.BtnKapici.AppearanceHovered.Options.UseBackColor = true;
            this.BtnKapici.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.BtnKapici.AppearancePressed.Options.UseBackColor = true;
            this.BtnKapici.Location = new System.Drawing.Point(0, 200);
            this.BtnKapici.Name = "BtnKapici";
            this.BtnKapici.Size = new System.Drawing.Size(240, 50);
            this.BtnKapici.TabIndex = 4;
            this.BtnKapici.Text = "👨‍💼  Kapıcı";
            this.BtnKapici.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnHavuz
            // 
            this.BtnHavuz.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnHavuz.Appearance.Options.UseFont = true;
            this.BtnHavuz.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnHavuz.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnHavuz.Appearance.Options.UseBackColor = true;
            this.BtnHavuz.Appearance.Options.UseForeColor = true;
            this.BtnHavuz.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(151)))), ((int)(((byte)(167)))));
            this.BtnHavuz.AppearanceHovered.Options.UseBackColor = true;
            this.BtnHavuz.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(92)))));
            this.BtnHavuz.AppearancePressed.Options.UseBackColor = true;
            this.BtnHavuz.Location = new System.Drawing.Point(0, 250);
            this.BtnHavuz.Name = "BtnHavuz";
            this.BtnHavuz.Size = new System.Drawing.Size(240, 50);
            this.BtnHavuz.TabIndex = 5;
            this.BtnHavuz.Text = "🏊  Havuz";
            this.BtnHavuz.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnOtopark
            // 
            this.BtnOtopark.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnOtopark.Appearance.Options.UseFont = true;
            this.BtnOtopark.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnOtopark.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnOtopark.Appearance.Options.UseBackColor = true;
            this.BtnOtopark.Appearance.Options.UseForeColor = true;
            this.BtnOtopark.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(90)))), ((int)(((byte)(100)))));
            this.BtnOtopark.AppearanceHovered.Options.UseBackColor = true;
            this.BtnOtopark.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.BtnOtopark.AppearancePressed.Options.UseBackColor = true;
            this.BtnOtopark.Location = new System.Drawing.Point(0, 300);
            this.BtnOtopark.Name = "BtnOtopark";
            this.BtnOtopark.Size = new System.Drawing.Size(240, 50);
            this.BtnOtopark.TabIndex = 6;
            this.BtnOtopark.Text = "🚗  Otopark";
            this.BtnOtopark.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnSikayetler
            // 
            this.BtnSikayetler.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnSikayetler.Appearance.Options.UseFont = true;
            this.BtnSikayetler.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnSikayetler.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnSikayetler.Appearance.Options.UseBackColor = true;
            this.BtnSikayetler.Appearance.Options.UseForeColor = true;
            this.BtnSikayetler.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(24)))), ((int)(((byte)(91)))));
            this.BtnSikayetler.AppearanceHovered.Options.UseBackColor = true;
            this.BtnSikayetler.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(14)))), ((int)(((byte)(79)))));
            this.BtnSikayetler.AppearancePressed.Options.UseBackColor = true;
            this.BtnSikayetler.Location = new System.Drawing.Point(0, 350);
            this.BtnSikayetler.Name = "BtnSikayetler";
            this.BtnSikayetler.Size = new System.Drawing.Size(240, 50);
            this.BtnSikayetler.TabIndex = 7;
            this.BtnSikayetler.Text = "📝  Şikayetler";
            this.BtnSikayetler.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnEnerjiTakip
            // 
            this.BtnEnerjiTakip.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnEnerjiTakip.Appearance.Options.UseFont = true;
            this.BtnEnerjiTakip.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnEnerjiTakip.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnEnerjiTakip.Appearance.Options.UseBackColor = true;
            this.BtnEnerjiTakip.Appearance.Options.UseForeColor = true;
            this.BtnEnerjiTakip.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.BtnEnerjiTakip.AppearanceHovered.Options.UseBackColor = true;
            this.BtnEnerjiTakip.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.BtnEnerjiTakip.AppearancePressed.Options.UseBackColor = true;
            this.BtnEnerjiTakip.Location = new System.Drawing.Point(0, 400);
            this.BtnEnerjiTakip.Name = "BtnEnerjiTakip";
            this.BtnEnerjiTakip.Size = new System.Drawing.Size(240, 50);
            this.BtnEnerjiTakip.TabIndex = 8;
            this.BtnEnerjiTakip.Text = "⚡  Enerji Takip";
            this.BtnEnerjiTakip.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnToplantiYonetimi
            // 
            this.BtnToplantiYonetimi.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnToplantiYonetimi.Appearance.Options.UseFont = true;
            this.BtnToplantiYonetimi.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnToplantiYonetimi.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnToplantiYonetimi.Appearance.Options.UseBackColor = true;
            this.BtnToplantiYonetimi.Appearance.Options.UseForeColor = true;
            this.BtnToplantiYonetimi.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.BtnToplantiYonetimi.AppearanceHovered.Options.UseBackColor = true;
            this.BtnToplantiYonetimi.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.BtnToplantiYonetimi.AppearancePressed.Options.UseBackColor = true;
            this.BtnToplantiYonetimi.Location = new System.Drawing.Point(0, 450);
            this.BtnToplantiYonetimi.Name = "BtnToplantiYonetimi";
            this.BtnToplantiYonetimi.Size = new System.Drawing.Size(240, 50);
            this.BtnToplantiYonetimi.TabIndex = 9;
            this.BtnToplantiYonetimi.Text = "🏛️  Toplantı Yönetimi";
            this.BtnToplantiYonetimi.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAITahminler
            // 
            this.BtnAITahminler.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.BtnAITahminler.Appearance.Options.UseFont = true;
            this.BtnAITahminler.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BtnAITahminler.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnAITahminler.Appearance.Options.UseBackColor = true;
            this.BtnAITahminler.Appearance.Options.UseForeColor = true;
            this.BtnAITahminler.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.BtnAITahminler.AppearanceHovered.Options.UseBackColor = true;
            this.BtnAITahminler.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(31)))), ((int)(((byte)(162)))));
            this.BtnAITahminler.AppearancePressed.Options.UseBackColor = true;
            this.BtnAITahminler.Location = new System.Drawing.Point(0, 500);
            this.BtnAITahminler.Name = "BtnAITahminler";
            this.BtnAITahminler.Size = new System.Drawing.Size(240, 50);
            this.BtnAITahminler.TabIndex = 10;
            this.BtnAITahminler.Text = "🤖  AI Tahminler";
            this.BtnAITahminler.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // PnlMain
            // 
            this.PnlMain.Controls.Add(this.LblSeciliMenu);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(240, 70);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(960, 630);
            this.PnlMain.TabIndex = 2;
            // 
            // LblSeciliMenu
            // 
            this.LblSeciliMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblSeciliMenu.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblSeciliMenu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.LblSeciliMenu.Location = new System.Drawing.Point(375, 300);
            this.LblSeciliMenu.Name = "LblSeciliMenu";
            this.LblSeciliMenu.Size = new System.Drawing.Size(230, 32);
            this.LblSeciliMenu.TabIndex = 0;
            this.LblSeciliMenu.Text = "Daire Yönetimi";
            // 
            // FrmAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.PnlLeft);
            this.Controls.Add(this.PnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimizeBox = true;
            this.MaximizeBox = true;
            this.Name = "FrmAdminPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Panel - Apartman Yönetim";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            ((System.ComponentModel.ISupportInitialize)(this.PnlTop)).EndInit();
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PnlLeft)).EndInit();
            this.PnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).EndInit();
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PnlTop;
        private DevExpress.XtraEditors.LabelControl LblWelcome;
        private DevExpress.XtraEditors.PictureEdit PicAdmin;
        private DevExpress.XtraEditors.SimpleButton BtnCikis;
        private DevExpress.XtraEditors.PanelControl PnlLeft;
        private DevExpress.XtraEditors.SimpleButton BtnDaireYonetim;
        private DevExpress.XtraEditors.SimpleButton BtnAidatGider;
        private DevExpress.XtraEditors.SimpleButton BtnDuyurular;
        private DevExpress.XtraEditors.SimpleButton BtnKullaniciYonetim;
        private DevExpress.XtraEditors.SimpleButton BtnKapici;
        private DevExpress.XtraEditors.SimpleButton BtnHavuz;
        private DevExpress.XtraEditors.SimpleButton BtnOtopark;
        private DevExpress.XtraEditors.SimpleButton BtnSikayetler;
        private DevExpress.XtraEditors.SimpleButton BtnEnerjiTakip;
        private DevExpress.XtraEditors.SimpleButton BtnToplantiYonetimi;
        private DevExpress.XtraEditors.SimpleButton BtnAITahminler;
        private DevExpress.XtraEditors.PanelControl PnlMain;
        private DevExpress.XtraEditors.LabelControl LblSeciliMenu;
    }
}
