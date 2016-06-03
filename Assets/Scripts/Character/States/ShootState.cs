using UnityEngine;
using System.Collections;

public class ShootState : CharacterState {

    protected bool isShooting = false;
    protected float shootingTimeLeft = 0f;

    public ShootState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (isShooting)
            shootingTimeLeft -= Time.deltaTime;
        if (shootingTimeLeft <= 0f)
        {
            details.UseBattery(details.fireballCost);
            details.FireBullet();
            return new DefaultState(details);
        }
        return currentState;
    }

    public override void Shoot()
    {
        base.Shoot();
        
        if (!isShooting)
        {
            details.animator.SetTrigger("toFireball");
            details.PlaySFX("fireball");
            isShooting = true;            
            shootingTimeLeft += details.shootTime;
        }
        //else if (details.CheckBattery(details.fireballCost))
        //{
        //    details.FireBullet();
        //    isShooting = true;
        //    details.UseBattery(details.fireballCost);
        //    shootingTimeLeft += details.shootTime;
        //}
        
    }

    public override void Move(int x, int z)
    {
        base.Move(x, z);
        //details.SetShootVelocity(x, z);
    }

}
