using UnityEngine;

public class PlayerCollisionBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // private void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.CompareTag("LevelTransition"))
    //     {
    //         Debug.Log("Player has entered level transition trigger");
    //         GameManager.instance.ChangeState(GameManager.GameState.LevelStart);
    //     }
    // }
}