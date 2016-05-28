using UnityEngine;
using System.Collections;

public class CharacterDetails {

    private Vector3 velocity;
    private Rigidbody rigidbody;
    private AttackHitboxLogic attackHitbox;
    public float defaultSpeed = 4f;
    public float rollTime = 0.7f;
    public float rollSpeed = 10f;
    public float attackTime = .5f;
    public float attackMoveSpeed = 2f;
    public int previousX;
    public int previousZ;
    // not used?
    public int facing; // 0 up 1 right 2 down 3 left

    public CharacterDetails(Rigidbody rb, AttackHitboxLogic ahl)
    {
        velocity = new Vector3(0, 0, 0);
        rigidbody = rb;
        attackHitbox = ahl;
    }

    // might want to change this to be SetWalkVelocity
    // theseare used when using default move speed
    // technically all these methods could be combined
    // we'd just need to require the state to pass a
    // parameter letting this know what "speed" to multipy by
    public void SetVelocityX(int x)
    {
        velocity.x = x * defaultSpeed;
        previousX = x;
    }

    public void SetVelocityZ(int z)
    {
        velocity.z = z * defaultSpeed;
        previousZ = z;
    }

    public void SetRollVelocity(int x, int z)
    {
        velocity.x = x * rollSpeed;
        velocity.z = z * rollSpeed;
        previousX = x;
        previousZ = z;
    }

    public void SetAttackVelocity(int x, int z)
    {
        velocity.x = x * attackMoveSpeed;
        velocity.z = z * attackMoveSpeed;
    }

    public void ActivateHitbox()
    {
        attackHitbox.Activate(previousX, previousZ);
    }

    public void DeactivateHitbox()
    {
        attackHitbox.Deactivate();
    }

    public void UpdateDetails()
    {
        rigidbody.velocity = velocity;
        velocity = Vector3.zero;
    }
	
}
