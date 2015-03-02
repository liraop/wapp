using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Threading;
using PrecisionTimers;
using System.Windows.Media;

namespace Homework03
{
    public partial class Model : INotifyPropertyChanged
    {
        System.Media.SoundPlayer _player = new System.Media.SoundPlayer();
        Random _randomNumber = new Random();
        private TimerQueueTimer.WaitOrTimerDelegate _bulletCallbackDelegate;
        private TimerQueueTimer _bulletHiResTimer;
        private double _bulletSpeed = 5;
        private bool _moveBullet = false;
        System.Drawing.Rectangle _bulletRectangle;

        private TimerQueueTimer.WaitOrTimerDelegate _enemyCallbackDelegate;
        private TimerQueueTimer _enemyHiResTimer;
        private double _enemySpeed;
        private bool _enemyOnScreen = false;
        System.Drawing.Rectangle _enemyRectangle;
        
        private Thread _threadPaddle = null;
        private ThreadStart _threadPaddleStart = null;
        private Boolean _threadPaddleIsSuspended = false;
        bool _movepaddleLeft = false;
        bool _movepaddleRight = false;
        System.Drawing.Rectangle _paddleRectangle;

        private double _windowHeight = 100;
        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        private double _windowWidth = 100;
        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        private double _scoreHeight = 100;
        public double ScoreTabHeight
        {
            get { return _scoreHeight; }
            set { _scoreHeight = value; }
        }

        private double _scoreWidth = 100;
        public double ScoreTabWidth
        {
            get { return _scoreWidth; }
            set { _scoreWidth = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NewGame(bool start)
        {
            if (start)
            {
                StartPosition();
            }
        }

        public void MoveLeft(bool move)
        {
            _movepaddleLeft = move;
        }

        public void MoveRight(bool move)
        {
            _movepaddleRight = move;
        }

        public void Shoot(bool shoot)
        {
            if (!_moveBullet)
            {
                BulletVisibility = System.Windows.Visibility.Visible;
                SetBulletStartPos();
                _moveBullet = shoot;
            }
        }

        public void InitModel()
        {
           if (_threadPaddle == null)
            {
                _threadPaddleStart = new ThreadStart(paddleThreadFunction);
                _threadPaddle = new Thread(_threadPaddleStart);
                _threadPaddle.Start();
            }

            _bulletCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(BulletMMTimerCallback);
            _bulletHiResTimer = new TimerQueueTimer();

            try
            {
                _bulletHiResTimer.Create(8, 8, _bulletCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create bullet timer. Error from GetLastError = {0}", ex.Error);
            }

            _enemyCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(EnemyMMTimerCallback);
            _enemyHiResTimer = new TimerQueueTimer();

            try
            {
                _enemyHiResTimer.Create(8, 8, _enemyCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create enemy timer. Error from GetLastError = {0}", ex.Error);
            }

        }

        public void StartPosition()
        {
            BulletHeight = 10;
            BulletWidth = 10;

            _moveBullet = false;
            _enemyOnScreen = true;

            PaddleWidth = 120;
            PaddleHeight = 5;

            PaddleCanvasLeft = _windowWidth / 2 - PaddleWidth / 2;
            PaddleCanvasTop = _windowHeight - PaddleHeight;

            EnemyHeight = 80;
            EnemyWidth = 40;

            SetBulletStartPos();
            setEnemyStartPos();
            _paddleRectangle = new System.Drawing.Rectangle((int)PaddleCanvasLeft, (int)PaddleCanvasTop, (int)PaddleWidth, (int)PaddleHeight);
        }

        private void BulletMMTimerCallback(IntPtr pWhat, bool success)
        {
            if (!_moveBullet)
                return;

            if (!_bulletHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            if (BulletCanvasTop > 0)
            {
                BulletCanvasTop -= _bulletSpeed;
            }
            else if (_bulletRectangle.IntersectsWith(_enemyRectangle))
            {
                Console.WriteLine("bateu porra!");
                BulletVisibility = System.Windows.Visibility.Hidden;
                _moveBullet = false;
            }
            else 
            {
                BulletVisibility = System.Windows.Visibility.Hidden;
                _moveBullet = false;
            }
    
            _bulletRectangle = new System.Drawing.Rectangle((int)BulletCanvasLeft, (int)BulletCanvasTop, (int)BulletWidth, (int)BulletHeight);
            _bulletHiResTimer.DoneExecutingCallback();
        }

        private void EnemyMMTimerCallback(IntPtr pWhat, bool success)
        {
            if (!_enemyOnScreen)
                return;

            if (!_enemyHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            if ( EnemyCanvasTop < _windowHeight)
            {
                EnemyCanvasTop += _enemySpeed;
            }
            else if (_enemyRectangle.IntersectsWith(_bulletRectangle) ||
                     _enemyRectangle.IntersectsWith(_paddleRectangle) )
            {
                Console.WriteLine("bateu porra!\n");
                EnemyVisibility = System.Windows.Visibility.Hidden;
                _enemyOnScreen = false;
            }
            else
            {
                EnemyVisibility = System.Windows.Visibility.Hidden;
                _enemyOnScreen = false;
            }
           
            _enemyRectangle = new System.Drawing.Rectangle((int)EnemyCanvasLeft, (int)EnemyCanvasTop, (int)EnemyWidth, (int)EnemyHeight);
            _enemyHiResTimer.DoneExecutingCallback();
        }

        private void SetBulletStartPos()

        {
            BulletCanvasLeft = PaddleCanvasLeft + PaddleWidth / 2;
            BulletCanvasTop = PaddleCanvasTop + PaddleHeight;
        }

        private void setEnemyStartPos()
        {
            EnemyVisibility = System.Windows.Visibility.Visible;
           EnemyCanvasLeft = _randomNumber.Next(0,(int) _windowWidth);
           _enemySpeed = _randomNumber.Next(1, 5); 
           EnemyCanvasTop = -24;
           _player = new System.Media.SoundPlayer("../../sounds/meow.wav");
           _player.Play();
        }
   
        public void CleanUp()
        {
            //***********************************
            // STOP THE PADDLE THREAD HERE
            //***********************************
            if (_threadPaddle != null && _threadPaddle.IsAlive)
            {
                _threadPaddle.Abort();
                _threadPaddle = null;
            }
        }

        private void paddleThreadFunction()
        {
            while (true)
            {
                if (_movepaddleLeft && PaddleCanvasLeft > 0)
                    PaddleCanvasLeft -= 2;
                else if (_movepaddleRight && PaddleCanvasLeft < _windowWidth - PaddleWidth)
                    PaddleCanvasLeft += 2;

                _paddleRectangle = new System.Drawing.Rectangle((int)PaddleCanvasLeft, (int)PaddleCanvasTop, (int)PaddleWidth, (int)PaddleHeight);
                Thread.Sleep(10);
            }

        }
    }
}
