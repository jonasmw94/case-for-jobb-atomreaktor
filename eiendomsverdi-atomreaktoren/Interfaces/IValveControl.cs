using System;
namespace eiendomsverdi_atomreaktoren.Interfaces;

public interface IValveControl
{
    public float GetValveOpeningPercent();
	public void Open();
	public void Close();
    public void UpdateValveStateAfterTSeconds(float t);
}

