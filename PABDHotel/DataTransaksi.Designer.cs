namespace PABDHotel
{
    partial class DataTransaksi
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
            this.PemilikID = new System.Windows.Forms.Label();
            this.HewanID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KamarID = new System.Windows.Forms.Label();
            this.CheckIn = new System.Windows.Forms.Label();
            this.CheckOut = new System.Windows.Forms.Label();
            this.Fasilitas = new System.Windows.Forms.Label();
            this.HargaFasilitas = new System.Windows.Forms.Label();
            this.cmbJenisHewan = new System.Windows.Forms.ComboBox();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.txtFasilitas = new System.Windows.Forms.TextBox();
            this.txtHargaFasilitas = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvTransaksi = new System.Windows.Forms.DataGridView();
            this.cmbPemilik = new System.Windows.Forms.ComboBox();
            this.cmbHewan = new System.Windows.Forms.ComboBox();
            this.cmbKamar = new System.Windows.Forms.ComboBox();
            this.btnAnalisis = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransaksi)).BeginInit();
            this.SuspendLayout();
            // 
            // PemilikID
            // 
            this.PemilikID.AutoSize = true;
            this.PemilikID.Location = new System.Drawing.Point(53, 49);
            this.PemilikID.Name = "PemilikID";
            this.PemilikID.Size = new System.Drawing.Size(91, 16);
            this.PemilikID.TabIndex = 0;
            this.PemilikID.Text = "Nama Pemilik";
            // 
            // HewanID
            // 
            this.HewanID.AutoSize = true;
            this.HewanID.Location = new System.Drawing.Point(53, 80);
            this.HewanID.Name = "HewanID";
            this.HewanID.Size = new System.Drawing.Size(89, 16);
            this.HewanID.TabIndex = 1;
            this.HewanID.Text = "Nama Hewan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Jenis Hewan";
            // 
            // KamarID
            // 
            this.KamarID.AutoSize = true;
            this.KamarID.Location = new System.Drawing.Point(53, 144);
            this.KamarID.Name = "KamarID";
            this.KamarID.Size = new System.Drawing.Size(81, 16);
            this.KamarID.TabIndex = 3;
            this.KamarID.Text = "Jenis Kamar";
            // 
            // CheckIn
            // 
            this.CheckIn.AutoSize = true;
            this.CheckIn.Location = new System.Drawing.Point(59, 172);
            this.CheckIn.Name = "CheckIn";
            this.CheckIn.Size = new System.Drawing.Size(59, 16);
            this.CheckIn.TabIndex = 4;
            this.CheckIn.Text = "Check-In";
            // 
            // CheckOut
            // 
            this.CheckOut.AutoSize = true;
            this.CheckOut.Location = new System.Drawing.Point(59, 201);
            this.CheckOut.Name = "CheckOut";
            this.CheckOut.Size = new System.Drawing.Size(69, 16);
            this.CheckOut.TabIndex = 5;
            this.CheckOut.Text = "Check-Out";
            // 
            // Fasilitas
            // 
            this.Fasilitas.AutoSize = true;
            this.Fasilitas.Location = new System.Drawing.Point(61, 233);
            this.Fasilitas.Name = "Fasilitas";
            this.Fasilitas.Size = new System.Drawing.Size(57, 16);
            this.Fasilitas.TabIndex = 6;
            this.Fasilitas.Text = "Fasilitas";
            // 
            // HargaFasilitas
            // 
            this.HargaFasilitas.AutoSize = true;
            this.HargaFasilitas.Location = new System.Drawing.Point(53, 264);
            this.HargaFasilitas.Name = "HargaFasilitas";
            this.HargaFasilitas.Size = new System.Drawing.Size(98, 16);
            this.HargaFasilitas.TabIndex = 7;
            this.HargaFasilitas.Text = "Harga Fasilitas";
            // 
            // cmbJenisHewan
            // 
            this.cmbJenisHewan.FormattingEnabled = true;
            this.cmbJenisHewan.Items.AddRange(new object[] {
            "Anjing",
            "Kucing"});
            this.cmbJenisHewan.Location = new System.Drawing.Point(215, 105);
            this.cmbJenisHewan.Name = "cmbJenisHewan";
            this.cmbJenisHewan.Size = new System.Drawing.Size(435, 24);
            this.cmbJenisHewan.TabIndex = 11;
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Location = new System.Drawing.Point(215, 166);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(435, 22);
            this.dtpCheckIn.TabIndex = 13;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Location = new System.Drawing.Point(215, 195);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(435, 22);
            this.dtpCheckOut.TabIndex = 14;
            // 
            // txtFasilitas
            // 
            this.txtFasilitas.Location = new System.Drawing.Point(215, 227);
            this.txtFasilitas.Name = "txtFasilitas";
            this.txtFasilitas.Size = new System.Drawing.Size(435, 22);
            this.txtFasilitas.TabIndex = 15;
            // 
            // txtHargaFasilitas
            // 
            this.txtHargaFasilitas.Location = new System.Drawing.Point(215, 258);
            this.txtHargaFasilitas.Name = "txtHargaFasilitas";
            this.txtHargaFasilitas.Size = new System.Drawing.Size(435, 22);
            this.txtHargaFasilitas.TabIndex = 16;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(687, 40);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 18;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(687, 106);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 19;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(687, 172);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(75, 23);
            this.btnUbah.TabIndex = 20;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(687, 233);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvTransaksi
            // 
            this.dgvTransaksi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransaksi.Location = new System.Drawing.Point(56, 334);
            this.dgvTransaksi.Name = "dgvTransaksi";
            this.dgvTransaksi.RowHeadersWidth = 51;
            this.dgvTransaksi.RowTemplate.Height = 24;
            this.dgvTransaksi.Size = new System.Drawing.Size(706, 157);
            this.dgvTransaksi.TabIndex = 22;
            this.dgvTransaksi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransaksi_CellClick);
            // 
            // cmbPemilik
            // 
            this.cmbPemilik.FormattingEnabled = true;
            this.cmbPemilik.Location = new System.Drawing.Point(215, 40);
            this.cmbPemilik.Name = "cmbPemilik";
            this.cmbPemilik.Size = new System.Drawing.Size(435, 24);
            this.cmbPemilik.TabIndex = 23;
            // 
            // cmbHewan
            // 
            this.cmbHewan.FormattingEnabled = true;
            this.cmbHewan.Location = new System.Drawing.Point(215, 75);
            this.cmbHewan.Name = "cmbHewan";
            this.cmbHewan.Size = new System.Drawing.Size(435, 24);
            this.cmbHewan.TabIndex = 24;
            // 
            // cmbKamar
            // 
            this.cmbKamar.FormattingEnabled = true;
            this.cmbKamar.Location = new System.Drawing.Point(215, 135);
            this.cmbKamar.Name = "cmbKamar";
            this.cmbKamar.Size = new System.Drawing.Size(435, 24);
            this.cmbKamar.TabIndex = 25;
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(215, 295);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisis.TabIndex = 26;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(391, 295);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 27;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(575, 295);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 28;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // DataTransaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 513);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.cmbKamar);
            this.Controls.Add(this.cmbHewan);
            this.Controls.Add(this.cmbPemilik);
            this.Controls.Add(this.dgvTransaksi);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtHargaFasilitas);
            this.Controls.Add(this.txtFasilitas);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.cmbJenisHewan);
            this.Controls.Add(this.HargaFasilitas);
            this.Controls.Add(this.Fasilitas);
            this.Controls.Add(this.CheckOut);
            this.Controls.Add(this.CheckIn);
            this.Controls.Add(this.KamarID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HewanID);
            this.Controls.Add(this.PemilikID);
            this.Name = "DataTransaksi";
            this.Text = "Data Transaksi";
            this.Load += new System.EventHandler(this.DataTransaksi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransaksi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PemilikID;
        private System.Windows.Forms.Label HewanID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label KamarID;
        private System.Windows.Forms.Label CheckIn;
        private System.Windows.Forms.Label CheckOut;
        private System.Windows.Forms.Label Fasilitas;
        private System.Windows.Forms.Label HargaFasilitas;
        private System.Windows.Forms.ComboBox cmbJenisHewan;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.TextBox txtFasilitas;
        private System.Windows.Forms.TextBox txtHargaFasilitas;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvTransaksi;
        private System.Windows.Forms.ComboBox cmbPemilik;
        private System.Windows.Forms.ComboBox cmbHewan;
        private System.Windows.Forms.ComboBox cmbKamar;
        private System.Windows.Forms.Button btnAnalisis;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnImport;
    }
}

