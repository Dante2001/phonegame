using UnityEngine;
using System.Collections;

public class FlyBackState : CharacterState {

    protected bool isFlyingBack = false;
    // might use this
    protected float flyTime = 0f;

    public FlyBackState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (flyTime > 0f)
        {
            flyTime -= Time.deltaTime;
            return this;
        }
        else
        {
            details.EndFlyBack();
            return new DefaultState(details);
        }
    }

    public override void FlyBack(GameObject attacker)
    {
        if (!isFlyingBack)
        {
            isFlyingBack = true;
            details.StartFlyBack(attacker);
            flyTime = details.flyTime;
        }
    }
}
