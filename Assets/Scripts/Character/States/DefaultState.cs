using UnityEngine;
using System.Collections;

public class DefaultState : CharacterState {

    public DefaultState(CharacterDetails dets) : base(dets) { }

    public override void Move(int x, int z)
    {
        base.Move(x,z);
        details.SetVelocityX(x);
        details.SetVelocityZ(z);
        //if (x == 1)
          //  details.SetVelocityX(1);
        //else if (x == -1)
          //  details.SetVelocityX(-1);

//        if (z == 1)
  //          details.SetVelocityZ(1);
    //    else if (z == -1)
      //      details.SetVelocityZ(-1);
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
        currentState = new SprintState(details);
        currentState.Sprint();
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
        if (details.CheckBattery(details.fireballCost))
        {
            currentState = new ShootState(details);
            currentState.Shoot();
        }
    }

    public override void Stungun()
    {
        base.Stungun();
        if (details.CheckBattery(details.stungunCost))
        {
            currentState = new StungunState(details);
            currentState.Stungun();
        }
    }

    public override void Heal()
    {
        base.Heal();
        if (details.CheckBattery(details.healCost))
        {
            currentState = new HealState(details);
            currentState.Heal();
        }
    }

}
