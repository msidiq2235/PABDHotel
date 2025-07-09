using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Caching;

namespace PABDHotel
{
    public static class AppCache
    {
        private static readonly ObjectCache cache = MemoryCache.Default;

        // Metode private untuk mengambil dan menyimpan data ke cache, agar tidak ada duplikasi kode.
        private static DataTable GetAndCacheData(string cacheKey, string storedProcedureName)
        {
            // 1. Periksa apakah data sudah ada di cache
            if (cache.Contains(cacheKey))
            {
                return (DataTable)cache[cacheKey];
            }
            else
            {
                // 2. Jika tidak ada, ambil dari database
                DataTable dt = new DataTable();

                // Menggunakan kelas Koneksi untuk mendapatkan connection string
                Koneksi kn = new Koneksi();
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }

                // 3. Buat kebijakan kedaluwarsa BARU setiap kali data disimpan
                var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5.0) };

                // 4. Simpan data ke cache dengan kebijakan baru
                cache.Set(cacheKey, dt, policy);

                return dt;
            }
        }

        // --- Metode Publik untuk Mengakses Cache ---

        public static DataTable GetPemilikHewan()
        {
            return GetAndCacheData("PemilikHewanCache", "GetSemuaPemilikHewan");
        }

        public static DataTable GetHewan()
        {
            return GetAndCacheData("HewanCache", "GetSemuaHewanDetail");
        }

        public static DataTable GetKamar()
        {
            return GetAndCacheData("KamarCache", "GetSemuaKamar");
        }

        // --- Metode Publik untuk Menghapus Cache ---

        public static void InvalidatePemilikCache()
        {
            cache.Remove("PemilikHewanCache");
        }

        public static void InvalidateHewanCache()
        {
            cache.Remove("HewanCache");
        }

        public static void InvalidateKamarCache()
        {
            cache.Remove("KamarCache");
        }
    }
}