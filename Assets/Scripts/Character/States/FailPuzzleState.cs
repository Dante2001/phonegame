using UnityEngine;
using System.Collections;

public class FailPuzzleState : CharacterState {

    protected bool isFailing = false;
    protected float failTime;

    public FailPuzzleState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (failTime > 0)
            failTime -= Time.deltaTime;
        else
        {
            details.RestartPuzzle();
            currentState = new DefaultAIState(details);
        }
        return currentState;
    }

    public override void FailPuzzle()
    {
        if (!isFailing)
        {
            Debug.Log("FAIL");
            isFailing = true;
            failTime = details.puzzleLoseTime;
        }
    }

    public override void FlyBack(GameObject attacker) { }
}
