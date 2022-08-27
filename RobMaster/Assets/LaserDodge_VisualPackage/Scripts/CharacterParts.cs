using UnityEngine;

public class CharacterParts : MonoBehaviour
{
    [SerializeField]
    Transform  leftHand, leftFoot, rightHand, rightFoot;
    [SerializeField]
    Transform[] targets;
    [SerializeField]
    Material[] targetMaterials; //0 green, 1 red
    [SerializeField]
    private LayerMask targetLayer, laserLayer;
    bool holding = false;
    public Transform selected;
    int doneCount = 0;

    private void Update()
    {
        doneCount = 0;

        if (Input.GetMouseButtonDown(0) && !holding) GetTarget();
        if (Input.GetMouseButton(0) && holding) { MoveTarget(); }
        else if (Input.GetMouseButtonUp(0) && holding) { DropTarget(); TargetMaterials(); ChekTargets(); }
        /* TargetMaterials function is needed in update and between DropTarget and CheckTargets both.
        If not done this way it doesn't update materials after droping the target and doesn't count the last body part you move as safe even if it is safe */
        TargetMaterials();
    }
    public void GetTarget()
    {
        GameObject cast = Cast();
        if (cast)
        {
            selected = cast.transform;
            holding = true;
        }
    }
    public void MoveTarget()
    {
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13f));
        selected.position = new Vector3(mouseToWorld.x, mouseToWorld.y, selected.position.z);
    }
    public void DropTarget()
    {
        holding = false;
        switch (selected.name)
        {
            case "LA":
                selected.position = leftHand.position;
                break;
            case "LL":
                selected.position = leftFoot.position;
                break;
            case "RA":
                selected.position = rightHand.position;
                break;
            case "RL":
                selected.position = rightFoot.position;
                break;
            default:
                break;
        }
        selected = null;
    }
    public void ChekTargets()
    {
        Debug.Log("Checking if targets are safe");
        foreach (var item in targets)
        {
            if (item.GetComponent<MeshRenderer>().material.color == targetMaterials[0].color)
            {
                doneCount++;
            }
        }
        Debug.Log(doneCount + " target safe");
        if (doneCount == 4)
        {
            Debug.Log("Encounter Completed");
            GameObject.Find("Enviroment").GetComponent<EncounterManager>().SetEncounterDone();
        }
    }
    public void TargetMaterials()
    {
        if (EncounterManager.inEncounter == false)
            foreach (var item in targets)
                item.gameObject.SetActive(false);
        if (EncounterManager.inEncounter == true)
        {
            foreach (var item in targets)
            {
                item.gameObject.SetActive(true);
                if (Physics.Raycast(item.transform.position, Vector3.forward, out RaycastHit hit, 7, laserLayer))
                {
                    item.GetComponent<MeshRenderer>().material = targetMaterials[1];
                }
                else
                    item.GetComponent<MeshRenderer>().material = targetMaterials[0];
            }
        }
    }
    private GameObject Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayer))
            return hit.collider.gameObject;
        return null;
    }
}
