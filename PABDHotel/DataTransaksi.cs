using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class DataTransaksi : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public DataTransaksi()
        {
            InitializeComponent();
        }

        private void DataTransaksi_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadTransaksi();
        }

        private void LoadComboBoxes()
        {
            try
            {
                // 1. Memuat data Pemilik dari Cache dan mendekripsinya
                DataTable dtPemilikFromCache = AppCache.GetPemilikHewan();
                DataTable dtPemilikDisplay = dtPemilikFromCache.Clone();
                // Pastikan kolom siap menampung string hasil dekripsi
                dtPemilikDisplay.Columns["NoHP"].DataType = typeof(string);
                dtPemilikDisplay.Columns["Email"].DataType = typeof(string);

                foreach (DataRow row in dtPemilikFromCache.Rows)
                {
                    DataRow newRow = dtPemilikDisplay.NewRow();
                    newRow["PemilikID"] = row["PemilikID"];
                    newRow["Nama"] = row["Nama"];
                    newRow["NoHP"] = EncryptionHelper.Decrypt(row["NoHP"].ToString());
                    newRow["Email"] = EncryptionHelper.Decrypt(row["Email"].ToString());
                    dtPemilikDisplay.Rows.Add(newRow);
                }
                cmbPemilik.DataSource = dtPemilikDisplay;
                cmbPemilik.DisplayMember = "Nama";
                cmbPemilik.ValueMember = "PemilikID";

                // 2. Memuat data Hewan dari Cache
                cmbHewan.DataSource = AppCache.GetHewan();
                cmbHewan.DisplayMember = "NamaHewan";
                cmbHewan.ValueMember = "HewanID";

                // 3. Memuat data Kamar dari Cache
                cmbKamar.DataSource = AppCache.GetKamar();
                cmbKamar.DisplayMember = "TipeKamar";
                cmbKamar.ValueMember = "KamarID";

                ClearComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data pilihan: " + ex.Message);
            }
        }

        private void LoadTransaksi()
        {
            try
            {
                // Data transaksi selalu diambil langsung dari DB
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSemuaTransaksiDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
                dgvTransaksi.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat daftar transaksi: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            ClearComboBoxes();
            dtpCheckIn.Value = DateTime.Now;
            dtpCheckOut.Value = DateTime.Now.AddDays(1);
            txtFasilitas.Clear();
            txtHargaFasilitas.Clear();
            dgvTransaksi.ClearSelection();
        }

        private void ClearComboBoxes()
        {
            cmbPemilik.SelectedIndex = -1;
            cmbHewan.SelectedIndex = -1;
            cmbKamar.SelectedIndex = -1;
        }

        private bool IsNameValid(string name)
        {
            if (string.IsNullOrEmpty(name)) return true; // Boleh kosong
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private void cmbHewan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pastikan ada item yang dipilih dan bukan event saat dikosongkan
            if (cmbHewan.SelectedItem is DataRowView drv)
            {
                // Ambil nilai dari kolom "Jenis" dari item yang dipilih
                txtJenisHewan.Text = drv["Jenis"].ToString();
            }
            else
            {
                // Kosongkan jika tidak ada hewan yang dipilih
                txtJenisHewan.Text = "";
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbPemilik.SelectedValue == null || cmbHewan.SelectedValue == null || cmbKamar.SelectedValue == null)
            {
                MessageBox.Show("Harap pilih Pemilik, Hewan, dan Tipe Kamar.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date)
            {
                MessageBox.Show("Tanggal Check-Out harus setelah Tanggal Check-In.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsNameValid(txtFasilitas.Text))
            {
                MessageBox.Show("Nama Fasilitas hanya boleh berisi huruf dan spasi.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtHargaFasilitas.Text, out decimal hargaFasilitas) && !string.IsNullOrEmpty(txtHargaFasilitas.Text))
            {
                MessageBox.Show("Harga Fasilitas harus berupa angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddTransaksi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);
                        cmd.Parameters.AddWithValue("@HewanID", cmbHewan.SelectedValue);
                        cmd.Parameters.AddWithValue("@KamarID", cmbKamar.SelectedValue);
                        cmd.Parameters.AddWithValue("@TanggalCheckIn", dtpCheckIn.Value);
                        cmd.Parameters.AddWithValue("@TanggalCheckOut", dtpCheckOut.Value);
                        cmd.Parameters.AddWithValue("@NamaFasilitas", string.IsNullOrWhiteSpace(txtFasilitas.Text) ? (object)DBNull.Value : txtFasilitas.Text);
                        cmd.Parameters.AddWithValue("@HargaFasilitas", hargaFasilitas);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Transaksi berhasil ditambahkan.");
                    }
                }
                LoadTransaksi();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambah transaksi: " + ex.Message);
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count == 0) { MessageBox.Show("Pilih transaksi yang ingin diubah!"); return; }
            if (cmbPemilik.SelectedValue == null || cmbHewan.SelectedValue == null || cmbKamar.SelectedValue == null) { MessageBox.Show("Harap pilih Pemilik, Hewan, dan Tipe Kamar."); return; }
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date) { MessageBox.Show("Tanggal Check-Out harus setelah Tanggal Check-In."); return; }

            if (!IsNameValid(txtFasilitas.Text))
            {
                MessageBox.Show("Nama Fasilitas hanya boleh berisi huruf dan spasi.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtHargaFasilitas.Text, out decimal hargaFasilitas) && !string.IsNullOrEmpty(txtHargaFasilitas.Text))
            {
                MessageBox.Show("Harga Fasilitas harus berupa angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateTransaksi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TransaksiID", transaksiId);
                        cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);
                        cmd.Parameters.AddWithValue("@HewanID", cmbHewan.SelectedValue);
                        cmd.Parameters.AddWithValue("@KamarID", cmbKamar.SelectedValue);
                        cmd.Parameters.AddWithValue("@TanggalCheckIn", dtpCheckIn.Value);
                        cmd.Parameters.AddWithValue("@TanggalCheckOut", dtpCheckOut.Value);
                        cmd.Parameters.AddWithValue("@NamaFasilitas", string.IsNullOrWhiteSpace(txtFasilitas.Text) ? (object)DBNull.Value : txtFasilitas.Text);
                        cmd.Parameters.AddWithValue("@HargaFasilitas", hargaFasilitas);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Transaksi berhasil diubah.");
                    }
                }
                LoadTransaksi();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat mengubah transaksi: " + ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count == 0) { MessageBox.Show("Pilih transaksi yang ingin dihapus!"); return; }

            var confirm = MessageBox.Show("Yakin ingin menghapus transaksi ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteTransaksi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TransaksiID", transaksiId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Transaksi berhasil dihapus.");
                    }
                }
                LoadTransaksi();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menghapus transaksi: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Paksa ambil data baru dari DB untuk semua cache
            AppCache.InvalidatePemilikCache();
            AppCache.InvalidateHewanCache();
            AppCache.InvalidateKamarCache();

            LoadComboBoxes();
            LoadTransaksi();
            ClearForm();
        }

        private void dgvTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvTransaksi.Rows[e.RowIndex];

                    // Mengisi ComboBox
                    cmbPemilik.SelectedValue = row.Cells["PemilikID"].Value;
                    cmbHewan.SelectedValue = row.Cells["HewanID"].Value;
                    cmbKamar.SelectedValue = row.Cells["KamarID"].Value;

                    // Setelah cmbHewan diisi, jenis hewan akan otomatis terisi
                    // karena event SelectedIndexChanged terpicu.
                    // Namun, kita bisa pastikan lagi di sini.
                    if (dgvTransaksi.Columns.Contains("Jenis") && row.Cells["Jenis"].Value != null)
                    {
                        txtJenisHewan.Text = row.Cells["Jenis"].Value.ToString();
                    }

                    // Mengisi kontrol lain
                    dtpCheckIn.Value = Convert.ToDateTime(row.Cells["TanggalCheckIn"].Value);
                    dtpCheckOut.Value = Convert.ToDateTime(row.Cells["TanggalCheckOut"].Value);
                    txtFasilitas.Text = row.Cells["NamaFasilitas"].Value?.ToString() ?? "";
                    txtHargaFasilitas.Text = row.Cells["HargaFasilitas"].Value?.ToString() ?? "";
                }
                catch (Exception) { /* Abaikan error */ }
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

            // Tentukan Stored Procedure 
            string queryToAnalyze = "EXEC GetSemuaTransaksiDetail;";


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
            FormLaporanTransaksi frm = new FormLaporanTransaksi();
            frm.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Pilih File Excel untuk Import Transaksi";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportDataFromExcel(openFileDialog.FileName);
                }
            }
        }

        private void ImportDataFromExcel(string filePath)
        {
            // Peringatan: Import transaksi sangat kompleks.
            // File Excel HARUS memiliki kolom: NamaPemilik, NamaHewan, TipeKamar, TanggalCheckIn, TanggalCheckOut, NamaFasilitas, HargaFasilitas
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
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
                        DataTable dtExcel = result.Tables[0];

                        // Ambil data lookup dari cache
                        DataTable dtPemilik = AppCache.GetPemilikHewan();
                        DataTable dtHewan = AppCache.GetHewan();
                        DataTable dtKamar = AppCache.GetKamar();

                        foreach (DataRow row in dtExcel.Rows)
                        {
                            try
                            {
                                // Baca data dari Excel
                                string namaPemilik = row["NamaPemilik"].ToString();
                                string namaHewan = row["NamaHewan"].ToString();
                                string tipeKamar = row["TipeKamar"].ToString();
                                DateTime tglCheckIn = Convert.ToDateTime(row["TanggalCheckIn"]);
                                DateTime tglCheckOut = Convert.ToDateTime(row["TanggalCheckOut"]);
                                string namaFasilitas = row["NamaFasilitas"].ToString();
                                decimal.TryParse(row["HargaFasilitas"].ToString(), out decimal hargaFasilitas);

                                // --- Cari ID ---
                                DataRow[] pemilikRows = dtPemilik.Select($"Nama = '{namaPemilik.Replace("'", "''")}'");
                                if (pemilikRows.Length == 0) { throw new Exception($"Pemilik '{namaPemilik}' tidak ditemukan."); }
                                int pemilikId = Convert.ToInt32(pemilikRows[0]["PemilikID"]);

                                DataRow[] hewanRows = dtHewan.Select($"NamaHewan = '{namaHewan.Replace("'", "''")}' AND PemilikID = {pemilikId}");
                                if (hewanRows.Length == 0) { throw new Exception($"Hewan '{namaHewan}' milik '{namaPemilik}' tidak ditemukan."); }
                                int hewanId = Convert.ToInt32(hewanRows[0]["HewanID"]);

                                DataRow[] kamarRows = dtKamar.Select($"TipeKamar = '{tipeKamar.Replace("'", "''")}'");
                                if (kamarRows.Length == 0) { throw new Exception($"Tipe Kamar '{tipeKamar}' tidak ditemukan."); }
                                int kamarId = Convert.ToInt32(kamarRows[0]["KamarID"]);

                                // --- Simpan ke DB ---
                                using (SqlConnection conn = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("AddTransaksi", conn))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@PemilikID", pemilikId);
                                        cmd.Parameters.AddWithValue("@HewanID", hewanId);
                                        cmd.Parameters.AddWithValue("@KamarID", kamarId);
                                        cmd.Parameters.AddWithValue("@TanggalCheckIn", tglCheckIn);
                                        cmd.Parameters.AddWithValue("@TanggalCheckOut", tglCheckOut);
                                        cmd.Parameters.AddWithValue("@NamaFasilitas", namaFasilitas);
                                        cmd.Parameters.AddWithValue("@HargaFasilitas", hargaFasilitas);
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                failCount++;
                                errors.AppendLine($"- Baris data gagal diproses: Error - {ex.Message}");
                            }
                        }
                    }
                }
                string summary = $"Proses import selesai.\n\nBerhasil: {successCount} data.\nGagal: {failCount} data.";
                if (failCount > 0) summary += "\n\nDetail Kegagalan:\n" + errors.ToString();
                MessageBox.Show(summary, "Laporan Import");
                LoadTransaksi(); // Refresh tabel transaksi
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
