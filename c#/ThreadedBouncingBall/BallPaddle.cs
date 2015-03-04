﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

// Brushes
using System.Windows.Media;


namespace ThreadedBouncingBall
{
    public partial class Model : INotifyPropertyChanged
    {
        private double _ballCanvasTop;
        public double BallCanvasTop
        {
            get { return _ballCanvasTop; }
            set
            {
                _ballCanvasTop = value;
                OnPropertyChanged("BallCanvasTop");
            }
        }

        private double _ballCanvasLeft;
        public double BallCanvasLeft
        {
            get { return _ballCanvasLeft; }
            set
            {
                _ballCanvasLeft = value;
                OnPropertyChanged("BallCanvasLeft");
            }
        }

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

        private double _ballHeight;
        public double BallHeight
        {
            get { return _ballHeight; }
            set
            {
                _ballHeight = value;
                OnPropertyChanged("BallHeight");
            }
        }

        private double _ballWidth;
        public double BallWidth
        {
            get { return _ballWidth; }
            set
            {
                _ballWidth = value;
                OnPropertyChanged("BallWidth");
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