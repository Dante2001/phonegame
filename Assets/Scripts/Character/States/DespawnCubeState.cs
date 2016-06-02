using UnityEngine;
using System.Collections;

public class DespawnCubeState : CharacterState {

    protected bool isDespawning;
    protected float despawnTime;

    public DespawnCubeState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (despawnTime > 0)
            despawnTime -= Time.deltaTime;
        else
        {
            details.DespawnCube();
            currentState = new DefaultAIState(details);
        }
        return currentState;
    }

    public override void DespawnCube()
    {
        if (!isDespawning)
        {
            isDespawning = true;
            despawnTime = details.despawnCubeTime;
        }
    }
}
