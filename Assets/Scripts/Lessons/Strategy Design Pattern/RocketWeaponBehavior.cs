using UnityEngine;

public class RocketWeaponBehavior : WeaponBehavior
{
    ShootInteractor shootInteractor;
    Transform shootPoint;
    public RocketWeaponBehavior(ShootInteractor interactor)
    {
        shootInteractor = interactor;
        shootPoint = interactor.GetShootPoint();

        shootInteractor.GetWeaponRenderer().material.color = Color.purple;
    }
    public void FireWeapon(Transform inTransform)
    {
        Debug.Log("Firing Projectile");

        PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject();

        if(pooledBullet != null)
        {
            Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;

            bullet.linearVelocity = shootPoint.forward * shootInteractor.shootVelocity;

            pooledBullet.DestroyWithTime(2f);
        }
        
    }
}