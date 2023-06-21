using System;
namespace eiendomsverdi_atomreaktoren.Interfaces;

public interface ISimulator
{
	public void SimulateStep(float stepDurationSeconds, int powerOutputLevel);
}
