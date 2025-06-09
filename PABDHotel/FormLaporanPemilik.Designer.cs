namespace PABDHotel
{
    partial class FormLaporanPemilik
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataPemilikBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.laporanDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.laporanDataSet = new PABDHotel.LaporanDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataPemilikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataPemilikBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataPemilikBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataPemilikBindingSource1
            // 
            this.dataPemilikBindingSource1.DataMember = "DataPemilik";
            this.dataPemilikBindingSource1.DataSource = this.laporanDataSetBindingSource;
            // 
            // laporanDataSetBindingSource
            // 
            this.laporanDataSetBindingSource.DataSource = this.laporanDataSet;
            this.laporanDataSetBindingSource.Position = 0;
            // 
            // laporanDataSet
            // 
            this.laporanDataSet.DataSetName = "LaporanDataSet";
            this.laporanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            reportDataSource3.Name = "LaporanDataSet";
            reportDataSource3.Value = this.dataPemilikBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PABDHotel.LaporanPemilik.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(776, 331);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataPemilikBindingSource
            // 
            this.DataPemilikBindingSource.DataMember = "DataPemilik";
            this.DataPemilikBindingSource.DataSource = this.laporanDataSet;
            // 
            // btnKembali
            // 
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembali.Location = new System.Drawing.Point(12, 391);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(776, 23);
            this.btnKembali.TabIndex = 1;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // FormLaporanPemilik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormLaporanPemilik";
            this.Text = "FormLaporanPemilik";
            this.Load += new System.EventHandler(this.FormLaporanPemilik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataPemilikBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataPemilikBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource laporanDataSetBindingSource;
        private LaporanDataSet laporanDataSet;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataPemilikBindingSource1;
        private System.Windows.Forms.BindingSource DataPemilikBindingSource;
        private System.Windows.Forms.Button btnKembali;
    }
}