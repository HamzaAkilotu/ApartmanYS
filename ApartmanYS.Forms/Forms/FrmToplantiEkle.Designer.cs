namespace ApartmanYS.Forms
{
    partial class FrmToplantiEkle
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
            this.LblToplantiKonusu = new DevExpress.XtraEditors.LabelControl();
            this.TxtToplantiKonusu = new DevExpress.XtraEditors.TextEdit();
            this.LblToplantiTarihi = new DevExpress.XtraEditors.LabelControl();
            this.DtToplantiTarihi = new DevExpress.XtraEditors.DateEdit();
            this.LblToplantiIcerik = new DevExpress.XtraEditors.LabelControl();
            this.TxtToplantiIcerik = new DevExpress.XtraEditors.MemoEdit();
            this.LblToplantiKararlari = new DevExpress.XtraEditors.LabelControl();
            this.TxtToplantiKararlari = new DevExpress.XtraEditors.MemoEdit();
            this.LblKatilanSayisi = new DevExpress.XtraEditors.LabelControl();
            this.SpinKatilanSayisi = new DevExpress.XtraEditors.SpinEdit();
            this.BtnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.BtnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiKonusu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtToplantiTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtToplantiTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiIcerik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiKararlari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinKatilanSayisi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // LblBaslik
            // 
            this.LblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.LblBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.LblBaslik.Location = new System.Drawing.Point(20, 20);
            this.LblBaslik.Name = "LblBaslik";
            this.LblBaslik.Size = new System.Drawing.Size(262, 32);
            this.LblBaslik.TabIndex = 0;
            this.LblBaslik.Text = "🏛️ Yeni Toplantı Ekle";
            // 
            // LblToplantiKonusu
            // 
            this.LblToplantiKonusu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblToplantiKonusu.Location = new System.Drawing.Point(20, 80);
            this.LblToplantiKonusu.Name = "LblToplantiKonusu";
            this.LblToplantiKonusu.Size = new System.Drawing.Size(105, 17);
            this.LblToplantiKonusu.TabIndex = 1;
            this.LblToplantiKonusu.Text = "Toplantı Konusu:";
            // 
            // TxtToplantiKonusu
            // 
            this.TxtToplantiKonusu.Location = new System.Drawing.Point(20, 105);
            this.TxtToplantiKonusu.Name = "TxtToplantiKonusu";
            this.TxtToplantiKonusu.Size = new System.Drawing.Size(550, 30);
            this.TxtToplantiKonusu.TabIndex = 2;
            // 
            // LblToplantiTarihi
            // 
            this.LblToplantiTarihi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblToplantiTarihi.Location = new System.Drawing.Point(20, 155);
            this.LblToplantiTarihi.Name = "LblToplantiTarihi";
            this.LblToplantiTarihi.Size = new System.Drawing.Size(95, 17);
            this.LblToplantiTarihi.TabIndex = 3;
            this.LblToplantiTarihi.Text = "Toplantı Tarihi:";
            // 
            // DtToplantiTarihi
            // 
            this.DtToplantiTarihi.EditValue = null;
            this.DtToplantiTarihi.Location = new System.Drawing.Point(20, 180);
            this.DtToplantiTarihi.Name = "DtToplantiTarihi";
            this.DtToplantiTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtToplantiTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtToplantiTarihi.Size = new System.Drawing.Size(550, 30);
            this.DtToplantiTarihi.TabIndex = 4;
            // 
            // LblToplantiIcerik
            // 
            this.LblToplantiIcerik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblToplantiIcerik.Location = new System.Drawing.Point(20, 230);
            this.LblToplantiIcerik.Name = "LblToplantiIcerik";
            this.LblToplantiIcerik.Size = new System.Drawing.Size(96, 17);
            this.LblToplantiIcerik.TabIndex = 5;
            this.LblToplantiIcerik.Text = "Toplantı İçeriği:";
            // 
            // TxtToplantiIcerik
            // 
            this.TxtToplantiIcerik.Location = new System.Drawing.Point(20, 255);
            this.TxtToplantiIcerik.Name = "TxtToplantiIcerik";
            this.TxtToplantiIcerik.Size = new System.Drawing.Size(550, 80);
            this.TxtToplantiIcerik.TabIndex = 6;
            // 
            // LblToplantiKararlari
            // 
            this.LblToplantiKararlari.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblToplantiKararlari.Location = new System.Drawing.Point(20, 350);
            this.LblToplantiKararlari.Name = "LblToplantiKararlari";
            this.LblToplantiKararlari.Size = new System.Drawing.Size(104, 17);
            this.LblToplantiKararlari.TabIndex = 7;
            this.LblToplantiKararlari.Text = "Toplantı Kararları:";
            // 
            // TxtToplantiKararlari
            // 
            this.TxtToplantiKararlari.Location = new System.Drawing.Point(20, 375);
            this.TxtToplantiKararlari.Name = "TxtToplantiKararlari";
            this.TxtToplantiKararlari.Size = new System.Drawing.Size(550, 100);
            this.TxtToplantiKararlari.TabIndex = 8;
            // 
            // LblKatilanSayisi
            // 
            this.LblKatilanSayisi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblKatilanSayisi.Location = new System.Drawing.Point(20, 490);
            this.LblKatilanSayisi.Name = "LblKatilanSayisi";
            this.LblKatilanSayisi.Size = new System.Drawing.Size(91, 17);
            this.LblKatilanSayisi.TabIndex = 9;
            this.LblKatilanSayisi.Text = "Katılan Sayısı:";
            // 
            // SpinKatilanSayisi
            // 
            this.SpinKatilanSayisi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinKatilanSayisi.Location = new System.Drawing.Point(20, 515);
            this.SpinKatilanSayisi.Name = "SpinKatilanSayisi";
            this.SpinKatilanSayisi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinKatilanSayisi.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SpinKatilanSayisi.Size = new System.Drawing.Size(550, 30);
            this.SpinKatilanSayisi.TabIndex = 10;
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.BtnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnKaydet.Appearance.Options.UseBackColor = true;
            this.BtnKaydet.Appearance.Options.UseForeColor = true;
            this.BtnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnKaydet.Location = new System.Drawing.Point(360, 570);
            this.BtnKaydet.Name = "BtnKaydet";
            this.BtnKaydet.Size = new System.Drawing.Size(100, 40);
            this.BtnKaydet.TabIndex = 11;
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
            this.BtnIptal.Location = new System.Drawing.Point(470, 570);
            this.BtnIptal.Name = "BtnIptal";
            this.BtnIptal.Size = new System.Drawing.Size(100, 40);
            this.BtnIptal.TabIndex = 12;
            this.BtnIptal.Text = "İptal";
            this.BtnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // FrmToplantiEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 640);
            this.Controls.Add(this.BtnIptal);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.SpinKatilanSayisi);
            this.Controls.Add(this.LblKatilanSayisi);
            this.Controls.Add(this.TxtToplantiKararlari);
            this.Controls.Add(this.LblToplantiKararlari);
            this.Controls.Add(this.TxtToplantiIcerik);
            this.Controls.Add(this.LblToplantiIcerik);
            this.Controls.Add(this.DtToplantiTarihi);
            this.Controls.Add(this.LblToplantiTarihi);
            this.Controls.Add(this.TxtToplantiKonusu);
            this.Controls.Add(this.LblToplantiKonusu);
            this.Controls.Add(this.LblBaslik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmToplantiEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Toplantı Ekle";
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiKonusu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtToplantiTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtToplantiTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiIcerik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtToplantiKararlari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinKatilanSayisi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblBaslik;
        private DevExpress.XtraEditors.LabelControl LblToplantiKonusu;
        private DevExpress.XtraEditors.TextEdit TxtToplantiKonusu;
        private DevExpress.XtraEditors.LabelControl LblToplantiTarihi;
        private DevExpress.XtraEditors.DateEdit DtToplantiTarihi;
        private DevExpress.XtraEditors.LabelControl LblToplantiIcerik;
        private DevExpress.XtraEditors.MemoEdit TxtToplantiIcerik;
        private DevExpress.XtraEditors.LabelControl LblToplantiKararlari;
        private DevExpress.XtraEditors.MemoEdit TxtToplantiKararlari;
        private DevExpress.XtraEditors.LabelControl LblKatilanSayisi;
        private DevExpress.XtraEditors.SpinEdit SpinKatilanSayisi;
        private DevExpress.XtraEditors.SimpleButton BtnKaydet;
        private DevExpress.XtraEditors.SimpleButton BtnIptal;
    }
}

