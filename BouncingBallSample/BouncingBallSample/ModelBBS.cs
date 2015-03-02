    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// observable collections
using System.Collections.ObjectModel;

// debug output
using System.Diagnostics;

// timer, sleep
using System.Threading;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

// hi res timer
using PrecisionTimers;

// Rectangle
// Must update References manually
using System.Drawing;

// INotifyPropertyChanged
using System.ComponentModel;

// observable collections
using System.Collections.ObjectModel;

namespace BuncingBallSample
{
    public partial class ModelBBS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // provide an observable collection for the bricks
        public ObservableCollection<Brick> BrickCollection;

        //Fields required for the paddle
        private TimerQueueTimer.WaitOrTimerDelegate _paddleTimerCallbackDelegate;
        private TimerQueueTimer _paddleHiResTimer;
        bool _movepaddleLeft = false;
        bool _movepaddleRight = false;
        System.Drawing.Rectangle _paddleRectangle;

        //Fields Required for the ball
        private TimerQueueTimer.WaitOrTimerDelegate _ballTimerCallbackDelegate;
        private TimerQueueTimer _ballHiResTimer;
        private double _ballXMove = 1;
        private double _ballYMove = 1;
        System.Drawing.Rectangle _ballRectangle;
        private bool _moveBall = false;

        //Fields for the Bricks
        // number of brick row and columns
        int _numBrickRows = 5;
        int _numBrickColumns = 15;

        //Random number to generate a random poisition after the ball hits the paddle
        Random _randomNumber = new Random();

        public bool MoveBall
        {
            get { return _moveBall; }
            set { _moveBall = value; }
        }

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

        public void InitModel()
        {

            // note that the brick hight, number of brick columns and rows
            // must match our window demensions.
            double _brickHeight = 25;
            double _brickWidth = _windowWidth / _numBrickColumns;

            // create brick collection
            // place them manually at the top of the item collection in the view
            BrickCollection = new ObservableCollection<Brick>();
            for (int outer = 0; outer < _numBrickRows; outer++)
            {
                for (int inner = 0; inner < _numBrickColumns; inner++)
                {
                    BrickCollection.Add(new Brick()
                    {
                        Fill = System.Windows.Media.Brushes.Red,
                        BrickHeight = _brickHeight,
                        BrickWidth = _brickWidth,
                        BrickVisibility = System.Windows.Visibility.Visible,
                        BrickCanvasLeft = inner * _brickWidth,
                        BrickCanvasTop = _brickHeight * outer
                    });
                }
            }

            _ballTimerCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(BallMMTimerCallback);
            _ballHiResTimer = new TimerQueueTimer();
            try
            {
                _ballHiResTimer.Create(8, 8, _ballTimerCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create Ball timer. Error from GetLastError = {0}", ex.Error);
            }

            _paddleTimerCallbackDelegate = new TimerQueueTimer.WaitOrTimerDelegate(paddleMMTimerCallback);
            _paddleHiResTimer = new TimerQueueTimer();

            try
            {
                // create a Multi Media Hi Res timer.
                _paddleHiResTimer.Create(4, 4, _paddleTimerCallbackDelegate);
            }
            catch (QueueTimerException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Failed to create paddle timer. Error from GetLastError = {0}", ex.Error);
            }
        }

        public void SetStartPosition()
        {
            BallHeight = 25;
            BallWidth = 25;


            BallCanvasLeft = _windowWidth / 2 - BallWidth / 2;
            BallCanvasTop = _windowHeight / 3;

            _moveBall = false;

            paddleWidth = 120;
            paddleHeight = 5;

            paddleCanvasLeft = _windowWidth / 2 - paddleWidth / 2;
            paddleCanvasTop = _windowHeight - paddleHeight;
            _paddleRectangle = new System.Drawing.Rectangle((int)paddleCanvasLeft, (int)paddleCanvasTop, (int)paddleWidth, (int)paddleHeight);
        }
        public void MoveLeft(bool move)
        {
            _movepaddleLeft = move;
        }

        public void MoveRight(bool move)
        {
            _movepaddleRight = move;
        }

        public void CleanUp()
        {
            _ballHiResTimer.Delete();
            _paddleHiResTimer.Delete();
        }

        private void paddleMMTimerCallback(IntPtr pWhat, bool success)
        {

            // start executing callback. this ensures we are synched correctly
            // if the form is abruptly closed
            // if this function returns false, we should exit the callback immediately
            // this means we did not get the mutex, and the timer is being deleted.
            if (!_paddleHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            if (_movepaddleLeft && paddleCanvasLeft > 0)
                paddleCanvasLeft -= 2;
            else if (_movepaddleRight && paddleCanvasLeft < _windowWidth - paddleWidth)
                paddleCanvasLeft += 2;

            _paddleRectangle = new System.Drawing.Rectangle((int)paddleCanvasLeft, (int)paddleCanvasTop, (int)paddleWidth, (int)paddleHeight);


            // done in callback. OK to delete timer
            _paddleHiResTimer.DoneExecutingCallback();
        }

        private void BallMMTimerCallback(IntPtr pWhat, bool success)
        {
            if (!_moveBall)
                return;

            if (!_ballHiResTimer.ExecutingCallback())
            {
                Console.WriteLine("Aborting timer callback.");
                return;
            }

            BallCanvasLeft += _ballXMove;
            BallCanvasTop += _ballYMove;

            if (BallCanvasTop + BallWidth >= _windowHeight)
            {
                _moveBall = false;
            }

            _ballRectangle = new System.Drawing.Rectangle((int)BallCanvasLeft, (int)BallCanvasTop, (int)BallWidth, (int)BallHeight);

            if (_ballRectangle.IntersectsWith(_paddleRectangle))
            {
                _ballYMove = -_ballYMove;
                BallCanvasTop += 2 * _ballYMove;
                BallCanvasLeft += _randomNumber.Next(5);
            }

            if ((BallCanvasLeft + BallWidth >= _windowWidth) ||
                (BallCanvasLeft <= 0))
                _ballXMove = -_ballXMove;

            if (BallCanvasTop <= 0)
                _ballYMove = -_ballYMove;

            bool found = false;
            //// check to see if we hit a visible block
            for (int brick = 0; brick < BrickCollection.Count; brick++)
            {
                // if the current brick is not visible, just continue on to the next brick
                if (BrickCollection[brick].BrickVisibility == Visibility.Hidden)
                    continue;

                //    // make a rectangle for the current brick so we can 
                //    // check for a collision with the ball
                Rectangle brickRectangle = new Rectangle(
                    (int)BrickCollection[brick].BrickCanvasLeft,
                    (int)BrickCollection[brick].BrickCanvasTop,
                    (int)BrickCollection[brick].BrickWidth - 1,
                    (int)BrickCollection[brick].BrickHeight - 1);

                //    // if the brick and ball do not intersect, just keep going
                //    // don't waste time findout out exactly where they intersect, because
                //    // they don'
                if (brickRectangle.IntersectsWith(_ballRectangle) == false)
                    continue;

                //    // ok, find out where they intersect so we can determine which
                //    // direction to bounce the ball
                InterectSide intersection = IntersectsAt(brickRectangle, _ballRectangle);
                switch (intersection)
                {
                    case InterectSide.NONE:
                        break;
                    case InterectSide.BOTTOM:
                    case InterectSide.TOP:
                        if (BrickCollection[brick].BrickVisibility == System.Windows.Visibility.Visible)
                        {
                            BrickCollection[brick].BrickVisibility = System.Windows.Visibility.Hidden;
                            _ballYMove = -_ballYMove;
                            found = true;
                        }
                        break;
                    case InterectSide.LEFT:
                    case InterectSide.RIGHT:
                        if (BrickCollection[brick].BrickVisibility == System.Windows.Visibility.Visible)
                        {
                            BrickCollection[brick].BrickVisibility = System.Windows.Visibility.Hidden;
                            _ballXMove = -_ballXMove;
                            found = true;
                        }
                        break;
                }
                //    // if we found a collision, break;
                if (found) break;
            }
            //// done in callback. OK to delete timer
            _ballHiResTimer.DoneExecutingCallback();
        }

        enum InterectSide { NONE, LEFT, RIGHT, TOP, BOTTOM };
        private InterectSide IntersectsAt(Rectangle brick, Rectangle ball)
        {
            if (brick.IntersectsWith(ball) == false)
                return InterectSide.NONE;

            Rectangle r = Rectangle.Intersect(brick, ball);

            // did we hit the top of the brick
            if (ball.Left + ball.Height - 1 == r.Top &&
                r.Height == 1)
                return InterectSide.TOP;

            if (ball.Top == r.Top &&
                r.Height == 1)
                return InterectSide.BOTTOM;

            if (ball.Left == r.Left &&
                r.Width == 1)
                return InterectSide.RIGHT;

            if (ball.Left + ball.Width - 1 == r.Left &&
                r.Width == 1)
                return InterectSide.LEFT;

            return InterectSide.NONE;
        }
    }
}

