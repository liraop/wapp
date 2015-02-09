using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;

// Author: Pedro de Oliveira Lira
namespace ObservableCollections
{
    class Model
    {
        public ObservableCollection<Tile> TileCollection;
        private static UInt32 _numTiles = 9;
        private UInt32[] _buttonPresses = new UInt32[_numTiles];
        Random _randomNumber = new Random();
        private int click;
        private Boolean thereIsWinner;


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
            string msg = "";

            if (!thereIsWinner)
            {
                string playlabel = "";

                int index = int.Parse(buttonSelected);
                _buttonPresses[index]++;
                if (_buttonPresses[index] == 1)
                {
                    click++;
                    if (click % 2 != 0)
                    {
                        playlabel = "X";

                    }
                    else
                    {
                        playlabel = "O";
                    }
                    TileCollection[index].TileLabel = playlabel;
                }
                else
                {
                    msg = "Try again! \n";

                }

                TileCollection[index].TileBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                thereIsWinner = this.isWinner();

                if (thereIsWinner)
                {
                    msg = "The winner is " + playlabel + "\n";
                }

                if (thereIsTie())
                {
                    msg = "We have a tie!";
                }
            }
            else
            {
                msg = "Please, play again!";
            }
            
            return msg;
        }

        private bool thereIsTie()
        {
            for (int i = 0; i < _numTiles; i++)
            {
                if(TileCollection[i].TileLabel == ""){
                    return false;
                }
            }
            return true;
        }

        public Boolean isWinner()
        {
            //rows
            if (checkChars(TileCollection[0].TileLabel, TileCollection[1].TileLabel,
                TileCollection[2].TileLabel))
            {

                changeTileBrushColors(0, 1, 2);
                return true;
            } 
            else if (checkChars(TileCollection[3].TileLabel, TileCollection[4].TileLabel, TileCollection[5].TileLabel))
            {

              changeTileBrushColors(3, 4, 5);
              return true;
          }

            else if (checkChars(TileCollection[6].TileLabel, TileCollection[7].TileLabel, TileCollection[8].TileLabel))
            {
                changeTileBrushColors(6, 7, 8);
                return true;
            }

            //columns
            else if (checkChars(TileCollection[0].TileLabel, TileCollection[3].TileLabel,TileCollection[6].TileLabel))
            {
              changeTileBrushColors(0, 3, 6);
              return true;
            }

            else if (checkChars(TileCollection[1].TileLabel, TileCollection[4].TileLabel, TileCollection[7].TileLabel))
            {
                changeTileBrushColors(1, 4, 7);
                return true;
            }

            else if (checkChars(TileCollection[2].TileLabel, TileCollection[5].TileLabel, TileCollection[8].TileLabel))
            {
                changeTileBrushColors(2, 5, 8);
                return true;
            }

            //diagonals

            else if (checkChars(TileCollection[0].TileLabel, TileCollection[4].TileLabel, TileCollection[8].TileLabel))
            {
                changeTileBrushColors(0, 4, 8);
                return true;
            }

            else if (checkChars(TileCollection[2].TileLabel, TileCollection[4].TileLabel, TileCollection[6].TileLabel))
            {
                changeTileBrushColors(2, 4, 6);
                return true;
            }

           return false;
        }

        private void changeTileBrushColors(int t1, int t2, int t3)
        {
            for (int i = 0; i < _numTiles; i++)
            {
                if (i == t1 || i == t2 || i == t3)
                {
                    TileCollection[i].TileBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                } else {
                    TileCollection[i].TileBrush = new SolidColorBrush(Color.FromRgb(213, 213, 213));
                }

            }
        }

        private Boolean checkChars(string c1, string c2,string c3)
        {                                    
            return ((c1 != "") && ((c1 == c2) && (c2 == c3)));
        }


        public void Clear()
        {
            for (int x = 0; x < _numTiles; x++)
            {
                TileCollection[x].TileBrush = Brushes.Black;
                TileCollection[x].TileLabel = "";
                _buttonPresses[x] = 0;
                thereIsWinner = false;
            }
        }

    }
}
