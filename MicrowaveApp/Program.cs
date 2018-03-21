using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

namespace MicrowaveApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Setup all the objects, 
            var door = new Door();
            var powerButton = new Button();
            var startButton = new Button();
            var timeButton = new Button();
            var output = new Output();
            var light = new Light(output);
            var powerTube = new PowerTube(output);
            var display = new Display(output);
            var timer = new Timer();
            var cookController = new CookController(timer, display, powerTube); 
            var ui = new UserInterface(powerButton, timeButton,startButton,door,display,light,cookController);
            cookController.UI = ui; 

            // Simulate user activities
            door.Open();
            door.Close();
            powerButton.Press();
            timeButton.Press();
            startButton.Press();

            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}
