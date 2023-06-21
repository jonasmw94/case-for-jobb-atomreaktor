using System;
using eiendomsverdi_atomreaktoren.Db;
using eiendomsverdi_atomreaktoren.Enums;
using eiendomsverdi_atomreaktoren.Interfaces;
using eiendomsverdi_atomreaktoren.Models;
using Microsoft.Extensions.Caching.Memory;

namespace eiendomsverdi_atomreaktoren.Services;

public class ReactorControlUnit : IReactorControlUnit
{
    private readonly IPressureSensor _pressureSensor;
    private readonly IValveControl _valveControl;
    private readonly DataContext _db;

    private static float time = 0f;

    public ReactorControlUnit(IPressureSensor pressureSensor, IValveControl valveControl, DataContext db)
    {
        _pressureSensor = pressureSensor ?? throw new ArgumentNullException(nameof(pressureSensor));
        _valveControl = valveControl ?? throw new ArgumentNullException(nameof(valveControl));
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public IEnumerable<PressureHistoryDataPoint> GetPressureHistory()
    {
        return _db.PressureHistoryDataPoints
                .OrderBy(x => x.Time);
    }

    // Should be called when pressure changes
    public void PressureControl(float timeSinceLastControlSeconds, int powerOutputLevel)
    {
        time += timeSinceLastControlSeconds;

        var pressureBounds = GetPressureBounds(powerOutputLevel);
        var sensorPressureReading = _pressureSensor.GetValue();

        if (sensorPressureReading < pressureBounds.MinValue)
        {
            _valveControl.Close();
        }
        else if (sensorPressureReading > pressureBounds.MaxValue)
        {
            _valveControl.Open();
        }

        UpdatePressureHistory(powerOutputLevel);
    }

    private PressureBoundsModel GetPressureBounds(int powerOutputLevel)
    {
        if (powerOutputLevel == (int) PowerOutput.LOW)
        {
            return new PressureBoundsModel(0.2f, 0.75f);
        }
        else if (powerOutputLevel == (int) PowerOutput.MEDIUM)
        {
            return new PressureBoundsModel(0.4f, 0.80f);
        }

        return new PressureBoundsModel(0.55f, 0.75f);
    }

    private void UpdatePressureHistory(int powerOutputLevel)
    {
        var historyObj = new PressureHistoryDataPoint() { Time = time, Pressure = _pressureSensor.GetValue(), PowerOutputLevelAtTime = powerOutputLevel };
        _db.PressureHistoryDataPoints.Add(historyObj);
        _db.SaveChanges();
    }
}
