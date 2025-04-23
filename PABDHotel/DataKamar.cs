using System;
using System.Data;
using System.Data.SqlClient;
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

        // Load data kamar ke DataGridView
        private void LoadKamarData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT KamarID, TipeKamar, HargaPerHari FROM Kamar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKamar.DataSource = dt;
            }
        }

        // Tambah kamar
        private void btnTambah_Click(object sender, EventArgs e)
        {
            string tipeKamar = cmbTipeKamar.Text;
            decimal hargaPerHari;
            if (string.IsNullOrWhiteSpace(tipeKamar) || !decimal.TryParse(txtHargaPerHari.Text, out hargaPerHari))
            {
                MessageBox.Show("Harap isi semua data dengan benar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Kamar (TipeKamar, HargaPerHari) VALUES (@TipeKamar, @HargaPerHari)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TipeKamar", tipeKamar);
                cmd.Parameters.AddWithValue("@HargaPerHari", hargaPerHari);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadKamarData();
        }

        // Ubah kamar
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvKamar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data kamar yang ingin diubah.");
                return;
            }

            int kamarId = Convert.ToInt32(dgvKamar.SelectedRows[0].Cells["KamarID"].Value);
            string tipeKamar = cmbTipeKamar.Text;
            decimal hargaPerHari;
            if (string.IsNullOrWhiteSpace(tipeKamar) || !decimal.TryParse(txtHargaPerHari.Text, out hargaPerHari))
            {
                MessageBox.Show("Harap isi semua data dengan benar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Kamar SET TipeKamar = @TipeKamar, HargaPerHari = @HargaPerHari WHERE KamarID = @KamarID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TipeKamar", tipeKamar);
                cmd.Parameters.AddWithValue("@HargaPerHari", hargaPerHari);
                cmd.Parameters.AddWithValue("@KamarID", kamarId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadKamarData();
        }

        // Hapus kamar
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvKamar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data kamar yang ingin dihapus.");
                return;
            }

            int kamarId = Convert.ToInt32(dgvKamar.SelectedRows[0].Cells["KamarID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Kamar WHERE KamarID = @KamarID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@KamarID", kamarId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadKamarData();
        }

        // Refresh data kamar
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKamarData();
        }

        // Event handler untuk klik pada baris DataGridView
        private void dgvKamar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Ambil data dari baris yang dipilih
                DataGridViewRow row = dgvKamar.Rows[e.RowIndex];

                // Isi ComboBox dan TextBox dengan data dari baris yang dipilih
                cmbTipeKamar.SelectedItem = row.Cells["TipeKamar"].Value.ToString();
                txtHargaPerHari.Text = row.Cells["HargaPerHari"].Value.ToString();
            }
        }

    }
}
