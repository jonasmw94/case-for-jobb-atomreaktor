using System;
using eiendomsverdi_atomreaktoren.Enums;
using eiendomsverdi_atomreaktoren.Models;

namespace eiendomsverdi_atomreaktoren.Interfaces;

public interface IReactorControlUnit
{
	public IEnumerable<PressureHistoryDataPoint> GetPressureHistory();
	public void PressureControl(float timeSinceLastControlSeconds, int powerOutputLevel);
}
