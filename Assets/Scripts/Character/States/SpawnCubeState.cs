using UnityEngine;
using System.Collections;

public class SpawnCubeState : CharacterState {

    protected bool isSpawning = false;
    protected float spawnTime;
    protected bool wonPuzzle = false;

    public SpawnCubeState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (spawnTime > 0)
            spawnTime -= Time.deltaTime;
        else if (spawnTime <= 0)
        {
            if (wonPuzzle)
            {
                currentState = new WinPuzzleState(details);
                currentState.WinPuzzle();
            }
            else
                currentState = new DefaultAIState(details);
        }
        return currentState;
    }

    public override void SpawnCube()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnTime = details.spawnCubeTime;
            wonPuzzle = details.SpawnCube();
        }
    }

    public override void FlyBack(GameObject attacker) { }

}
