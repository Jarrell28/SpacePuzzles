using UnityEngine;

public class BulletWeaponBehavior : WeaponBehavior
{
    ShootInteractor shootInteractor;
    Transform shootPoint;

    public BulletWeaponBehavior(ShootInteractor interactor)
    {
        shootInteractor = interactor;
        shootPoint = interactor.GetShootPoint();

        shootInteractor.GetWeaponRenderer().material.color = Color.red;
    }

    public void FireWeapon(Transform inTransform)
    {
        Debug.Log("Firing Raycast Weapon");

         PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject();

        if(pooledBullet != null)
        {
            ProjectileScript projectileScript = pooledBullet.GetComponent<ProjectileScript>();
            projectileScript.Initialize(shootInteractor.gameObject.tag);
            
            Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;

            bullet.linearVelocity = shootPoint.forward * shootInteractor.shootVelocity;

            pooledBullet.DestroyWithTime(2f);
        }
    }
}