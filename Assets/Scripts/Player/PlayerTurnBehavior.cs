using UnityEngine;

public class PlayerTurnBehavior : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * input.mouseXInput);
    }
}
