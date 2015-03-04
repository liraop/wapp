using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

// Threading
using System.Threading;

// debug
using System.Diagnostics;


namespace SampleThread
{
    public partial class Model
    {

        private Random _randomNumber;
        // threads
        private Thread _threadA = null;
        private Thread _threadB = null;

       //Create two flags for thread status
        private bool _threadAisSuspended = false;
        private bool _threadBisSuspended = false;

        //thread start
        private ThreadStart threadAStart = null;
        private ThreadStart threadBStart = null;

        /// <summary>
        /// constructor
        /// </summary>
        public Model()
        {
            _randomNumber = new Random();

        }

       
        public String ButtonClicked(String buttonName)
        {
            if (buttonName.Contains("Start"))
            {
                return Start(buttonName);
            }
            else if (buttonName.Contains("Stop"))
            {
                return Stop(buttonName);
            }
            else if (buttonName.Contains("Suspend"))
            {
                return Suspend(buttonName);
            }
            else if (buttonName.Contains("Resume"))
            {
                return Resume(buttonName);
            }

            return "Unknown Button Clicked\n";
        }

        
        String Start(String name)
        {
            if (name.Contains("A"))
            {
                if (_threadA == null)
                {
                    threadAStart = new ThreadStart(ThreadAFunction);
                    _threadA = new Thread(threadAStart);
                    _threadA.Start();
                    return "Thread A is started";
                }
                else
                {
                    return "Thread already started";
                }
            }
            else if (name.Contains("B"))
            {
                if (_threadB == null)
                {
                    threadBStart = new ThreadStart(ThreadBFunction);
                    _threadB = new Thread(threadBStart);
                    _threadB.Start();
                    return "Thread B is started";
                }
                else
                {
                    return "Thread already started";
                }
            }

            return "";
        }

       
        String Stop(String name)
        {
            if (name.Contains("A"))
            {
                if (_threadA != null && _threadA.IsAlive)
                {
                    _threadA.Abort();
                    _threadA = null;
                    return "Threat A aborted.";
                }
                else
                {
                    return "Failed. Thread is not running.";
                }
            }
            if (name.Contains("B"))
            {
                if (_threadB != null && _threadB.IsAlive)
                {
                    _threadB.Abort();
                    _threadB = null;
                    return "Threat B aborted.";
                }
                else
                {
                    return "Failed. Thread is not running.";
                }
            }

            return "";
        }

      
        String Suspend(String name)
        {
            if (name.Contains("A"))
            {
                if (_threadA != null && _threadA.IsAlive)
                {
                    _threadAisSuspended = true;
                    
                    return "Thread A is suspended.";
                }
                else
                {
                    return "Failed. Thread A is not running.";
                }
            }
            else if (name.Contains("B"))
            {
                if (_threadB != null && _threadB.IsAlive)
                {
                    _threadBisSuspended = true;
                    
                    return "Thread B is suspended.";
                }
                else
                {
                    return "Failed. Thread B is not running.";
                }
            }

            return "";
        }

        /// <summary>
        /// resume a suspended thread
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        String Resume(String name)
        {
            if (name.Contains("A"))
            {
                if (_threadA != null && _threadA.IsAlive && _threadAisSuspended)
                {
                    //_threadA.Resume();
                    _threadAisSuspended = false;
                    return "Threat A resumed.";
                }
                else
                {
                    return "Failed. Thread A is not running or suspended.";
                }
            }
            else if (name.Contains("B"))
            {
                if (_threadB != null && _threadB.IsAlive && _threadBisSuspended)
                {
                    //_threadB.Resume();
                    _threadBisSuspended = false;
                    return "Threat B resumed.";
                }
                else
                {
                    return "Failed. Thread B is not running or suspended.";
                }
            }

            return "";
        }

        /// <summary>
        /// Thread A
        /// </summary>
        void ThreadAFunction()
        {
            try
            {
               //Implement Thread A Functionality
                while (true)
                {
                    Thread.Sleep(500);
                    if (_threadAisSuspended) continue;
                    ThreadAData = _randomNumber.Next().ToString();
                }
            }
            catch (ThreadAbortException)
            {
                Debug.Write("Thread A Aborted\n");
            }
        }

        void ThreadBFunction()
        {
            try
            {
               //Implement Thread B Functionality
                while (true)
                {
                    Thread.Sleep(500);
                    if (_threadBisSuspended) continue;
                    ThreadBData = _randomNumber.Next().ToString();
                }
            }
            catch (ThreadAbortException)
            {
                Debug.Write("Thread B Aborted\n");
            }
        }

        public void CleanUp()
        {
           //Implement CleanUp Functionality
            if (_threadA != null && _threadA.IsAlive)
            {
                _threadA.Abort();
                _threadA = null;
            }

            if (_threadB != null && _threadB.IsAlive)
            {
                _threadB.Abort();
                _threadB = null;
            }
        }

    }
}
