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

// Student: Pedro de Oliveira Lira
namespace Homework03
{
    public partial class Model : INotifyPropertyChanged
    {
        Random _randomNumber = new Random();
        private TimerQueueTimer.WaitOrTimerDelegate _bulletCallbackDelegate;
        private TimerQueueTimer _bulletHiResTimer;
        private double _bulletSpeed = 5;
        private bool _moveBullet = false;
        System.Drawing.Rectangle _bulletRectangle;

        private TimerQueueTimer.WaitOrTimerDelegate _enemyCallbackDelegate;
        private TimerQueueTimer _enemyHiResTimer;
        private double _enemySpeed = 1;
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

        public void NewGame()
        {
            InitModel();
            StartPosition();
        }

        public void InitModel()
        {
           if (_threadPaddle == null)
            {
                _threadPaddleStart = new ThreadStart(paddleThreadFunction);
                _threadPaddle = new Thread(_threadPaddleStart);
                _threadPaddle.Start();
            }

           if (_bulletCallbackDelegate == null)
           {
               _bulletCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(BulletMMTimerCallback);
           }
            
            if (_bulletHiResTimer == null)
            {
                _bulletHiResTimer = new TimerQueueTimer();

                try
                {
                    _bulletHiResTimer.Create(100, 10, _bulletCallbackDelegate);
                }
                catch (QueueTimerException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Failed to create bullet timer. Error from GetLastError = {0}", ex.Error);
                }

                if (_enemyCallbackDelegate == null)
                {
                    _enemyCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(EnemyMMTimerCallback);
                }
            }



            if (_enemyHiResTimer == null)
            {
                _enemyHiResTimer = new TimerQueueTimer();


                try
                {
                    _enemyHiResTimer.Create(100, 10, _enemyCallbackDelegate);
                }
                catch (QueueTimerException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Failed to create enemy timer. Error from GetLastError = {0}", ex.Error);
                }
            }

        }

        public void StartPosition()
        {
            
            Points = 0;
            BulletHeight = 10;
            BulletWidth = 10;
            GoVisibility = System.Windows.Visibility.Hidden;
            _moveBullet = false;

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
            
            if (BulletCanvasTop <= 0)
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
             
            if (_enemyRectangle.IntersectsWith(_bulletRectangle) && 
                BulletVisibility == System.Windows.Visibility.Visible &&
                EnemyVisibility == System.Windows.Visibility.Visible)
            {
                EnemyVisibility = System.Windows.Visibility.Hidden;
                BulletVisibility = System.Windows.Visibility.Hidden;
                Points += 1;
                (new System.Media.SoundPlayer("../../sounds/death.wav")).Play();
                setEnemyStartPos();
            }

            if ((_enemyRectangle.IntersectsWith(_paddleRectangle) ||
                EnemyCanvasTop >= _windowHeight ) &&
                EnemyVisibility == System.Windows.Visibility.Visible)
            {
                EnemyVisibility = System.Windows.Visibility.Hidden;                                 
                Gameover();
            }
            
            _enemyRectangle = new System.Drawing.Rectangle((int)EnemyCanvasLeft, (int)EnemyCanvasTop, (int)EnemyWidth, (int)EnemyHeight);

            try
            {
                _enemyHiResTimer.DoneExecutingCallback();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void Gameover()
        {
            GoMessage = "CAT WON, LOOSER!";
            GoWidth = WindowHeight;
            GoHeight = WindowWidth;
            GoCanvasLeft = WindowWidth/2 - GoWidth/2;
            GoCanvasTop = WindowHeight/2;
            GoVisibility = System.Windows.Visibility.Visible;
            (new System.Media.SoundPlayer("../../sounds/death.wav")).Play();
        }

        private void SetBulletStartPos()

        {
            BulletCanvasLeft = PaddleCanvasLeft + PaddleWidth / 2;
            BulletCanvasTop = PaddleCanvasTop + PaddleHeight;
        }

        private void setEnemyStartPos()
        {
           _enemyOnScreen = true;
           EnemyVisibility = System.Windows.Visibility.Visible;
           EnemyCanvasLeft = _randomNumber.Next(0,(int) (_windowWidth - EnemyWidth/2));
           _enemySpeed = _randomNumber.Next(1, 3); 
           EnemyCanvasTop = -24;

        }

        public void SetKeyStrokes()
        {
            KeyStrokes = "Arrows for paddle move\n Space for shoot \n Enter for start the game.";
        }
   
        public void CleanUp()
        {
            try
            {
                _enemyHiResTimer.Delete();
                _bulletHiResTimer.Delete();

            }
            catch (Exception e)
            {
                
            }

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
                Thread.Sleep(2);
            }

        }
    }
}