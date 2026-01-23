namespace ApartmanYS.Forms
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlMain = new DevExpress.XtraEditors.PanelControl();
            this.PictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.LblBaslik = new DevExpress.XtraEditors.LabelControl();
            this.LblKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            this.LblSifre = new DevExpress.XtraEditors.LabelControl();
            this.TxtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this.TxtSifre = new DevExpress.XtraEditors.TextEdit();
            this.BtnGiris = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCikis = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).BeginInit();
            this.PnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSifre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.Appearance.BackColor = System.Drawing.Color.White;
            this.PnlMain.Appearance.Options.UseBackColor = true;
            this.PnlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PnlMain.Controls.Add(this.PictureEdit);
            this.PnlMain.Controls.Add(this.LblBaslik);
            this.PnlMain.Controls.Add(this.LblKullaniciAdi);
            this.PnlMain.Controls.Add(this.LblSifre);
            this.PnlMain.Controls.Add(this.TxtKullaniciAdi);
            this.PnlMain.Controls.Add(this.TxtSifre);
            this.PnlMain.Controls.Add(this.BtnGiris);
            this.PnlMain.Controls.Add(this.BtnCikis);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(500, 450);
            this.PnlMain.TabIndex = 0;
            // 
            // PictureEdit
            // 
            this.PictureEdit.Location = new System.Drawing.Point(175, 30);
            this.PictureEdit.Name = "PictureEdit";
            this.PictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.PictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.PictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.PictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.PictureEdit.Size = new System.Drawing.Size(150, 150);
            this.PictureEdit.TabIndex = 7;
            // 
            // LblBaslik
            // 
            this.LblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.LblBaslik.Appearance.Options.UseFont = true;
            this.LblBaslik.Appearance.Options.UseForeColor = true;
            this.LblBaslik.Location = new System.Drawing.Point(100, 190);
            this.LblBaslik.Name = "LblBaslik";
            this.LblBaslik.Size = new System.Drawing.Size(300, 32);
            this.LblBaslik.TabIndex = 6;
            this.LblBaslik.Text = "Apartman Yönetim Sistemi";
            // 
            // LblKullaniciAdi
            // 
            this.LblKullaniciAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblKullaniciAdi.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.LblKullaniciAdi.Appearance.Options.UseFont = true;
            this.LblKullaniciAdi.Appearance.Options.UseForeColor = true;
            this.LblKullaniciAdi.Location = new System.Drawing.Point(75, 240);
            this.LblKullaniciAdi.Name = "LblKullaniciAdi";
            this.LblKullaniciAdi.Size = new System.Drawing.Size(78, 17);
            this.LblKullaniciAdi.TabIndex = 0;
            this.LblKullaniciAdi.Text = "Kullanıcı Adı:";
            // 
            // LblSifre
            // 
            this.LblSifre.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblSifre.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.LblSifre.Appearance.Options.UseFont = true;
            this.LblSifre.Appearance.Options.UseForeColor = true;
            this.LblSifre.Location = new System.Drawing.Point(75, 290);
            this.LblSifre.Name = "LblSifre";
            this.LblSifre.Size = new System.Drawing.Size(34, 17);
            this.LblSifre.TabIndex = 1;
            this.LblSifre.Text = "Şifre:";
            // 
            // TxtKullaniciAdi
            // 
            this.TxtKullaniciAdi.Location = new System.Drawing.Point(75, 265);
            this.TxtKullaniciAdi.Name = "TxtKullaniciAdi";
            this.TxtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            this.TxtKullaniciAdi.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.TxtKullaniciAdi.Size = new System.Drawing.Size(350, 22);
            this.TxtKullaniciAdi.TabIndex = 2;
            // 
            // TxtSifre
            // 
            this.TxtSifre.Location = new System.Drawing.Point(75, 315);
            this.TxtSifre.Name = "TxtSifre";
            this.TxtSifre.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtSifre.Properties.Appearance.Options.UseFont = true;
            this.TxtSifre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.TxtSifre.Properties.PasswordChar = '*';
            this.TxtSifre.Size = new System.Drawing.Size(350, 22);
            this.TxtSifre.TabIndex = 3;
            // 
            // BtnGiris
            // 
            this.BtnGiris.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnGiris.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnGiris.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnGiris.Appearance.Options.UseBackColor = true;
            this.BtnGiris.Appearance.Options.UseFont = true;
            this.BtnGiris.Appearance.Options.UseForeColor = true;
            this.BtnGiris.Location = new System.Drawing.Point(75, 360);
            this.BtnGiris.Name = "BtnGiris";
            this.BtnGiris.Size = new System.Drawing.Size(165, 45);
            this.BtnGiris.TabIndex = 4;
            this.BtnGiris.Text = "Giriş Yap";
            this.BtnGiris.Click += new System.EventHandler(this.BtnGiris_Click);
            // 
            // BtnCikis
            // 
            this.BtnCikis.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.BtnCikis.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCikis.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnCikis.Appearance.Options.UseBackColor = true;
            this.BtnCikis.Appearance.Options.UseFont = true;
            this.BtnCikis.Appearance.Options.UseForeColor = true;
            this.BtnCikis.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.BtnCikis.AppearanceHovered.Options.UseBackColor = true;
            this.BtnCikis.Location = new System.Drawing.Point(260, 360);
            this.BtnCikis.Name = "BtnCikis";
            this.BtnCikis.Size = new System.Drawing.Size(165, 45);
            this.BtnCikis.TabIndex = 5;
            this.BtnCikis.Text = "Çıkış";
            this.BtnCikis.Click += new System.EventHandler(this.BtnCikis_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcı Girişi";
            ((System.ComponentModel.ISupportInitialize)(this.PnlMain)).EndInit();
            this.PnlMain.ResumeLayout(false);
            this.PnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtSifre.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl PnlMain;
        private DevExpress.XtraEditors.PictureEdit PictureEdit;
        private DevExpress.XtraEditors.LabelControl LblBaslik;
        private DevExpress.XtraEditors.LabelControl LblKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl LblSifre;
        private DevExpress.XtraEditors.TextEdit TxtKullaniciAdi;
        private DevExpress.XtraEditors.TextEdit TxtSifre;
        private DevExpress.XtraEditors.SimpleButton BtnGiris;
        private DevExpress.XtraEditors.SimpleButton BtnCikis;
    }
}
