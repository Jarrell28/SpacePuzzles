using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy properties
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float detectinRange = 10f;

    public GameObject target = null;

    public GameObject projectilePrefab;
    public GameObject projectileSpawnReference;

    public bool canAttack = true;

    public Animator enemyAnimator;

    public AudioSource enemyShootAudio;
    
    //Enemy states
    EnemyState currentState = null;
    
    private void Awake()
    {
        ChangeState(new EnemyIdleState(this));
    }

    private void Update()
    {
        if(currentState != null) currentState.OnStateUpdate();

    }

    public void ChangeState(EnemyState newState)
    {   
        //Call Exit state before changing state
        if(currentState != null)
        {
            currentState.OnStateExit();
        }
        //Set the new state
        currentState = newState;

        //Call the new state's OnStateEntered method
        currentState.OnStateEntered();
    }

    public IEnumerator AttackDelay()
    {
        canAttack = false;

        yield return new WaitForSeconds(2f);

        canAttack = true;
    }
}
