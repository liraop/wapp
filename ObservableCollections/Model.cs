using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ObservableCollections
{
    class Model
    {
        public ObservableCollection<Tile> TileCollection;
        private static UInt32 _numTiles = 9;
        private UInt32[] _buttonPresses = new UInt32[_numTiles];
        Random _randomNumber = new Random();
        private int click;


        public Model()
        {
            TileCollection = new ObservableCollection<Tile>();
            for (int i = 0; i < _numTiles; i++)
            {
                TileCollection.Add(new Tile() { TileBrush = Brushes.Black, TileLabel = "", TileName = i.ToString() });
            }
        }

        public string UserSelection(String buttonSelected)
        {
            Debug.Write("Button selected was" + buttonSelected + "\n");
            int index = int.Parse(buttonSelected);
            _buttonPresses[index]++;
            if (_buttonPresses[index] == 1)
            {
                click++;
            }
            string playlabel = "";
            if (click % 2 != 0)
            {
                playlabel = "X";
                
            }
            else
            {
                playlabel = "O";
            }

            if (_buttonPresses[index] == 1)
            {
                 TileCollection[index].TileLabel = playlabel;
            }
      
            TileCollection[index].TileBrush = new SolidColorBrush(Color.FromArgb(255, (byte)_randomNumber.Next(255),(byte)_randomNumber.Next(255), (byte) _randomNumber.Next(255)));
            return "User Selected Button" + index.ToString() + "\n";
        }

        public void Clear()
        {
            for (int x = 0; x < _numTiles; x++)
            {
                TileCollection[x].TileBrush = Brushes.Black;
                TileCollection[x].TileLabel = "";
                _buttonPresses[x] = 0;
            }
        }

    }
}
