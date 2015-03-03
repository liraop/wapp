using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

// Student: Pedro de Oliveira Lira
namespace Homework03
{
    public partial class Model : INotifyPropertyChanged
    {

        private double _goCanvasTop;
        public double GoCanvasTop
        {
            get { return _goCanvasTop; }
            set
            {
                _goCanvasTop = value;
                OnPropertyChanged("GoCanvasTop");
            }
        }

        private double _goCanvasLeft;
        public double GoCanvasLeft
        {
            get { return _goCanvasLeft; }
            set
            {
                _goCanvasLeft = value;
                OnPropertyChanged("GoCanvasLeft");
            }
        }

        private double _goHeight;
        public double GoHeight
        {
            get { return _goHeight; }
            set
            {
                _goHeight = value;
                OnPropertyChanged("GoHeight");
            }
        }

        private double _goWidth;
        public double GoWidth
        {
            get { return _goWidth; }
            set
            {
                _goWidth = value;
                OnPropertyChanged("GoWidth");
            }
        }

        private String _goMessage;
        public String GoMessage
        {
            get { return _goMessage; }
            set
            {
                _goMessage = value;
                OnPropertyChanged("GoMessage");
            }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged("Points");
            }
        }

        private String _keyStrokes;
        public String KeyStrokes
        {
            get { return _keyStrokes; }
            set
            {
                _keyStrokes = value;
                OnPropertyChanged("KeyStrokes");
            }
        }

        private Visibility _goVisibility;
        public Visibility GoVisibility
        {
            get { return _goVisibility; }
            set
            {
                _goVisibility = value;
                OnPropertyChanged("GoVisibility");
            }
        }

    }
}
