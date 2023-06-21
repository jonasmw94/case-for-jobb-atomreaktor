using System;
namespace eiendomsverdi_atomreaktoren.Interfaces
{
	public interface IReactorPressure
	{
		public void CalculatePressureAfterTSeconds(float t);
	}
}
