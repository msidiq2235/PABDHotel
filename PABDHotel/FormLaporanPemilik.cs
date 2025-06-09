using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class FormLaporanPemilik : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public FormLaporanPemilik()
        {
            InitializeComponent();
        }

        private void FormLaporanPemilik_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Ambil data dari database (hasilnya masih terenkripsi)
                DataTable dtEncrypted = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSemuaPemilikHewan", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dtEncrypted);
                    }
                }

                // 2. Buat tabel baru untuk menampung data yang sudah didekripsi
                DataTable dtDecrypted = dtEncrypted.Clone();
                // Pastikan tipe data kolom adalah string untuk menampung hasil dekripsi
                dtDecrypted.Columns["NoHP"].DataType = typeof(string);
                dtDecrypted.Columns["Email"].DataType = typeof(string);

                // 3. Lakukan perulangan untuk mendekripsi setiap baris
                foreach (DataRow row in dtEncrypted.Rows)
                {
                    DataRow newRow = dtDecrypted.NewRow();

                    newRow["PemilikID"] = row["PemilikID"];
                    newRow["Nama"] = row["Nama"];

                    // == PERUBAHAN DI SINI ==
                    // Sekarang NoHP dan Email keduanya didekripsi
                    newRow["NoHP"] = EncryptionHelper.Decrypt(row["NoHP"].ToString());
                    newRow["Email"] = EncryptionHelper.Decrypt(row["Email"].ToString());

                    dtDecrypted.Rows.Add(newRow);
                }

                // 4. Kirim data yang SUDAH BISA DI BACA ke ReportDataSource
                ReportDataSource rds = new ReportDataSource("LaporanDataSet", dtDecrypted);

                // 5. Tampilkan laporan
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
