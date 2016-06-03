using UnityEngine;
using System.Collections;

public class DeathState : CharacterState
{

    protected bool isDieing = false;
    protected float deathTime;
    protected bool isRespawned = false;

    public DeathState(CharacterDetails dets) : base(dets) 
    {
        if (!isDieing)
        {
            details.animator.SetTrigger("toDeath");
            details.PlaySFX("death");
            isDieing = true;
            deathTime = details.deathTime;
        }
    }

    public override void FlyBack(GameObject attacker) { }

    public override CharacterState UpdateState()
    {
        if (deathTime > 0)
        {
            deathTime -= Time.deltaTime;
            return currentState;
        }
        else
        {
            details.animator.SetTrigger("toIdle");
            details.Respawn();
            return new DefaultState(details);
        }
    }
    public virtual void Puzzle(FirewallManager fwm) { }
    public override CharacterState CheckAlive()
    {
        return currentState;
    }
}
