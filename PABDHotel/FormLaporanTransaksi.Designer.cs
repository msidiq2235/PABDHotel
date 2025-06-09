namespace PABDHotel
{
    partial class FormLaporanTransaksi
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
            this.dataTransaksiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.laporanDataSet = new PABDHotel.LaporanDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataTransaksiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataTransaksiBindingSource
            // 
            this.dataTransaksiBindingSource.DataMember = "DataTransaksi";
            this.dataTransaksiBindingSource.DataSource = this.laporanDataSet;
            // 
            // laporanDataSet
            // 
            this.laporanDataSet.DataSetName = "LaporanDataSet";
            this.laporanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            reportDataSource3.Name = "LaporanTransaksi";
            reportDataSource3.Value = this.dataTransaksiBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PABDHotel.LaporanTransaksi.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(939, 339);
            this.reportViewer1.TabIndex = 0;
            // 
            // btnKembali
            // 
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembali.Location = new System.Drawing.Point(12, 387);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(939, 23);
            this.btnKembali.TabIndex = 2;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            // 
            // FormLaporanTransaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 450);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormLaporanTransaksi";
            this.Text = "FormLaporanTransaksi";
            this.Load += new System.EventHandler(this.FormLaporanTransaksi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTransaksiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataTransaksiBindingSource;
        private LaporanDataSet laporanDataSet;
        private System.Windows.Forms.Button btnKembali;
    }
}