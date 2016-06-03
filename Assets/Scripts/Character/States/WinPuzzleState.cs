using UnityEngine;
using System.Collections;

public class WinPuzzleState : CharacterState {

    protected bool isWinning = false;
    protected float winTime;

    public WinPuzzleState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (winTime > 0)
            winTime -= Time.deltaTime;
        else
        {
            details.WinFirewall();
            GameManager.isAI = false;
            currentState = new DefaultState(GameManager.playerDetails);
        }
        return currentState;
    }

    public override void WinPuzzle()
    {
        if (!isWinning)
        {
            details.animator.SetTrigger("toWin");
            details.PlaySFX("winPuzzle");
            isWinning = true;
            winTime = details.puzzleWinTime;
        }
    }

    public override void FlyBack(GameObject attacker) { }

}
