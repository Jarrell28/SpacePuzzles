using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] bool invertMouse;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpVelocity = 5f;
    [SerializeField] float sprintMultiplier = 2f;

    [Header("GroundChecks")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] Rigidbody bulletPrefab; 
    [SerializeField] float shootForce; 
    [SerializeField] Transform shootPoint; 



    CharacterController characterController;

    Vector3 playerVelocity;
    bool isGrounded;
    float moveMultiplier = 1f;

    //inputs
    Vector2 moveInput;
    Vector2 lookInput;
    float camXRotation;
    bool sprinting;
    bool jumpPressed;
    bool shootPressed;


    #region InputCallbacks

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        sprinting = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed) jumpPressed = true;
    }

    public void OnShoot(InputValue value)
    {
        if(value.isPressed) shootPressed = true;
    }

    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        CursorSetup();
    }

    private void CursorSetup()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        GroundCheck();
        MovePlayer();
        if (jumpPressed)
        {
            JumpCheck();
            jumpPressed = false;
        }

        if (shootPressed)
        {
            ShootBullet();
            shootPressed = false;
        }
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * lookInput.x * turnSpeed * Time.deltaTime);
        camXRotation += lookInput.y * turnSpeed * Time.deltaTime * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);
        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    void MovePlayer()
    {
        moveMultiplier = sprinting ? sprintMultiplier : 1f;
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;

        characterController.Move(move * moveSpeed * moveMultiplier * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayer);
    }

    void JumpCheck()
    {
        if (isGrounded)
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    void ShootBullet()
    {
        Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        Destroy(bullet.gameObject, 5f);
    }
}
