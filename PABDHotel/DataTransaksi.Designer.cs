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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PemilikID = new System.Windows.Forms.Label();
            this.HewanID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KamarID = new System.Windows.Forms.Label();
            this.CheckIn = new System.Windows.Forms.Label();
            this.CheckOut = new System.Windows.Forms.Label();
            this.Fasilitas = new System.Windows.Forms.Label();
            this.HargaFasilitas = new System.Windows.Forms.Label();
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
            this.btnKembali = new System.Windows.Forms.Button();
            this.txtJenisHewan = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransaksi)).BeginInit();
            this.SuspendLayout();
            // 
            // PemilikID
            // 
            this.PemilikID.AutoSize = true;
            this.PemilikID.BackColor = System.Drawing.Color.Transparent;
            this.PemilikID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PemilikID.ForeColor = System.Drawing.Color.White;
            this.PemilikID.Location = new System.Drawing.Point(48, 44);
            this.PemilikID.Name = "PemilikID";
            this.PemilikID.Size = new System.Drawing.Size(103, 20);
            this.PemilikID.TabIndex = 0;
            this.PemilikID.Text = "Nama Pemilik";
            // 
            // HewanID
            // 
            this.HewanID.AutoSize = true;
            this.HewanID.BackColor = System.Drawing.Color.Transparent;
            this.HewanID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HewanID.ForeColor = System.Drawing.Color.White;
            this.HewanID.Location = new System.Drawing.Point(48, 79);
            this.HewanID.Name = "HewanID";
            this.HewanID.Size = new System.Drawing.Size(101, 20);
            this.HewanID.TabIndex = 1;
            this.HewanID.Text = "Nama Hewan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(48, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Jenis Hewan";
            // 
            // KamarID
            // 
            this.KamarID.AutoSize = true;
            this.KamarID.BackColor = System.Drawing.Color.Transparent;
            this.KamarID.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KamarID.ForeColor = System.Drawing.Color.White;
            this.KamarID.Location = new System.Drawing.Point(48, 139);
            this.KamarID.Name = "KamarID";
            this.KamarID.Size = new System.Drawing.Size(90, 20);
            this.KamarID.TabIndex = 3;
            this.KamarID.Text = "Jenis Kamar";
            // 
            // CheckIn
            // 
            this.CheckIn.AutoSize = true;
            this.CheckIn.BackColor = System.Drawing.Color.Transparent;
            this.CheckIn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckIn.ForeColor = System.Drawing.Color.White;
            this.CheckIn.Location = new System.Drawing.Point(48, 168);
            this.CheckIn.Name = "CheckIn";
            this.CheckIn.Size = new System.Drawing.Size(68, 20);
            this.CheckIn.TabIndex = 4;
            this.CheckIn.Text = "Check-In";
            // 
            // CheckOut
            // 
            this.CheckOut.AutoSize = true;
            this.CheckOut.BackColor = System.Drawing.Color.Transparent;
            this.CheckOut.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckOut.ForeColor = System.Drawing.Color.White;
            this.CheckOut.Location = new System.Drawing.Point(48, 197);
            this.CheckOut.Name = "CheckOut";
            this.CheckOut.Size = new System.Drawing.Size(80, 20);
            this.CheckOut.TabIndex = 5;
            this.CheckOut.Text = "Check-Out";
            // 
            // Fasilitas
            // 
            this.Fasilitas.AutoSize = true;
            this.Fasilitas.BackColor = System.Drawing.Color.Transparent;
            this.Fasilitas.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fasilitas.ForeColor = System.Drawing.Color.White;
            this.Fasilitas.Location = new System.Drawing.Point(48, 229);
            this.Fasilitas.Name = "Fasilitas";
            this.Fasilitas.Size = new System.Drawing.Size(61, 20);
            this.Fasilitas.TabIndex = 6;
            this.Fasilitas.Text = "Fasilitas";
            // 
            // HargaFasilitas
            // 
            this.HargaFasilitas.AutoSize = true;
            this.HargaFasilitas.BackColor = System.Drawing.Color.Transparent;
            this.HargaFasilitas.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HargaFasilitas.ForeColor = System.Drawing.Color.White;
            this.HargaFasilitas.Location = new System.Drawing.Point(48, 260);
            this.HargaFasilitas.Name = "HargaFasilitas";
            this.HargaFasilitas.Size = new System.Drawing.Size(107, 20);
            this.HargaFasilitas.TabIndex = 7;
            this.HargaFasilitas.Text = "Harga Fasilitas";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckIn.Location = new System.Drawing.Point(215, 166);
            this.dtpCheckIn.MinDate = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(435, 25);
            this.dtpCheckIn.TabIndex = 13;
            this.dtpCheckIn.Value = new System.DateTime(2025, 6, 10, 19, 2, 22, 0);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckOut.Location = new System.Drawing.Point(215, 195);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(435, 25);
            this.dtpCheckOut.TabIndex = 14;
            // 
            // txtFasilitas
            // 
            this.txtFasilitas.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFasilitas.Location = new System.Drawing.Point(215, 227);
            this.txtFasilitas.Name = "txtFasilitas";
            this.txtFasilitas.Size = new System.Drawing.Size(435, 25);
            this.txtFasilitas.TabIndex = 15;
            // 
            // txtHargaFasilitas
            // 
            this.txtHargaFasilitas.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHargaFasilitas.Location = new System.Drawing.Point(215, 258);
            this.txtHargaFasilitas.Name = "txtHargaFasilitas";
            this.txtHargaFasilitas.Size = new System.Drawing.Size(435, 25);
            this.txtHargaFasilitas.TabIndex = 16;
            // 
            // btnTambah
            // 
            this.btnTambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.Location = new System.Drawing.Point(687, 79);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 18;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHapus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.Location = new System.Drawing.Point(687, 122);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 19;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbah.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUbah.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUbah.Location = new System.Drawing.Point(687, 165);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(75, 23);
            this.btnUbah.TabIndex = 20;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(687, 207);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvTransaksi
            // 
            this.dgvTransaksi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransaksi.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransaksi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransaksi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTransaksi.Location = new System.Drawing.Point(56, 334);
            this.dgvTransaksi.Name = "dgvTransaksi";
            this.dgvTransaksi.RowHeadersWidth = 51;
            this.dgvTransaksi.RowTemplate.Height = 24;
            this.dgvTransaksi.Size = new System.Drawing.Size(706, 137);
            this.dgvTransaksi.TabIndex = 22;
            this.dgvTransaksi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransaksi_CellClick);
            // 
            // cmbPemilik
            // 
            this.cmbPemilik.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPemilik.FormattingEnabled = true;
            this.cmbPemilik.Location = new System.Drawing.Point(215, 40);
            this.cmbPemilik.Name = "cmbPemilik";
            this.cmbPemilik.Size = new System.Drawing.Size(435, 25);
            this.cmbPemilik.TabIndex = 23;
            this.cmbPemilik.SelectedIndexChanged += new System.EventHandler(this.cmbPemilik_SelectedIndexChanged);
            // 
            // cmbHewan
            // 
            this.cmbHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbHewan.FormattingEnabled = true;
            this.cmbHewan.Location = new System.Drawing.Point(215, 71);
            this.cmbHewan.Name = "cmbHewan";
            this.cmbHewan.Size = new System.Drawing.Size(435, 25);
            this.cmbHewan.TabIndex = 24;
            this.cmbHewan.SelectedIndexChanged += new System.EventHandler(this.cmbHewan_SelectedIndexChanged);
            // 
            // cmbKamar
            // 
            this.cmbKamar.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKamar.FormattingEnabled = true;
            this.cmbKamar.Location = new System.Drawing.Point(215, 135);
            this.cmbKamar.Name = "cmbKamar";
            this.cmbKamar.Size = new System.Drawing.Size(435, 25);
            this.cmbKamar.TabIndex = 25;
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnalisis.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAnalisis.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(575, 295);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 28;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnKembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembali.ForeColor = System.Drawing.Color.White;
            this.btnKembali.Location = new System.Drawing.Point(680, 478);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(82, 23);
            this.btnKembali.TabIndex = 29;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // txtJenisHewan
            // 
            this.txtJenisHewan.BackColor = System.Drawing.Color.White;
            this.txtJenisHewan.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJenisHewan.Location = new System.Drawing.Point(215, 104);
            this.txtJenisHewan.Name = "txtJenisHewan";
            this.txtJenisHewan.ReadOnly = true;
            this.txtJenisHewan.Size = new System.Drawing.Size(435, 25);
            this.txtJenisHewan.TabIndex = 30;
            this.txtJenisHewan.TabStop = false;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(687, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Lihat Grafik";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataTransaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PABDHotel.Properties.Resources.Form_background_image_2;
            this.ClientSize = new System.Drawing.Size(801, 513);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtJenisHewan);
            this.Controls.Add(this.btnKembali);
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
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.TextBox txtJenisHewan;
        private System.Windows.Forms.Button button1;
    }
}

