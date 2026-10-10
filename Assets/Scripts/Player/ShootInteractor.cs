using System;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input inputType;

    [Header("Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] public float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMovementBehavior playerMovementBehavior;
    [SerializeField] private MeshRenderer weaponRenderer;
    private WeaponBehavior currentWeapon;

    [SerializeField] AudioSource shootAudio;

    private float finalShootVelocity;

    void Start()
    {
        SwitchWeapon(new BulletWeaponBehavior(this));
    }

    public override void Interact()
    {
        if(inputType == Input.Primary && PlayerInput.Instance.primaryShootPressed || inputType == Input.Secondary && PlayerInput.Instance.secondaryShootPressed)
        {
            // Shoot();
            currentWeapon.FireWeapon(shootPoint);
            shootAudio.Play();
        }

        if (PlayerInput.Instance.alpha1Pressed)
        {
            SwitchWeapon(new BulletWeaponBehavior(this));
        }

        // if (PlayerInput.Instance.alpha2Pressed)
        // {
        //     SwitchWeapon(new RocketWeaponBehavior(this));
        // }
    }

    void Shoot()
    {
        finalShootVelocity = playerMovementBehavior.GetForwardSpeed() + shootVelocity;

        PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject();

        if(pooledBullet != null)
        {
            Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;

            bullet.linearVelocity = shootPoint.forward * shootVelocity;

            pooledBullet.DestroyWithTime(2f);
        }

        // Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        // bullet.linearVelocity = shootPoint.forward * finalShootVelocity;
        // Destroy(bullet.gameObject, 7f);
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }

    public MeshRenderer GetWeaponRenderer()
    {
        return weaponRenderer;
    }

    public void SwitchWeapon(WeaponBehavior newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Switched weapon behaviour to: " + newWeapon.GetType().Name);
        Debug.Log("ShootInteractor Game Tag: " + gameObject.tag);
    }
}

public enum Input
{
    Primary,
    Secondary
}