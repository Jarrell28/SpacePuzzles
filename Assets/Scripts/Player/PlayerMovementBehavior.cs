using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBehavior : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    [Header("PlayerMovement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 0.2f;

    private CharacterController characterController;

    private Vector3 playerVelocity;

    public bool isGrounded { get; private set; }

    private float moveMultiplier = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveMultiplier = input.sprintHeld ? sprintMultiplier : 1f;

        Vector3 move = transform.forward * input.verticalInput + transform.right * input.horizontalInput;

        characterController.Move(move * moveSpeed * moveMultiplier * Time.deltaTime);

        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    public void SetVelocity(float value)
    {
        playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return input.verticalInput * moveSpeed * moveMultiplier;
    }
}
