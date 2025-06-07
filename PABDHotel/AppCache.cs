using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Caching;
using System.Windows.Forms;

namespace PABDHotel
{
    public static class AppCache
    {
        // Mendapatkan instance cache default dari .NET
        private static readonly ObjectCache cache = MemoryCache.Default;

        // Sesuaikan connection string Anda
        private static string connectionString = "Data Source=LAPTOP-0LTDAB53\\MSIDIQ;Initial Catalog=HotelHewanPeliharaanKuan;Integrated Security=True";

        // Kebijakan kadaluwarsa cache (bisa digunakan bersama)
        private static CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5.0) };

        // --- Cache untuk Pemilik Hewan ---
        public static DataTable GetPemilikHewan()
        {
            string cacheKey = "PemilikHewanCache";
            if (cache.Contains(cacheKey))
            {
                return (DataTable)cache[cacheKey];
            }
            else
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSemuaPemilikHewan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
                cache.Set(cacheKey, dt, policy);
                return dt;
            }
        }

        public static void InvalidatePemilikCache()
        {
            cache.Remove("PemilikHewanCache");
        }

        // --- Cache untuk Hewan ---
        public static DataTable GetHewan()
        {
            string cacheKey = "HewanCache";
            if (cache.Contains(cacheKey))
            {
                return (DataTable)cache[cacheKey];
            }
            else
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSemuaHewanDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
                cache.Set(cacheKey, dt, policy);
                return dt;
            }
        }

        public static void InvalidateHewanCache()
        {
            cache.Remove("HewanCache");
        }

        // --- Cache untuk Kamar ---
        public static DataTable GetKamar()
        {
            string cacheKey = "KamarCache";
            if (cache.Contains(cacheKey))
            {
                return (DataTable)cache[cacheKey];
            }
            else
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSemuaKamar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
                cache.Set(cacheKey, dt, policy);
                return dt;
            }
        }

        public static void InvalidateKamarCache()
        {
            cache.Remove("KamarCache");
        }
    }
}
