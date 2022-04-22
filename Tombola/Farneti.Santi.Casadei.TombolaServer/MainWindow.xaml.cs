using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace Farneti.Santi.Casadei.TombolaServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class TestConnessione : WebSocketBehavior
        {
            //Lista di client connessi
            private static List<WebSocket> _clientSockets = new List<WebSocket>();
            //numero di client connessi
            private static int count;

            protected override void OnOpen()
            {
                WebSocket clientN = Context.WebSocket;
                count = _clientSockets.Count;
                Console.WriteLine("Richiesta connessione da client: " + (count + 1).ToString());
                //accetto solo due client
                if (count > 1)
                {
                    Console.WriteLine("Chiusa connessione con client: " + (count + 1).ToString());
                    Context.WebSocket.Close();
                }
                else
                {
                    _clientSockets.Add(clientN);
                }
            }
            protected override void OnMessage(MessageEventArgs e)
            {
                //invio ad un client i messaggi dell'altro
                Console.WriteLine("Received message from client: " + e.Data);
                if (Context.WebSocket == _clientSockets[0])
                {
                    _clientSockets[1].Send(e.Data);
                }
                else
                {
                    _clientSockets[0].Send(e.Data);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Attivo un server con 3 servizi attivi: Echo;EchoAll;PrivateChat
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:9000");

            wssv.AddWebSocketService<TestConnessione>("/TestConnessione");
            wssv.Start();
            wssv.Stop();
        }
    }
}
