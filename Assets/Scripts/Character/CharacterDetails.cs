using UnityEngine;
using System.Collections;

public class CharacterDetails {

    private Vector3 velocity;
    private Rigidbody rigidbody;
    private AttackHitboxLogic attackHitbox;
    private RangedAttackLogic rangedAttackLogic;
    private StungunLogic stungunLogic;
    private Hitpoints hitpoints;
    private BatteryCharge battery;

    public float defaultSpeed = 4f;

    public float rollTime = 0.4f;
    public float rollSpeed = 15f;

    public float attackTime = .5f;
    public float attackMoveSpeed = 2f;

    public float sprintSpeed = 8f;

    public float shootTime = 0.2f;
    public float shootMoveSpeed = 3f;
    public float fireballSpeed = 5f;

    public float stungunMoveSpeed = 4f;

    public string enemyTag = "Monster";

    public float flyTime = 0.4f;

    public int previousX;
    public int previousZ;

    public Transform lastRespawn;
    public float deathTime = 1f;
    public float healTime = 0.2f;

    public int maxHP = 4;
    public float batteryRecharge = 10f;
    public float batterySlowRecharge = 7f;
    public float batteryBP = 40f;

    public float healCost = 100f;
    public float fireballCost = 30f;
    public float stungunCost = 33f;

    // not used?
    public int facing; // 0 up 1 right 2 down 3 left

    public CharacterDetails(Rigidbody rb, AttackHitboxLogic ahl, RangedAttackLogic ral, StungunLogic sl,
        Hitpoints hp, BatteryCharge bat)
    {
        velocity = new Vector3(0, 0, 0);
        rigidbody = rb;
        attackHitbox = ahl;
        rangedAttackLogic = ral;
        stungunLogic = sl;
        hitpoints = hp;
        battery = bat;
        hitpoints.InitializeHP(maxHP);
        battery.InitializeBattery(batteryRecharge, batterySlowRecharge, batteryBP);
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

    public void SetStungunVelocity(int x, int z)
    {
        velocity.x = x * stungunMoveSpeed;
        velocity.z = z * stungunMoveSpeed;
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

    public void  ActivateStungunHitbox()
    {
        stungunLogic.Activate(previousX, previousZ);
    }

    public void DeactivateStungunHitbox()
    {
        stungunLogic.Deactivate();
    }

    public void FireBullet()
    {
        rangedAttackLogic.FireBullet(fireballSpeed);
    }

    public GameObject GetSelf()
    {
        return rigidbody.gameObject;
    }

    public string GetEnemyTag()
    {
        return enemyTag;
    }

    public void StartFlyBack(GameObject attacker)
    {
        float force = 2400;
        float xForce;
        float zForce;
        if (attacker.transform.position.x > rigidbody.position.x)   
        {
            xForce = -force;
        }
        else
        {
            xForce = force;
        }
        if (attacker.transform.position.z > rigidbody.position.z)
        {
            zForce = -force;
        }
        else
        {
            zForce = force;
        }

        rigidbody.AddForce(xForce, force / 2, zForce);
        //Debug.Log("applied force");
    }

    public void EndFlyBack()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public bool IsAlive()
    {
        if (hitpoints.CheckHP() <= 0)
            return false;
        return true;
    }

    public void Respawn()
    {
        rigidbody.position = lastRespawn.position;
        hitpoints.Respawn();
        battery.Respawn();
    }

    public void LoseHP()
    {
        hitpoints.LoseHP();
    }

    public bool CheckBattery(float cost)
    {
        return battery.CheckAmount(cost);
    }

    public void UseBattery(float amount)
    {
        battery.DrainBattery(amount);
    }

    public void RegainHP()
    {
        hitpoints.GainHP();
    }

    public void UpdateDetails()
    {
        rigidbody.velocity = velocity;
        velocity = Vector3.zero;
    }
	
}
