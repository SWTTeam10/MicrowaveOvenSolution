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
using NUnit.Framework.Internal;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture()]
    class IT3_UI_Cookcontroller
    {
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;
        private ICookController _cook;
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
        }

        [Test]
        public void OnStartCancelPressed_Default_PowerIs50()
        {
            _ui.OnPowerPressed(_powerButton, EventArgs.Empty);
            _ui.OnTimePressed(_timeButton, EventArgs.Empty);

            _ui.OnStartCancelPressed(_startButton, EventArgs.Empty);

            _output.Received().OutputLine($"PowerTube works with {50} %");
        }

        [Test]
        public void CookingIsDone_hej_hej()
        {
            _cook.StartCooking(50,2);
            //_timer.Expired += (sender,args)

            Thread.Sleep(2050);

            //_output.Received().OutputLine($"Display cleared");
            _output.Received().OutputLine($"PowerTube turned off");
        }
    }
}
