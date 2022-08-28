using UnityEngine;
using UnityEngine.SceneManagement;

public class HoldRings : MonoBehaviour
{
    [SerializeField] LayerMask rayCharacterLayer;
    [SerializeField] GameObject[] bodies;

    static bool holding = false;
    GameObject holdingRing;
    public Rings first, current;

    [SerializeField]
    GameObject ghostRing;
    GameObject ghost;

    static int completeCount = 0;
    bool completed = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !holding) GetRing();
        if (Input.GetMouseButton(0) && holding) { MoveRing(); GhostRings(); }
        else if (Input.GetMouseButtonUp(0)) { DropRing(); ResetAll(); }
        //check if completed
        if (completeCount == bodies.Length - 1 && !completed)
            foreach (var item in bodies)
                if (item.GetComponent<Rings>().GetInteractable())
                {
                    completed = true;
                    item.GetComponentInChildren<Animator>().SetTrigger("Dance");
                    item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y + 2.3f, item.transform.position.z);
                    Invoke("Restart", 2f);
                    //Needs to be reseted even before restarting the scene because its static
                    completeCount = 0;
                }
    }
    #region Update
    void GetRing()
    {
        GameObject body = Cast();
        if (body)
        {
            first = body.GetComponent<Rings>() as Rings;
            if (first.GetInteractable() && first.GetArrayLenght() > 0)
            {
                holdingRing = first.Pop();
                SetHolding(true);
            }
        }
    }
    void MoveRing()
    {
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21.17f));
        holdingRing.transform.position = new Vector3(mouseToWorld.x, 10f, holdingRing.transform.position.z);
    }
    void GhostRings()
    {
        //makse sure even ghost rings does enter the arrays it doesnt counts them as rings and sets the arrays full by making them pop immidietly when cursor leaves
        GameObject body = null;
        body = Cast();
        if (body)
        {
            if (!body.GetComponent<Rings>().isFull())
                current = body.GetComponent<Rings>() as Rings;
            if (current && !current.SearchForGhost() && current != first && !current.isFull() && current.GetInteractable())
            {
                ghost = Instantiate(ghostRing, transform.position, Quaternion.identity);
                ghost.name = "Ghost";
                current.Push(ghost);
            }
        }
        else if (current != null) { foreach (var item in bodies) item.GetComponent<Rings>().PopGhosts(); current = null; }
    }
    void DropRing()
    {
        if (current != null)
        {
            if (!current.isFull())
            {
                current.PopGhosts();
                current.Push(holdingRing);
            }
            else
                first.Push(holdingRing);
        }
        else
            first.Push(holdingRing);
        SetHolding(false);
    }
    void ResetAll()
    {
        foreach (var item in bodies) item.GetComponent<Rings>().PopGhosts();
        if (current) current.CheckMeshes();
        ResetVariables();
    }
    #endregion
    private static void SetHolding(bool value) => holding = value;
    private void ResetVariables()
    {
        holdingRing = null;
        current = null;
    }
    private GameObject Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, rayCharacterLayer))
            return hit.collider.gameObject;
        return null;
    }
    public static void SetCompleteCount() { completeCount++; }
    private void Restart() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

}
