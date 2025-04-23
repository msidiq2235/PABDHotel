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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTransaksi();
            dgvTransaksi.CellClick += dgvTransaksi_CellClick;
        }

        private void ClearForm()
        {
            txtNamaPemilik.Clear();
            txtNamaHewan.Clear();
            cmbJenisHewan.SelectedIndex = -1;
            cmbTipeKamar.SelectedIndex = -1;
            dtpCheckIn.Value = DateTime.Now;
            dtpCheckOut.Value = DateTime.Now;
            txtFasilitas.Clear();
            txtHargaFasilitas.Clear();
            txtTotalBiaya.Text = "";

            txtNamaPemilik.Focus();
        }

        private void LoadTransaksi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT t.TransaksiID, ph.Nama AS NamaPemilik, h.NamaHewan, h.Jenis,
                               k.TipeKamar, t.TanggalCheckIn, t.TanggalCheckOut,
                               t.NamaFasilitas, t.HargaFasilitas, t.TotalBiaya
                        FROM Transaksi t
                        JOIN PemilikHewan ph ON t.PemilikID = ph.PemilikID
                        JOIN Hewan h ON t.HewanID = h.HewanID
                        JOIN Kamar k ON t.KamarID = k.KamarID";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTransaksi.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Insert PemilikHewan jika belum ada
                    string namaPemilik = txtNamaPemilik.Text.Trim();
                    int pemilikId = GetOrCreatePemilik(conn, namaPemilik);

                    // 2. Insert Hewan
                    string namaHewan = txtNamaHewan.Text.Trim();
                    string jenis = cmbJenisHewan.Text;
                    int hewanId = InsertHewan(conn, namaHewan, jenis, pemilikId);

                    // 3. Ambil ID kamar dari tipe kamar
                    string tipeKamar = cmbTipeKamar.Text;
                    int kamarId = GetKamarId(conn, tipeKamar);

                    // 4. Hitung total biaya
                    DateTime checkIn = dtpCheckIn.Value;
                    DateTime checkOut = dtpCheckOut.Value;
                    decimal hargaFasilitas = string.IsNullOrEmpty(txtHargaFasilitas.Text) ? 0 : Convert.ToDecimal(txtHargaFasilitas.Text);
                    decimal hargaKamar = GetHargaKamar(conn, kamarId);
                    int totalHari = (checkOut - checkIn).Days;
                    decimal totalBiaya = (hargaKamar * totalHari) + hargaFasilitas;

                    // 5. Insert ke Transaksi
                    string query = @"
                        INSERT INTO Transaksi (PemilikID, HewanID, KamarID, TanggalCheckIn, TanggalCheckOut, NamaFasilitas, HargaFasilitas, TotalBiaya)
                        VALUES (@PemilikID, @HewanID, @KamarID, @CheckIn, @CheckOut, @Fasilitas, @HargaFasilitas, @TotalBiaya)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PemilikID", pemilikId);
                        cmd.Parameters.AddWithValue("@HewanID", hewanId);
                        cmd.Parameters.AddWithValue("@KamarID", kamarId);
                        cmd.Parameters.AddWithValue("@CheckIn", checkIn);
                        cmd.Parameters.AddWithValue("@CheckOut", checkOut);
                        cmd.Parameters.AddWithValue("@Fasilitas", string.IsNullOrWhiteSpace(txtFasilitas.Text) ? DBNull.Value : (object)txtFasilitas.Text);
                        cmd.Parameters.AddWithValue("@HargaFasilitas", hargaFasilitas);
                        cmd.Parameters.AddWithValue("@TotalBiaya", totalBiaya);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Transaksi berhasil ditambahkan.");
                        LoadTransaksi();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count > 0 &&
                dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value != null)
            {
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);

                // Ambil nilai dari form
                string namaPemilik = txtNamaPemilik.Text.Trim();
                string namaHewan = txtNamaHewan.Text.Trim();
                object selectedJenisHewan = cmbJenisHewan.SelectedItem;
                object selectedTipeKamar = cmbTipeKamar.SelectedItem;
                DateTime checkIn = dtpCheckIn.Value;
                DateTime checkOut = dtpCheckOut.Value;
                string namaFasilitas = txtFasilitas.Text.Trim();
                decimal hargaFasilitas = string.IsNullOrEmpty(txtHargaFasilitas.Text) ? 0 : Convert.ToDecimal(txtHargaFasilitas.Text);

                // Validasi isian
                if (string.IsNullOrWhiteSpace(namaPemilik) ||
                    string.IsNullOrWhiteSpace(namaHewan) ||
                    selectedJenisHewan == null ||
                    selectedTipeKamar == null)
                {
                    MessageBox.Show("Harap isi semua data!");
                    return;
                }

                string jenisHewan = selectedJenisHewan.ToString();
                string tipeKamar = selectedTipeKamar.ToString();

                // Validasi tanggal
                if (checkIn.Date > checkOut.Date)
                {
                    MessageBox.Show("Tanggal check-in tidak boleh setelah tanggal check-out.");
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Cek/Insert pemilik
                        int pemilikId = GetOrCreatePemilik(conn, namaPemilik);

                        // Cek/Insert hewan
                        int hewanId = InsertHewan(conn, namaHewan, jenisHewan, pemilikId);

                        // Ambil ID kamar
                        int kamarId = GetKamarId(conn, tipeKamar);

                        // Hitung total biaya
                        decimal hargaKamar = GetHargaKamar(conn, kamarId);
                        int totalHari = (checkOut - checkIn).Days;
                        decimal totalBiaya = (hargaKamar * totalHari) + hargaFasilitas;

                        // Update transaksi
                        string query = @"
                    UPDATE Transaksi
                    SET PemilikID = @PemilikID,
                        HewanID = @HewanID,
                        KamarID = @KamarID,
                        TanggalCheckIn = @CheckIn,
                        TanggalCheckOut = @CheckOut,
                        NamaFasilitas = @Fasilitas,
                        HargaFasilitas = @HargaFasilitas,
                        TotalBiaya = @TotalBiaya
                    WHERE TransaksiID = @TransaksiID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@PemilikID", pemilikId);
                            cmd.Parameters.AddWithValue("@HewanID", hewanId);
                            cmd.Parameters.AddWithValue("@KamarID", kamarId);
                            cmd.Parameters.AddWithValue("@CheckIn", checkIn);
                            cmd.Parameters.AddWithValue("@CheckOut", checkOut);
                            cmd.Parameters.AddWithValue("@Fasilitas", string.IsNullOrWhiteSpace(namaFasilitas) ? DBNull.Value : (object)namaFasilitas);
                            cmd.Parameters.AddWithValue("@HargaFasilitas", hargaFasilitas);
                            cmd.Parameters.AddWithValue("@TotalBiaya", totalBiaya);
                            cmd.Parameters.AddWithValue("@TransaksiID", transaksiId);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Transaksi berhasil diubah.");
                    LoadTransaksi();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat ubah data: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pilih transaksi yang ingin diubah!");
            }
        }



        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadTransaksi();
        }


        private int GetOrCreatePemilik(SqlConnection conn, string nama)
        {
            // Validasi input: pastikan nama tidak kosong atau null
            if (string.IsNullOrWhiteSpace(nama))
            {
                throw new ArgumentException("Nama pemilik tidak boleh kosong!");
            }

            // Cek apakah pemilik dengan nama yang diberikan sudah ada
            string query = "SELECT PemilikID FROM PemilikHewan WHERE Nama = @Nama";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nama", nama);
            object result = cmd.ExecuteScalar();

            // Jika ada, kembalikan PemilikID yang sudah ada
            if (result != null)
                return (int)result;

            // Jika tidak ada, buat pemilik baru dengan data dummy
            string noHpDummy = GenerateUniqueNoHP(conn);  // Memastikan NoHP unik
            string emailDummy = "user_" + DateTime.Now.Ticks + "@dummy.com";  // Dummy email

            query = "INSERT INTO PemilikHewan (Nama, NoHP, Email) OUTPUT INSERTED.PemilikID VALUES (@Nama, @NoHP, @Email)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nama", nama);
            cmd.Parameters.AddWithValue("@NoHP", noHpDummy);
            cmd.Parameters.AddWithValue("@Email", emailDummy);

            // Masukkan data baru dan kembalikan PemilikID
            return (int)cmd.ExecuteScalar();
        }

        // Fungsi untuk menghasilkan NoHP yang unik
        private string GenerateUniqueNoHP(SqlConnection conn)
        {
            // Membuat NoHP dengan GUID
            string noHp = "000" + Guid.NewGuid().ToString("N").Substring(0, 7);  // Mengambil 7 karakter pertama dari GUID yang unik

            // Memastikan keunikannya
            string query = "SELECT COUNT(1) FROM PemilikHewan WHERE NoHP = @NoHP";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NoHP", noHp);
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                // Jika sudah ada, coba lagi dengan GUID baru
                return GenerateUniqueNoHP(conn);
            }

            return noHp;
        }

        private int InsertHewan(SqlConnection conn, string namaHewan, string jenis, int pemilikId)
        {
            string query = @"INSERT INTO Hewan (NamaHewan, Jenis, PemilikID) OUTPUT INSERTED.HewanID 
                             VALUES (@NamaHewan, @Jenis, @PemilikID)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NamaHewan", namaHewan);
            cmd.Parameters.AddWithValue("@Jenis", jenis);
            cmd.Parameters.AddWithValue("@PemilikID", pemilikId);
            return (int)cmd.ExecuteScalar();
        }

        private int GetKamarId(SqlConnection conn, string tipeKamar)
        {
            string query = "SELECT KamarID FROM Kamar WHERE TipeKamar = @TipeKamar";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TipeKamar", tipeKamar);
            return (int)cmd.ExecuteScalar();
        }

        private decimal GetHargaKamar(SqlConnection conn, int kamarId)
        {
            string query = "SELECT HargaPerHari FROM Kamar WHERE KamarID = @KamarID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@KamarID", kamarId);
            return (decimal)cmd.ExecuteScalar();
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (dgvTransaksi.SelectedRows.Count > 0)
            {
                // Ambil TransaksiID yang dipilih
                int transaksiId = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["TransaksiID"].Value);

                // Konfirmasi penghapusan
                DialogResult result = MessageBox.Show("Yakin ingin menghapus transaksi ini?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            // Query untuk menghapus transaksi berdasarkan TransaksiID
                            string query = "DELETE FROM Transaksi WHERE TransaksiID = @TransaksiID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@TransaksiID", transaksiId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Transaksi berhasil dihapus.");
                            LoadTransaksi();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang ingin dihapus.");
            }
        }

        private void dgvTransaksi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Pastikan baris yang diklik valid
            {
                DataGridViewRow row = dgvTransaksi.Rows[e.RowIndex];

                // Ambil TransaksiID dari baris yang dipilih
                int transaksiId = Convert.ToInt32(row.Cells["TransaksiID"].Value);

                // Ambil data dari database berdasarkan TransaksiID
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT t.TransaksiID, p.Nama, h.NamaHewan, h.Jenis,
                       k.TipeKamar, t.TanggalCheckIn, t.TanggalCheckOut,
                       t.NamaFasilitas, t.HargaFasilitas
                FROM Transaksi t
                JOIN PemilikHewan p ON t.PemilikID = p.PemilikID
                JOIN Hewan h ON t.HewanID = h.HewanID
                JOIN Kamar k ON t.KamarID = k.KamarID
                WHERE t.TransaksiID = @TransaksiID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransaksiID", transaksiId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNamaPemilik.Text = reader["Nama"].ToString();
                                txtNamaHewan.Text = reader["NamaHewan"].ToString();
                                cmbJenisHewan.SelectedItem = reader["Jenis"].ToString();
                                cmbTipeKamar.SelectedItem = reader["TipeKamar"].ToString();
                                dtpCheckIn.Value = Convert.ToDateTime(reader["TanggalCheckIn"]);
                                dtpCheckOut.Value = Convert.ToDateTime(reader["TanggalCheckOut"]);
                                txtFasilitas.Text = reader["NamaFasilitas"] == DBNull.Value ? "" : reader["NamaFasilitas"].ToString();
                                txtHargaFasilitas.Text = reader["HargaFasilitas"] == DBNull.Value ? "" : reader["HargaFasilitas"].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
