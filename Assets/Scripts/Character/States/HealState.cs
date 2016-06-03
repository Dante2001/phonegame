using UnityEngine;
using System.Collections;

public class HealState : CharacterState {

    protected float healTime;
    protected bool isHealing = false;

    public HealState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (healTime > 0f)
        {
            healTime -= Time.deltaTime;
        }
        else if (healTime <= 0f)
        {
            details.RegainHP();
            currentState = new DefaultState(details);
        }
        return currentState;
    }

    public override void Heal()
    {
        base.Heal();
        if (!isHealing)
        {
            details.animator.SetTrigger("toHeal");
            details.PlaySFX("heal");
            int rand = Random.Range(0, 8);
            if (rand == 4)
                details.PlayOther("paiOther2");
            isHealing = true;
            healTime = details.healTime;
            details.UseBattery(details.healCost);
        }
    }

}
