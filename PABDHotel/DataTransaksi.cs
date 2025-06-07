using System;
using System.Data;
using System.Data.SqlClient;
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
            // Memuat semua data saat form pertama kali dibuka
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

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi input
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

                        decimal hargaFasilitas = 0;
                        decimal.TryParse(txtHargaFasilitas.Text, out hargaFasilitas);
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
            // Validasi lainnya
            if (cmbPemilik.SelectedValue == null || cmbHewan.SelectedValue == null || cmbKamar.SelectedValue == null) { MessageBox.Show("Harap pilih Pemilik, Hewan, dan Tipe Kamar."); return; }
            if (dtpCheckIn.Value.Date >= dtpCheckOut.Value.Date) { MessageBox.Show("Tanggal Check-Out harus setelah Tanggal Check-In."); return; }

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

                        decimal hargaFasilitas = 0;
                        decimal.TryParse(txtHargaFasilitas.Text, out hargaFasilitas);
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
                    cmbPemilik.SelectedValue = row.Cells["PemilikID"].Value;
                    cmbHewan.SelectedValue = row.Cells["HewanID"].Value;
                    cmbKamar.SelectedValue = row.Cells["KamarID"].Value;
                    dtpCheckIn.Value = Convert.ToDateTime(row.Cells["TanggalCheckIn"].Value);
                    dtpCheckOut.Value = Convert.ToDateTime(row.Cells["TanggalCheckOut"].Value);
                    txtFasilitas.Text = row.Cells["NamaFasilitas"].Value?.ToString() ?? "";

                    // Harga fasilitas tidak bisa diambil langsung, jadi dikosongkan
                    txtHargaFasilitas.Text = "";
                }
                catch (Exception)
                {
                    // Error bisa terjadi jika user klik saat data sedang di-refresh.
                    // Abaikan saja agar aplikasi tidak crash.
                }
            }
        }
    }
}
