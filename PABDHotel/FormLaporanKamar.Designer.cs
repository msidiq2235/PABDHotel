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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.dataKamarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.laporanDataSet = new PABDHotel.LaporanDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataKamarBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataKamarBindingSource
            // 
            this.dataKamarBindingSource.DataMember = "DataKamar";
            this.dataKamarBindingSource.DataSource = this.laporanDataSet;
            // 
            // laporanDataSet
            // 
            this.laporanDataSet.DataSetName = "LaporanDataSet";
            this.laporanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            reportDataSource3.Name = "LaporanKamar";
            reportDataSource3.Value = this.dataKamarBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PABDHotel.LaporanKamar.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(27, 27);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(721, 334);
            this.reportViewer1.TabIndex = 0;
            // 
            // btnKembali
            // 
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembali.Location = new System.Drawing.Point(27, 403);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(721, 23);
            this.btnKembali.TabIndex = 2;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // FormLaporanKamar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormLaporanKamar";
            this.Text = "FormLaporanKamar";
            this.Load += new System.EventHandler(this.FormLaporanKamar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataKamarBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laporanDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dataKamarBindingSource;
        private LaporanDataSet laporanDataSet;
        private System.Windows.Forms.Button btnKembali;
    }
}