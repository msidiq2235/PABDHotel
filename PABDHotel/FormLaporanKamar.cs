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
    public partial class FormLaporanKamar : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";
        public FormLaporanKamar() { InitializeComponent(); }

        private void FormLaporanKamar_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Panggil SP untuk data kamar
                    using (SqlCommand cmd = new SqlCommand("GetSemuaKamar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

                ReportDataSource rds = new ReportDataSource("LaporanKamar", dt);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex) { MessageBox.Show("Gagal memuat laporan kamar: " + ex.Message); }
        }
    }
}