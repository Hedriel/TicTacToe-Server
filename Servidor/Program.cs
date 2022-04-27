using System;
using System.Threading;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Net;
using System.Collections;
using Cliente.Modelo;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Servidor;

namespace Server
{
    class Servidor
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Iniciado servidor");
            TcpListener serverSocket = new TcpListener(8000);
         

            serverSocket.Start();
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                HandlerClient client = new HandlerClient();

                client.startClient(clientSocket);

            }
        }
    }
}

    