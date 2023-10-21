using Solid.Arduino;
using Solid.Arduino.Firmata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;


namespace PendantNamespace
{
    public class Pendant : IDisposable
    {
        public InputStates Inputs = new InputStates();
            
        private Dictionary<int, long> analogInputsCashe = new Dictionary<int,long>();

        private ArduinoSession session;
        private ISerialConnection connection;

        public bool Enabled {
            get { return Inputs.Enable; }
        }

        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        public event EventHandler<MPGRotateEventArgs> MPGRotated;

        public event EventHandler<PendantButtonEventArgs> ButtonDown;
        protected virtual void onButtonDown(PendantButtonEventArgs e)
        {
            ButtonDown?.Invoke(this, e);
        }
        public event EventHandler<PendantButtonEventArgs> ButtonUp;
        protected virtual void onButtonUp(PendantButtonEventArgs e)
        {
            ButtonUp?.Invoke(this, e);
        }

        public Pendant()
        {
            Inputs.PropertyChanged += onInputStatesPropertyChanged;
            connection = EnhancedSerialConnection.Find();
            if (connection == null)
            {
                Console.WriteLine("Arduino connection not found! Waiting 1s");
                Thread.Sleep(1000);
            }
            connection = EnhancedSerialConnection.Find();
            if (connection == null)
            {
                Console.WriteLine("Arduino connection not found!");
                return;
            }

            WatchPinState(connection);
        }

        private void onInputStatesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
            //var ee = (InputStatesPropertyChangedEventArgs<object>) e;

            //Console.WriteLine($"Speed: {ins.SpeedPC} Feed: {ins.FeedPC} Dir: {ins.JogDirection} Step: {ins.MpgStepMultiplier}");
        }

        private void WatchPinState(ISerialConnection connection)
        {

            session = new ArduinoSession(connection);

            session.SetAnalogReportMode(0, true);
            session.SetAnalogReportMode(1, true);
            session.SetAnalogReportMode(2, true);
            session.SetAnalogReportMode(3, true);
            //session.SetAnalogReportMode(4, true);

            session.SetDigitalReportMode(0, true);
            session.SetDigitalReportMode(1, true);

            session.SetDigitalPinMode(2, PinMode.InputPullup);
            session.SetDigitalPinMode(3, PinMode.InputPullup);
            session.SetDigitalPinMode(4, PinMode.InputPullup);
            session.SetDigitalPinMode(5, PinMode.InputPullup);
            session.SetDigitalPinMode(6, PinMode.InputPullup);
            session.SetDigitalPinMode(7, PinMode.InputPullup);
            session.SetDigitalPinMode(8, PinMode.InputPullup);
            session.SetDigitalPinMode(9, PinMode.InputPullup);
            session.SetDigitalPinMode(10, PinMode.InputPullup);

            session.SetDigitalPinMode(11, PinMode.InputPullup);

            session.SetDigitalPinMode(12, PinMode.InputPullup);
            //session.SetDigitalPinMode(13, PinMode.InputPullup);

            //session.MessageReceived += MessageReceived;
            session.DigitalStateReceived += DigitalStateReceived;
            session.AnalogStateReceived += AnalogStateReceived;
            session.StringReceived += StringReceived;
                
            

        }

        

        private void StringReceived(object sender, StringEventArgs eventArgs)
        {
            //Console.WriteLine(sender);
            //Console.WriteLine(eventArgs.Text);
        }

        private void MessageReceived(object sender, FirmataMessageEventArgs eventArgs)
        {
            //Console.WriteLine(sender);
            //Console.WriteLine(eventArgs.Value);
        }

        private void AnalogStateReceived(object sender, FirmataEventArgs<AnalogState> eventArgs)
        {
            if (!Inputs.Enable)
            {
                return;
            }


            if (analogInputsCashe.ContainsKey(eventArgs.Value.Channel))
            {
                if (analogInputsCashe[eventArgs.Value.Channel] - 1 > eventArgs.Value.Level || analogInputsCashe[eventArgs.Value.Channel] + 1 < eventArgs.Value.Level)
                {
                    analogInputsCashe[eventArgs.Value.Channel] = eventArgs.Value.Level;
                    Console.WriteLine($"Analog Ch: {eventArgs.Value.Channel} Val: {eventArgs.Value.Level}");
                }
            }
            else
            {
                analogInputsCashe[eventArgs.Value.Channel] = eventArgs.Value.Level;
                Console.WriteLine($"Analog Ch: {eventArgs.Value.Channel} Val: {eventArgs.Value.Level}");
            }


            //Console.WriteLine(sender);
            if (eventArgs.Value.Channel == 0)
            {
                if (eventArgs.Value.Level >= 1024 - 50)
                    Inputs.JogAxis = JogAxis.X;
                else if (eventArgs.Value.Level >= 512 - 50 && eventArgs.Value.Level <= 512 + 50)
                    Inputs.JogAxis = JogAxis.Y;
                else if (eventArgs.Value.Level >= 340 - 50 && eventArgs.Value.Level <= 340 + 50)
                    Inputs.JogAxis = JogAxis.Z;
                else if (eventArgs.Value.Level >= 255 - 50 && eventArgs.Value.Level <= 255 + 50)
                    Inputs.JogAxis = JogAxis.A;
                else
                    Inputs.JogAxis = JogAxis.NONE;
            }
            if (eventArgs.Value.Channel == 1)
            {
                if (eventArgs.Value.Level >= 1024 - 50)
                    Inputs.MpgStepMultiplier = 1;
                else if (eventArgs.Value.Level >= 512 - 50 && eventArgs.Value.Level <= 512 + 50)
                    Inputs.MpgStepMultiplier = 2;
                else if (eventArgs.Value.Level >= 340 - 50 && eventArgs.Value.Level <= 340 + 50)
                    Inputs.MpgStepMultiplier = 3;
                else if (eventArgs.Value.Level >= 255 - 50 && eventArgs.Value.Level <= 255 + 50)
                    Inputs.MpgStepMultiplier = 4;
                else
                    Inputs.JogAxis = 0;
            }
            if (eventArgs.Value.Channel == 2)
            {
                int val = Convert.ToInt32(Convert.ToDouble(eventArgs.Value.Level) / 1024 * 200);
                Inputs.SpeedPC = val;
            }
            if (eventArgs.Value.Channel == 3)
            {
                int val = Convert.ToInt32(Convert.ToDouble(eventArgs.Value.Level) / 1024 * 200);
                Inputs.FeedPC = val;
            }

        }



        private void DigitalStateReceived(object sender, FirmataEventArgs<DigitalPortState> eventArgs)
        {
            //Console.WriteLine(sender);
            var port = eventArgs.Value.Port;

            string binaryString = Convert.ToString(eventArgs.Value.Pins, 2); // Convert to binary
            binaryString = binaryString.PadLeft(9, '0');
            Console.WriteLine($"Digital Port: {port} Pins : {binaryString}");


            if (port == 0 && eventArgs.Value.IsSet(2))
            {
                if (PressButton(PendantButton.Enable))
                {
                    Inputs.Enable = !Inputs.Enable;
                    session.SetDigitalPin(13, Inputs.Enable);
                }
            } else
                ReleaseButton(PendantButton.Enable);


            if (!Inputs.Enable)
            {
                return;
            }


            if (port == 1 && eventArgs.Value.IsSet(2))
                PressButton(PendantButton.M1);
            else 
                ReleaseButton(PendantButton.M1);
            
            if (port == 1 && eventArgs.Value.IsSet(1))
                PressButton(PendantButton.M2);
            else 
                ReleaseButton(PendantButton.M2);

            if (port == 1 && eventArgs.Value.IsSet(0))
                PressButton(PendantButton.SafeZ);
            else
                ReleaseButton(PendantButton.SafeZ);

            if (port == 0 && eventArgs.Value.IsSet(7))
                PressButton(PendantButton.JogPos);
            else
                ReleaseButton(PendantButton.JogPos);

            if (port == 0 && eventArgs.Value.IsSet(6))
                PressButton(PendantButton.JogNeg);
            else
                ReleaseButton(PendantButton.JogNeg);


            if (port == 0 && eventArgs.Value.IsSet(3))
                PressButton(PendantButton.Start);
            else
                ReleaseButton(PendantButton.Start);

            if (port == 0 && eventArgs.Value.IsSet(4))
                PressButton(PendantButton.Pause);
            else
                ReleaseButton(PendantButton.Pause);


            if (port == 0 && eventArgs.Value.IsSet(5))
                PressButton(PendantButton.Stop);
            else
                ReleaseButton(PendantButton.Stop);

            if (port == 1 && eventArgs.Value.IsSet(4) && !eventArgs.Value.IsSet(3))
            {
                if (Inputs.MpgDirection == MPGDirecton.NONE)
                {
                    //Console.WriteLine($"MPG Start Positive");
                    Inputs.MpgDirection = MPGDirecton.Positive;
                } else if (Inputs.MpgDirection == MPGDirecton.Positive)
                {
                    //encoder lost a step. Register it as one step
                    //Console.WriteLine($"MPG Incr Positive");
                    MPGRotated?.Invoke(this, new MPGRotateEventArgs() { direction = MPGDirecton.Positive });
                }
            }

            if (port == 1 && !eventArgs.Value.IsSet(4) && eventArgs.Value.IsSet(3))
            {
                if (Inputs.MpgDirection == MPGDirecton.NONE)
                {
                    //Console.WriteLine($"MPG Start Negative");
                    Inputs.MpgDirection = MPGDirecton.Negative;

                } else if (Inputs.MpgDirection == MPGDirecton.Negative) {
                    //encoder lost a step. Register it as one step
                    //Console.WriteLine($"MPG Incr Negative");
                    MPGRotated?.Invoke(this, new MPGRotateEventArgs() { direction = MPGDirecton.Negative });
                }
            }

            if (port == 1 && eventArgs.Value.IsSet(4) && eventArgs.Value.IsSet(3))
            {
                if (Inputs.MpgDirection == MPGDirecton.Positive)
                {
                    //Console.WriteLine($"MPG Step Positive");
                    MPGRotated?.Invoke(this, new MPGRotateEventArgs() { direction = MPGDirecton.Positive });
                }
                else if (Inputs.MpgDirection == MPGDirecton.Negative)
                {
                    //Console.WriteLine($"MPG Step Negative");
                    MPGRotated?.Invoke(this, new MPGRotateEventArgs() { direction = MPGDirecton.Negative });
                }

            }

            if (port == 1 && !eventArgs.Value.IsSet(4) && !eventArgs.Value.IsSet(3))
            {
                if (Inputs.MpgDirection != MPGDirecton.NONE)
                {
                    Inputs.MpgDirection = MPGDirecton.NONE;
                }
            }



            /*Console.Write($"port: {eventArgs.Value.Port} Pins 0: {(eventArgs.Value.IsSet(0) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 1: {(eventArgs.Value.IsSet(1) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 2: {(eventArgs.Value.IsSet(2) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 3: {(eventArgs.Value.IsSet(3) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 4: {(eventArgs.Value.IsSet(4) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 5: {(eventArgs.Value.IsSet(5) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 6: {(eventArgs.Value.IsSet(6) ? 'X' : ' ') }");
            Console.Write($"port: {eventArgs.Value.Port} Pins 7: {(eventArgs.Value.IsSet(7) ? 'X' : ' ') }");
            Console.WriteLine("-------------------------------------------------------------------------------------");*/
        }

        private bool PressButton(PendantButton btn)
        {
            if (Inputs.ButtonDown(btn))
            {
                onButtonDown(new PendantButtonEventArgs() { button = btn });
                return true;
            }
            return false;
        }

        private void ReleaseButton(PendantButton btn)
        {
            if (Inputs.ButtonUp(btn))
                onButtonUp(new PendantButtonEventArgs() { button = btn });
        }


        private  void DisplayPortCapabilities()
        {
            using (var session = new ArduinoSession(new EnhancedSerialConnection("COM4", SerialBaudRate.Bps_57600)))
            {
                BoardCapability cap = session.GetBoardCapability();
                Console.WriteLine();
                Console.WriteLine("Board Capability:");

                foreach (var pin in cap.Pins)
                {
                    Console.WriteLine("Pin {0}: Input: {1}, Output: {2}, Analog: {3}, Analog-Res: {4}, PWM: {5}, PWM-Res: {6}, Servo: {7}, Servo-Res: {8}, Serial: {9}, Encoder: {10}, Input-pullup: {11}",
                        pin.PinNumber,
                        pin.DigitalInput,
                        pin.DigitalOutput,
                        pin.Analog,
                        pin.AnalogResolution,
                        pin.Pwm,
                        pin.PwmResolution,
                        pin.Servo,
                        pin.ServoResolution,
                        pin.Serial,
                        pin.Encoder,
                        pin.InputPullup);
                }
            }
        }

        public void Dispose()
        {
            if (session != null)
            {
                session.SetAnalogReportMode(0, false);
                session.SetAnalogReportMode(1, false);
                session.SetAnalogReportMode(2, false);
                session.SetAnalogReportMode(3, false);
                session.SetDigitalReportMode(0, false);
                session.SetDigitalReportMode(1, false);
                Inputs.Enable = false;
                session.SetDigitalPin(13, Inputs.Enable);
                session.ResetBoard();
                session.Dispose();
                connection.Close();
            }
            if (connection != null)
            {
                connection.Close();
            }
            Thread.Sleep(500);

        }
    }

    public class PendantButtonEventArgs : EventArgs
    {
        public PendantButton button;
    }

    public class MPGRotateEventArgs : EventArgs
    {
        public MPGDirecton direction;
    }

}
