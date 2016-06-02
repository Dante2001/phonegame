using UnityEngine;
using System.Collections;

public class DefaultAIState : CharacterState {

    public DefaultAIState(CharacterDetails dets) : base(dets) 
    {
        details.SetPAIsLastPos();
    }

    public override CharacterState UpdateState()
    {
        if (!details.CheckBattery(0.1f))
        {
            currentState = new FailPuzzleState(details);
            currentState.FailPuzzle();
        }
        return currentState;
    }

    public override void Move(int x, int z)
    {
        //Debug.Log("move ai");
        details.SetVelocityX(x);
        details.SetVelocityZ(z);
        if (x != 0 || z != 0)
           details.DrainBatteryFromMovement();
    }

    public override void Charging(bool onPlate)
    {
        if (onPlate)
        {
            currentState = new ChargingState(details);
            currentState.Charging(onPlate);
        }
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
