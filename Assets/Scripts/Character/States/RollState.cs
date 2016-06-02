using UnityEngine;
using System.Collections;

public class RollState : CharacterState {

    protected bool isRolling = false;
    protected Vector2 rollDirection;
    protected float rollTime;

    public RollState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (rollTime > 0)
        {
            rollTime -= Time.deltaTime;
            details.SetRollVelocity((int)rollDirection.x, (int)rollDirection.y);
            return currentState;
        }
        else
            return new DefaultState(details);
    }

    public override void Roll(int x, int z)
    {
        if (!isRolling)
        {
            details.animator.SetTrigger("toRoll");
            isRolling = true;
            rollDirection = new Vector2(x, z);
            rollTime = details.rollTime;
        }
    }

    public override void FlyBack(GameObject attacker) { }
}
