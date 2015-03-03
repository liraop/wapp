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

        private double _bulletCanvasTop;
        public double BulletCanvasTop
        {
            get { return _bulletCanvasTop; }
            set
            {
                _bulletCanvasTop = value;
                OnPropertyChanged("BulletCanvasTop");
            }
        }

        private double _bulletCanvasLeft;
        public double BulletCanvasLeft
        {
            get { return _bulletCanvasLeft; }
            set
            {
                _bulletCanvasLeft = value;
                OnPropertyChanged("BulletCanvasLeft");
            }
        }

        private double _bulletHeight;
        public double BulletHeight
        {
            get { return _bulletHeight; }
            set
            {
                _bulletHeight = value;
                OnPropertyChanged("BulletHeight");
            }
        }

        private double _bulletWidth;
        public double BulletWidth
        {
            get { return _bulletWidth; }
            set
            {
                _bulletWidth = value;
                OnPropertyChanged("BulletWidth");
            }
        }

        private Visibility _bulletVisibility;
        public Visibility BulletVisibility
        {
            get { return _bulletVisibility; }
            set
            {
                _bulletVisibility = value;
                OnPropertyChanged("BulletVisibility");
            }
        }
    }
}
