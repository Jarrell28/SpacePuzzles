using UnityEngine;

public class EnemyFollowState : EnemyState
{
    public EnemyFollowState(EnemyController enemyController) : base(enemyController)
    {
        controller = enemyController;
    }


    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Follow State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Follow State");
    }

    public override void OnStateUpdate()
    {
        // Debug.Log("Follow State Updating");
        RaycastHit ray;
        Vector3 direction = (controller.target.transform.position - controller.transform.position).normalized;
        // direction.x = 0f; 

        if(Physics.Raycast(controller.transform.position, direction, out ray, 40f))
        {
            if (ray.transform.CompareTag("Player"))
            {
                //Move towards player
                // controller.gameObject.transform.LookAt(new Vector3(controller.gameObject.transform.position.x, 0, controller.gameObject.transform.position.z));
                controller.gameObject.transform.LookAt(controller.target.transform.position);
                // controller.gameObject.transform.rotation = Quaternion.LookRotation(direction);
                controller.gameObject.transform.position = Vector3.MoveTowards(controller.gameObject.transform.position, controller.target.transform.position, controller.moveSpeed * Time.deltaTime);
            }
            
        }

        

        //Get distance to the player
        float distance = Vector3.Distance(controller.transform.position, controller.target.transform.position);

        //Check if player is in range to attack and switch to attack
        if(distance <= controller.attackRange)
        {
            controller.ChangeState(new EnemyAttackState(controller));
            // controller.enemyAnimator.SetBool("Attack", true);
            // controller.enemyAnimator.SetBool("Follow", false);
        }

        //Check if player is out of detection range and switch to idle
        if(distance > controller.detectinRange)
        {
            controller.ChangeState(new EnemyIdleState(controller));
            // controller.enemyAnimator.SetBool("Follow", false);
        }
    }
}