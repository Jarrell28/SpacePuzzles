// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerWeapon : MonoBehaviour
// {
//     private WeaponBehavior currentWeapon;
//     public GameObject shotPoint;
//     public PlayerInput input;
//     public GameObject weaponReference;
//     public GameObject rocketProjectile;
//     public GameObject bulletProjectile;

//     private void Start()
//     {
//         SwitchWeapon(new ProjectileBehavior(this));
//     }

//     private void Update()
//     {
//         //When left mouse button down
//         if (input.primaryShootPressed)
//         {
//             currentWeapon.FireWeapon(shotPoint.transform);
//         }


//         //When the 1 key is pressed, switch to projectile behavior
//         if(Keyboard.current != null && Keyboard.current.digit1Key.wasPressedThisFrame)
//         {
//             SwitchWeapon(new ProjectileBehavior(this));
//         }

//         //When the 2 key is pressed, switch to projectile behavior
//         if(Keyboard.current != null && Keyboard.current.digit2Key.wasPressedThisFrame)
//         {
//             SwitchWeapon(new RaycastWeaponBehavior(this));
//         }

        
//     }

//     public void SwitchWeapon(WeaponBehavior newWeapon)
//     {
//         currentWeapon = newWeapon;
//         Debug.Log("Switched weapon behaviour to: " + newWeapon.GetType().Name);
//     }
// }