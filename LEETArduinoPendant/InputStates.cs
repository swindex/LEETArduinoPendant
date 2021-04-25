using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PendantNamespace
{
    public class InputStates//: 
    {
        public List<PendantButton> downButtons = new List<PendantButton>();
        public Dictionary<PendantButton, DateTime> btnDownTime= new Dictionary<PendantButton, DateTime>();  
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void NotifyPropertyChanged<T>(string propertyName, T newValue)
        {
            OnPropertyChanged(new InputStatesPropertyChangedEventArgs<T>(propertyName, newValue));
        }

        public bool IsButtonDown(PendantButton btn)
        {
            return downButtons.Contains(btn);
        }

        public void storeBtnDownTime(PendantButton btn)
        {
            if (btnDownTime.ContainsKey(btn))
            
                btnDownTime[btn] = DateTime.UtcNow;
            else
                btnDownTime.Add(btn, DateTime.UtcNow);
        }

        public bool LastDownWasLongTimeAgo(PendantButton btn)
        {
            var dist = DateTime.UtcNow.Subtract(btnDownTime.ContainsKey(btn) ? btnDownTime[btn] : DateTime.UtcNow).TotalMilliseconds;
            return !btnDownTime.ContainsKey(btn) || dist > 500;
        }

        public bool ButtonDown(PendantButton btn)
        {
            if (!LastDownWasLongTimeAgo(btn))
            {
                return false;
            }
            if (!IsButtonDown(btn))
            {
                downButtons.Add(btn);
                storeBtnDownTime(btn);
                return true;
            }
            return false;
        }
        public bool ButtonUp(PendantButton btn)
        {
            if (IsButtonDown(btn))
            {
                downButtons.Remove(btn);
                return true;
            }
            return false;

        }

        private bool enable = false;
        public bool Enable
        {
            get { return enable; }
            set
            {
                if (value != enable)
                {
                    enable = value;
                    NotifyPropertyChanged("Enable", value);
                }
            }
        }

        private int speedPC = 0;
        public int SpeedPC
        {
            get { return speedPC; }
            set
            {
                if (value != speedPC)
                {
                    speedPC = value;
                    NotifyPropertyChanged("SpeedPC", value);
                }
            }
        }

        private int feedPC = 0;
        public int FeedPC
        {
            get { return feedPC; }
            set
            {
                if (value != feedPC)
                {
                    feedPC = value;
                    NotifyPropertyChanged("FeedPC", value);
                }
            }
        }


        private JogAxis jogAxis = JogAxis.NONE;
        public JogAxis JogAxis
        {
            get { return jogAxis; }
            set
            {
                if (value != jogAxis)
                {
                    jogAxis = value;
                    NotifyPropertyChanged("JogAxis", value);
                }
            }
        }

        private MPGDirecton mpgDirection = MPGDirecton.NONE;
        public MPGDirecton MpgDirection
        {
            get { return mpgDirection; }
            set
            {
                if (value != mpgDirection)
                {
                    mpgDirection = value;
                }
            }
        }

        private int mpgStepMultiplier = 0;
        public int MpgStepMultiplier
        {
            get { return mpgStepMultiplier; }
            set
            {
                if (value != mpgStepMultiplier)
                {
                    mpgStepMultiplier = value;
                    NotifyPropertyChanged("MpgStepMultiplier", value);
                }
            }
        }

    }

    public class InputStatesPropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public virtual T NewValue { get; private set; }

        public InputStatesPropertyChangedEventArgs(string propertyName, T newValue)
            : base(propertyName)
        {
            NewValue = newValue;
        }
    }

    public enum JogAxis
    {
        NONE = 0,
        X = 1,
        Y = 2,
        Z = 3,
        A = 4
    }

    public enum MPGDirecton
    {
        NONE,
        Positive,
        Negative
    }



    public enum PendantButton
    {
        Enable,
        M1,
        M2,
        SafeZ,
        JogPos,
        JogNeg,
        Start,
        Pause,
        Stop
    }
}
