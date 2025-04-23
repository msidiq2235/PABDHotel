using PABDHotel;
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
    }
}
