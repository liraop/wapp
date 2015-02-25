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

// Author : Pedro de Oliveira Lira - pdeolive@syr.edu
namespace ObservableCollections
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Model _model;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            _model = new Model();
            MyItemsControl.ItemsSource = _model.TileCollection;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string temp;
            var selectedButton = e.OriginalSource as FrameworkElement;
            if (selectedButton != null)
            {
                var currentTile = selectedButton.DataContext as Tile;
                temp = _model.UserSelection(currentTile.TileName);
                Error_Label.Content = temp;
            }
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            _model.Clear();
            Error_Label.Content = "";
        }
    }
}
