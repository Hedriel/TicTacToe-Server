using Cliente.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{ 
   public class Jugador
    {
        Usuario user;
        TcpClient cliente;

        public Jugador(Usuario user, TcpClient cliente)
        {
            this.user = user;
            this.cliente = cliente;
        }

        

        public Usuario getDatos()
        {
            return user;
        }

        public TcpClient getClient()
        {
            return cliente;
        }
    }
}
