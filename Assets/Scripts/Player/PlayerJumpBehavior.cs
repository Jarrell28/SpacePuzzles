using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehavior))]
public class PlayerJumpBehavior : Interactor
{
    [SerializeField] private float jumpVelocity = 10f;

    private PlayerMovementBehavior playerMovementBehavior;

    void Start()
    {
        playerMovementBehavior = GetComponent<PlayerMovementBehavior>();
    }


    public override void Interact()
    {
        if(PlayerInput.Instance.jumpPressed && playerMovementBehavior.isGrounded)
        {
            playerMovementBehavior.SetVelocity(jumpVelocity);
        }    
    }   
}
