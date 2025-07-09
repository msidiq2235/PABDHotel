namespace PABDHotel
{
    partial class GrafikJenisHewan
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartJenisHewan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbJenisHewan = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartJenisHewan)).BeginInit();
            this.SuspendLayout();
            // 
            // chartJenisHewan
            // 
            chartArea4.Name = "ChartArea1";
            this.chartJenisHewan.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartJenisHewan.Legends.Add(legend4);
            this.chartJenisHewan.Location = new System.Drawing.Point(52, 116);
            this.chartJenisHewan.Name = "chartJenisHewan";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartJenisHewan.Series.Add(series4);
            this.chartJenisHewan.Size = new System.Drawing.Size(692, 300);
            this.chartJenisHewan.TabIndex = 0;
            this.chartJenisHewan.Text = "chartJenisHewan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(49, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cari Berdasarkan";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbJenisHewan
            // 
            this.cmbJenisHewan.FormattingEnabled = true;
            this.cmbJenisHewan.Location = new System.Drawing.Point(186, 81);
            this.cmbJenisHewan.Name = "cmbJenisHewan";
            this.cmbJenisHewan.Size = new System.Drawing.Size(541, 24);
            this.cmbJenisHewan.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(213, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 46);
            this.label2.TabIndex = 3;
            this.label2.Text = "GRAFIK JENIS HEWAN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // GrafikJenisHewan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PABDHotel.Properties.Resources.Form_background_image_2;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbJenisHewan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartJenisHewan);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "GrafikJenisHewan";
            this.Text = "GrafikJenisHewan";
            this.Load += new System.EventHandler(this.GrafikJenisHewan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartJenisHewan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartJenisHewan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbJenisHewan;
        private System.Windows.Forms.Label label2;
    }
}