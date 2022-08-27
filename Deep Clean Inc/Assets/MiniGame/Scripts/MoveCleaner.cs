using UnityEngine;

public class MoveCleaner : MonoBehaviour
{
    [SerializeField]
    private LayerMask roomLayer;
    void Update()
    {
        if (Input.GetMouseButton(0)) { MoveTarget(); }
    }
    private void MoveTarget(){ transform.position = Cast(); }

    private Vector3 Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, roomLayer))
            if (hit.collider != null)
                return new Vector3(hit.point.x, -1.18f,hit.point.z);
        return new Vector3(0,0,0);
    }
}
