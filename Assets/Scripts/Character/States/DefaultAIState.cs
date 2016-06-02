using UnityEngine;
using System.Collections;

public class DefaultAIState : CharacterState {

    public DefaultAIState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        return currentState;
    }

    public override void Move(int x, int z)
    {
        details.SetVelocityX(x);
        details.SetVelocityZ(z);
    }

    public override void SpawnCube() 
    {
        currentState = new SpawnCubeState(details);
        currentState.SpawnCube();
    }

    public override void DespawnCube() 
    {
        currentState = new DespawnCubeState(details);
        currentState.SpawnCube();
    }

    public override void FailPuzzle() 
    {
        currentState = new FailPuzzleState(details);
        currentState.FailPuzzle();
    }

    public override void WinPuzzle()
    {
        currentState = new WinPuzzleState(details);
        currentState.WinPuzzle();
    }

    public override void Puzzle(FirewallManager fwm) { }
}
