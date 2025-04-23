using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class DataPemilik : Form
    {
        private string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        public DataPemilik()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PemilikHewan", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvPemilik.DataSource = dt;
            }
        }

        private void ClearForm()
        {
            txtNama.Text = "";
            txtNoHP.Text = "";
            txtEmail.Text = "";
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtNoHP.Text))
            {
                MessageBox.Show("Nama dan No HP wajib diisi.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO PemilikHewan (Nama, NoHP, Email) VALUES (@Nama, @NoHP, @Email)", conn);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                cmd.ExecuteNonQuery();
            }

            LoadData();
            ClearForm();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvPemilik.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diubah!");
                return;
            }

            int id = Convert.ToInt32(dgvPemilik.SelectedRows[0].Cells["PemilikID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE PemilikHewan SET Nama = @Nama, NoHP = @NoHP, Email = @Email WHERE PemilikID = @id", conn);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@NoHP", txtNoHP.Text);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            LoadData();
            ClearForm();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPemilik.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
                return;
            }

            int id = Convert.ToInt32(dgvPemilik.SelectedRows[0].Cells["PemilikID"].Value);

            var confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM PemilikHewan WHERE PemilikID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearForm();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void dgvPemilik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNama.Text = dgvPemilik.Rows[e.RowIndex].Cells["Nama"].Value.ToString();
                txtNoHP.Text = dgvPemilik.Rows[e.RowIndex].Cells["NoHP"].Value.ToString();
                txtEmail.Text = dgvPemilik.Rows[e.RowIndex].Cells["Email"].Value?.ToString() ?? "";
            }
        }
    }
}
