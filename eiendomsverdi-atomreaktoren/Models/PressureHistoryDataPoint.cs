using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eiendomsverdi_atomreaktoren.Enums;

namespace eiendomsverdi_atomreaktoren.Models;

public class PressureHistoryDataPoint
{
    public Guid Id { get; set; }
    public float Time { get; init; }
	public float Pressure { get; init; }
	public int PowerOutputLevelAtTime { get; init; }
}
