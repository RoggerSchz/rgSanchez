using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;           
using System.Net.Sockets;
using System.IO;
namespace cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            Conectar();

        }
        public static void Conectar()
        {
         try 
         { 
            TcpClient tcpclnt = new TcpClient(); 
            Console.WriteLine("Connectando...");

            tcpclnt.Connect("127.0.0.1", 1234); 

            Console.WriteLine("Conectado");
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                
            while (true)
            {
                Console.Write("> ");

                String str = Console.ReadLine();
                string json = javaScriptSerializer.Serialize(str);
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(json);

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[1200];
                int k = stm.Read(bb, 0, 1200);

                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
            }
            tcpclnt.Close(); 
          } 

          catch (Exception e) 
          { 
                Console.WriteLine("Error..... " + e.StackTrace); 
          } 

          Console.ReadKey(); 
            } 
       }   
 }

