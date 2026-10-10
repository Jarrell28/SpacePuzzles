using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    public UnityEvent doorTrigger;
    

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Trigger"))
        {
            doorTrigger?.Invoke();
        }
    }

}
