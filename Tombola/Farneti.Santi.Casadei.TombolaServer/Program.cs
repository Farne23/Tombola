using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Collections.Generic;

namespace Farneti.Santi.Casadei.TombolaServer
{
    //public class Echo : WebSocketBehavior
    //{
    //    protected override void OnMessage(MessageEventArgs e)
    //    {
    //        //invio al client il suo stesso messaggio
    //        Console.WriteLine("Received message from client: " + e.Data);
    //        Send(e.Data);
    //    }
    //}


    public class TestConnessione : WebSocketBehavior
    {

        protected override void OnOpen()
        {
            Send("Connessione Stabilita");      
        }
    }

    public class Gioco : WebSocketBehavior
    {
            //Lista di client connessi
            private static List<WebSocket> _clientSockets = new List<WebSocket>();
            private static List<int> _playersID = new List<int>();

            //numero di client connessi
            private static int count;

            protected override void OnOpen()
            {
                int idprogressivo = 0;
                WebSocket clientN = Context.WebSocket;
                count = _clientSockets.Count;

                //Accetto massimo sei giocatori
                if (count > 6)
                {
                    Context.WebSocket.Close();
                }
                else
                {
                    _clientSockets.Add(clientN);
                    idprogressivo++;
                     _playersID.Add(idprogressivo);

                }
            }
            //protected override void OnMessage(MessageEventArgs e)
            //{
            //    //invio ad un client i messaggi dell'altro
            //    Console.WriteLine("Received message from client: " + e.Data);
            //    if (Context.WebSocket == _clientSockets[0])
            //    {
            //        _clientSockets[1].Send(e.Data);
            //    }
            //    else
            //    {
            //        _clientSockets[0].Send(e.Data);
            //    }
            //}
        }

    class Program
    {

        static void Main(string[] args)
        {
            //Attivo un server con 3 servizi attivi: Echo;EchoAll;PrivateChat
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:9000");
            wssv.AddWebSocketService<TestConnessione>("/TestConnessione");
            wssv.AddWebSocketService<Gioco>("/Gioco");
            wssv.Start();

            //interrompo il server dopo la pressione di un tasto
            Console.ReadKey();
            wssv.Stop();
        }
    }
}
