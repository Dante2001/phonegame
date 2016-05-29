using UnityEngine;
using System.Collections;

public class CharacterDetails {

    private Vector3 velocity;
    private Rigidbody rigidbody;
    private AttackHitboxLogic attackHitbox;
    private RangedAttackLogic rangedAttackLogic;
    public float defaultSpeed = 4f;
    public float rollTime = 0.4f;
    public float rollSpeed = 15f;
    public float attackTime = .5f;
    public float attackMoveSpeed = 2f;
    public float sprintSpeed = 8f;
    public float shootTime = 0.2f;
    public float shootMoveSpeed = 3f;
    public float bulletSpeed = 5f;
    public string enemyTag = "Monster";
    public int previousX;
    public int previousZ;
    // not used?
    public int facing; // 0 up 1 right 2 down 3 left

    public CharacterDetails(Rigidbody rb, AttackHitboxLogic ahl, RangedAttackLogic ral)
    {
        velocity = new Vector3(0, 0, 0);
        rigidbody = rb;
        attackHitbox = ahl;
        rangedAttackLogic = ral;
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

    public void SetSprintVelocity(int x, int z)
    {
        velocity.x = x * sprintSpeed;
        velocity.z = z * sprintSpeed;
        previousX = x;
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

    public void SetShootVelocity(int x, int z)
    {
        velocity.x = x * shootMoveSpeed;
        velocity.z = z * shootMoveSpeed;
    }

    public void ActivateHitbox()
    {
        attackHitbox.Activate(previousX, previousZ);
    }

    public void DeactivateHitbox()
    {
        attackHitbox.Deactivate();
    }

    public void FireBullet(GameObject target)
    {
        rangedAttackLogic.FireBullet(target, bulletSpeed);
    }

    public GameObject GetSelf()
    {
        return rigidbody.gameObject;
    }

    public string GetEnemyTag()
    {
        return enemyTag;
    }

    public void UpdateDetails()
    {
        rigidbody.velocity = velocity;
        velocity = Vector3.zero;
    }
	
}
