namespace ApartmanYS.Forms
{
    partial class FrmDaireDetay
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
            this.LblDaireNo = new DevExpress.XtraEditors.LabelControl();
            this.LblSahip = new DevExpress.XtraEditors.LabelControl();
            this.LblTelefon = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.GridAidat = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LabelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.GridArac = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BtnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridAidat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridArac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // LblDaireNo
            // 
            this.LblDaireNo.Location = new System.Drawing.Point(12, 12);
            this.LblDaireNo.Name = "LblDaireNo";
            this.LblDaireNo.Size = new System.Drawing.Size(50, 13);
            this.LblDaireNo.TabIndex = 0;
            this.LblDaireNo.Text = "Daire No:";
            // 
            // LblSahip
            // 
            this.LblSahip.Location = new System.Drawing.Point(12, 40);
            this.LblSahip.Name = "LblSahip";
            this.LblSahip.Size = new System.Drawing.Size(38, 13);
            this.LblSahip.TabIndex = 1;
            this.LblSahip.Text = "Sahip:";
            // 
            // LblTelefon
            // 
            this.LblTelefon.Location = new System.Drawing.Point(12, 68);
            this.LblTelefon.Name = "LblTelefon";
            this.LblTelefon.Size = new System.Drawing.Size(48, 13);
            this.LblTelefon.TabIndex = 2;
            this.LblTelefon.Text = "Telefon:";
            // 
            // LabelControl1
            // 
            this.LabelControl1.Location = new System.Drawing.Point(12, 110);
            this.LabelControl1.Name = "LabelControl1";
            this.LabelControl1.Size = new System.Drawing.Size(33, 13);
            this.LabelControl1.TabIndex = 3;
            this.LabelControl1.Text = "Aidat:";
            // 
            // LabelControl2
            // 
            this.LabelControl2.Location = new System.Drawing.Point(300, 110);
            this.LabelControl2.Name = "LabelControl2";
            this.LabelControl2.Size = new System.Drawing.Size(32, 13);
            this.LabelControl2.TabIndex = 4;
            this.LabelControl2.Text = "Araç:";
            // 
            // GridAidat
            // 
            this.GridAidat.Location = new System.Drawing.Point(12, 129);
            this.GridAidat.MainView = this.gridView1;
            this.GridAidat.Name = "GridAidat";
            this.GridAidat.Size = new System.Drawing.Size(280, 200);
            this.GridAidat.TabIndex = 5;
            this.GridAidat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GridAidat;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // LabelControl3
            // 
            this.LabelControl3.Location = new System.Drawing.Point(300, 110);
            this.LabelControl3.Name = "LabelControl3";
            this.LabelControl3.Size = new System.Drawing.Size(32, 13);
            this.LabelControl3.TabIndex = 4;
            this.LabelControl3.Text = "Araç:";
            // 
            // GridArac
            // 
            this.GridArac.Location = new System.Drawing.Point(300, 129);
            this.GridArac.MainView = this.gridView2;
            this.GridArac.Name = "GridArac";
            this.GridArac.Size = new System.Drawing.Size(280, 200);
            this.GridArac.TabIndex = 6;
            this.GridArac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.GridArac;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // BtnKapat
            // 
            this.BtnKapat.Location = new System.Drawing.Point(505, 350);
            this.BtnKapat.Name = "BtnKapat";
            this.BtnKapat.Size = new System.Drawing.Size(75, 23);
            this.BtnKapat.TabIndex = 7;
            this.BtnKapat.Text = "Kapat";
            this.BtnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            // 
            // FrmDaireDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 385);
            this.Controls.Add(this.BtnKapat);
            this.Controls.Add(this.GridArac);
            this.Controls.Add(this.GridAidat);
            this.Controls.Add(this.LabelControl3);
            this.Controls.Add(this.LabelControl2);
            this.Controls.Add(this.LabelControl1);
            this.Controls.Add(this.LblTelefon);
            this.Controls.Add(this.LblSahip);
            this.Controls.Add(this.LblDaireNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDaireDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daire Detayı";
            ((System.ComponentModel.ISupportInitialize)(this.GridAidat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridArac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblDaireNo;
        private DevExpress.XtraEditors.LabelControl LblSahip;
        private DevExpress.XtraEditors.LabelControl LblTelefon;
        private DevExpress.XtraEditors.LabelControl LabelControl1;
        private DevExpress.XtraEditors.LabelControl LabelControl2;
        private DevExpress.XtraGrid.GridControl GridAidat;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl LabelControl3;
        private DevExpress.XtraGrid.GridControl GridArac;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton BtnKapat;
    }
}
