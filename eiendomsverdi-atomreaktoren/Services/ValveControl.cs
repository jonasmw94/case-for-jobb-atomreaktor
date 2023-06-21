using System;
using eiendomsverdi_atomreaktoren.Enums;
using eiendomsverdi_atomreaktoren.Interfaces;

namespace eiendomsverdi_atomreaktoren.Services;

public class ValveControl : IValveControl
{
    private const float VALVE_OPEN_TIME_SECONDS = 2f;
    private readonly object valveControlLock = new object();

    private static ValveState state = ValveState.READY;
    private static float ValveOpeningPercent = 0f;

    public void InteractWithValve(ValveState newState)
    {
        lock (valveControlLock)
        {
            if (state is not ValveState.READY)
            {
                return;
            }
            state = newState;
        }
    }

    public float GetValveOpeningPercent()
    {
        return ValveOpeningPercent;
    }

    // Vil holde meg til interfacet som definert i oppgave.
    // Ville egentlig heller kalt den AttemptClose i mitt tilfelle.
    public void Close()
    {
        InteractWithValve(ValveState.CLOSING);
    }

    // Vil holde meg til interfacet som definert i oppgave.
    // Ville egentlig heller kalt den AttemptOpen i mitt tilfelle.
    public void Open()
    {
        InteractWithValve(ValveState.OPENING);
    }

    public void UpdateValveStateAfterTSeconds(float t)
    {
        if (state is ValveState.READY)
        {
            return;
        }

        var direction = state == ValveState.OPENING ? 1 : -1;
        var deltaOpeningPercent = direction * t / VALVE_OPEN_TIME_SECONDS;

        UpdateValveOpeningFactor(ValveOpeningPercent + deltaOpeningPercent);
    }

    private void UpdateValveOpeningFactor(float newOpeningPercent)
    {
        ValveOpeningPercent = Math.Clamp(newOpeningPercent, 0f, 1f);
        if (ValveOpeningPercent == 0f || ValveOpeningPercent == 1f)
        {
            state = ValveState.READY;
        }
    }
}
