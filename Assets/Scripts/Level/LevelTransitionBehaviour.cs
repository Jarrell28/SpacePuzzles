using UnityEngine;
using UnityEngine.Playables;


public class LevelTransitionBehaviour : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    public PlayableDirector playableDirector = null;
    public LevelManager manager;
    private bool transitionTriggered;

    private void OnTriggerEnter(Collider collider)
    {
        if (!transitionTriggered && collider.CompareTag("Player"))
        {
            GameManager.instance.currentLevel = manager;

            Debug.Log("Player has entered level transition trigger");
            Debug.Log("Loading next level: " + nextLevelName);
            
            if(playableDirector) playableDirector.Play();

            transitionTriggered = true;

            GameManager.instance.ChangeState(GameManager.GameState.LevelStart);
        }
    }
}