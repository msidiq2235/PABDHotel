using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class FormLaporanHewan : Form
    {
        private readonly Koneksi kn = new Koneksi();

        public FormLaporanHewan()
        {
            InitializeComponent();
        }

        private void FormLaporanHewan_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    // Panggil SP untuk data hewan
                    using (SqlCommand cmd = new SqlCommand("GetSemuaHewanDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

                // Perhatian: Nama DataSet harus cocok dengan yang ada di RDLC
                ReportDataSource rds = new ReportDataSource("LaporanHewan", dt);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan hewan: " + ex.Message);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}