using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ToneGenerator.Models;
using System.Windows.Input;
using ToneGenerator.Commands;
using System.Threading;

namespace ToneGenerator.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        public int BaudRate
        {
            get { return _connection.BaudRate; }
            set { _connection.BaudRate = value; }
        }

        public string CommPort
        {
            get
            {
                return _connection.CommPort;
            }
            set
            {
                _connection.CommPort = value;
                OnPropertyChanged("CommPort");
            }
        }
        public string Parity => _connection.Parity;

        public string Handshake => _connection.Handshake;

        public int DataBits => _connection.DataBits;

        public int StopBits => _connection.StopBits;

        public ICommand UpdateCommand { get; }
        public ICommand initialize { get;  }
        public ICommand StartCommand { get; }
        private Connection _connection { get; }

        public ConnectionViewModel()
        {
            if(_connection == null)
            {
                _connection = new Connection("Comm1", 5, 5, "Joe", 1, "None");
            }
            UpdateCommand = new ConnectionUpdateCommand(this);
            StartCommand = new ConnectionStartCommand(this);
        }

        public ConnectionViewModel(Connection connection)
        {
            _connection = connection;
            UpdateCommand = new ConnectionUpdateCommand(this);
            StartCommand = new ConnectionStartCommand(this);
        }


        public void sendCommand()
        {
            int i = 0;
            while (true)
            {
                i++;
            }
        }

        public void receiveCommand()
        {
            int i = 0; 
            while(true) {
                i++;
            }
        }

        public void beginCommunication()
        {
            Thread receiveThread = new Thread(receiveCommand);
            receiveThread.Start();
            Thread sendThread = new Thread(sendCommand);
            sendThread.Start();
        }
        public void SaveChanges()
        {
           
        }
        public void createObject()
        {
            new ConnectionViewModel();
        }

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
