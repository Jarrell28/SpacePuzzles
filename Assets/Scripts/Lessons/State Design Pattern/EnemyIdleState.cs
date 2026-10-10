using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController enemyController) : base(enemyController)
    {
        controller = enemyController;
    }


    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Idle State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Idle State");
    }

    public override void OnStateUpdate()
    {
        // Debug.Log("Idle State Updating");

        //Scanning for the player
            //Distance check
            //Raycast check

        float distance = Vector3.Distance(controller.transform.position, controller.target.transform.position);
        Vector3 direction = (controller.target.transform.position - controller.transform.position).normalized;
        RaycastHit hit;

        //Check if player/target in range
        if(distance <= controller.detectinRange)
        {   
            //Raycast to check if we see the player
            if(Physics.Raycast(controller.transform.position, direction, out hit, controller.detectinRange))
            {
                //Check if the raycast hit the player directly
                if(hit.collider.gameObject == controller.target)
                {
                    controller.ChangeState(new EnemyFollowState(controller));
                    // controller.enemyAnimator.SetBool("Follow", true);
                    
                }
            }
        }
    }
}