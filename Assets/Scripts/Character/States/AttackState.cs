﻿using UnityEngine;
using System.Collections;

public class AttackState : CharacterState {

    protected float attackTime;
    protected bool isAttacking = false;

    public AttackState(CharacterDetails dets) : base(dets) { }

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
            details.DeactivateHitbox();
            return new DefaultState(details);
        }
    }

    public override void Attack()
    {
        if (!isAttacking)
        {
            details.animator.SetTrigger("toAttack");
            details.PlaySFX("attack");
            int rand = Random.Range(0, 8);
            if (rand == 4)
                details.PlayOther("otherAttack");
            isAttacking = true;
            attackTime = details.attackTime;
            details.ActivateHitbox();
        }
        // activate hitbox
    }

    public override void Move(int x, int z)
    {
        details.SetAttackVelocity(x, z);
    }

    public override void FlyBack(GameObject attacker)
    {
        details.DeactivateHitbox();
        base.FlyBack(attacker);
    }

}
