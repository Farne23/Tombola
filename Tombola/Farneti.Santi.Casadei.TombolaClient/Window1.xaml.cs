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

namespace Farneti.Santi.Casadei.TombolaClient
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
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
    }
}
