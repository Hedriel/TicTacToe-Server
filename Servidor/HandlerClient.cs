using Cliente.Modelo;
using Newtonsoft.Json;
using Servidor;
using System;
using System.Collections;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Threading;

public class HandlerClient
{
    TcpClient clientSocket;
    ArrayList usuarios;
    dynamic delivered;
    Boolean logeado = false;
    Usuario user;
    NetworkStream networkStream;
    Partida p;

    byte[] bytesFrom;
    Byte[] sendBytes;
    string serverResponse = null;

    public HandlerClient()
    {
      
    }

    public void startClient(TcpClient clientSocket)
    {
        generarUsuarios();
        this.clientSocket = clientSocket;
        Thread threadClient = new Thread(doChat);
        threadClient.Start();
        Console.WriteLine("Cliente Conectado");
    }

    private void doChat()
    {
        bytesFrom = new byte[10025];
        string dataFromClient = null;
        
        while (true)
        {
            // Recibi mensaje
            networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 0, bytesFrom.Length);
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

            if (!logeado) {
                delivered = JsonConvert.DeserializeObject<Usuario>(dataFromClient);    
                chequearUsuario();
            }
            else {
                Console.WriteLine("Llego la linea : " + dataFromClient);
                interpretarMensaje(dataFromClient);
            }
        }
    }

    private void generarUsuarios()
    {
        usuarios = new ArrayList();
        usuarios.Add(new Usuario("a", "a",1500));
        usuarios.Add(new Usuario("b", "b",1490));
        usuarios.Add(new Usuario("b", "b",1490));
        usuarios.Add(new Usuario("Maradona", "Boquita",1510));
        usuarios.Add(new Usuario("Marcelo", "Marcelo",1500));
    }

   private void chequearUsuario()
    {
     
        foreach (Usuario usuario in usuarios)
        {      
            if (usuario.getNombre().Equals(delivered.getNombre()) && usuario.getPassword().Equals(delivered.getPassword()))
            {

                this.logeado = true;
                bytesFrom = new byte[1];
                serverResponse = "aceptado";
                user = usuario;
                break;
            }
            else {
                serverResponse = "denegado";
            }
        }
        sendBytes = System.Text.Encoding.ASCII.GetBytes(serverResponse);
        networkStream.Write(sendBytes, 0, sendBytes.Length);
        networkStream.Flush();
    }

    private void interpretarMensaje(string delivered)
    {
        if (delivered.Equals("o"))
        {
            ListadoJugadores.sacarDeCola(user.getNombre());
            //falta finalizar conexion
        }
        else if (delivered.Equals("c"))
        {
            this.p=ListadoJugadores.agregarCliente(new Jugador(user, clientSocket));
            if(p != null)
            {
                /////////////////// Manda Usuario al "Host"
                string rival = JsonConvert.SerializeObject(p.getRival()); ;
                var sendBytes = System.Text.Encoding.ASCII.GetBytes(rival);
                byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

                networkStream.Write(intBytes, 0, intBytes.Length);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();

                //////////////////// Manda Usuario al "Rival"
                string host = JsonConvert.SerializeObject(user); 
                var sendBytesRival = System.Text.Encoding.ASCII.GetBytes(host);
                byte[] intBytesRival = BitConverter.GetBytes(sendBytes.Length);


                p.getRivalTcp().GetStream().Write(intBytes, 0, intBytesRival.Length);
                p.getRivalTcp().GetStream().Write(sendBytesRival, 0, sendBytesRival.Length);
            } 
        }

        else if (delivered.Equals("d")) 
        {
            ListadoJugadores.sacarDeCola(user.getNombre());
        }
        else if (delivered.Equals("w")) 
        {
            if (p!=null)
            {
                string host = p.getRival().getNombre();
                var sendBytes = System.Text.Encoding.ASCII.GetBytes(host);
                byte[] intBytes = BitConverter.GetBytes(sendBytes.Length);

                networkStream.Write(intBytes, 0, intBytes.Length);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();

            }
            
        }

    }
}

