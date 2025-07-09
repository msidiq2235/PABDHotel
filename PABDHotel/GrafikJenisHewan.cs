using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq; // Pastikan ini ada untuk .AsEnumerable() dan .FirstOrDefault()
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Pastikan ini ada

namespace PABDHotel // Sesuaikan dengan namespace proyek Anda
{
    public partial class GrafikJenisHewan : Form // Nama form Anda
    {
        private readonly Koneksi kn = new Koneksi();
        public GrafikJenisHewan()
        {
            InitializeComponent();
        }

        

        private void cmbJenisHewan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbJenisHewan.SelectedItem.ToString();
            LoadChartData(selected);
        }

        private void LoadChartData(string filter)
        {
            // Bersihkan chart yang sudah ada sebelum mengisi yang baru
            chartJenisHewan.Series.Clear();
            chartJenisHewan.Titles.Clear();
            chartJenisHewan.Legends.Clear();
            chartJenisHewan.ChartAreas.Clear();

            // Tambahkan ChartArea dan atur propertinya
            ChartArea ca = new ChartArea("JenisHewanArea");
            ca.AxisX.Title = "Jenis Hewan";
            ca.AxisY.Title = "Jumlah Transaksi";
            ca.AxisX.LabelStyle.Angle = -45; // Memiringkan label X-axis jika terlalu panjang
            ca.BackColor = System.Drawing.Color.AliceBlue; // Contoh warna latar belakang ChartArea
            chartJenisHewan.ChartAreas.Add(ca);

            // Connection string di-hardcode langsung di sini
            string connectionString = kn.connectionString();
            string query = @"
            SELECT
                H.Jenis,
                COUNT(T.TransaksiID) AS Jumlah
            FROM Transaksi T
            JOIN Hewan H ON T.HewanID = H.HewanID
            GROUP BY H.Jenis;
            ";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            if (filter == "Anjing")
            {
                Series sAnjing = new Series("Anjing")
                {
                    ChartType = SeriesChartType.Column, // Menggunakan Column Chart
                    Color = System.Drawing.Color.DodgerBlue, // Warna biru untuk Anjing
                    IsValueShownAsLabel = true // Tampilkan nilai di atas kolom
                };

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Jenis"].ToString() == "Anjing")
                    {
                        // Ambil dari kolom "Jumlah", bukan "Pemasukan"
                        int jumlah = Convert.ToInt32(row["Jumlah"]);
                        // Sumbu X adalah status itu sendiri, bukan "NamaOrganisasi"
                        sAnjing.Points.AddXY("Anjing", jumlah);
                    }
                }
                chartJenisHewan.Series.Add(sAnjing);

            }

            if (filter == "Kucing")
            {
                Series sKucing = new Series("Kucing")
                {
                    ChartType = SeriesChartType.Column, // Menggunakan Column Chart
                    Color = System.Drawing.Color.DarkOrange, // Warna oranye untuk Kucing
                    IsValueShownAsLabel = true
                };

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Jenis"].ToString() == "Kucing")
                    {
                        // Ambil dari kolom "Jumlah", bukan "Pemasukan"
                        int jumlah = Convert.ToInt32(row["Jumlah"]);
                        // Sumbu X adalah status itu sendiri, bukan "NamaOrganisasi"
                        sKucing.Points.AddXY("Kucing", jumlah);
                    }
                }
                chartJenisHewan.Series.Add(sKucing);
            }

            chartJenisHewan.Titles.Add("Grafik Jenis Hewan");
            chartJenisHewan.Legends.Add(new Legend("Legenda"));
        }

        private void GrafikJenisHewan_Load(object sender, EventArgs e)
        {
            cmbJenisHewan.Items.AddRange(new string[] { "Anjing", "Kucing" });
            cmbJenisHewan.SelectedIndex = 0;

            LoadChartData("Anjing");

            cmbJenisHewan.SelectedIndexChanged += cmbJenisHewan_SelectedIndexChanged;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}