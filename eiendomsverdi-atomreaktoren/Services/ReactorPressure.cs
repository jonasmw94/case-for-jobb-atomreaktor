using System;
using eiendomsverdi_atomreaktoren.Interfaces;

namespace eiendomsverdi_atomreaktoren.Services;

public class ReactorPressure : IReactorPressure
{
    private const float PRESSURE_CHANGE_FACTOR_CLOSED_VALVE_PERCENT = .03f;
    private const float PRESSURE_CHANGE_FACTOR_OPEN_VALVE_PERCENT = -.06f;
    private const float REACTOR_INSTABILITY_FACTOR = 2f;

    private readonly IValveControl _valveControl;

    public static float currentPressurePercent { get; private set; }

    public ReactorPressure(IValveControl valveControl)
    {
        _valveControl = valveControl ?? throw new ArgumentNullException(nameof(valveControl));
    }

    public void CalculatePressureAfterTSeconds(float t)
    {
        var closedPercentage = Math.Abs(1 - _valveControl.GetValveOpeningPercent());

        var pressureChange = closedPercentage * PRESSURE_CHANGE_FACTOR_CLOSED_VALVE_PERCENT +
            _valveControl.GetValveOpeningPercent() * PRESSURE_CHANGE_FACTOR_OPEN_VALVE_PERCENT;
        var instabilityValue = GenerateInstabilityValuePercent();

        currentPressurePercent = MakePressurePercentageValid(currentPressurePercent + pressureChange + instabilityValue);
    }

    private float MakePressurePercentageValid(float newPressure)
    {
        return Math.Clamp(newPressure, 0f, 1f);
    }

    private float GenerateInstabilityValuePercent()
    {
        Random rand = new Random();
        var coinFlip = rand.NextInt64(0, 2);

        if (coinFlip == 0)
        {
            var randomNumberMinusOneToOne = (float)(rand.NextDouble() * 2 - 1);
            return randomNumberMinusOneToOne / 100 * REACTOR_INSTABILITY_FACTOR;
        }

        return 0;
    }
}
