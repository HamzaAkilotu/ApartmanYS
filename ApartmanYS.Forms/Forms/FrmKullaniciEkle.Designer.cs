namespace ApartmanYS.Forms
{
    partial class FrmKullaniciEkle
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
            this.LblBaslik = new DevExpress.XtraEditors.LabelControl();
            this.LblAdSoyad = new DevExpress.XtraEditors.LabelControl();
            this.TxtAdSoyad = new DevExpress.XtraEditors.TextEdit();
            this.LblKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            this.TxtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this.LblSifre = new DevExpress.XtraEditors.LabelControl();
            this.TxtSifre = new DevExpress.XtraEditors.TextEdit();
            this.LblKullaniciTipi = new DevExpress.XtraEditors.LabelControl();
            this.CmbKullaniciTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.LblEmail = new DevExpress.XtraEditors.LabelControl();
            this.TxtEmail = new DevExpress.XtraEditors.TextEdit();
            this.LblTelefon = new DevExpress.XtraEditors.LabelControl();
            this.TxtTelefon = new DevExpress.XtraEditors.TextEdit();
            this.BtnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.BtnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKullaniciTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTelefon.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaslik
            // 
            this.LblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.LblBaslik.Location = new System.Drawing.Point(20, 20);
            this.LblBaslik.Name = "LblBaslik";
            this.LblBaslik.Size = new System.Drawing.Size(189, 32);
            this.LblBaslik.TabIndex = 0;
            this.LblBaslik.Text = "👥 Yeni Kullanıcı Ekle";
            // 
            // LblAdSoyad
            // 
            this.LblAdSoyad.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblAdSoyad.Location = new System.Drawing.Point(20, 80);
            this.LblAdSoyad.Name = "LblAdSoyad";
            this.LblAdSoyad.Size = new System.Drawing.Size(79, 17);
            this.LblAdSoyad.TabIndex = 1;
            this.LblAdSoyad.Text = "Ad Soyad:";
            // 
            // TxtAdSoyad
            // 
            this.TxtAdSoyad.Location = new System.Drawing.Point(20, 105);
            this.TxtAdSoyad.Name = "TxtAdSoyad";
            this.TxtAdSoyad.Size = new System.Drawing.Size(400, 30);
            this.TxtAdSoyad.TabIndex = 2;
            // 
            // LblKullaniciAdi
            // 
            this.LblKullaniciAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblKullaniciAdi.Location = new System.Drawing.Point(20, 155);
            this.LblKullaniciAdi.Name = "LblKullaniciAdi";
            this.LblKullaniciAdi.Size = new System.Drawing.Size(84, 17);
            this.LblKullaniciAdi.TabIndex = 3;
            this.LblKullaniciAdi.Text = "Kullanıcı Adı:";
            // 
            // TxtKullaniciAdi
            // 
            this.TxtKullaniciAdi.Location = new System.Drawing.Point(20, 180);
            this.TxtKullaniciAdi.Name = "TxtKullaniciAdi";
            this.TxtKullaniciAdi.Size = new System.Drawing.Size(400, 30);
            this.TxtKullaniciAdi.TabIndex = 4;
            // 
            // LblSifre
            // 
            this.LblSifre.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblSifre.Location = new System.Drawing.Point(20, 230);
            this.LblSifre.Name = "LblSifre";
            this.LblSifre.Size = new System.Drawing.Size(34, 17);
            this.LblSifre.TabIndex = 5;
            this.LblSifre.Text = "Şifre:";
            // 
            // TxtSifre
            // 
            this.TxtSifre.Location = new System.Drawing.Point(20, 255);
            this.TxtSifre.Name = "TxtSifre";
            this.TxtSifre.Properties.UseSystemPasswordChar = true;
            this.TxtSifre.Size = new System.Drawing.Size(400, 30);
            this.TxtSifre.TabIndex = 6;
            // 
            // LblKullaniciTipi
            // 
            this.LblKullaniciTipi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblKullaniciTipi.Location = new System.Drawing.Point(20, 305);
            this.LblKullaniciTipi.Name = "LblKullaniciTipi";
            this.LblKullaniciTipi.Size = new System.Drawing.Size(84, 17);
            this.LblKullaniciTipi.TabIndex = 7;
            this.LblKullaniciTipi.Text = "Kullanıcı Tipi:";
            // 
            // CmbKullaniciTipi
            // 
            this.CmbKullaniciTipi.Location = new System.Drawing.Point(20, 330);
            this.CmbKullaniciTipi.Name = "CmbKullaniciTipi";
            this.CmbKullaniciTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbKullaniciTipi.Properties.Items.AddRange(new object[] {
            "Admin",
            "User"});
            this.CmbKullaniciTipi.Size = new System.Drawing.Size(400, 30);
            this.CmbKullaniciTipi.TabIndex = 8;
            // 
            // LblEmail
            // 
            this.LblEmail.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblEmail.Location = new System.Drawing.Point(20, 380);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(47, 17);
            this.LblEmail.TabIndex = 9;
            this.LblEmail.Text = "E-mail:";
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(20, 405);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(400, 30);
            this.TxtEmail.TabIndex = 10;
            // 
            // LblTelefon
            // 
            this.LblTelefon.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTelefon.Location = new System.Drawing.Point(20, 455);
            this.LblTelefon.Name = "LblTelefon";
            this.LblTelefon.Size = new System.Drawing.Size(50, 17);
            this.LblTelefon.TabIndex = 11;
            this.LblTelefon.Text = "Telefon:";
            // 
            // TxtTelefon
            // 
            this.TxtTelefon.Location = new System.Drawing.Point(20, 480);
            this.TxtTelefon.Name = "TxtTelefon";
            this.TxtTelefon.Size = new System.Drawing.Size(400, 30);
            this.TxtTelefon.TabIndex = 12;
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(80)))));
            this.BtnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKaydet.Appearance.Options.UseBackColor = true;
            this.BtnKaydet.Appearance.Options.UseForeColor = true;
            this.BtnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnKaydet.Location = new System.Drawing.Point(260, 530);
            this.BtnKaydet.Name = "BtnKaydet";
            this.BtnKaydet.Size = new System.Drawing.Size(100, 40);
            this.BtnKaydet.TabIndex = 13;
            this.BtnKaydet.Text = "✓ Kaydet";
            this.BtnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // BtnIptal
            // 
            this.BtnIptal.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(149)))), ((int)(((byte)(149)))));
            this.BtnIptal.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnIptal.Appearance.Options.UseBackColor = true;
            this.BtnIptal.Appearance.Options.UseForeColor = true;
            this.BtnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BtnIptal.Location = new System.Drawing.Point(370, 530);
            this.BtnIptal.Name = "BtnIptal";
            this.BtnIptal.Size = new System.Drawing.Size(100, 40);
            this.BtnIptal.TabIndex = 14;
            this.BtnIptal.Text = "İptal";
            this.BtnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmKullaniciEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.Controls.Add(this.BtnIptal);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.TxtTelefon);
            this.Controls.Add(this.LblTelefon);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.CmbKullaniciTipi);
            this.Controls.Add(this.LblKullaniciTipi);
            this.Controls.Add(this.TxtSifre);
            this.Controls.Add(this.LblSifre);
            this.Controls.Add(this.TxtKullaniciAdi);
            this.Controls.Add(this.LblKullaniciAdi);
            this.Controls.Add(this.TxtAdSoyad);
            this.Controls.Add(this.LblAdSoyad);
            this.Controls.Add(this.LblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmKullaniciEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Kullanıcı Ekle";
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbKullaniciTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTelefon.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblBaslik;
        private DevExpress.XtraEditors.LabelControl LblAdSoyad;
        private DevExpress.XtraEditors.TextEdit TxtAdSoyad;
        private DevExpress.XtraEditors.LabelControl LblKullaniciAdi;
        private DevExpress.XtraEditors.TextEdit TxtKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl LblSifre;
        private DevExpress.XtraEditors.TextEdit TxtSifre;
        private DevExpress.XtraEditors.LabelControl LblKullaniciTipi;
        private DevExpress.XtraEditors.ComboBoxEdit CmbKullaniciTipi;
        private DevExpress.XtraEditors.LabelControl LblEmail;
        private DevExpress.XtraEditors.TextEdit TxtEmail;
        private DevExpress.XtraEditors.LabelControl LblTelefon;
        private DevExpress.XtraEditors.TextEdit TxtTelefon;
        private DevExpress.XtraEditors.SimpleButton BtnKaydet;
        private DevExpress.XtraEditors.SimpleButton BtnIptal;
    }
}
