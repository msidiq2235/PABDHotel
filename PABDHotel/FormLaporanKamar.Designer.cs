namespace PABDHotel
{
    partial class FormLaporanKamar
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.laporanDataSet = new PABDHotel.LaporanDataSet();
            this.dataKamarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataKamarBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "LaporanKamar";
            reportDataSource1.Value = this.dataKamarBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PABDHotel.LaporanKamar.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(27, 27);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(721, 334);
            this.reportViewer1.TabIndex = 0;
            // 
            // laporanDataSet
            // 
            this.laporanDataSet.DataSetName = "LaporanDataSet";
            this.laporanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataKamarBindingSource
            // 
            this.dataKamarBindingSource.DataMember = "DataKamar";
            this.dataKamarBindingSource.DataSource = this.laporanDataSet;
            // 
            // FormLaporanKamar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormLaporanKamar";
            this.Text = "FormLaporanKamar";
            this.Load += new System.EventHandler(this.FormLaporanKamar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataKamarBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataKamarBindingSource;
        private LaporanDataSet laporanDataSet;
    }
}