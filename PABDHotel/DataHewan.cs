using System;
using System.Data;
using System.Data.SqlClient;
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
            // Pindahkan baris-baris ini
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

    }
}
