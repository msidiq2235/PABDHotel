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
    // (using statements sama seperti sebelumnya)
    public partial class FormLaporanTransaksi : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";
        public FormLaporanTransaksi() { InitializeComponent(); }

        private void FormLaporanTransaksi_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Panggil SP untuk data transaksi
                    using (SqlCommand cmd = new SqlCommand("GetSemuaTransaksiDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

                ReportDataSource rds = new ReportDataSource("LaporanTransaksi", dt);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex) { MessageBox.Show("Gagal memuat laporan transaksi: " + ex.Message); }
        }
    }
}

