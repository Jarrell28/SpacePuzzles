using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemyController) : base(enemyController)
    {
        controller = enemyController;
    }


    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Attack State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Attack State");
    }

    public override void OnStateUpdate()
    {
        
            RaycastHit ray;
            Vector3 direction = (controller.target.transform.position - controller.transform.position).normalized;
            // direction.x = 0f; 

            if(Physics.Raycast(controller.transform.position, direction, out ray, 40f))
            {
                if (ray.transform.CompareTag("Player"))
                {
                    controller.gameObject.transform.LookAt(controller.target.transform.position);
                    // controller.gameObject.transform.rotation = Quaternion.LookRotation(direction);
                    
                    if (controller.canAttack)
                    {
                        GameObject bullet = GameObject.Instantiate(controller.projectilePrefab, controller.projectileSpawnReference.transform.position, controller.transform.rotation);
                        bullet.GetComponent<EnemyProjectileScript>().Initialize(controller.gameObject.tag);

                        controller.enemyShootAudio.Play();

                        controller.StartCoroutine(controller.AttackDelay());
                    }   
                }
            }

            
            
        

        //Read distance to player
        float distance = Vector3.Distance(controller.transform.position, controller.target.transform.position);
        //Check if player is out of attack Range and switch back to Follow
        if(distance > controller.attackRange)
        {
            controller.ChangeState(new EnemyFollowState(controller));
            // controller.enemyAnimator.SetBool("Follow", true);
            // controller.enemyAnimator.SetBool("Attack", false);
        }
    }    
}