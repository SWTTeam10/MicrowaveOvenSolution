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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT5_UI_Door
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
            _powerButton = Substitute.For<IButton>();
            _startButton = Substitute.For<IButton>();
            _timerButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();

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

    }
}
