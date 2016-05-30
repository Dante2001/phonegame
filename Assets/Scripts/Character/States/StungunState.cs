using UnityEngine;
using System.Collections;

public class StungunState : CharacterState
{

    protected float attackTime;
    protected bool isFiring = false;

    public StungunState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
            // check for hits
            // store hit objects as to not hit them again
            return this;
        }
        else
        {
            details.DeactivateStungunHitbox();
            return new DefaultState(details);
        }
    }

    public override void Stungun()
    {
        if (!isFiring)
        {
            isFiring = true;
            attackTime = details.attackTime;
            details.ActivateStungunHitbox();
            details.UseBattery(details.stungunCost);
        }
        // activate hitbox
    }

    public override void Move(int x, int z)
    {
        details.SetStungunVelocity(x, z);
    }

    public override void FlyBack(GameObject attacker)
    {
        details.DeactivateStungunHitbox();
        base.FlyBack(attacker);
    }

}
