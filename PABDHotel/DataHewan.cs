using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class DataHewan : Form
    {
        string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public DataHewan()
        {
            InitializeComponent();
        }

        private void DataHewan_Load(object sender, EventArgs e)
        {
            LoadPemilik();
            LoadData();
        }

        private void LoadPemilik()
        {
            try
            {
                // Menggunakan AppCache untuk efisiensi
                DataTable dt = AppCache.GetPemilikHewan();

                // Dekripsi data sebelum ditampilkan di ComboBox
                DataTable dtDecrypted = dt.Clone();
                dtDecrypted.Columns["NoHP"].DataType = typeof(string);
                dtDecrypted.Columns["Email"].DataType = typeof(string);

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = dtDecrypted.NewRow();
                    newRow["PemilikID"] = row["PemilikID"];
                    newRow["Nama"] = row["Nama"];
                    newRow["NoHP"] = EncryptionHelper.Decrypt(row["NoHP"].ToString());
                    newRow["Email"] = EncryptionHelper.Decrypt(row["Email"].ToString());
                    dtDecrypted.Rows.Add(newRow);
                }

                cmbPemilik.DataSource = dtDecrypted;
                cmbPemilik.DisplayMember = "Nama";
                cmbPemilik.ValueMember = "PemilikID";
                cmbPemilik.SelectedIndex = -1; // Kosongkan pilihan awal
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data pemilik: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                // Menggunakan AppCache untuk data hewan
                dgvHewan.DataSource = AppCache.GetHewan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data hewan: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtNamaHewan.Clear();
            cmbJenis.SelectedIndex = -1;
            cmbPemilik.SelectedIndex = -1;
            dgvHewan.ClearSelection();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // VALIDASI
            if (string.IsNullOrWhiteSpace(txtNamaHewan.Text))
            {
                MessageBox.Show("Nama hewan wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaHewan.Focus();
                return;
            }
            if (cmbJenis.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih jenis hewan.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPemilik.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih pemilik hewan.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ===================================

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Jenis", cmbJenis.Text);
                        cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data hewan berhasil ditambah.");
                    }
                }
                AppCache.InvalidateHewanCache(); // Bersihkan cache setelah menambah
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvHewan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diubah!");
                return;
            }

            // VALIDASI
            if (string.IsNullOrWhiteSpace(txtNamaHewan.Text))
            {
                MessageBox.Show("Nama hewan wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaHewan.Focus();
                return;
            }
            if (cmbJenis.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih jenis hewan.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbPemilik.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih pemilik hewan.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ===================================

            try
            {
                int id = Convert.ToInt32(dgvHewan.SelectedRows[0].Cells["HewanID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HewanID", id);
                        cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Jenis", cmbJenis.Text);
                        cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data hewan berhasil diubah.");
                    }
                }
                AppCache.InvalidateHewanCache(); // Bersihkan cache setelah mengubah
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvHewan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus data hewan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                int id = Convert.ToInt32(dgvHewan.SelectedRows[0].Cells["HewanID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HewanID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data hewan berhasil dihapus.");
                    }
                }
                AppCache.InvalidateHewanCache(); // Bersihkan cache setelah menghapus
                LoadData();
                ClearForm();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show("Gagal menghapus. Hewan ini masih memiliki riwayat transaksi.", "Operasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHewan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNamaHewan.Text = dgvHewan.Rows[e.RowIndex].Cells["NamaHewan"].Value.ToString();
                cmbJenis.Text = dgvHewan.Rows[e.RowIndex].Cells["Jenis"].Value.ToString();
                cmbPemilik.SelectedValue = dgvHewan.Rows[e.RowIndex].Cells["PemilikID"].Value;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Memaksa ambil data baru dari DB untuk semua
            AppCache.InvalidatePemilikCache();
            AppCache.InvalidateHewanCache();
            LoadPemilik();
            LoadData();
            ClearForm();
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
            string queryToAnalyze = "EXEC GetSemuaHewanDetail;";

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
            FormLaporanHewan frm = new FormLaporanHewan();
            frm.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Pilih File Excel untuk Import Hewan";
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

                        // Ambil daftar pemilik untuk lookup ID
                        DataTable dtPemilik = AppCache.GetPemilikHewan();

                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string namaHewan = row["NamaHewan"].ToString();
                                string jenis = row["Jenis"].ToString();
                                string namaPemilik = row["NamaPemilik"].ToString();

                                if (string.IsNullOrWhiteSpace(namaHewan) || string.IsNullOrWhiteSpace(jenis) || string.IsNullOrWhiteSpace(namaPemilik))
                                {
                                    failCount++; continue;
                                }

                                // Cari PemilikID berdasarkan NamaPemilik
                                DataRow[] pemilikRows = dtPemilik.Select($"Nama = '{namaPemilik.Replace("'", "''")}'");
                                if (pemilikRows.Length == 0)
                                {
                                    failCount++;
                                    errors.AppendLine($"- Baris '{namaHewan}': Pemilik '{namaPemilik}' tidak ditemukan.");
                                    continue;
                                }
                                int pemilikId = Convert.ToInt32(pemilikRows[0]["PemilikID"]);

                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("AddHewan", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@NamaHewan", namaHewan);
                                        cmd.Parameters.AddWithValue("@Jenis", jenis);
                                        cmd.Parameters.AddWithValue("@PemilikID", pemilikId);
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                failCount++;
                                errors.AppendLine($"- Baris '{row["NamaHewan"]}': Error - {ex.Message}");
                            }
                        }
                    }
                }
                string summary = $"Proses import selesai.\n\nBerhasil: {successCount} data.\nGagal: {failCount} data.";
                if (failCount > 0)
                {
                    summary += "\n\nDetail Kegagalan:\n" + errors.ToString();
                }
                MessageBox.Show(summary, "Laporan Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppCache.InvalidateHewanCache();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca file Excel.\nError: " + ex.Message, "Error Import");
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
