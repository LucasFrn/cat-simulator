using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = (transform.position - BuildingSystem.GetMouseWorldPosition());
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.instance.SnapCordinateToGrid(new Vector3(pos.x,0,pos.z));
    }
}
