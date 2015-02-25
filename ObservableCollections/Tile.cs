﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using System.Windows.Media;

// Author: Pedro de Oliveira Lira - pdeolive@syr.edu
namespace ObservableCollections
{
    public class Tile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _tileName;
        public string TileName
        {
            get { return _tileName; }
            set {
                _tileName = value;
                OnPropertyChanged("TileName");
            }
        }

        private string _tileLabel;
        public string TileLabel
        {
            get { return _tileLabel; }
            set
            {
                _tileLabel = value;
                OnPropertyChanged("TileLabel");
            }
        }

        private Brush _tileBrush;
        public Brush TileBrush
        {
            get { return _tileBrush; }
            set
            {
                _tileBrush = value;
                OnPropertyChanged("TileBrush");
            }
        }
    }
}
