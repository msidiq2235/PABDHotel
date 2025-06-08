using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class DataKamar : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public DataKamar()
        {
            InitializeComponent();
        }

        private void DataKamar_Load(object sender, EventArgs e)
        {
            LoadKamarData();
        }

        private void LoadKamarData()
        {
            try
            {
                // Mengambil data dari cache untuk performa lebih baik
                dgvKamar.DataSource = AppCache.GetKamar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data kamar: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            cmbTipeKamar.SelectedIndex = -1;
            txtHargaPerHari.Clear();
            dgvKamar.ClearSelection();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (cmbTipeKamar.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih tipe kamar.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHargaPerHari.Text) || !decimal.TryParse(txtHargaPerHari.Text, out decimal harga))
            {
                MessageBox.Show("Harga per hari harus diisi dengan angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Menggunakan Stored Procedure AddKamar
                    using (SqlCommand cmd = new SqlCommand("AddKamar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TipeKamar", cmbTipeKamar.Text);
                        cmd.Parameters.AddWithValue("@HargaPerHari", harga);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data kamar berhasil ditambahkan.");
                    }
                }
                AppCache.InvalidateKamarCache(); // Hapus cache setelah perubahan
                LoadKamarData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error menambah data: " + ex.Message);
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvKamar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data kamar yang ingin diubah.");
                return;
            }

            // Validasi input
            if (cmbTipeKamar.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih tipe kamar.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHargaPerHari.Text) || !decimal.TryParse(txtHargaPerHari.Text, out decimal harga))
            {
                MessageBox.Show("Harga per hari harus diisi dengan angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int kamarId = Convert.ToInt32(dgvKamar.SelectedRows[0].Cells["KamarID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Menggunakan Stored Procedure UpdateKamar
                    using (SqlCommand cmd = new SqlCommand("UpdateKamar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KamarID", kamarId);
                        cmd.Parameters.AddWithValue("@TipeKamar", cmbTipeKamar.Text);
                        cmd.Parameters.AddWithValue("@HargaPerHari", harga);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data kamar berhasil diubah.");
                    }
                }
                AppCache.InvalidateKamarCache(); // Hapus cache setelah perubahan
                LoadKamarData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error mengubah data: " + ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvKamar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data kamar yang ingin dihapus.");
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus kamar ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                int kamarId = Convert.ToInt32(dgvKamar.SelectedRows[0].Cells["KamarID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Menggunakan Stored Procedure DeleteKamar
                    using (SqlCommand cmd = new SqlCommand("DeleteKamar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KamarID", kamarId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data kamar berhasil dihapus.");
                    }
                }
                AppCache.InvalidateKamarCache(); // Hapus cache setelah perubahan
                LoadKamarData();
                ClearForm();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Gagal menghapus. Kamar ini sedang digunakan dalam transaksi.", "Operasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error menghapus data: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AppCache.InvalidateKamarCache();
            LoadKamarData();
            ClearForm();
        }

        private void dgvKamar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKamar.Rows[e.RowIndex];
                cmbTipeKamar.SelectedItem = row.Cells["TipeKamar"].Value.ToString();
                txtHargaPerHari.Text = row.Cells["HargaPerHari"].Value.ToString();
            }
        }

        private System.Text.StringBuilder analysisResult;

        private void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            analysisResult.AppendLine(e.Message);
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            // Inisialisasi StringBuilder setiap kali tombol diklik
            analysisResult = new System.Text.StringBuilder();

            // Tentukan Stored Procedure yang ingin dianalisis
            string queryToAnalyze = "EXEC GetSemuaKamar;";

            MessageBox.Show("Memulai analisis query, mohon tunggu...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Daftarkan event handler SEBELUM membuka koneksi
                    conn.InfoMessage += OnInfoMessage;

                    conn.Open();

                    // Gabungkan semua perintah dalam satu string
                    string commandText = $@"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;

                    {queryToAnalyze}

                    SET STATISTICS IO OFF;
                    SET STATISTICS TIME OFF;";

                    using (SqlCommand cmd = new SqlCommand(commandText, conn))
                    {
                        // Gunakan ExecuteNonQuery karena kita tidak tertarik dengan hasil datanya,
                        // hanya pesan statistiknya.
                        cmd.ExecuteNonQuery();
                    }
                }

                // Tampilkan semua pesan statistik yang sudah terkumpul
                MessageBox.Show(analysisResult.ToString(), "Hasil Analisis Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal melakukan analisis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            FormLaporanKamar frm = new FormLaporanKamar();
            frm.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Pilih File Excel untuk Import Kamar";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportDataFromExcel(openFileDialog.FileName);
                }
            }
        }

        private void ImportDataFromExcel(string filePath)
        {
            int successCount = 0;
            int failCount = 0;
            var errors = new System.Text.StringBuilder();
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                        });
                        DataTable dt = result.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string tipeKamar = row["TipeKamar"].ToString();
                                if (!decimal.TryParse(row["HargaPerHari"].ToString(), out decimal hargaPerHari))
                                {
                                    failCount++;
                                    errors.AppendLine($"- Baris '{tipeKamar}': HargaPerHari tidak valid.");
                                    continue;
                                }

                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("AddKamar", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@TipeKamar", tipeKamar);
                                        cmd.Parameters.AddWithValue("@HargaPerHari", hargaPerHari);
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                failCount++;
                                errors.AppendLine($"- Baris '{row["TipeKamar"]}': Error - {ex.Message}");
                            }
                        }
                    }
                }
                string summary = $"Proses import selesai.\n\nBerhasil: {successCount} data.\nGagal: {failCount} data.";
                if (failCount > 0) summary += "\n\nDetail Kegagalan:\n" + errors.ToString();
                MessageBox.Show(summary, "Laporan Import");
                AppCache.InvalidateKamarCache();
                LoadKamarData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca file Excel.\nError: " + ex.Message, "Error Import");
            }
        }
    }
}
