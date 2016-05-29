using UnityEngine;
using System.Collections;

public class SprintState : CharacterState {

    public SprintState(CharacterDetails dets) : base(dets) { }

    protected bool isSprinting = false;

    public override CharacterState UpdateState()
    {
        if (!isSprinting)
            return new DefaultState(details);
        else
            isSprinting = false;
        return currentState;
    }

    public override void Move(int x, int z)
    {
        base.Move(x,z);
        details.SetSprintVelocity(x, z);
    }

    public override void Roll(int x, int z)
    {
        base.Roll(x, z);
        currentState = new RollState(details);
        currentState.Roll(x, z);
    }

    public override void Sprint()
    {
        base.Sprint();
        isSprinting = true;
    }

    public override void Attack()
    {
        base.Attack();
        currentState = new AttackState(details);
        currentState.Attack();
    }

    public override void Shoot()
    {
        base.Shoot();
        currentState = new ShootState(details);
        currentState.Shoot();
    }

}
