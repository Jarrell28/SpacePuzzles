using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovementBehavior : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertedMouse;

    private float camXRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        RotateCamera();
    }

    // Update is called once per frame
    void RotateCamera()
    {
        camXRotation += Time.deltaTime * input.mouseYInput * turnSpeed * (invertedMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }
}
