namespace ApartmanYS.Forms
{
    partial class FrmDuyuruEkle
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
            this.LblDuyuruBaslik = new DevExpress.XtraEditors.LabelControl();
            this.TxtDuyuruBaslik = new DevExpress.XtraEditors.TextEdit();
            this.LblDuyuruIcerik = new DevExpress.XtraEditors.LabelControl();
            this.TxtDuyuruIcerik = new DevExpress.XtraEditors.MemoEdit();
            this.LblBitisTarihi = new DevExpress.XtraEditors.LabelControl();
            this.DtBitisTarihi = new DevExpress.XtraEditors.DateEdit();
            this.BtnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.BtnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDuyuruBaslik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDuyuruIcerik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtBitisTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtBitisTarihi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaslik
            // 
            this.LblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.LblBaslik.Location = new System.Drawing.Point(20, 20);
            this.LblBaslik.Name = "LblBaslik";
            this.LblBaslik.Size = new System.Drawing.Size(184, 32);
            this.LblBaslik.TabIndex = 0;
            this.LblBaslik.Text = "📢 Yeni Duyuru Ekle";
            // 
            // LblDuyuruBaslik
            // 
            this.LblDuyuruBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDuyuruBaslik.Location = new System.Drawing.Point(20, 80);
            this.LblDuyuruBaslik.Name = "LblDuyuruBaslik";
            this.LblDuyuruBaslik.Size = new System.Drawing.Size(91, 17);
            this.LblDuyuruBaslik.TabIndex = 1;
            this.LblDuyuruBaslik.Text = "Duyuru Başlığı:";
            // 
            // TxtDuyuruBaslik
            // 
            this.TxtDuyuruBaslik.Location = new System.Drawing.Point(20, 105);
            this.TxtDuyuruBaslik.Name = "TxtDuyuruBaslik";
            this.TxtDuyuruBaslik.Size = new System.Drawing.Size(550, 30);
            this.TxtDuyuruBaslik.TabIndex = 2;
            // 
            // LblDuyuruIcerik
            // 
            this.LblDuyuruIcerik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDuyuruIcerik.Location = new System.Drawing.Point(20, 155);
            this.LblDuyuruIcerik.Name = "LblDuyuruIcerik";
            this.LblDuyuruIcerik.Size = new System.Drawing.Size(93, 17);
            this.LblDuyuruIcerik.TabIndex = 3;
            this.LblDuyuruIcerik.Text = "Duyuru İçeriği:";
            // 
            // TxtDuyuruIcerik
            // 
            this.TxtDuyuruIcerik.Location = new System.Drawing.Point(20, 180);
            this.TxtDuyuruIcerik.Name = "TxtDuyuruIcerik";
            this.TxtDuyuruIcerik.Size = new System.Drawing.Size(550, 100);
            this.TxtDuyuruIcerik.TabIndex = 4;
            // 
            // LblBitisTarihi
            // 
            this.LblBitisTarihi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblBitisTarihi.Location = new System.Drawing.Point(20, 295);
            this.LblBitisTarihi.Name = "LblBitisTarihi";
            this.LblBitisTarihi.Size = new System.Drawing.Size(75, 17);
            this.LblBitisTarihi.TabIndex = 5;
            this.LblBitisTarihi.Text = "Bitiş Tarihi:";
            // 
            // DtBitisTarihi
            // 
            this.DtBitisTarihi.EditValue = null;
            this.DtBitisTarihi.Location = new System.Drawing.Point(20, 320);
            this.DtBitisTarihi.Name = "DtBitisTarihi";
            this.DtBitisTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtBitisTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtBitisTarihi.Size = new System.Drawing.Size(550, 30);
            this.DtBitisTarihi.TabIndex = 6;
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(80)))));
            this.BtnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKaydet.Appearance.Options.UseBackColor = true;
            this.BtnKaydet.Appearance.Options.UseForeColor = true;
            this.BtnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnKaydet.Location = new System.Drawing.Point(360, 375);
            this.BtnKaydet.Name = "BtnKaydet";
            this.BtnKaydet.Size = new System.Drawing.Size(100, 40);
            this.BtnKaydet.TabIndex = 7;
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
            this.BtnIptal.Location = new System.Drawing.Point(470, 375);
            this.BtnIptal.Name = "BtnIptal";
            this.BtnIptal.Size = new System.Drawing.Size(100, 40);
            this.BtnIptal.TabIndex = 8;
            this.BtnIptal.Text = "İptal";
            this.BtnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmDuyuruEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 440);
            this.Controls.Add(this.BtnIptal);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.DtBitisTarihi);
            this.Controls.Add(this.LblBitisTarihi);
            this.Controls.Add(this.TxtDuyuruIcerik);
            this.Controls.Add(this.LblDuyuruIcerik);
            this.Controls.Add(this.TxtDuyuruBaslik);
            this.Controls.Add(this.LblDuyuruBaslik);
            this.Controls.Add(this.LblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDuyuruEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Duyuru Ekle";
            ((System.ComponentModel.ISupportInitialize)(this.TxtDuyuruBaslik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDuyuruIcerik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtBitisTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtBitisTarihi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblBaslik;
        private DevExpress.XtraEditors.LabelControl LblDuyuruBaslik;
        private DevExpress.XtraEditors.TextEdit TxtDuyuruBaslik;
        private DevExpress.XtraEditors.LabelControl LblDuyuruIcerik;
        private DevExpress.XtraEditors.MemoEdit TxtDuyuruIcerik;
        private DevExpress.XtraEditors.LabelControl LblBitisTarihi;
        private DevExpress.XtraEditors.DateEdit DtBitisTarihi;
        private DevExpress.XtraEditors.SimpleButton BtnKaydet;
        private DevExpress.XtraEditors.SimpleButton BtnIptal;
    }
}
