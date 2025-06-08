namespace PABDHotel
{
    partial class menu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDataHewan = new System.Windows.Forms.Button();
            this.btnDataKamar = new System.Windows.Forms.Button();
            this.btnDataTransaksi = new System.Windows.Forms.Button();
            this.btnDataPemilik = new System.Windows.Forms.Button();
            this.btnKeluar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Pemilik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Hewan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Data Kamar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Data Transaksi";
            // 
            // btnDataHewan
            // 
            this.btnDataHewan.Location = new System.Drawing.Point(432, 136);
            this.btnDataHewan.Name = "btnDataHewan";
            this.btnDataHewan.Size = new System.Drawing.Size(75, 23);
            this.btnDataHewan.TabIndex = 5;
            this.btnDataHewan.Text = "Go";
            this.btnDataHewan.UseVisualStyleBackColor = true;
            this.btnDataHewan.Click += new System.EventHandler(this.btnHewan_Click);
            // 
            // btnDataKamar
            // 
            this.btnDataKamar.Location = new System.Drawing.Point(432, 195);
            this.btnDataKamar.Name = "btnDataKamar";
            this.btnDataKamar.Size = new System.Drawing.Size(75, 23);
            this.btnDataKamar.TabIndex = 6;
            this.btnDataKamar.Text = "Go";
            this.btnDataKamar.UseVisualStyleBackColor = true;
            this.btnDataKamar.Click += new System.EventHandler(this.btnKamar_Click);
            // 
            // btnDataTransaksi
            // 
            this.btnDataTransaksi.Location = new System.Drawing.Point(432, 250);
            this.btnDataTransaksi.Name = "btnDataTransaksi";
            this.btnDataTransaksi.Size = new System.Drawing.Size(75, 23);
            this.btnDataTransaksi.TabIndex = 7;
            this.btnDataTransaksi.Text = "Go";
            this.btnDataTransaksi.UseVisualStyleBackColor = true;
            this.btnDataTransaksi.Click += new System.EventHandler(this.btnTransaksi_Click);
            // 
            // btnDataPemilik
            // 
            this.btnDataPemilik.Location = new System.Drawing.Point(432, 88);
            this.btnDataPemilik.Name = "btnDataPemilik";
            this.btnDataPemilik.Size = new System.Drawing.Size(75, 23);
            this.btnDataPemilik.TabIndex = 8;
            this.btnDataPemilik.Text = "Go";
            this.btnDataPemilik.UseVisualStyleBackColor = true;
            this.btnDataPemilik.Click += new System.EventHandler(this.btnPemilikHewan_Click);
            // 
            // btnKeluar
            // 
            this.btnKeluar.Location = new System.Drawing.Point(663, 397);
            this.btnKeluar.Name = "btnKeluar";
            this.btnKeluar.Size = new System.Drawing.Size(75, 23);
            this.btnKeluar.TabIndex = 9;
            this.btnKeluar.Text = "Keluar";
            this.btnKeluar.UseVisualStyleBackColor = true;
            this.btnKeluar.Click += new System.EventHandler(this.btnKeluar_Click);
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKeluar);
            this.Controls.Add(this.btnDataPemilik);
            this.Controls.Add(this.btnDataTransaksi);
            this.Controls.Add(this.btnDataKamar);
            this.Controls.Add(this.btnDataHewan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "menu";
            this.Text = "menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDataHewan;
        private System.Windows.Forms.Button btnDataKamar;
        private System.Windows.Forms.Button btnDataTransaksi;
        private System.Windows.Forms.Button btnDataPemilik;
        private System.Windows.Forms.Button btnKeluar;
    }
}