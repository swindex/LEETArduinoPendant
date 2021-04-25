using PendantNamespace;
using System;
using System.ComponentModel;
using System.Threading.Tasks;


/**
 * Copy Solid.Arduino.dll into C:\UCCNC\
 * Copy LEETArduinoPendant.dll into C:\UCCNC\Plugins\
 */

namespace LEETArduinoPendant
{
    public class UCCNCplugin //Class name must be UCCNCplugin to work! 
    {
        public Plugininterface.Entry UC;
        public Pendant pen = null;
        public bool loopstop = false;
        public bool loopworking = false;
        public bool InitCalled = false;
        public string Error = "";

        public JogAxis curr_jog_axis = JogAxis.NONE;
        public bool curr_jog_dir = false;


        public UCCNCplugin()
        {

        }

        public Plugininterface.Entry.Pluginproperties Getproperties_event(Plugininterface.Entry.Pluginproperties Properties)
        {
            Properties.author = "Eldar Gerfanov";
            Properties.pluginname = "LEET Arduino Pendant";
            Properties.pluginversion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Properties;
        }

        public void Init_event(Plugininterface.Entry UC)
        {
            this.UC = UC;
            //Console.WriteLine($"Init_event");
            try
            {
                Init();
            } catch (Exception Ex) {
                Error += Ex.Message + " At:" + Ex.StackTrace;
            }               
        }

        public void Showup_event()
        {
            
            

        }

        public void Init()
        {
            InitCalled = true;
            //Console.WriteLine($"Init {pen}");
            //Start in the background!
            if (pen == null)
                new Task(() => {
                    //Console.WriteLine($"Initializing Pendant !");
                    pen = new Pendant();

                    pen.ButtonDown += onButtonDown;
                    pen.ButtonUp += onButtonUp;
                    pen.MPGRotated += onMPGRotated;
                    pen.PropertyChanged += onPropertyChanged;
                    //Console.WriteLine($"Pendant initialized!");
                }).Start();

            
        }

        public void Loop_event()
        {
            if (pen == null) {
                //Console.WriteLine($"InitCalled {InitCalled} Pendant is not initialized!");
                //Console.WriteLine(Error);
                return;
            }
            if (!pen.Enabled) return;

            
        }

        void Shutdown_event()
        {
            if (this.pen != null)
                this.pen.Dispose();
        }

        private void onMPGRotated(object sender, MPGRotateEventArgs e)
        {
            if (isHomed())
            {
                //Console.WriteLine($"Handwheel {e.direction}");
                if (e.direction == PendantNamespace.MPGDirecton.Positive)
                    JogWheel(1);
                if (e.direction == PendantNamespace.MPGDirecton.Negative)
                    JogWheel(-1);
            }

        }

        private void onPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Console.WriteLine($"FeedPC: {pen.Inputs.FeedPC}");
            this.UC.Setslider( pen.Inputs.FeedPC, 232);
            this.UC.Setslider( pen.Inputs.SpeedPC, 233);

            var JogFeedPC = pen.Inputs.FeedPC;
            if (JogFeedPC > 100) JogFeedPC = 100;
            UC.Setfield(false, JogFeedPC, 913);

            if (e.PropertyName == "JogAxis" && curr_jog_axis != JogAxis.NONE)
            {
                JogStop(curr_jog_axis, curr_jog_dir);
            }
        }

        private bool isHomed()
        {
            return (UC.GetLED(56) && UC.GetLED(57) && UC.GetLED(58)); // If machine was not homed then it is unsafe to move in machine coordinates, stop here...
        }

        private bool isIdle()
        {
            return UC.GetLED(18);
        }

        private bool isRunning()
        {
            return UC.GetLED(19);
        }

        private void onButtonDown(object sender, PendantButtonEventArgs e)
        {
            if (!isHomed()) return;

            if (e.button == PendantNamespace.PendantButton.Start && isIdle())
            {
                this.UC.Callbutton(128);
            }
            if (e.button == PendantNamespace.PendantButton.Stop && isRunning())
            {
                this.UC.Callbutton(130);
            }
            if (e.button == PendantNamespace.PendantButton.Pause)
            {
                this.UC.Callbutton(522);
            }


            if (e.button == PendantNamespace.PendantButton.SafeZ)
            {
                this.UC.Callbutton(216);//safeZ
            }

            if (e.button == PendantNamespace.PendantButton.M1)
            {
                this.UC.Callbutton(196); //Tool Measure
            }
            if (e.button == PendantNamespace.PendantButton.M2)
            {
                this.UC.Callbutton(193); //safeZ Y Forward
            }

            if (e.button == PendantNamespace.PendantButton.JogPos)
            {
                //Console.WriteLine($"JOG+ Down!, Direction: {pen.Inputs.JogAxis}");
                Jog(pen.Inputs.JogAxis, false);
            }
            if (e.button == PendantNamespace.PendantButton.JogNeg)
            {
               // Console.WriteLine($"JOG- Down!, Direction: {pen.Inputs.JogAxis}");
                Jog(pen.Inputs.JogAxis, true);
            }
        }

        void Jog(PendantNamespace.JogAxis axis, bool neg)
        {
            if (axis == PendantNamespace.JogAxis.NONE) return;

            //this.UC.AddLinearMoveRel(CInt(direction), stp, Math.Abs(steps), Settings.feedrate, steps < 0);
            var JogFeedPC = UC.Getfielddouble(false, 913);
            if (JogFeedPC > 100) JogFeedPC = 100;
            this.UC.JogOnSpeed(Convert.ToInt32(axis) - 1, neg, JogFeedPC);

            curr_jog_axis = axis;
            curr_jog_dir = neg;
        }

        void JogStop(PendantNamespace.JogAxis axis, bool neg)
        {
            if (axis == PendantNamespace.JogAxis.NONE) return;
            this.UC.JogOnSpeed(Convert.ToInt32(axis) - 1, neg, 0);

            curr_jog_axis = JogAxis.NONE;

        }

        void JogWheel(int steps) {

            double dist = Math.Abs(steps * 0.0001 * Math.Pow(10, (pen.Inputs.MpgStepMultiplier - 1)));

            var JogFeedPC = UC.Getfielddouble(false, 913);
            if (JogFeedPC > 100) JogFeedPC = 100;

            //Console.WriteLine($"Handwheel dist {dist}");

            UC.AddLinearMoveRel(Convert.ToInt32(pen.Inputs.JogAxis) - 1, dist, Math.Abs(steps), JogFeedPC, steps < 0);

   

        }

        private void onButtonUp(object sender, PendantButtonEventArgs e)
        {
            if (!isHomed()) return;

            if (e.button == PendantNamespace.PendantButton.JogPos)
            {
                JogStop(pen.Inputs.JogAxis, false);
            }
            if (e.button == PendantNamespace.PendantButton.JogNeg)
            {
                JogStop(pen.Inputs.JogAxis, true);
            }
        }




        //This is a direct function call addressed to this plugin dll
        //The function can be called by macros or by another plugin
        //The passed parameter is an object and the return value is also an object
        public object Informplugin_event(object Message)
        {
            return Message;
        }

        //This is a function call made to all plugin dll files
        //The function can be called by macros or by another plugin
        //The passed parameter is an object and there is no return value
        public void Informplugins_event(object Message)
        {
        }

        //Called when the user presses a button on the UCCNC GUI or if a Callbutton function is executed.
        //The int buttonnumber parameter is the ID of the caller button.
        // The bool onscreen parameter is true if the button was pressed on the GUI and is false if the Callbutton function was called.
        public void Buttonpress_event(int buttonnumber, bool onscreen)
        {
            if (onscreen)
            {
                if (buttonnumber == 128)
                {

                }
            }
        }

        //Called when the user clicks the toolpath viewer
        //The parameters X and Y are the click coordinates in the model space of the toolpath viewer
        //The Istopview parameter is true if the toolpath viewer is rotated into the top view,
        //because the passed coordinates are only precise when the top view is selected.
        public void Toolpathclick_event(double X, double Y, bool Istopview)
        {

        }

        //Called when the user clicks and enters a Textfield on the screen
        //The labelnumber parameter is the ID of the accessed Textfield
        //The bool Ismainscreen parameter is true is the Textfield is on the main screen and false if it is on the jog screen
        public void Textfieldclick_event(int labelnumber, bool Ismainscreen)
        {
            if (Ismainscreen)
            {
                if (labelnumber == 1000)
                {

                }
            }
        }

        //Called when the user enters text into the Textfield and it gets validated
        //The labelnumber parameter is the ID of the accessed Textfield
        //The bool Ismainscreen parameter is true is the Textfield is on the main screen and is false if it is on the jog screen.
        //The text parameter is the text entered and validated by the user
        public void Textfieldtexttyped_event(int labelnumber, bool Ismainscreen, string text)
        {
            if (Ismainscreen)
            {
                if (labelnumber == 1000)
                {

                }
            }
        }

        //Called when the user click an imageview control on the UCCNC GUI.
        //The MouseEventArgs e parameter contains the click coordinates on the control and the mouse button used to click etc.
        //The int labelnumber parameter is the ID of the clicked imageview.
        // The bool onscreen parameter is true if the imageview was clicked on the GUI and is false if it was clicked on the jog screen.
        public void Imageviewclick_event(object e, int labelnumber, bool Ismainscreen)
        {

        }

        //Called when the user presses the Cycle start button and before the Cycle starts
        //This event may be used to show messages or do actions on Cycle start 
        //For example to cancel the Cycle if a condition met before the Cycle starts with calling the Button code 130 Cycle stop
        public void Cyclethreadstart_event()
        {
            //MessageBox.Show("Cycle is starting...");
        }
    }
}