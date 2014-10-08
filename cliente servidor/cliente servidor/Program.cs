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
        static String ExecuteCommand(string _Command)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + _Command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = false;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            return result;
        }
        static void Main(string[] args)
        {
            Conectar();

        }
        public static void Conectar()
        {
            string command = "";
            string salida;
           
          Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          IPEndPoint miDireccion = new IPEndPoint(IPAddress.Any, 1234);
          try
          {
              miPrimerSocket.Bind(miDireccion);
              miPrimerSocket.Listen(1);
              Socket Escuchar = miPrimerSocket.Accept();
              
              byte[] b = new byte[100];
              while (true)
              {
                  int k = Escuchar.Receive(b);
                  Console.WriteLine("Esperando...");
                  for (int i = 0; i < k; i++)
                  {
                      Console.Write(Convert.ToChar(b[i]));
                      command = command + (Convert.ToChar(b[i]));

                  }
                  ASCIIEncoding asen = new ASCIIEncoding();
                  var des = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<string>(command);
                  salida = ExecuteCommand(des);
                  command = "";
                  Console.WriteLine("Contenido de la cadena" + salida);
                  if (salida == "")
                  {
                      Escuchar.Send(asen.GetBytes("Error"));
                  }
                  else {
                      Escuchar.Send(asen.GetBytes(salida));
                  }
              }
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
