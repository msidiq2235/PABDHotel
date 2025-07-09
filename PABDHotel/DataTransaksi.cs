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
        private readonly Koneksi kn = new Koneksi();
        private System.Text.StringBuilder analysisResult;

        public DataTransaksi()
        {
            InitializeComponent();
        }

        private void DataTransaksi_Load(object sender, EventArgs e)
        {
            // Panggil semua fungsi pemuatan data saat form pertama kali dibuka
            LoadComboBoxes();
            LoadTransaksi();
        }

        private void DataTransaksi_Activated(object sender, EventArgs e)
        {
            // Otomatis refresh ComboBox saat form kembali aktif
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            try
            {
                // 1. Memuat data Pemilik dari Cache dan mendekripsinya
                DataTable dtPemilikFromCache = AppCache.GetPemilikHewan();
                DataTable dtPemilikDisplay = dtPemilikFromCache.Clone();
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

                // 2. Memuat data Kamar dari Cache
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
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
            txtJenisHewan.Clear();
            dgvTransaksi.ClearSelection();
        }

        private void ClearComboBoxes()
        {
            cmbPemilik.SelectedIndex = -1;
            cmbHewan.DataSource = null; // Kosongkan pilihan hewan
            cmbKamar.SelectedIndex = -1;
        }

        private bool IsNameValid(string name)
        {
            if (string.IsNullOrEmpty(name)) return true;
            return name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbPemilik.SelectedValue == null || cmbHewan.SelectedValue == null || cmbKamar.SelectedValue == null) { MessageBox.Show("Harap pilih Pemilik, Hewan, dan Tipe Kamar.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date) { MessageBox.Show("Tanggal Check-Out harus setelah Tanggal Check-In.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!IsNameValid(txtFasilitas.Text)) { MessageBox.Show("Nama Fasilitas hanya boleh berisi huruf dan spasi.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!decimal.TryParse(txtHargaFasilitas.Text, out decimal hargaFasilitas) && !string.IsNullOrEmpty(txtHargaFasilitas.Text)) { MessageBox.Show("Harga Fasilitas harus berupa angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var confirmResult = MessageBox.Show("Apakah Anda yakin ingin menambah transaksi ini?", "Konfirmasi Tambah", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
            catch (Exception ex) { MessageBox.Show("Error saat menambah transaksi: " + ex.Message); }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count == 0) { MessageBox.Show("Pilih transaksi yang ingin diubah!"); return; }
            if (cmbPemilik.SelectedValue == null || cmbHewan.SelectedValue == null || cmbKamar.SelectedValue == null) { MessageBox.Show("Harap pilih Pemilik, Hewan, dan Tipe Kamar."); return; }
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date) { MessageBox.Show("Tanggal Check-Out harus setelah Tanggal Check-In."); return; }
            if (!IsNameValid(txtFasilitas.Text)) { MessageBox.Show("Nama Fasilitas hanya boleh berisi huruf dan spasi.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!decimal.TryParse(txtHargaFasilitas.Text, out decimal hargaFasilitas) && !string.IsNullOrEmpty(txtHargaFasilitas.Text)) { MessageBox.Show("Harga Fasilitas harus berupa angka yang valid.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var confirmResult = MessageBox.Show("Apakah Anda yakin ingin mengubah transaksi ini?", "Konfirmasi Ubah", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
            catch (Exception ex) { MessageBox.Show("Error saat mengubah transaksi: " + ex.Message); }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count == 0) { MessageBox.Show("Pilih transaksi yang ingin dihapus!"); return; }
            var confirm = MessageBox.Show("Yakin ingin menghapus transaksi ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;
            try
            {
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
            catch (Exception ex) { MessageBox.Show("Error saat menghapus transaksi: " + ex.Message); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AppCache.InvalidatePemilikCache();
            AppCache.InvalidateHewanCache();
            AppCache.InvalidateKamarCache();
            LoadComboBoxes();
            LoadTransaksi();
            ClearForm();

            MessageBox.Show("Data berhasil di-refresh.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvTransaksi.Rows[e.RowIndex];
                    cmbPemilik.SelectedValue = row.Cells["PemilikID"].Value;
                    cmbHewan.SelectedValue = row.Cells["HewanID"].Value;
                    cmbKamar.SelectedValue = row.Cells["KamarID"].Value;

                    if (dgvTransaksi.Columns.Contains("Jenis") && row.Cells["Jenis"].Value != null)
                    {
                        txtJenisHewan.Text = row.Cells["Jenis"].Value.ToString();
                    }

                    dtpCheckIn.Value = Convert.ToDateTime(row.Cells["TanggalCheckIn"].Value);
                    dtpCheckOut.Value = Convert.ToDateTime(row.Cells["TanggalCheckOut"].Value);
                    txtFasilitas.Text = row.Cells["NamaFasilitas"].Value?.ToString() ?? "";
                    txtHargaFasilitas.Text = row.Cells["HargaFasilitas"].Value?.ToString() ?? "";
                }
                catch (Exception) { /* Abaikan error jika user klik saat data refresh */ }
            }
        }

        private void cmbPemilik_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Jika tidak ada pemilik yang dipilih, kosongkan ComboBox hewan
            if (cmbPemilik.SelectedValue == null)
            {
                cmbHewan.DataSource = null;
                return;
            }

            try
            {
                // Ambil ID pemilik yang dipilih
                int selectedPemilikID = Convert.ToInt32(cmbPemilik.SelectedValue);

                // Ambil daftar LENGKAP hewan dari cache
                DataTable dtHewan = AppCache.GetHewan();

                // Buat DataView untuk memfilter daftar hewan tersebut
                DataView dvHewan = new DataView(dtHewan);
                // Terapkan filter: hanya tampilkan hewan dengan PemilikID yang cocok
                dvHewan.RowFilter = $"PemilikID = {selectedPemilikID}";

                // Atur ComboBox hewan untuk menggunakan data yang sudah difilter
                cmbHewan.DataSource = dvHewan;
                cmbHewan.DisplayMember = "NamaHewan";
                cmbHewan.ValueMember = "HewanID";

                // Kosongkan pilihan awal
                cmbHewan.SelectedIndex = -1;
                txtJenisHewan.Text = "";
            }
            catch (Exception)
            {
                // Jika terjadi error (misal saat form baru dimuat), kosongkan saja
                cmbHewan.DataSource = null;
            }
        }

        private void cmbHewan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHewan.SelectedItem is DataRowView drv)
            {
                txtJenisHewan.Text = drv["Jenis"].ToString();
            }
            else
            {
                txtJenisHewan.Text = "";
            }
        }

        private void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            if (analysisResult != null)
                analysisResult.AppendLine(e.Message);
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            analysisResult = new System.Text.StringBuilder();
            string queryToAnalyze = "EXEC GetSemuaTransaksiDetail;";
            MessageBox.Show("Memulai analisis query...", "Info");
            try
            {
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    conn.InfoMessage += OnInfoMessage;
                    conn.Open();
                    string commandText = $@"SET STATISTICS IO ON; SET STATISTICS TIME ON; {queryToAnalyze} SET STATISTICS IO OFF; SET STATISTICS TIME OFF;";
                    using (SqlCommand cmd = new SqlCommand(commandText, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show(analysisResult.ToString(), "Hasil Analisis Query");
            }
            catch (Exception ex) { MessageBox.Show("Gagal melakukan analisis: " + ex.Message, "Error"); }
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

                        DataTable dtPemilik = AppCache.GetPemilikHewan();
                        DataTable dtHewan = AppCache.GetHewan();
                        DataTable dtKamar = AppCache.GetKamar();

                        foreach (DataRow row in dtExcel.Rows)
                        {
                            try
                            {
                                string namaPemilik = row["NamaPemilik"].ToString();
                                string namaHewan = row["NamaHewan"].ToString();
                                string tipeKamar = row["TipeKamar"].ToString();
                                DateTime tglCheckIn = Convert.ToDateTime(row["TanggalCheckIn"]);
                                DateTime tglCheckOut = Convert.ToDateTime(row["TanggalCheckOut"]);
                                string namaFasilitas = row["NamaFasilitas"].ToString();
                                decimal.TryParse(row["HargaFasilitas"].ToString(), out decimal hargaFasilitas);

                                DataRow[] pemilikRows = dtPemilik.Select($"Nama = '{namaPemilik.Replace("'", "''")}'");
                                if (pemilikRows.Length == 0) { throw new Exception($"Pemilik '{namaPemilik}' tidak ditemukan."); }
                                int pemilikId = Convert.ToInt32(pemilikRows[0]["PemilikID"]);

                                DataRow[] hewanRows = dtHewan.Select($"NamaHewan = '{namaHewan.Replace("'", "''")}' AND PemilikID = {pemilikId}");
                                if (hewanRows.Length == 0) { throw new Exception($"Hewan '{namaHewan}' milik '{namaPemilik}' tidak ditemukan."); }
                                int hewanId = Convert.ToInt32(hewanRows[0]["HewanID"]);

                                DataRow[] kamarRows = dtKamar.Select($"TipeKamar = '{tipeKamar.Replace("'", "''")}'");
                                if (kamarRows.Length == 0) { throw new Exception($"Tipe Kamar '{tipeKamar}' tidak ditemukan."); }
                                int kamarId = Convert.ToInt32(kamarRows[0]["KamarID"]);

                                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
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
                LoadTransaksi();
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormLaporanTransaksi frm = new FormLaporanTransaksi();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GrafikJenisHewan gf = new GrafikJenisHewan();
            gf.ShowDialog();
            this.Close();
        }
    }
}
