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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.laporanDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.laporanDataSet = new PABDHotel.LaporanDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataPemilikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataPemilikBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataPemilikBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPemilikBindingSource1)).BeginInit();
            this.SuspendLayout();
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
            reportDataSource1.Name = "LaporanDataSet";
            reportDataSource1.Value = this.dataPemilikBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
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
            // dataPemilikBindingSource1
            // 
            this.dataPemilikBindingSource1.DataMember = "DataPemilik";
            this.dataPemilikBindingSource1.DataSource = this.laporanDataSetBindingSource;
            // 
            // FormLaporanPemilik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormLaporanPemilik";
            this.Text = "FormLaporanPemilik";
            this.Load += new System.EventHandler(this.FormLaporanPemilik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataPemilikBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataPemilikBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource laporanDataSetBindingSource;
        private LaporanDataSet laporanDataSet;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataPemilikBindingSource1;
        private System.Windows.Forms.BindingSource DataPemilikBindingSource;
    }
}