// Student : Pedro de Oliveira Lira - pdeolive@syr.edu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace homework01
{
    class ModelHW : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IntegerSet setA;
        private IntegerSet setB;

        public ModelHW()
        {
            setA = new IntegerSet();
            setB = new IntegerSet();
        }

        public void Update()
        {
            ClearSets();

            if (TextToIntset(InputA, setA) && TextToIntset(InputB, setB))
            {
                StatusOut = "Input properly typed.";
                Union = setA.Union(setB).ToString();
                Intersec = setA.Intersection(setB).ToString();
            }
            else
            {
                StatusOut = "Inputs not properly set.\nEvery number n (n < 100) must be followed by comma, as follows:\n 1,2,3,4";
            }
        }
       
        private string _inputA;
        public string InputA
        {
            get { return _inputA; }
            set
            {
                _inputA = value;
                OnPropertyChanged("InputA");

            }
        }

        private string _inputB;
        public string InputB
        {
            get { return _inputB; }
            set
            {
                _inputB = value;
                OnPropertyChanged("InputB");

            }
        }

        private string _union;
        public string Union
        {
            get { return _union; }
            set
            {
                _union = value;
                OnPropertyChanged("Union");

            }
        }

        private string _intersec;
        public string Intersec
        {
            get { return _intersec; }
            set
            {
                _intersec = value;
                OnPropertyChanged("Intersec");

            }
        }

        private string _sout;
        public string StatusOut
        {
            get { return _sout; }
            set
            {
                _sout = value;
                OnPropertyChanged("StatusOut");

            }
        }

        private Boolean TextToIntset(string text, IntegerSet set)
        {

            // check if the input text is empty or null
            if (String.IsNullOrWhiteSpace(text))
            {
                set.Clear();
                return true;
            }
           // if the text has characters, try to add in a set
           try
            {
                foreach (var x in text.Split(','))
                {
                    try
                    {
                        set.InsertElement(Int32.Parse(x));
                    }
                    catch (FormatException e)
                    {
                        set.Clear();
                        StatusOut = e.Message;
                        return false;
                    }
                    catch (ArgumentNullException e)
                    {
                        set.Clear();
                        StatusOut = e.Message;
                        return false;
                    }
                    catch (OverflowException e)
                    {
                        set.Clear();
                        StatusOut = e.Message;
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                StatusOut = e.Message;
                return false;
            }

            return true;
        }

        // to clear the sets and status message
        private void ClearSets()
        {
            StatusOut = "";
            setA.Clear();
            setB.Clear();
        }

        private void OnPropertyChanged( string propertyName)
        {
            if ( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
