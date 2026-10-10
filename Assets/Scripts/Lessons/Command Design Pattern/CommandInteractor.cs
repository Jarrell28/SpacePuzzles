using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    Queue<Command> commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    private Command currentCommand;

    public override void Interact()
    {
        if (PlayerInput.Instance.commandPressed)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
            if(Physics.Raycast(ray, out var hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    GameObject pointer = Instantiate(pointerPrefab);
                    pointer.transform.position = hitInfo.point;

                    commands.Enqueue(new MoveCommand(agent, hitInfo.point));
                }
            }
        }

        ProcessCommands();
    }

    void ProcessCommands()
    {
        //If i currently have a command and its not done, then dont do anything new until i finish current command
        if(currentCommand != null && !currentCommand.IsComplete) return;

        //If i dont have any commands in my queue, do nothing
        if(commands.Count == 0) return;

        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }

}