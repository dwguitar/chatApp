using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Server
{
    class Program
    {
		private static Hashtable clientList = new Hashtable();
		static void Main(string[] args)
        {
			try
			{
				while (true)
				{
					IPAddress ipAd = IPAddress.Parse("127.0.0.1");
					TcpListener tcpList = new TcpListener(ipAd, 8001);
					tcpList.Start();
					Console.WriteLine("The server is running at port 8001...");
					Console.WriteLine("The local End point is  :" +
					tcpList.LocalEndpoint);
					Console.WriteLine("Waiting for a connection.....");
					Socket socket = tcpList.AcceptSocket();
					Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);
					byte[] b = new byte[100];
					int k = socket.Receive(b);
					Console.WriteLine("Recieved...");
					string Command = string.Empty;
					for (int i = 0; i < k; i++)
					{
						Command = Command + Convert.ToChar(b[i]);
					}
					Console.WriteLine(Command);
					ASCIIEncoding asen = new ASCIIEncoding();
					clientList.Add(asen, socket);
					socket.Send(asen.GetBytes("The string was recieved by the server."));
					Console.WriteLine("\nSent Acknowledgement");
					socket.Close();
					tcpList.Stop();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error..... " + e.StackTrace);
			}
		}
    }
}
