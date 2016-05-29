using UnityEngine;
using System.Collections;

public class FlyBackState : CharacterState {

    protected bool isFlyingBack = false;
    protected float flyTime = 0f;

    public FlyBackState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (isFlyingBack && flyTime >= 0f)
        {
            flyTime -= Time.deltaTime;
            return currentState;
        }
        else
        {
            details.EndFlyBack();
            //Debug.Log("stop flying");
            return new DefaultState(details);
        }
    }

    public override void FlyBack(GameObject attacker)
    {
        if (!isFlyingBack)
        {
            flyTime = details.flyTime;
            isFlyingBack = true;
            details.StartFlyBack(attacker);
            //Debug.Log("start flyign");
        }
    }
}
