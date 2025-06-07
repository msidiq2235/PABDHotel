using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

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
    }
}
