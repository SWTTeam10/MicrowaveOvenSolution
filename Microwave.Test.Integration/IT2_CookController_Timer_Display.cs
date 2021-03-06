﻿using System;
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
    class IT2_CookController_Timer_Display
    {
        private ITimer _timer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _output;
        private ICookController _cookController;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
        }

        [Test]
        public void StartCooking_TimesRunOut_PowerTubeTurnedOff()
        {
            _cookController.StartCooking(50, 2); // Tiden står i sekunder

            Thread.Sleep(2050);

            _output.Received().OutputLine($"PowerTube turned off");
        }

    }
}
