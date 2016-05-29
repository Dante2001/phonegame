using UnityEngine;
using System.Collections;

public class ShootState : CharacterState {

    protected bool isShooting = false;
    protected float shootingTimeLeft = 0f;

    public ShootState(CharacterDetails dets) : base(dets) { }

    public override CharacterState UpdateState()
    {
        if (!isShooting)
            shootingTimeLeft -= Time.deltaTime;
        if (shootingTimeLeft <= 0f)
        {
            return new DefaultState(details);
        }
        return this;
    }

    public override void Shoot()
    {
        base.Shoot();
        details.FireBullet(GetTarget());
        if (!isShooting)
            isShooting = true;
        shootingTimeLeft += details.shootTime;
    }

    public override void Move(int x, int z)
    {
        base.Move(x, z);
        details.SetShootVelocity(x, z);
    }

    protected virtual GameObject GetTarget()
    {
        GameObject self = details.GetSelf();
        string targetTag = details.GetEnemyTag();
        return new GameObject();
    }

}
