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
using System.Windows.Shapes;
using WebSocketSharp;
namespace TombolaGUI
{
    /// <summary>
    /// Logica di interazione per Gioco.xaml
    /// </summary>
    public partial class Gioco : Window
    {
        public Gioco()
        {
            InitializeComponent();

            using (WebSocket ws = new("ws://127.0.0.1:9000/Lobby"))
            {

                ws.OnMessage += Ws_CheckConnessione;
                ws.Connect();
                ws.Accept();

                ws.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
