using UnityEngine;
using System.Collections;

public class DeathState : CharacterState
{

    protected bool isDieing = false;
    protected float deathTime;

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

    public override CharacterState UpdateState()
    {
        if (deathTime > 0)
        {
            deathTime -= Time.deltaTime;
            return currentState;
        }
        else
        {
            details.Respawn();
            return new DefaultState(details);
        }
    }

    public override CharacterState CheckAlive()
    {
        return currentState;
    }
}
