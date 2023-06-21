using System;
using eiendomsverdi_atomreaktoren.Interfaces;

namespace eiendomsverdi_atomreaktoren.Services
{
	public class ReactorSimulator : ISimulator
	{
        private readonly IReactorControlUnit _reactorControlUnit;
        private readonly IReactorPressure _pressure;
        private readonly IValveControl _valveControl;
        private readonly object simulationLock = new object();

        public ReactorSimulator(IReactorControlUnit reactorControlUnit, IReactorPressure pressure, IValveControl valveControl)
        {
            _reactorControlUnit = reactorControlUnit ?? throw new ArgumentNullException(nameof(reactorControlUnit));
            _pressure = pressure ?? throw new ArgumentNullException(nameof(pressure));
            _valveControl = valveControl ?? throw new ArgumentNullException(nameof(valveControl));
        }

        public void SimulateStep(float timeSeconds, int powerOutputLevel)
        {
            lock (simulationLock)
            {
                _valveControl.UpdateValveStateAfterTSeconds(timeSeconds);
                _pressure.CalculatePressureAfterTSeconds(timeSeconds);
                _reactorControlUnit.PressureControl(timeSeconds, powerOutputLevel);
            }
        }
    }
}

