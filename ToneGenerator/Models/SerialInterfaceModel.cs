using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToneGenerator.Models
{
    public class SerialInterfaceModel
    {
        
        public SerialInterfaceModel(string CommPort, int BaudRate, int DataBits, string Parity, int StopBits, string Handshake)
        {
            this.CommPort = CommPort;
            this.BaudRate = BaudRate;
            this.DataBits = DataBits;
            this.Parity = Parity;
            this.StopBits = StopBits;
            this.Handshake = Handshake;
        }
        #region Getters and Setters
        public string CommPort { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string Parity { get; set; }
        public int StopBits { get; set; }
        public string Handshake { get; set; }
        #endregion
    }
}
