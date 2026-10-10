using UnityEngine;

public class PickupInteractor : Interactor
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private bool isPicked = false;
    private RaycastHit rayCastHit;
    private IPickable pickable;

    public override void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out rayCastHit, pickupDistance, pickupLayer))
        {
            if(PlayerInput.Instance.activatePressed && !isPicked)
            {
                pickable = rayCastHit.transform.GetComponent<IPickable>();
                if(pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }

        }

        if(PlayerInput.Instance.activatePressed && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }

    }
}
