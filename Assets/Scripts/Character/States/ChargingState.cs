using UnityEngine;
using System.Collections;

public class ChargingState : CharacterState {

    public ChargingState(CharacterDetails dets) : base(dets) { }

    public override void Move(int x, int z)
    {
        details.SetVelocityX(x);
        details.SetVelocityZ(z);
        if (x != 0 || z != 0)
            details.DrainBatteryFromMovement();
    }

    public override void Charging(bool onPlate)
    {
        if (onPlate)
            details.RechargeBattery();
        else
            currentState = new DefaultAIState(details);
    }

    public override void FlyBack(GameObject attacker) { }

}
