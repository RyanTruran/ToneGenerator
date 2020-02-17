using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;

namespace ToneGenerator.Models
{
    public class Connection
    {
        #region Connection Constructor
        public Connection(string CommPort, int BaudRate, int DataBits, string Parity, string StopBits, string Handshake)
        {
            this.CommPort = CommPort;
            this.BaudRate = BaudRate;
            this.DataBits = DataBits;
            this.Parity = Parity;
            this.StopBits = StopBits;
            this.Handshake = Handshake;
            this.Connected = false;
        }
        #endregion
        /// <summary>
        /// Getters and setters, used when communicating with the ViewModel.
        /// </summary>
        #region Getters and Setters
        public bool Connected { get; set; }
        public LinkedList<string> ConsoleLog = new LinkedList<string>();
        public string CommPort { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string Parity { get; set; }
        public string StopBits { get; set; }
        public string Handshake { get; set; }
        #endregion
        
    }
}
