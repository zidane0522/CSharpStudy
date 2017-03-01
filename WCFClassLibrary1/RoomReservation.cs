using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFClassLibrary1
{
    [DataContract]
    public class RoomReservation:INotifyPropertyChanged
    {
        private int id;
        [DataMember]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string roomName;
        [DataMember]
        [StringLength(30)]
        public string RoomName
        {
            get { return roomName; }
            set { SetProperty(ref roomName, value); }
        }

        private string contract;
        [DataMember]
        [StringLength(30)]
        public string Contract
        {
            get { return contract; }
            set { SetProperty(ref contract, value); }
        }

        private string text;
        [DataMember]
        [StringLength(50)]
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        private DateTime startTime;
        [DataMember]
        public DateTime StartTime
        {
            get { return startTime; }
            set { SetProperty(ref startTime, value); }
        }

        private DateTime endTime;
        [DataMember]
        public DateTime EndTime
        {
            get { return endTime; }
            set { SetProperty(ref endTime, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler eventHandler = PropertyChanged;
            if (eventHandler!=null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item,value))
            {
                item = value;
                OnNotifyPropertyChanged(propertyName);
            }
        }
    }
}
