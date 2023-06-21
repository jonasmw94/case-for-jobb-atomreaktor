using System;
using eiendomsverdi_atomreaktoren.Interfaces;

namespace eiendomsverdi_atomreaktoren.Services
{
	public class PressureSensor : IPressureSensor
    {
        private readonly IReactorPressure _reactorPressure;

        public PressureSensor(IReactorPressure reactorPressure)
        {
            _reactorPressure = reactorPressure ?? throw new ArgumentNullException(nameof(reactorPressure));
        }

        public float GetValue()
        {
            return ReactorPressure.currentPressurePercent;
        }
    }
}
