using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class IT7_UI_Button
    {
        private IUserInterface _ui;
        private IDoor _door;
        private IButton _startButton;
        private IButton _timerButton;
        private IButton _powerButton;
        private ILight _light;
        private IOutput _output;
        private CookController _cook;
        private IDisplay _display;
        private ITimer _timer;
        private IPowerTube _powerTube;


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _startButton = new Button();
            _timerButton = new Button();   
            _door = new Door();
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cook = new CookController(_timer, _display, _powerTube);
            _light = new Light(_output);
            _ui = new UserInterface(_powerButton, _timerButton, _startButton, _door, _display, _light,
                _cook);
            _cook.UI = _ui;
        }

        [Test]
        public void PressPowerButton__PressPowerOnes__PowerIsDefault50()
        {
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void PressPowerButton__PressPowerTwoTimes__PowerIsDefault100()
        {
            _powerButton.Press();
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("100")));
        }

        [Test]
        public void PressTimeButton__PressTimeOnes__TimeIsDefault1minut()
        {
            _powerButton.Press();
            _timerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void PressTimeButton__PressTimeTwoTimes__TimeIsDefault1minut()
        {
            _powerButton.Press();
            _timerButton.Press();
            _timerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }

        [Test] 
        public void PressStartCancelButton_StartAndCancelled_DisplayCleared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startButton.Press();
            _startButton.Press();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [Test] 
        public void PressStartCancelButton_OnlyStart_DidNotDisplayCleared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startButton.Press();
            _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
