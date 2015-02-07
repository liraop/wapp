using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BouncingBallSample
{
    public partial class Model: INotifyPropertyChanged
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
    }
}
