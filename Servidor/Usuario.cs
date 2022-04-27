using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo
{
    public class Usuario
    {
        public String usuario { get; set; }
        public String password { get; set; }
        public int score { get; set; }

        public string getNombre()
        {
            return usuario;
        }
        public string getPassword()
        {
            return password;
        }

        public int getScore()
        {
            return score;
        }

        public Usuario(string usuario, string password, int score)
        {
            this.usuario = usuario;
            this.password = password;
            this.score = score; 
          
        }

        public static implicit operator TcpClient(Usuario v)
        {
            throw new NotImplementedException();
        }
    }
}
    

