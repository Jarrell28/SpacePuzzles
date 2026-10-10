using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    public float mouseXInput { get; private set; }
    public float mouseYInput { get; private set; }


    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool activatePressed { get; private set; }
    public bool primaryShootPressed { get; private set; }
    public bool secondaryShootPressed { get; private set; }
    public bool alpha1Pressed { get; private set; }
    public bool alpha2Pressed { get; private set; }
    public bool commandPressed { get; private set; }
    public bool pausePressed { get; private set; }

    [Header("InputActions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference sprintAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference activateAction;
    [SerializeField] InputActionReference primaryShootAction;
    [SerializeField] InputActionReference secondaryShootAction;
    [SerializeField] InputActionReference alpha1PressedAction;
    [SerializeField] InputActionReference alpha2PressedAction;
    [SerializeField] InputActionReference commandPressedAction;
    [SerializeField] InputActionReference pausePressedAction;

    private bool clearInputs;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        sprintAction.action.Enable();
        jumpAction.action.Enable();
        activateAction.action.Enable();
        primaryShootAction.action.Enable();
        secondaryShootAction.action.Enable();
        alpha1PressedAction.action.Enable();
        alpha2PressedAction.action.Enable();
        commandPressedAction.action.Enable();
        pausePressedAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        sprintAction.action.Disable();
        jumpAction.action.Disable();
        activateAction.action.Disable();
        primaryShootAction.action.Disable();
        secondaryShootAction.action.Disable();
        alpha1PressedAction.action.Disable();
        alpha2PressedAction.action.Disable();
        commandPressedAction.action.Disable();
        pausePressedAction.action.Disable();
    }

    private void Update()
    {
        ClearInputs();
        ProcessInputs();
    }

    private void ProcessInputs()
    {

        pausePressed |= pausePressedAction.action.WasPressedThisFrame();

        if(GameManager.instance.currentGameState != GameManager.GameState.GamePlaying) return;

        Vector2 move = moveAction.action.ReadValue<Vector2>();
        Vector2 look = lookAction.action.ReadValue<Vector2>();

        horizontalInput = move.x;
        verticalInput = move.y;

        mouseXInput = look.x;
        mouseYInput = look.y;

        sprintHeld = sprintAction.action.IsPressed();

        jumpPressed |= jumpAction.action.WasPressedThisFrame();
        activatePressed |= activateAction.action.WasPressedThisFrame();

        primaryShootPressed |= primaryShootAction.action.WasPressedThisFrame();
        secondaryShootPressed |= secondaryShootAction.action.WasPressedThisFrame();

        alpha1Pressed |= alpha1PressedAction.action.WasPressedThisFrame();
        alpha2Pressed |= alpha2PressedAction.action.WasPressedThisFrame();

        commandPressed |= commandPressedAction.action.WasPressedThisFrame();
    }

    private void LateUpdate()
    {
        clearInputs = true;
    }

    private void ClearInputs()
    {
        if(!clearInputs) return;

        horizontalInput = 0;
        verticalInput = 0;
        mouseXInput = 0;
        mouseYInput = 0;

        sprintHeld = false;
        jumpPressed = false;
        activatePressed = false;

        primaryShootPressed = false;
        secondaryShootPressed = false;

        alpha1Pressed = false;
        alpha2Pressed = false;

        commandPressed = false;
        pausePressed = false;

        clearInputs = false;
    }
}
