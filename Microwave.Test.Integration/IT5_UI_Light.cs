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
    [TestFixture]
    class IT5_UI_Light
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
            _powerButton = Substitute.For<IButton>();
            _startButton = Substitute.For<IButton>();
            _timerButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>(); 

            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cook = new CookController(_timer,_display,_powerTube);
            _light = new Light(_output);
            _ui = new UserInterface(_powerButton, _timerButton, _startButton, _door, _display, _light,
                _cook);
            _cook.UI = _ui;

        }

        [Test]
        public void OnDoorOpened_TurnOn_LightIsOn()
        {
            _ui.OnDoorOpened(_door,EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void OnDoorClosed_TurnOnTurnOff_LightIsOff()
        {
            _ui.OnDoorOpened(_door, EventArgs.Empty);
            _ui.OnDoorClosed(_door, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void OnStartCancelledPressed_TurnOnTurnOffTurnOn_LightIsOn()
        {
            _ui.OnDoorOpened(_door, EventArgs.Empty);
            _ui.OnDoorClosed(_door, EventArgs.Empty);
            _ui.OnStartCancelPressed(_startButton,EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void CookingIsDone_TurnOnTurnOffTurnOnTurnOff_LightIsOff()
        {
            _ui.OnDoorOpened(_door, EventArgs.Empty);
            _ui.OnDoorClosed(_door, EventArgs.Empty);
            _ui.OnStartCancelPressed(_startButton, EventArgs.Empty);
            _ui.CookingIsDone(); 
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

    }
}
