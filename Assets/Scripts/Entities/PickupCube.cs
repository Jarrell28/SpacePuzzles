using UnityEngine;

public class PickupCube : MonoBehaviour, IPickable
{

    FixedJoint joint;
    Rigidbody cubeRb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPicked(Transform attachPoint)
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);

        cubeRb.isKinematic = true;
        cubeRb.useGravity = false;
    }

    public void OnDropped()
    {
        Destroy(joint);
        cubeRb.isKinematic = false;
        cubeRb.useGravity = true;
        transform.SetParent(null);
    }
}
