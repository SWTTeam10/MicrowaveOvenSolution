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
    public class IT1_CookControllerPowerTube
    {
        private IOutput _output;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ICookController _cookController;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _powerTube = new PowerTube(_output);
            _display = Substitute.For<IDisplay>();
            _cookController = new CookController(_timer, _display, _powerTube);
        }


        [TestCase(60,10)]
        public void Start_Start_OutlineTurnedOff(int power, int time)
        {
            _cookController.StartCooking(power, time);

            _output.Received().OutputLine($"PowerTube works with {power} %");
        }

        [Test]
        public void Stop_StartAndStop_OutlineTurnedOff()
        {
            _cookController.StartCooking(60,10);
            _cookController.Stop();
          
            _output.Received().OutputLine($"PowerTube turned off");        
        }

        [Test]
        public void Stop_NoStart_DidNotRecivedOutline()
        {
            _cookController.Stop();
            _output.DidNotReceive().OutputLine($"PowerTube turned off");
        }

    }
}
