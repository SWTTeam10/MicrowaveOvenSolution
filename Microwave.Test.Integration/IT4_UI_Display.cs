using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT4_UI_Display
    {
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;
        private CookController _cook;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startButton;
        private IDoor _door;
        private ILight _light;
        private IUserInterface _ui;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cook = new CookController(_timer, _display, _powerTube);
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _ui = new UserInterface(_powerButton, _timeButton, _startButton, _door, _display, _light, _cook);
            _cook.UI = _ui;
        }

        [Test]
        public void OnPowerPressed_DefaultPowerValue_DisplayShows50W()
        {
            _ui.OnPowerPressed(_powerButton,EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50")));
        }

        [Test]
        public void OnTimePressed_DefaultTimeValuek_DisplayShows1Minut()
        {
            _ui.OnPowerPressed(_powerButton, EventArgs.Empty);
            _ui.OnTimePressed(_timeButton,EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void OnStartCancelPressed_TimesRunOut_DisplayCleared()
        {
            _ui.OnPowerPressed(_powerButton, EventArgs.Empty);
            _ui.OnTimePressed(_timeButton, EventArgs.Empty);
            _ui.OnStartCancelPressed(_startButton,EventArgs.Empty);
            _ui.CookingIsDone();
            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
