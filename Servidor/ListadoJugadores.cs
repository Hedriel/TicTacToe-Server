using Cliente.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    static class ListadoJugadores     
    {
        static ArrayList conexiones=new ArrayList();
        
        public static Partida agregarCliente(Jugador jugador)
        {
            conexiones.Add(jugador); 
            Console.WriteLine(conexiones.Count + "/" + jugador.getDatos().getNombre());
            return buscarMatch(jugador);
        }

        internal static void sacarDeCola(string userName)
        {
            foreach (Jugador jug in conexiones)
            {
                if (jug.getDatos().getNombre().Equals(userName))
                {
                    Console.WriteLine("Se va a remover a " + jug.getDatos().getNombre());
                    conexiones.Remove(jug);
                    Console.WriteLine(conexiones.Count);
                    break;
                }
            }
        }

        private static Partida buscarMatch(Jugador jugador)
        {
            Partida p = null; 
            foreach (Jugador jug in conexiones)
            {
                if(jugador.getDatos().getNombre() != jug.getDatos().getNombre()) 
                {
                    int diferencia = jugador.getDatos().getScore() - jug.getDatos().getScore();

                    if (diferencia <= 10 && diferencia >= -10)
                    {
                        Jugador j1 = jugador;
                        Jugador j2 = jug;
                        
                         p = new Partida(j1, j2);

                        conexiones.Remove(jug);
                        conexiones.Remove(jugador);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("No encontro :(");
                    }
                }
                    
            }
            return p;
        }
    }
}
