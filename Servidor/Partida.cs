using Cliente.Modelo;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    class Partida
    {
        Jugador j1;
        Jugador j2;
    
        public Partida(Jugador j1, Jugador j2)
        {
            this.j1 = j1;
            this.j2 = j2;
        }

        public Usuario getRival()
        {
            return j2.getDatos();
        }

        public TcpClient getRivalTcp()
        {
            return j2.getClient();
        }
    }
}
