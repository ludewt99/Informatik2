using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ServerSocket1
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                serverSocket = new TcpListener(localAddr, 8080);
                serverSocket.Start();

                while (true)
                {
                    Console.WriteLine("Warte auf Verbindung...");

                    TcpClient client = serverSocket.AcceptTcpClient();
                    Console.WriteLine("Verbindung hergestellt");

                    NetworkStream stream = client.GetStream();


                    Byte[] daten = new Byte[256];

                    // Nachricht empfangen
                    int i = stream.Read(daten, 0, daten.Length);
                    Console.WriteLine("Die Nachricht hat {0} bytes", i);

                    string RecNachricht = Encoding.ASCII.GetString(daten, 0, i);
                    Console.WriteLine("Empfangene Nachricht: " + RecNachricht);

                    //Serveranfragen
                    switch(RecNachricht)
                    {
                        case "Online?":
                            {
                                daten = Encoding.ASCII.GetBytes("Server ist online");
                                stream.Write(daten, 0, daten.Length);
                                break;
                            }

                        case "Info":
                            {
                                daten = Encoding.ASCII.GetBytes("Server-IP: 127.0.0.1 , Port: 8080");
                                stream.Write(daten, 0, daten.Length);
                                break;
                            }

                        case "Matrikelnummer":
                            {
                                daten = Encoding.ASCII.GetBytes("Matr. Nr.: 767440");
                                stream.Write(daten, 0, daten.Length);
                                break;
                            }
                        
                        default:
                            {
                                daten = Encoding.ASCII.GetBytes("Anfrage kann nicht verarbeitet werden");
                                stream.Write(daten, 0, daten.Length);
                                break;
                            }
                            
                    }
                    
                   
                }
            }
            catch(ArgumentException e1)
            {
                Console.WriteLine(e1.Message);
            }
            catch(IOException e2)
            {
                Console.WriteLine(e2.Message);
            }
        }
    }
}
