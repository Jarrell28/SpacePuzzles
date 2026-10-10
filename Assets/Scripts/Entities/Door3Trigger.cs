using UnityEngine;
using UnityEngine.Events;

public class Door3Trigger : MonoBehaviour
{
    public UnityEvent doorTrigger;
    private int triggerCount;
    private bool doorTriggered;
    private AudioSource triggerAudioSource;

    void Start()
    {
        triggerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!doorTriggered && triggerCount >= 3)
        {
            doorTriggered = true;
            doorTrigger?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!doorTriggered && collider.CompareTag("Trigger"))
        {
            triggerCount += 1;
            triggerAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!doorTriggered && collider.CompareTag("Trigger"))
        {
            triggerCount -= 1;
        }
    }

}
