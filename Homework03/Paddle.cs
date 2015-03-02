using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Homework03
{
    public partial class Model : INotifyPropertyChanged
    {

        private double _paddleCanvasTop;
        public double PaddleCanvasTop
        {
            get { return _paddleCanvasTop; }
            set
            {
                _paddleCanvasTop = value;
                OnPropertyChanged("PaddleCanvasTop");
            }
        }

        private double _paddleCanvasLeft;
        public double PaddleCanvasLeft
        {
            get { return _paddleCanvasLeft; }
            set
            {
                _paddleCanvasLeft = value;
                OnPropertyChanged("PaddleCanvasLeft");
            }
        }

        private double _paddleHeight;
        public double PaddleHeight
        {
            get { return _paddleHeight; }
            set
            {
                _paddleHeight = value;
                OnPropertyChanged("PaddleHeight");
            }
        }

        private double _paddleWidth;
        public double PaddleWidth
        {
            get { return _paddleWidth; }
            set
            {
                _paddleWidth = value;
                OnPropertyChanged("PaddleWidth");
            }
        }
    }
}
