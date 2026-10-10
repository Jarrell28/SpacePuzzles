using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleInteractor : Interactor
{
    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance;

    private RaycastHit rayCastHit;
    private ISelectable selectable;

    public override void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out rayCastHit, interactionDistance, interactionLayer))
        {
            selectable = rayCastHit.transform.GetComponent<ISelectable>();

            if(selectable != null)
            {
                selectable.OnHoverEnter();

                if (PlayerInput.Instance.activatePressed)
                {
                    selectable.OnSelect();
                }
            }
        }

        if(rayCastHit.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactionDistance, Color.red);
    }
}
