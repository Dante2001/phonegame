using UnityEngine;
using System.Collections;

public class CharacterState {

    protected CharacterState currentState;
    protected CharacterDetails details;

    public CharacterState(CharacterDetails dets)
    {
        currentState = this;
        details = dets;
    }

    public virtual CharacterState UpdateState()
    {
        return currentState;
    }
    public virtual void Attack() { }
    public virtual void Roll(int x, int z) { }
    public virtual void Sprint() { }
    public virtual void Heal() { }
    public virtual void Move(int x, int z) { }
    public virtual void Shoot() { }
    public virtual void Stungun() { }
    public virtual void FlyBack(GameObject attacker) 
    {
        currentState = new FlyBackState(details);
        currentState.FlyBack(attacker);
    }
    public virtual void Puzzle(FirewallManager fwm) 
    {
        currentState = new PuzzleState(details);
        currentState.Puzzle(fwm);
    }
    public virtual void SpawnCube() { }
    public virtual void DespawnCube() { }
    public virtual void Charging(bool onPlate) { }
    public virtual void FailPuzzle() { }
    public virtual void WinPuzzle() { }
    //public virtual void Recover() { }
    //public virtual void Stagger() { }
    public virtual CharacterState CheckAlive()
    {
        if (!details.IsAlive())
            currentState = new DeathState(details);
        return currentState;
    }
}
