
using PendantNamespace;
using System;
using System.ComponentModel;
using System.Threading;

/**
 * This console app is used for developing the Pendant class.
 * Don't build for Release
 */

namespace DevConsoleApp
{
    class Program
    {
        static Pendant pen = null;
        static int AxisCounter = 0;

        static void Main()
        {
            using (pen = new Pendant())
            {
                pen.ButtonDown += onButtonDown;
                pen.ButtonUp += onButtonUp;
                pen.MPGRotated += onMPGRotated;
                pen.PropertyChanged += onPropertyChanged;

                Console.ReadKey(true);
                Thread.Sleep(1000);
            }
        }

        private static void onPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine($"            Feed %: {pen.Inputs.FeedPC}");
            Console.WriteLine($"            Speed %: {pen.Inputs.SpeedPC}");
            Console.WriteLine($"            Jog Axis: {pen.Inputs.JogAxis.ToString()}");
            Console.WriteLine($"            Step : {pen.Inputs.MpgStepMultiplier.ToString()}");
        }

        private static void onMPGRotated(object sender, MPGRotateEventArgs e)
        {
            AxisCounter += (e.direction == MPGDirecton.Positive ? 1 : -1);
            Console.WriteLine($"        MPG Pos:  { AxisCounter }");
        }

        private static void onButtonUp(object sender, PendantButtonEventArgs e)
        {
            Console.WriteLine($"        Button Up: {e.button}");
        }

        static void onButtonDown(object sender, PendantButtonEventArgs e)
        {
            Console.WriteLine($"        Button Down: {e.button}");
        }
    }
}
