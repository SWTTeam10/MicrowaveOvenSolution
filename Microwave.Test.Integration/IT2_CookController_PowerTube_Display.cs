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
    class IT2_CookController_PowerTube_Display
    {
        private IOutput _output;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ICookController _uut;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _uut = new CookController(_timer, _display, _powerTube);
        }

        [Test]
        public void XXXX_XXXXX_OutlineTurnedXXXX()
        {
            _uut.StartCooking(65, 100);
            _timer.TimeRemaining.Returns(100); 
            int min = _timer.TimeRemaining/60;
            int sec = _timer.TimeRemaining % 60; 
            _display.ShowTime(min,sec);
            _output.Received().OutputLine($"Display shows: {min:D2}:{sec:D2}");
        }

    }
}
