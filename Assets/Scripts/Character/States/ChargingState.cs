using UnityEngine;
using System.Collections;

public class ChargingState : CharacterState {

    public ChargingState(CharacterDetails dets) : base(dets) 
    {
        details.PlaySFX("charging");
        details.animator.SetTrigger("toCharge");
    }

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
        {
            currentState = new DefaultAIState(details);
            details.StopSFX();
        }
    }

    public override void FlyBack(GameObject attacker) { }

}
