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

namespace Farneti.Santi.Casadei.TombolaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Connection = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window gioco = new Window();

            using (WebSocket ws = new("ws://127.0.0.1:9000/TestConnection"))
            {

                ws.OnMessage += Ws_CheckConnessione;
                ws.Connect();
                ws.Accept();

                do
                {
                    if (Connection == true)
                    {
                        this.Close();
                        gioco.Show();
                        break;
                    }
                }
                while (true);

                ws.Close();
            }
        }

        public void Ws_CheckConnessione(object sender, MessageEventArgs e)
        {
            Window gioco = new Window();
            Connection = true;
        }
    }
}
