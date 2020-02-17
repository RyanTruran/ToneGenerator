using System;
using System.ComponentModel;
using ToneGenerator.Models;
using System.Windows.Input;
using ToneGenerator.Commands;
using System.Threading;
using System.Runtime.CompilerServices;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;

namespace ToneGenerator.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Getters and Setters that are exposed to bindings on View.
        /// </summary>
        #region
        private Connection _connection { get; }

        public int BaudRate
        {
            get { return _connection.BaudRate; }
            set { _connection.BaudRate = value; }
        }
        public string CommPort
        {
            get { return _connection.CommPort; }
            set { _connection.CommPort = value; }
        }
        public List<string> AvailablePorts
        {
            get
            {
                List<string> availablePorts = new List<string>();
                foreach (string s in SerialPort.GetPortNames())
                {
                    availablePorts.Add(s);
                }
                return availablePorts;
            }
        }

        public List<string> AvailableParity
        {
            get
            {
                List<string> availableParity = new List<string>();
                foreach (string s in Enum.GetNames(typeof(Parity)))
                {
                    availableParity.Add(s.ToString());
                }
                return availableParity;
            }
        }
        public List<string> AvailableStopBits
        {
            get
            {
                List<string> availableStopBits = new List<string>();
                foreach (string s in Enum.GetNames(typeof(StopBits)))
                {
                    availableStopBits.Add(s.ToString());
                }
                return availableStopBits;
            }
        }
        public List<string> AvailableHandshaking
        {
            get
            {
                List<string> availableHandshaking = new List<string>();
                foreach (string s in Enum.GetNames(typeof(Handshake)))
                {
                    availableHandshaking.Add(s.ToString());
                }
                return availableHandshaking;
            }
        }
        public string Console
        {
            get {
                string ConsoleString = "";
                try
                {
                    foreach (string LogMessage in _connection.ConsoleLog)
                    {
                        ConsoleString += LogMessage;
                    }
                 
                }
                catch (System.InvalidOperationException)
                {
                }
                return ConsoleString;
            }
            set {
                _connection.ConsoleLog.AddFirst($"{value}\n");
                OnPropertyChanged("Console");
            }
        }
        public bool Connected
        {
            get { return _connection.Connected; }
            set {
                _connection.Connected = value;
                OnPropertyChanged("Connected");
                OnPropertyChanged("ConnectedString");
            }
        }
        public string ConnectedString
        {
            get { return _connection.Connected ? "Connected" : "Disconnected"; }           
        }

        public string Parity
        {
            get { return _connection.Parity; }
            set { _connection.Parity = value; }
        }

        public string Handshake
        {
            get { return _connection.Handshake; }
            set { _connection.Handshake = value; }
        }

        public int DataBits
        {
            get { return _connection.DataBits; }
            set { _connection.DataBits = value; }
        }

        public string StopBits
        {
            get { return _connection.StopBits; }
            set { _connection.StopBits = value; }
        }
        #endregion
        /// <summary>
        /// ICommands for Buttons on the View.
        /// </summary>
        #region
            public ICommand WindowClosing { get; }
            public ICommand ButtonPress{ get; }
            public ICommand StartCommand { get; }
        #endregion


        /// <summary>
        /// Constructor for View model, settings are not stored in a persistent manner since only one 
        /// connection is allowed per the spec, and the settings could change between use.
        /// </summary>
        /// <param name="connection"></param>
            public ConnectionViewModel(Connection connection)
        {
            _connection = connection;
            WindowClosing = new WindowClosingCommand(this);
            StartCommand = new ConnectionStartCommand(this);
            ButtonPress = new ButtonPressCommand(this);
        }
        /// <summary>
        /// SendNote prepares the the desired note for the sendThread
        /// </summary>
        /// <param name="Note"></param>
        #region
        internal void SendNote(string Note)
        {
            message = $"{Note}\r\n";
        }
        #endregion

        internal void CloseWindow()
        {
            sendThread.Join();
            receiveThread.Join();
            _serialPort.Close();
        }

        #region Serial Communication
        /// <summary>
        /// I Use three commands to communicate to the TI123GXL.
        /// beginCommunication() to establish a connection from the provided settings, and create 2 threads one for sending and one for receiving.
        /// 
        /// receiveCommand, as the name implies, is the receiving half of the communication.
        /// sendCommand, is for the sending half of the communication.
        /// </summary>
        static SerialPort _serialPort;
        string message;
        static Thread receiveThread;
        static Thread sendThread;
        public void beginCommunication()
        {
            _serialPort = new SerialPort(CommPort);
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits.ToString());
            _serialPort.BaudRate = BaudRate;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);
            _serialPort.DataBits = DataBits;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits.ToString());
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Handshake);
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            
            try
            {
                _serialPort.Open();
                Connected = _serialPort.IsOpen;
                receiveThread = new Thread(receiveCommand);
                receiveThread.Start();
                sendThread = new Thread(sendCommand);
                sendThread.Start();
            }
            catch (System.IO.IOException ex)
            {
                //Typically caused by either using wrong Com Port or the device not being connected.
                Console = $"{ex.Message}";
                Console = $"Verify settings in Connection->Settings are correct and that the device is connected! {i}";
                i++;
            }
        }
        int i = 0;
        //receive serial communication from device, and update Console message data on UI.
        public void receiveCommand()
        {
            while (true)
                try
                {
                    string receivedMessage = _serialPort.ReadLine();
                    //update Console messages with data.
                    Console = $"Received:{receivedMessage}";
                }
                catch (TimeoutException) { }
                catch (System.InvalidOperationException)
                    {
                        Connected = _serialPort.IsOpen;
                        receiveThread.Join();
                    }
                    catch (System.IO.IOException)
                    {
                        Console = "Lost Connection with device";
                    }
            
        }

        //send Command over serial communication to TIVA TI123GXL running ToneGenerator-Embedded.
        public void sendCommand()
        {
            while (true)
            {
                //Write Message to Board with structure 'Note\r\n'
                if (message != null)
                {
                    _serialPort.Write(message);
                }
                //default to a non note string to begin counter on embedded system.
                message = "Stop";
                //Sleep to avoid sending command when ToneGenerator-Embedded is not listening for a command.
                Thread.Sleep(200);
            }
        }
        #endregion
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([CallerMemberName]string propertyName = "")
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
    }
}
