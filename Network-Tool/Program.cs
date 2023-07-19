using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using System.Web;
using System.Runtime.ConstrainedExecution;

namespace Network_Tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            MenuDisp();
        }

        static void MenuDisp()
        {
            string eingabe;

            do
            {
                Console.Clear();
                Console.WriteLine("Network-Tool (by Noel Malchow)");
                Console.WriteLine("---------------------");

                Console.Write("Different Tools to test your ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Network");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\nType the Programm Number to Start");
                Console.WriteLine("---------------------");
                Console.WriteLine("Programm 1: Ping with/without repeat");
                Console.WriteLine("Programm 2: None");
                Console.WriteLine("Programm 3: None");
                Console.WriteLine("---------------------");
				Console.WriteLine("To Reload: Reload");
				Console.WriteLine("To Exit: exit");
				
				Console.Write("\nProgramm: ");
				eingabe = Convert.ToString(Console.ReadLine());

				switch (eingabe)
				{
					case "1":
						PingTest();
						break;
				
					case "2":
						break;

					case "exit":
						break;

					case "reload":
						Console.Clear();
						txtcolor("Reloading the Programm ", "DarkYellow", "Write");
						for (int i = 3; i > 0; i--)
						{
							txtcolor(". ", "Cyan", "Write");
							Task.Delay(1000).Wait();
						}
						txtcolor("Reloading Now", "Green", "WriteLine");

						progreload(true, 1000);
						break;

					default:
						Console.Clear();
						txtcolor("ERROR\t\tERROR\t\tERROR", "DarkYellow", "WriteLine");
						Console.WriteLine("\nFehler 404 Programm Not Found");
						
						progreload(true, 2000);
						break;
				}

				if (eingabe != "exit")
				{
					Console.ReadKey();
				}

			} while (eingabe != "exit");
		}


		// Use Programm Methods !! Used for the Programm itself !!

		static void PingTest()
		{
            IPAddress address;
            string ipAdress = ""; // IP Adress of the Computer
            string dnsAdress = "None"; // DNS Name
            string timeoutTime = "1000"; // Timeout time of the Ping
            Console.Clear();
            Console.Write("Please enter the IP Adress: ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            ipAdress = Console.ReadLine();
            if (!IPAddress.TryParse(ipAdress, out address))
            {
                /*switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        // we have IPv4
                        break;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        // we have IPv6
                        break;
                    default:
                        // umm... yeah... I'm going to need to take your red packet and...
                        break;
                }*/
                dnsAdress = ipAdress;
            }
            Console.ResetColor();

            Console.Write("Please enter the timeout time of the Ping [1000ms]: ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            timeoutTime = Console.ReadLine();
            if (!timeoutTime.All(Char.IsDigit) || timeoutTime == "")
            {
                timeoutTime = "1000";
			}
            Console.WriteLine(System.Environment.NewLine);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            try
            {
                Ping testPing = new Ping();
                PingReply reply = testPing.Send(ipAdress, Convert.ToInt32(timeoutTime));
                if (reply != null)
                {
                    if (dnsAdress != "None")
                    {
                        Console.WriteLine("Ping Address: " + dnsAdress + " [" + reply.Address + "]" + "\nStatus :  " + reply.Status + "\nPing Time : " + reply.RoundtripTime.ToString() + "ms");
                    }
                    else
                    {
                        Console.WriteLine("Ping Address: " + reply.Address + "\nStatus :  " + reply.Status + "\nPing Time : " + reply.RoundtripTime.ToString() + "ms");
                        //Console.WriteLine(reply.ToString());
                    }
                }
            }
            catch
            {
                Console.WriteLine("ERROR: Request timed out.");
                //Console.WriteLine(ipAdress + "" + timeoutTime);
            }

            Console.ResetColor();
            //Console.WriteLine(ipAdress + "" + timeoutTime);
        }




        // Core System Methods !! Do Not Touch !!


        // (Code from https://stackoverflow.com/questions/5706497/how-restart-the-console-app)
        ///<summary>
        ///Small Programm to Reload the Console.
        ///Must have using System.Reflection; 
        ///</summary>
        ///<param name="pActive">Tests if the programm should be Reloaded</param>
        ///<param name="pTermWait">Time to Wait before the Reload initialize in ms (2000ms = 2sec)</param>
        static void progreload(bool pActive, int pTermWait)
		{
			if (pActive)
			{
				Task.Delay(pTermWait).Wait();
				var fileName = Assembly.GetExecutingAssembly().Location;
				System.Diagnostics.Process.Start(fileName);
				Environment.Exit(0);
			}
		}


        // (Code from https://stackoverflow.com/questions/19275947/c-sharp-how-can-i-append-a-string-variable-on-to-the-end-of-consolecolor)
        /// <summary>
        /// A Little Programm to Color the Input.
        ///</summary>
        ///<param name="pText">The Text that should be displayed</param>
        ///<param name="pColor">The Color that is changing the Text</param>
        ///<param name="pMode">This changes if it should be a WriteLine or just a Write in the Console</param>
        static void txtcolor(string pText, string pColor, string pMode)
		{
			Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), pColor);
			if (pMode == "Write")
			{
				Console.Write(pText);
			}
			else if (pMode == "WriteLine")
			{
				Console.WriteLine(pText);
			}
			Console.ResetColor();
		}

	}
}
