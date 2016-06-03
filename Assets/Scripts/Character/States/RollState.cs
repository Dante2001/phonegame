using UnityEngine;
using System.Collections;

public class RollState : CharacterState {

    protected bool isRolling = false;
    protected Vector2 rollDirection;
    protected float rollTime;
    protected float multiplier = 1f;

    public RollState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (rollTime > 0)
        {
            rollTime -= Time.deltaTime;
            details.SetRollVelocity((int)rollDirection.x, (int)rollDirection.y, multiplier);
            multiplier *= details.rollMultiplier;
            return currentState;
        }
        else
            return new DefaultState(details);
    }

    public override void Roll(int x, int z)
    {
        if (!isRolling)
        {
            float rollDir;
            if (z > 0)
                rollDir = 0f;
            else if (z < 0)
                rollDir = 0.5f;
            else if (x > 0)
                rollDir = 1f;
            else
                rollDir = 1.5f;
            details.animator.SetFloat("DirectionRoll", rollDir);
            details.PlaySFX("roll");
            int rand = Random.Range(0, 8);
            if (rand == 4)
                details.PlayOther("paiOther");
            details.animator.SetTrigger("toRoll");
            isRolling = true;
            rollDirection = new Vector2(x, z);
            rollTime = details.rollTime;
        }
    }

    public override void FlyBack(GameObject attacker) { }
}
