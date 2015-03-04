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

// Reference the SocketSetupWindow Dialogue
using SocketSetup;

namespace SampleUDPPeer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _model;

        public MainWindow()
        {
            InitializeComponent();

            _model = new Model();
            this.DataContext = _model;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void SocketSetup_Button_Click(object sender, RoutedEventArgs e)
        {
            //****************************************************
            // Open a new window for setting up socket connection
            //****************************************************

            SocketSetupWindow socketSetupWindow = new SocketSetupWindow();
            socketSetupWindow.ShowDialog();
           

            //***************************************************
            //Set a Title for the Window
            //***************************************************
            this.Title = socketSetupWindow.SocketData.LocalIPString + "@" + socketSetupWindow.SocketData.LocalPort.ToString();

            //**************************************************
            //Update Model with The SetUp Data
            //*************************************************
            _model.SetLocalNetworkSettings(socketSetupWindow.SocketData.LocalPort, socketSetupWindow.SocketData.LocalIPString);
            _model.SetRemoteNetworkSettings(socketSetupWindow.SocketData.RemotePort, socketSetupWindow.SocketData.RemoteIPString);

            //*************************************************
            //Initialize the Model
            //************************************************* 
            _model.InitModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _model.Model_Cleanup();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            //**************************************************
            //Call Send Message
            //**************************************************
            _model.SendMessage();
        }
    }
}
