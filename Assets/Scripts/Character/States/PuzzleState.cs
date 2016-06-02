using UnityEngine;
using System.Collections;

public class PuzzleState : CharacterState {

    protected float timeTillStart;
    protected bool isStarting = false;
    protected FirewallManager firewallManager;

    public PuzzleState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (timeTillStart > 0)
        {
            timeTillStart -= Time.deltaTime;
        }
        else
        {
            currentState = new DefaultAIState(details);
            GameManager.isAI = true;
            firewallManager.StartFirewall();
        }
        return currentState;
    }

    public override void Puzzle(FirewallManager fwm)
    {
        if (!isStarting)
        {
            timeTillStart = details.puzzleTime;
            isStarting = true;
            firewallManager = fwm;
        }
    }

}
