using System;
using eiendomsverdi_atomreaktoren.Services;
using eiendomsverdi_atomreaktoren.Interfaces;
using eiendomsverdi_atomreaktoren.Models;
using eiendomsverdi_atomreaktoren.Enums;
using Microsoft.AspNetCore.Mvc;
using eiendomsverdi_atomreaktoren.Db;

namespace eiendomsverdi_atomreaktoren.Controllers;

[Route("api/reactor-simulator")]
[ApiController]
public class ReactorSimulatorController : ControllerBase
{
	private ISimulator _reactorSimulator;
	private IReactorControlUnit _reactorControlUnit;

    public ReactorSimulatorController(ISimulator reactorSimulator, IReactorControlUnit reactorControlUnit)
	{
        _reactorSimulator = reactorSimulator;
		_reactorControlUnit = reactorControlUnit;

    }

    [HttpPost("simulate")]
	public void Simulate(SimulationModelDto simulationModel)
	{
		for (var i = 0; i < simulationModel.Steps; i++)
		{
            _reactorSimulator.SimulateStep(simulationModel.TimePerStepSeconds, simulationModel.reactorPowerOutputLevel);
		}
	}

	[HttpGet("pressure-history")]
	public IEnumerable<PressureHistoryDataPoint> GetPressureHistory(int nLatestPoints = 200)
	{
		return _reactorControlUnit.GetPressureHistory();

    }

}
