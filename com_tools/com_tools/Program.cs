using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Mono.Options;
using System.IO.Ports;

namespace com_tools
{
    class Program
    {
        public static void Main( String[] args )
        {
            bool show_help = false;
            bool show_names = false;
            List<byte> tx_data;
            string port = "COM3";
            int baud = 115200;

            var p = new Mono.Options.OptionSet()
            {
                {"p|port=", "The {PORT} to be used for communications.",  v => port = v  },
                {"b|baud=", "The {BAUD} used in communications.",         v=> baud = Convert.ToInt32(v) },
                {"l|list", "List the available Com port names", v=> show_names = v != null },
                {"h|help", "Show this message", v=> { show_help = v!= null; } },

            };
            List<string> extra;
            try {
                extra = p.Parse(args);
            }
            catch( OptionException e)
            {
                Console.Write("com_tools: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try using 'com_tools --help' for more information");
                return;
            }

            if (show_help)
            {
                Console.WriteLine("Usage: com_tools [OPTIONS]+ {Tx Byte} {tx Byte} ...");
                Console.WriteLine("Send data to the serial port specified and read the response");
                Console.WriteLine("if there is any response. Respnse data is converted to ASCII");
                Console.WriteLine("bytes.");
                Console.WriteLine();
                Console.WriteLine("Options:");
                p.WriteOptionDescriptions(Console.Out);
                return;
            }
            if (show_names)
            {
                string[] names = SerialPort.GetPortNames();
                foreach( string s in names)
                {
                    Console.Write($"{s} ");
                }
                Console.WriteLine();
                return;
            }
            /* generate the transmit data buffer from the extra command line arguments. */
            tx_data = new List<Byte>();
            foreach ( string s in extra)
            {
                try
                {
                    tx_data.Add(Convert.ToByte(s));
                }
                catch ( Exception e )
                {
                    Console.WriteLine("com_tools: Error processing transmit data.");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try using 'com_tools --help' for more information");
                    return;
                }

            }

            SerialPort serial = new SerialPort(port, baud, Parity.None, 8, StopBits.One);
            try
            {
                serial.Open();
                if (tx_data.Count > 0) {
                    /* sending data */
                    serial.Write(tx_data.ToArray(), 0, tx_data.Count);
                    Thread.Sleep(20);
                }
                while (serial.BytesToRead > 0)
                {
                    Byte[] rx_data = new byte[serial.BytesToRead];
                    serial.Read(rx_data, 0, serial.BytesToRead);
                    /* dump data to the console */
                    foreach (byte b in rx_data) {
                        Console.Write($"{b} ");
                    }
                    Thread.Sleep(10);
                }
                Console.WriteLine();
                serial.Close();
            }
            catch( Exception e )
            {
                Console.Write("com_tools: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try using 'com_tools --help' for more information");
                return;
            }
        }
    }
}
