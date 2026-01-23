namespace ApartmanYS.Forms
{
    partial class FrmAidatEkle
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
            this.LblDaire = new DevExpress.XtraEditors.LabelControl();
            this.CmbDaire = new DevExpress.XtraEditors.ComboBoxEdit();
            this.LblTutar = new DevExpress.XtraEditors.LabelControl();
            this.TxtTutar = new DevExpress.XtraEditors.TextEdit();
            this.LblTarih = new DevExpress.XtraEditors.LabelControl();
            this.DtTarih = new DevExpress.XtraEditors.DateEdit();
            this.BtnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.BtnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.CmbDaire.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTarih.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaslik
            // 
            this.LblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.LblBaslik.Location = new System.Drawing.Point(20, 20);
            this.LblBaslik.Name = "LblBaslik";
            this.LblBaslik.Size = new System.Drawing.Size(155, 32);
            this.LblBaslik.TabIndex = 0;
            this.LblBaslik.Text = "💰 Yeni Aidat Ekle";
            // 
            // LblDaire
            // 
            this.LblDaire.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblDaire.Location = new System.Drawing.Point(20, 80);
            this.LblDaire.Name = "LblDaire";
            this.LblDaire.Size = new System.Drawing.Size(74, 17);
            this.LblDaire.TabIndex = 1;
            this.LblDaire.Text = "Daire No:";
            // 
            // CmbDaire
            // 
            this.CmbDaire.Location = new System.Drawing.Point(20, 105);
            this.CmbDaire.Name = "CmbDaire";
            this.CmbDaire.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbDaire.Size = new System.Drawing.Size(550, 30);
            this.CmbDaire.TabIndex = 2;
            // 
            // LblTutar
            // 
            this.LblTutar.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTutar.Location = new System.Drawing.Point(20, 155);
            this.LblTutar.Name = "LblTutar";
            this.LblTutar.Size = new System.Drawing.Size(38, 17);
            this.LblTutar.TabIndex = 3;
            this.LblTutar.Text = "Tutar:";
            // 
            // TxtTutar
            // 
            this.TxtTutar.Location = new System.Drawing.Point(20, 180);
            this.TxtTutar.Name = "TxtTutar";
            this.TxtTutar.Size = new System.Drawing.Size(550, 30);
            this.TxtTutar.TabIndex = 4;
            // 
            // LblTarih
            // 
            this.LblTarih.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblTarih.Location = new System.Drawing.Point(20, 230);
            this.LblTarih.Name = "LblTarih";
            this.LblTarih.Size = new System.Drawing.Size(37, 17);
            this.LblTarih.TabIndex = 5;
            this.LblTarih.Text = "Tarih:";
            // 
            // DtTarih
            // 
            this.DtTarih.EditValue = null;
            this.DtTarih.Location = new System.Drawing.Point(20, 255);
            this.DtTarih.Name = "DtTarih";
            this.DtTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTarih.Size = new System.Drawing.Size(550, 30);
            this.DtTarih.TabIndex = 6;
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(80)))));
            this.BtnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKaydet.Appearance.Options.UseBackColor = true;
            this.BtnKaydet.Appearance.Options.UseForeColor = true;
            this.BtnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnKaydet.Location = new System.Drawing.Point(360, 315);
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
            this.BtnIptal.Location = new System.Drawing.Point(470, 315);
            this.BtnIptal.Name = "BtnIptal";
            this.BtnIptal.Size = new System.Drawing.Size(100, 40);
            this.BtnIptal.TabIndex = 8;
            this.BtnIptal.Text = "İptal";
            this.BtnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmAidatEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 380);
            this.Controls.Add(this.BtnIptal);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.DtTarih);
            this.Controls.Add(this.LblTarih);
            this.Controls.Add(this.TxtTutar);
            this.Controls.Add(this.LblTutar);
            this.Controls.Add(this.CmbDaire);
            this.Controls.Add(this.LblDaire);
            this.Controls.Add(this.LblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAidatEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Aidat Ekle";
            ((System.ComponentModel.ISupportInitialize)(this.CmbDaire.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTarih.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblBaslik;
        private DevExpress.XtraEditors.LabelControl LblDaire;
        private DevExpress.XtraEditors.ComboBoxEdit CmbDaire;
        private DevExpress.XtraEditors.LabelControl LblTutar;
        private DevExpress.XtraEditors.TextEdit TxtTutar;
        private DevExpress.XtraEditors.LabelControl LblTarih;
        private DevExpress.XtraEditors.DateEdit DtTarih;
        private DevExpress.XtraEditors.SimpleButton BtnKaydet;
        private DevExpress.XtraEditors.SimpleButton BtnIptal;
    }
}
