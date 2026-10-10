using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnLevelStart;
    [SerializeField] UnityEvent OnLevelEnd;
    [SerializeField] GameObject levelEnemies;


    private bool levelTwoActive;
    private bool levelThreeActive;
    private bool levelFiveActive;
    private bool levelFiveDoorTrigger;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if(levelTwoActive) LevelTwoPuzzle();
        if(levelThreeActive) LevelThreePuzzle();
        if(levelFiveActive) LevelFivePuzzle();
    }

    public void LevelStart()
    {
        // Debug.Log("Invoke level start");
        OnLevelStart?.Invoke();
        GameManager.instance.ChangeState(GameManager.GameState.GamePlaying);
    }

    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    private void LevelTwoPuzzle()
    {
        // int enemiesRemaining = levelEnemies.transform.childCount;
        int enemiesRemaining = 0;
        foreach(Transform gameObj in levelEnemies.transform)
        {
            if(gameObj.transform.Find("Jammer"))
            {
                enemiesRemaining += 1;
            }
        }
        // Debug.Log("Level two active");
        // Debug.Log("Enemy Count: " + enemiesRemaining);

        if(enemiesRemaining == 0) {
            levelTwoActive = false;    
            LevelEnd();
        }
    }

    public void SetLevelTwoActive()
    {
        levelTwoActive = true;
        // Debug.Log("Invoke level two active");
    }

    private void LevelThreePuzzle()
    {
        int enemiesRemaining = levelEnemies.transform.childCount;
        // Debug.Log("Level three active");
        // Debug.Log("Enemy Count: " + enemiesRemaining);

        if(enemiesRemaining == 0) {
            levelThreeActive = false;    
            LevelEnd();
        }
    }

    public void SetLevelThreeActive()
    {
        levelThreeActive = true;
        // Debug.Log("Invoke level three active");
    }

    public void SetLevelFiveActive()
    {
        levelFiveActive = true;
        // Debug.Log("Invoke level five active");
    }

    public void SetLevelFiveDoorTrigger()
    {
        levelFiveDoorTrigger = true;
        // Debug.Log("Invoke level five door trigger true");
    }

    private void LevelFivePuzzle()
    {
        // int enemiesRemaining = levelEnemies.transform.childCount;
        int enemiesRemaining = 0;

        foreach(Transform gameObj in levelEnemies.transform)
        {
            if(gameObj.name == "Robot_Bad")
            {
                enemiesRemaining += 1;
            } else if(gameObj.transform.Find("Jammer"))
            {
                enemiesRemaining += 1;
            }
    
        }
        // Debug.Log("Level five active");
        // Debug.Log("Enemy Count: " + enemiesRemaining);

        if(enemiesRemaining == 0 && levelFiveDoorTrigger) {
            levelFiveActive = false;    
            LevelEnd();
        }
    }

    public void EndGame()
    {
        GameManager.instance.ChangeState(GameManager.GameState.GameEnd);
    }





    
}