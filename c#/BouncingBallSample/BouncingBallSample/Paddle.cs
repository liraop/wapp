﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

// Brushes
using System.Windows.Media;

namespace BuncingBallSample
{
    public partial class ModelBBS : INotifyPropertyChanged
    {

        private double _paddleCanvasTop;
        public double paddleCanvasTop
        {
            get { return _paddleCanvasTop; }
            set
            {
                _paddleCanvasTop = value;
                OnPropertyChanged("paddleCanvasTop");
            }
        }

        private double _paddleCanvasLeft;
        public double paddleCanvasLeft
        {
            get { return _paddleCanvasLeft; }
            set
            {
                _paddleCanvasLeft = value;
                OnPropertyChanged("paddleCanvasLeft");
            }
        }

        private double _paddleHeight;
        public double paddleHeight
        {
            get { return _paddleHeight; }
            set
            {
                _paddleHeight = value;
                OnPropertyChanged("paddleHeight");
            }
        }

        private double _paddleWidth;
        public double paddleWidth
        {
            get { return _paddleWidth; }
            set
            {
                _paddleWidth = value;
                OnPropertyChanged("paddleWidth");
            }
        }
    }
}
