using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ToneGenerator.Models
{
    public class Connection : INotifyPropertyChanged
    {
        private string commPort;
        private int baudRate;
        private int dataBits;
        private string parity;
        private int stopBits;
        private string handshake;

        public Connection(string CommPort, int BaudRate, int DataBits, string Parity, int StopBits, string Handshake)
        {
            commPort = CommPort;
            baudRate = BaudRate;
            dataBits = DataBits;
            parity   = Parity;
            stopBits = StopBits;
            handshake = Handshake;
        }
        #region Getters and Setters
        public string CommPort
        {
            get
            {
                return commPort;
            }
            set
            {
                commPort = value;
            }
        }
        public int BaudRate
        {
            get
            {
                return baudRate;
            }
            set
            {
                baudRate = value;
            }
        }
        public int DataBits
        {
            get
            {
                return dataBits;
            }
            set
            {
                dataBits = value;
            }
        }
        public string Parity
        {
            get
            {
                return parity;
            }
            set
            {
                parity = value;
            }
        }
        public int StopBits
        {
            get
            {
                return stopBits;
            }
            set
            {
                stopBits = value;
            }
        }
        public string Handshake
        {
            get
            {
                return handshake;
            }
            set
            {
                handshake = value;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
