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
    class IT2_CookController_PowerTube_Timer_Display
    {
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;
        private ICookController _uut;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _uut = new CookController(_timer, _display, _powerTube);
        }

        [Test]
        public void StartCooking_TimerSetAt2Sec_DisplayShows1sec()
        {
            _uut.StartCooking(50, 2); // Tiden står i sekunder

            Thread.Sleep(1050);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:01")));
        }

    }
}
