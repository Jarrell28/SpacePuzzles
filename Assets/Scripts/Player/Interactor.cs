using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    // [SerializeField] protected PlayerInput input;

    // Update is called once per frame
    void Update()
    {
        Interact();
    }


    public abstract void Interact();
}