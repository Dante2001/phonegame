﻿using UnityEngine;
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

    public override void Attack()
    {
        base.Attack();
        currentState = new AttackState(details);
        currentState.Attack();
    }

}
