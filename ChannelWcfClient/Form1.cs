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
using WCFClassLibrary1;

namespace ChannelWcfClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            roomReservation = new RoomReservation { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            roomReservation.Contract = this.ContactTxt.Text;
            roomReservation.RoomName = this.RoomTxt.Text;
            roomReservation.Text = this.EventTxt.Text;
            OnReserveRoomByChannel();
        }
        private RoomReservation roomReservation;

        private void OnReserveRoomByChannel()
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:9000/RoomReservation");
            var factory = new ChannelFactory<IRoomService>(binding, address);
            IRoomService channel = factory.CreateChannel();
            if (channel.ReserveRoom(roomReservation))
            {
                MessageBox.Show("Success");
            }
        }

        private void CallBackWcfBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
