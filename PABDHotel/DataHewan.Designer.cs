namespace PABDHotel
{
    partial class DataHewan
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
            this.dgvHewan = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamaHewan = new System.Windows.Forms.TextBox();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.cmbPemilik = new System.Windows.Forms.ComboBox();
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHewan)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHewan
            // 
            this.dgvHewan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHewan.Location = new System.Drawing.Point(43, 184);
            this.dgvHewan.Name = "dgvHewan";
            this.dgvHewan.RowHeadersWidth = 51;
            this.dgvHewan.RowTemplate.Height = 24;
            this.dgvHewan.Size = new System.Drawing.Size(714, 222);
            this.dgvHewan.TabIndex = 0;
            this.dgvHewan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHewan_CellClick);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(43, 140);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 1;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(145, 140);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 2;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(253, 140);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(75, 23);
            this.btnUbah.TabIndex = 3;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(352, 139);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nama Hewan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Jenis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pemilik";
            // 
            // txtNamaHewan
            // 
            this.txtNamaHewan.Location = new System.Drawing.Point(253, 20);
            this.txtNamaHewan.Name = "txtNamaHewan";
            this.txtNamaHewan.Size = new System.Drawing.Size(476, 22);
            this.txtNamaHewan.TabIndex = 8;
            // 
            // cmbJenis
            // 
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Items.AddRange(new object[] {
            "Anjing",
            "Kucing"});
            this.cmbJenis.Location = new System.Drawing.Point(253, 60);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(476, 24);
            this.cmbJenis.TabIndex = 9;
            // 
            // cmbPemilik
            // 
            this.cmbPemilik.FormattingEnabled = true;
            this.cmbPemilik.Location = new System.Drawing.Point(253, 100);
            this.cmbPemilik.Name = "cmbPemilik";
            this.cmbPemilik.Size = new System.Drawing.Size(476, 24);
            this.cmbPemilik.TabIndex = 10;
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(456, 140);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisis.TabIndex = 11;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(553, 140);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 12;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(661, 139);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 13;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(682, 415);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(75, 23);
            this.btnKembali.TabIndex = 14;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // DataHewan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.cmbPemilik);
            this.Controls.Add(this.cmbJenis);
            this.Controls.Add(this.txtNamaHewan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dgvHewan);
            this.Name = "DataHewan";
            this.Text = "DataHewan";
            this.Load += new System.EventHandler(this.DataHewan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHewan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHewan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamaHewan;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.ComboBox cmbPemilik;
        private System.Windows.Forms.Button btnAnalisis;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnKembali;
    }
}