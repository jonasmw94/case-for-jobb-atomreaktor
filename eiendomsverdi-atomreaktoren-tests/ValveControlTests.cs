using eiendomsverdi_atomreaktoren.Interfaces;
using eiendomsverdi_atomreaktoren.Services;

namespace eiendomsverdi_atomreaktoren_tests;

public class ValveControlTests
{
    [Fact]
    public void TestThatValveGoesToOpeningStateCorrectly()
    {
        var valveControl = new ValveControl();
        valveControl.InteractWithValve(eiendomsverdi_atomreaktoren.Enums.ValveState.OPENING);
        Assert.Equal(0, valveControl.GetValveOpeningPercent());
        valveControl.UpdateValveStateAfterTSeconds(0.5f);
        Assert.True(valveControl.GetValveOpeningPercent() > 0);
    }

    [Fact]
    public void TestThatValveCantGoToClosingStateWhenInOpeningState()
    {
        var valveControl = new ValveControl();
        valveControl.InteractWithValve(eiendomsverdi_atomreaktoren.Enums.ValveState.OPENING);
        valveControl.UpdateValveStateAfterTSeconds(0.5f);
        valveControl.InteractWithValve(eiendomsverdi_atomreaktoren.Enums.ValveState.CLOSING);
        var preChangeStateOpeningPercent = valveControl.GetValveOpeningPercent();
        valveControl.UpdateValveStateAfterTSeconds(0.5f);

        Assert.True(valveControl.GetValveOpeningPercent() > preChangeStateOpeningPercent);
    }

    [Fact]
    public void TestThatValveCanCloseAfterBeingFullyOpened()
    {
        var valveControl = new ValveControl();
        valveControl.InteractWithValve(eiendomsverdi_atomreaktoren.Enums.ValveState.OPENING);
        valveControl.UpdateValveStateAfterTSeconds(2f);
        valveControl.InteractWithValve(eiendomsverdi_atomreaktoren.Enums.ValveState.CLOSING);
        valveControl.UpdateValveStateAfterTSeconds(2f);
        Assert.Equal(0, valveControl.GetValveOpeningPercent());
    }
}
