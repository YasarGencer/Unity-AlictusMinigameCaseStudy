using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    [SerializeField]
    private GameObject[] moneyPrefab;
    [SerializeField]
    LayerMask targetLayer;
    [SerializeField]
    private Transform shred, collect;
    [SerializeField]
    TextMeshProUGUI text;

    GameObject[] uvMask, glassMask, moneyMask;

    private Transform selected;
    private Vector3 startPos;
    private bool holding;
    private void Awake()
    {
        Instantiate(moneyPrefab[Random.Range(0, moneyPrefab.Length)], new Vector3(-0.42f, -0.29f, -1.376251f), Quaternion.identity);
        if (gameManager == null) gameManager = this;

        moneyMask = GameObject.FindGameObjectsWithTag("MaskForMoney");
        uvMask = GameObject.FindGameObjectsWithTag("MaskForUV");
        glassMask = GameObject.FindGameObjectsWithTag("MaskForGlass");

        foreach (var item in moneyMask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !holding) GetTarget();
        if (Input.GetMouseButton(0) && holding) { MoveTarget(); }
        else if (Input.GetMouseButtonUp(0) && holding) { DropTarget(); }
    }
    public void GetTarget()
    {
        GameObject cast = Cast();
        if (cast)
        {
            selected = cast.transform;
            startPos = selected.transform.position;
            selected.GetComponent<Rotate>().holding = true;
            holding = true;
            if (selected.CompareTag("UV"))
                MaskFor(glassMask, uvMask);
            else if (selected.CompareTag("Glass"))
                MaskFor(uvMask, glassMask);
        }
    }
    public void MoveTarget()
    {
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8.42f));
        selected.position = new Vector3(mouseToWorld.x, mouseToWorld.y, -3.5f);
    }
    public void DropTarget()
    {
        holding = false;
        if (selected.CompareTag("Money"))
        {
            if (Vector3.Distance(selected.position, shred.position) < 2) Shred();
            else if (Vector3.Distance(selected.position, collect.position) < 3) Collect();
        }
        if (selected)
        {
            selected.transform.position = startPos;
            selected.GetComponent<Rotate>().holding = false;
            selected = null;
            UnMask();
        }
    }
    private GameObject Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, targetLayer))
            return hit.collider.gameObject;
        return null;
    }
    private bool Check() { return selected.name.Split(' ')[0] == "moneyClean"; }
    private void Shred()
    {
        shred.GetComponent<Animator>().SetTrigger("Shred");
        text.gameObject.GetComponent<Animator>().SetTrigger("Text");
        if (!Check())
            text.text = "WIN";
        else
            text.text = "FAIL";
        Destroy(selected.gameObject);
        Invoke("Restart", 1.5f);
    }
    private void Collect()
    {
        collect.GetComponent<Animator>().SetTrigger("Collect");
        text.gameObject.GetComponent<Animator>().SetTrigger("Text");
        if (Check())
            text.text = "WIN";
        else
            text.text = "FAIL";
        Destroy(selected.gameObject);
        Invoke("Restart", 1.5f);
    }
    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    private void MaskFor(GameObject[] mask, GameObject[] unmask)
    {
        foreach (var item in unmask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3000;
        foreach (var item in mask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
    private void UnMask()
    {
        foreach (var item in glassMask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3000;
        foreach (var item in uvMask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3000;
    }
}
