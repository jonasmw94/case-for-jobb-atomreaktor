using System;
namespace eiendomsverdi_atomreaktoren.Models;

public record SimulationModelDto(int Steps, float TimePerStepSeconds, int reactorPowerOutputLevel);
