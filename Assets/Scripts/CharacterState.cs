﻿using UnityEngine;
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
    public virtual void Spell() { }
    public virtual void FlyBack() { }
    public virtual void Recover() { }
    public virtual void Stagger() { }

}