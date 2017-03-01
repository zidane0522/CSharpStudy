using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfClient;
using WcfClient.NewWcfService;

namespace WcfClient
{
    public partial class Form1 : Form
    {
        private RoomReservation reservation;
        public Form1()
        {
            InitializeComponent();
            reservation = new RoomReservation { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
           
        }

        private async void OnReserveRoom()
        {
            try
            {
                var client = new RoomServiceClient();
                bool reserved = await client.ReserveRoomAsync(reservation);
                client.Close();
                if (reserved)
                {
                    MessageBox.Show("reservation OK");
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reservation!=null)
            {
                reservation.Contract = this.ContactTxt.Text;
                reservation.RoomName = this.RoomTxt.Text;
                reservation.Text = this.EventTxt.Text;
            }
            OnReserveRoom();
        }

    }
}
