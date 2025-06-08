using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;

namespace PABDHotel
{
    public partial class DataPemilik : Form
    {
        
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public DataPemilik()
        {
            InitializeComponent();
        }

        private void DataPemilik_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // --- FUNGSI UTAMA ---

        private void LoadData()
        {
            DataTable dtEncrypted = new DataTable();
            try
            {
                // Mengambil data terenkripsi dari AppCache
                dtEncrypted = AppCache.GetPemilikHewan();

                // Buat tabel baru untuk menampung data yang sudah didekripsi
                DataTable dtDecrypted = dtEncrypted.Clone();
                dtDecrypted.Columns["Email"].DataType = typeof(string);
                dtDecrypted.Columns["NoHP"].DataType = typeof(string);

                // Lakukan perulangan untuk mendekripsi setiap baris
                foreach (DataRow row in dtEncrypted.Rows)
                {
                    DataRow newRow = dtDecrypted.NewRow();
                    newRow["PemilikID"] = row["PemilikID"];
                    newRow["Nama"] = row["Nama"];
                    newRow["NoHP"] = EncryptionHelper.Decrypt(row["NoHP"].ToString());
                    newRow["Email"] = EncryptionHelper.Decrypt(row["Email"].ToString());
                    dtDecrypted.Rows.Add(newRow);
                }

                // Tampilkan data yang sudah bisa dibaca ke DataGridView
                dgvPemilik.DataSource = dtDecrypted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtNama.Text = "";
            txtNoHP.Text = "";
            txtEmail.Text = "";
            dgvPemilik.ClearSelection();
        }

        // --- FUNGSI VALIDASI ---

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true;
            try { new System.Net.Mail.MailAddress(email); return true; }
            catch { return false; }
        }

        private bool IsNumeric(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            return text.All(char.IsDigit);
        }

        

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi
            if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtNoHP.Text)) { MessageBox.Show("Nama dan No HP wajib diisi."); return; }
            if (!IsNumeric(txtNoHP.Text)) { MessageBox.Show("Nomor HP hanya boleh berisi angka.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!IsValidEmail(txtEmail.Text)) { MessageBox.Show("Format email tidak valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddPemilikHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@NoHP", EncryptionHelper.Encrypt(txtNoHP.Text));
                        cmd.Parameters.AddWithValue("@Email", EncryptionHelper.Encrypt(txtEmail.Text));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan.");
                    }
                }
                AppCache.InvalidatePemilikCache(); // Hapus cache setelah perubahan
                LoadData();
                ClearForm();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) { MessageBox.Show("Error: Nomor HP atau Email sudah terdaftar.", "Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else { MessageBox.Show("Error Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvPemilik.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diubah!");
                return;
            }
            // Validasi
            if (!IsNumeric(txtNoHP.Text)) { MessageBox.Show("Nomor HP hanya boleh berisi angka.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!IsValidEmail(txtEmail.Text)) { MessageBox.Show("Format email tidak valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                int id = Convert.ToInt32(dgvPemilik.SelectedRows[0].Cells["PemilikID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdatePemilikHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PemilikID", id);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        // NoHP dan Email juga dienkripsi saat diubah
                        cmd.Parameters.AddWithValue("@NoHP", EncryptionHelper.Encrypt(txtNoHP.Text));
                        cmd.Parameters.AddWithValue("@Email", EncryptionHelper.Encrypt(txtEmail.Text));
                        // =======================
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diubah.");
                    }
                }
                AppCache.InvalidatePemilikCache(); // Hapus cache setelah perubahan
                LoadData();
                ClearForm();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPemilik.SelectedRows.Count == 0) { MessageBox.Show("Pilih data yang ingin dihapus!"); return; }

            var confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvPemilik.SelectedRows[0].Cells["PemilikID"].Value);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("DeletePemilikHewan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PemilikID", id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus.");
                        }
                    }
                    AppCache.InvalidatePemilikCache(); // Hapus cache setelah perubahan
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) { MessageBox.Show("Gagal menghapus. Pemilik ini masih memiliki riwayat transaksi.", "Operasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else { MessageBox.Show("Error Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AppCache.InvalidatePemilikCache();
            LoadData();
            ClearForm();
        }

        private void dgvPemilik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPemilik.Rows[e.RowIndex];
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtNoHP.Text = row.Cells["NoHP"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            }
        }

        private void txtNoHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        // Variabel untuk menampung hasil analisis dari SQL Server
        private System.Text.StringBuilder analysisResult;

        // Fungsi ini akan menangkap pesan statistik (IO, Time) dari SQL Server
        private void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // Tambahkan setiap pesan ke dalam variabel analysisResult
            analysisResult.AppendLine(e.Message);
        }

        // Event handler untuk tombol Analisis Query
        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            // Inisialisasi StringBuilder setiap kali tombol diklik
            analysisResult = new System.Text.StringBuilder();

            // Tentukan Stored Procedure yang ingin dianalisis
            string queryToAnalyze = "EXEC GetSemuaPemilikHewan;";

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
            FormLaporanPemilik frm = new FormLaporanPemilik();
            frm.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // Buka dialog untuk memilih file Excel
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Pilih File Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportDataFromExcel(filePath);
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
                // Encoding diperlukan untuk library ini
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Baca data dari Excel ke dalam DataSet
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Anggap baris pertama adalah header
                            }
                        });

                        DataTable dt = result.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string nama = row["Nama"].ToString();
                                string noHp = row["NoHP"].ToString();
                                string email = row["Email"].ToString();

                                // Validasi data dari Excel
                                if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(noHp))
                                {
                                    failCount++;
                                    errors.AppendLine($"- Baris untuk '{nama}' gagal: Nama dan NoHP wajib diisi.");
                                    continue;
                                }
                                if (!IsNumeric(noHp))
                                {
                                    failCount++;
                                    errors.AppendLine($"- Baris untuk '{nama}' gagal: NoHP harus angka.");
                                    continue;
                                }
                                if (!IsValidEmail(email))
                                {
                                    failCount++;
                                    errors.AppendLine($"- Baris untuk '{nama}' gagal: Format email tidak valid.");
                                    continue;
                                }

                                // Simpan ke database menggunakan Stored Procedure
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("AddPemilikHewan", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Nama", nama);
                                        cmd.Parameters.AddWithValue("@NoHP", EncryptionHelper.Encrypt(noHp));
                                        cmd.Parameters.AddWithValue("@Email", EncryptionHelper.Encrypt(email));
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                            }
                            catch (SqlException ex)
                            {
                                // Tangani jika ada data duplikat dari Excel
                                failCount++;
                                if (ex.Number == 2627 || ex.Number == 2601)
                                    errors.AppendLine($"- Baris untuk '{row["Nama"]}' gagal: NoHP atau Email sudah ada di database.");
                                else
                                    errors.AppendLine($"- Baris untuk '{row["Nama"]}' gagal: Error SQL - {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                failCount++;
                                errors.AppendLine($"- Baris untuk '{row["Nama"]}' gagal: Error - {ex.Message}");
                            }
                        }
                    }
                }

                // Tampilkan laporan hasil import
                string summary = $"Proses import selesai.\n\nBerhasil: {successCount} data.\nGagal: {failCount} data.";
                if (failCount > 0)
                {
                    summary += "\n\nDetail Kegagalan:\n" + errors.ToString();
                }
                MessageBox.Show(summary, "Laporan Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh data setelah import selesai
                AppCache.InvalidatePemilikCache();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tidak dapat membaca file Excel. Pastikan file tidak sedang dibuka dan formatnya benar.\nError: " + ex.Message, "Error Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
