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
            LoadPemilik();
            LoadData();
        }

        private void LoadPemilik()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT PemilikID, Nama FROM PemilikHewan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbPemilik.DataSource = dt;
                cmbPemilik.DisplayMember = "Nama";
                cmbPemilik.ValueMember = "PemilikID";
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT H.HewanID, H.NamaHewan, H.Jenis, P.Nama AS Pemilik
                    FROM Hewan H
                    JOIN PemilikHewan P ON H.PemilikID = P.PemilikID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvHewan.DataSource = dt;
            }
        }

        private void ClearForm()
        {
            txtNamaHewan.Clear();
            cmbJenis.SelectedIndex = -1;
            cmbPemilik.SelectedIndex = -1;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaHewan.Text) || cmbJenis.SelectedIndex == -1 || cmbPemilik.SelectedIndex == -1)
            {
                MessageBox.Show("Harap isi semua data!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Hewan (NamaHewan, Jenis, PemilikID) VALUES (@NamaHewan, @Jenis, @PemilikID)", conn);
                cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text.Trim());
                cmd.Parameters.AddWithValue("@Jenis", cmbJenis.Text);
                cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);
                cmd.ExecuteNonQuery();
            }

            LoadData();
            ClearForm();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvHewan.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvHewan.SelectedRows[0].Cells["HewanID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Hewan SET NamaHewan = @NamaHewan, Jenis = @Jenis, PemilikID = @PemilikID WHERE HewanID = @HewanID", conn);
                    cmd.Parameters.AddWithValue("@NamaHewan", txtNamaHewan.Text.Trim());
                    cmd.Parameters.AddWithValue("@Jenis", cmbJenis.Text);
                    cmd.Parameters.AddWithValue("@PemilikID", cmbPemilik.SelectedValue);
                    cmd.Parameters.AddWithValue("@HewanID", id);
                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diubah!");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvHewan.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvHewan.SelectedRows[0].Cells["HewanID"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Hewan WHERE HewanID = @HewanID", conn);
                    cmd.Parameters.AddWithValue("@HewanID", id);
                    cmd.ExecuteNonQuery();
                }

                LoadData();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void dgvHewan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNamaHewan.Text = dgvHewan.Rows[e.RowIndex].Cells["NamaHewan"].Value.ToString();
                cmbJenis.Text = dgvHewan.Rows[e.RowIndex].Cells["Jenis"].Value.ToString();
                cmbPemilik.Text = dgvHewan.Rows[e.RowIndex].Cells["Pemilik"].Value.ToString();
            }
        }
    }
}
