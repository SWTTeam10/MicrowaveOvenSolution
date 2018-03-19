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
    class IT6_UI_Button
    {
        private IUserInterface _userInterface;
        private IDoor _door;
        private IButton _startButton;
        private IButton _timerButton;
        private IButton _powerButton;
        private ILight _light;
        private IOutput _output;
        private ICookController _cookController;
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
            _cookController = new CookController(_timer, _display, _powerTube);
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timerButton, _startButton, _door, _display, _light,
                _cookController);

        }

        [Test]
        public void PressPowerButton__PowerIsOn__PowerIsDefault50()
        {
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void PressTimeButton__TimeIsShown__TimeIsDefault1minut()
        {
            _powerButton.Press();
            _timerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test] // hvorfor går testen igennem hvis vi kun har en startbutton.press()
        public void PressStartCancelButton_StartAllready_DisplayCleared()
        {
            _powerButton.Press();
            _timerButton.Press();
            _startButton.Press();
            _startButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
