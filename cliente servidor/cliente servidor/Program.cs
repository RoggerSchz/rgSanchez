using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace cliente_servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            Conectar();

        }
        public static void Conectar()
        { 
          Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          IPEndPoint miDireccion = new IPEndPoint(IPAddress.Any, 1234);
          try
          {
              miPrimerSocket.Bind(miDireccion);
              miPrimerSocket.Listen(1);
              Console.Write("Escuchando");
              Socket Escuchar = miPrimerSocket.Accept();
              Console.Write("Cliente conectado");

              Console.WriteLine("Connecion aceptada" + Escuchar.RemoteEndPoint); 
              byte[] b = new byte[100];
              int k = Escuchar.Receive(b);
              Console.WriteLine("Recibiendo...");
              for (int i = 0; i < k; i++)
                  Console.Write(Convert.ToChar(b[i]));

              ASCIIEncoding asen = new ASCIIEncoding();
              Escuchar.Send(asen.GetBytes("Cadena recibida desde el cliente"));
             
              miPrimerSocket.Close();

          }
          catch(Exception error) 
          {
              Console.WriteLine("Error:", error.ToString());

          }
          Console.WriteLine("Pesione  una tecla para termianr");
          Console.ReadLine();
        }
    }
}
