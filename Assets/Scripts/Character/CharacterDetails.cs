using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDetails {

    private Vector3 velocity;
    private NavMeshAgent rigidbody;
    private AttackHitboxLogic attackHitbox;
    private RangedAttackLogic rangedAttackLogic;
    private StungunLogic stungunLogic;
    private CubeSpawnDespawner cubeLogic;
    private Hitpoints hitpoints;
    private BatteryCharge battery;
    private PersonalSoundManager personalSoundManager;
    private Dictionary<string, List<AudioClip>> dictOfClips;

    public Animator animator;

    public float defaultSpeed = 4f;

    public float rollTime = 0.4f;
    public float rollSpeed = 8f;
    public float rollMultiplier = 1f;

    public float attackTime = .5f;
    public float attackMoveSpeed = 2f;

    public float sprintSpeed = 8f;

    public float shootTime = 0.2f;
    public float shootMoveSpeed = 3f;
    public float fireballSpeed = 5f;

    public float stungunMoveSpeed = 4f;
    public float stungunTime = 0.4f;

    public string enemyTag = "Monster";

    public float flyTime = 0.4f;
    public float flySpeed = 20f;
    public Vector3 flyBackVel;

    public int previousX;
    public int previousZ;

    public float puzzleTime = 1f; // time to wait before puzzle starts

    public float spawnCubeTime = 0.4f;
    public float despawnCubeTime = 0.4f;
    public float puzzleLoseTime = 1f;
    public float puzzleWinTime = 1f;

    public Transform lastRespawn;
    public float deathTime = 1f;
    public float healTime = 0.2f;

    public int maxHP = 4;
    public float batteryRecharge = 1f;
    public float batterySlowRecharge = 7f;
    public float batteryBP = 40f;

    public float healCost = 100f;
    public float fireballCost = 30f;
    public float stungunCost = 33f;

    public FirewallManager lastFirewall;
    public float cubeSpawnCost = 30f;
    public float cubeDespawnCost = 20f;
    public float paisMovementCost = 3;
    public float paisBatteryRechargeCost = 40f;

    public float maxCubeSpawnDistance = 2f;
    public float maxCubeDespawnDistance = 2f;

    public Vector3 paisLastPos;

    // not used?
    public int facing; // 0 up 1 right 2 down 3 left

    public CharacterDetails(NavMeshAgent rb, AttackHitboxLogic ahl, RangedAttackLogic ral, StungunLogic sl,
        Hitpoints hp, BatteryCharge bat, CubeSpawnDespawner csd, PersonalSoundManager psm, Dictionary<string, List<AudioClip>> dac)
    {
        velocity = new Vector3(0, 0, 0);
        rigidbody = rb;
        attackHitbox = ahl;
        rangedAttackLogic = ral;
        stungunLogic = sl;
        hitpoints = hp;
        battery = bat;
        cubeLogic = csd;
        personalSoundManager = psm;
        dictOfClips = dac;
        if (hitpoints != null)
            hitpoints.InitializeHP(maxHP);
        if (battery != null)
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

    public void SetRollVelocity(int x, int z, float multiplier)
    {
        velocity.x = x * rollSpeed * multiplier;
        velocity.z = z * rollSpeed * multiplier;
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
        //float force = 2400;
        float xForce;
        float zForce;
        if (attacker.transform.position.x > rigidbody.transform.position.x)   
        {
            xForce = -flySpeed;
        }
        else
        {
            xForce = flySpeed;
        }
        if (attacker.transform.position.z > rigidbody.transform.position.z)
        {
            zForce = -flySpeed;
        }
        else
        {
            zForce = flySpeed;
        }

        flyBackVel = new Vector3(xForce, 0, zForce);
        // cause apparently navmesh work better with isKinematic =[
        //rigidbody.AddForce(xForce, force / 2, zForce);
        //Debug.Log("applied force");
    }

    public void UpdateFlyBack(float multiplier)
    {
        velocity += flyBackVel * multiplier;
    }

    public void EndFlyBack()
    {
        rigidbody.velocity = Vector3.zero;
        //rigidbody.angularVelocity = Vector3.zero;
    }

    public bool IsAlive()
    {
        if (hitpoints.CheckHP() <= 0)
            return false;
        return true;
    }

    public void Respawn()
    {
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(lastRespawn.position, out closestHit, 500, 1))
        {
            rigidbody.transform.position = closestHit.position;
        }
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            monster.GetComponent<MonsterRespawn>().Respawn();
        }
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

    public bool SpawnCube()
    {
        if (cubeLogic.SpawnCube(maxCubeSpawnDistance))
        {
            UseBattery(cubeSpawnCost);
        }
        return lastFirewall.CheckForCompletion();
    }

    public void DespawnCube()
    {
        if (cubeLogic.DespawnCube(maxCubeDespawnDistance))
        {
            battery.RegainBattery(cubeDespawnCost);
        }
    }

    public void RestartPuzzle()
    {
        lastFirewall.FirewallLose();
        cubeLogic.RestartPuzzle();
        battery.Respawn();
    }

    public void WinFirewall()
    {
        lastFirewall.FirewallCompleted();
    }

    public void DrainBatteryFromMovement()
    {
        float drainAmt = (rigidbody.transform.position - paisLastPos).magnitude * paisMovementCost * Time.deltaTime;
        UseBattery(drainAmt);
    }

    public void SetPAIsLastPos()
    {
        paisLastPos = rigidbody.transform.position;
    }

    public void RechargeBattery()
    {
        battery.RegainBattery(Time.deltaTime * paisBatteryRechargeCost);
    }

    public void PlaySFX(string sfx)
    {
        personalSoundManager.PlayEfxRandom(dictOfClips[sfx]);
    }

    public void PlayOther(string sfx)
    {
        personalSoundManager.PlayOtherRandom(dictOfClips[sfx]);
    }

    public void StopSFX()
    {
        personalSoundManager.StopSFX();
    }

    public void StopOther()
    {
        personalSoundManager.StopOther();
    }

    public void UpdateDetails()
    {
        rigidbody.velocity = velocity;
        velocity = Vector3.zero;
    }
	
}
