using System;
using System.ComponentModel;
using ToneGenerator.Models;
using System.Windows.Input;
using ToneGenerator.Commands;
using System.Threading;
using System.Runtime.CompilerServices;
using System.IO.Ports;
using System.Collections;

namespace ToneGenerator.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Getters and Setters that are exposed to bindings on View.
        /// </summary>
        #region
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
        public string Console
        {
            get {
                string ConsoleString = "";
                foreach (string LogMessage in _connection.ConsoleLog)
                {
                    ConsoleString += LogMessage;
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

        public int StopBits
        {
            get { return _connection.StopBits; }
            set { _connection.StopBits = value; }
        }
        #endregion
        /// <summary>
        /// ICommands for Buttons on the View.
        /// </summary>
        #region
        public ICommand ButtonPress{ get; }
        public ICommand StartCommand { get; }
        #endregion

        private Connection _connection { get; }

        /// <summary>
        /// Constructor for View model, settings are not stored in a persistent manner since only one 
        /// connection is allowed per the spec, and the settings could change between use.
        /// </summary>
        /// <param name="connection"></param>
        public ConnectionViewModel(Connection connection)
        {
            _connection = connection;
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
            {
                while(true)
                try
                {
                    string receivedMessage = _serialPort.ReadLine();
                    //update Console messages with data.
                    Console = $"{receivedMessage}";
                }
                catch (TimeoutException) { }
                catch (System.InvalidOperationException)
                    {
                        Connected = _serialPort.IsOpen;
                        receiveThread.Join();
                        sendThread.Join();
                    }
                    catch (System.IO.IOException)
                    {
                        Console = "Lost Connection with device";
                    }
            }
        }

        //send Command over serial communication to TIVA TI123GXL running ToneGenerator-Embedded.
        public void sendCommand()
        {

            //Write Message to Board with structure 'Note\r\n'

            if(message!=null)
                _serialPort.Write(message);
            //Sleep to avoid sending command when ToneGenerator-Embedded is not listening for a command.
            Thread.Sleep(500);
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
