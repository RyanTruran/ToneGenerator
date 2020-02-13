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
    public class ConnectionViewModel
    {
        public Connection connection;
        public ConnectionViewModel(ConnectionViewModel ConnectionVM)
        {            
            UpdateCommand = new ConnectionUpdateCommand(this);
            StartCommand = new ConnectionStartCommand(this);
        }
        public ConnectionViewModel( string commPort, int baudRate, int dataBits, string parity, int stopBits, string handshake)
        {
            connection = new Connection(commPort, baudRate, dataBits, parity, stopBits, handshake);
            UpdateCommand = new ConnectionUpdateCommand(this);
            StartCommand = new ConnectionStartCommand(this);
        }


        public String CommPort
        {
            get
            {
                return connection.CommPort;
            }
        }
        public int BaudRate
        {
            get
            {
                return connection.BaudRate;
            }
            set
            {
                this.BaudRate = 121121;
            }
        }
        public String Parity
        {
            get
            {
                return connection.Parity;
            }
        }
        public String Handshake
        {
            get
            {
                return connection.Handshake;
            }
        }

        public int DataBits
        {
            get
            {
                return connection.DataBits;
            }
        }

        public int StopBits
        {
            get
            {
                return connection.StopBits;
            }
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
        
        public ICommand UpdateCommand
        {
            get;
            private set;
        }

        public ICommand StartCommand
        {
            get;
            private set;
        }
    }
}
