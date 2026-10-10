using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController controller;
    public EnemyState(EnemyController enemyController)
    {
        controller = enemyController;
    }

    public abstract void OnStateEntered();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();

    void Update()
    {
        
    }
}