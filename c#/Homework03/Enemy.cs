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

        private double _enemyCanvasTop;
        public double EnemyCanvasTop
        {
            get { return _enemyCanvasTop; }
            set
            {
                _enemyCanvasTop = value;
                OnPropertyChanged("EnemyCanvasTop");
            }
        }

        private double _enemyCanvasLeft;
        public double EnemyCanvasLeft
        {
            get { return _enemyCanvasLeft; }
            set
            {
                _enemyCanvasLeft = value;
                OnPropertyChanged("EnemyCanvasLeft");
            }
        }

        private double _enemyHeight;
        public double EnemyHeight
        {
            get { return _enemyHeight; }
            set
            {
                _enemyHeight = value;
                OnPropertyChanged("EnemyHeight");
            }
        }

        private double _enemyWidth;
        public double EnemyWidth
        {
            get { return _enemyWidth; }
            set
            {
                _enemyWidth = value;
                OnPropertyChanged("EnemyWidth");
            }
        }

        private Visibility _enemyVisibility;
        public Visibility EnemyVisibility
        {
            get { return _enemyVisibility; }
            set
            {
                _enemyVisibility = value;
                OnPropertyChanged("EnemyVisibility");
            }
        }
    }
}
