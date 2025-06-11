using System;
using System.Windows.Forms;

namespace PABDHotel
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void btnPemilikHewan_Click(object sender, EventArgs e)
        {
            DataPemilik pemilikForm = new DataPemilik();
            pemilikForm.ShowDialog();
        }

        private void btnHewan_Click(object sender, EventArgs e)
        {
            DataHewan hewanForm = new DataHewan();
            hewanForm.ShowDialog();
        }

        private void btnKamar_Click(object sender, EventArgs e)
        {
            DataKamar kamarForm = new DataKamar();
            kamarForm.ShowDialog();
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {
            DataTransaksi transaksiForm = new DataTransaksi();
            transaksiForm.ShowDialog();
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            var hasil = MessageBox.Show("Apakah Anda yakin ingin keluar dari aplikasi?",
                                        "Konfirmasi Keluar",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (hasil == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void menu_Load(object sender, EventArgs e)
        {
            DatabaseHelper.EnsureIndexes();

            lblStatus.Text = "Selamat datang. Silakan pilih menu yang tersedia.";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
