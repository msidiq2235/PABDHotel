using System;
using System.Data.SqlClient;

namespace PABDHotel
{
    public static class DatabaseHelper
    {
        // Fungsi ini akan memeriksa dan membuat semua indeks yang diperlukan jika belum ada.
        public static void EnsureIndexes()
        {
            try
            {
                // 1. Buat instance dari kelas Koneksi
                Koneksi kn = new Koneksi();

                // 2. Gunakan method .connectionString() dari kelas Koneksi
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    conn.Open();

                    // Skrip SQL untuk membuat indeks (tidak ada perubahan di sini)
                    string indexScript = @"
                        -- Indeks untuk PemilikHewan
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_PemilikHewan_Nama' AND object_id = OBJECT_ID('dbo.PemilikHewan'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX idx_PemilikHewan_Nama ON dbo.PemilikHewan(Nama);
                        END;

                        -- Indeks untuk Hewan
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Hewan_PemilikID' AND object_id = OBJECT_ID('dbo.Hewan'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX idx_Hewan_PemilikID ON dbo.Hewan(PemilikID);
                        END;

                        -- Indeks untuk Kamar
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Kamar_TipeKamar' AND object_id = OBJECT_ID('dbo.Kamar'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX idx_Kamar_TipeKamar ON dbo.Kamar(TipeKamar);
                        END;

                        -- Indeks untuk Transaksi
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Transaksi_TanggalCheckIn' AND object_id = OBJECT_ID('dbo.Transaksi'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX idx_Transaksi_TanggalCheckIn ON dbo.Transaksi(TanggalCheckIn);
                        END;
                    ";

                    using (SqlCommand cmd = new SqlCommand(indexScript, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Penanganan error (tidak ada perubahan di sini)
                System.Windows.Forms.MessageBox.Show($"Error saat memastikan indeks database: {ex.Message}", "Database Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}