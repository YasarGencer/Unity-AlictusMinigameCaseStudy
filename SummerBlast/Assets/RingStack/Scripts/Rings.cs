using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
    //public List<GameObject> ringList;
    [SerializeField]
    private GameObject[] startRingArray;
    [SerializeField]
    private GameObject[] ringArray;
    [SerializeField]
    private int index = -1;
    private bool interactable = true;
    private void Start()
    {
        //pushes starting rings to array
        ringArray = new GameObject[3];
        foreach (var item in startRingArray)
            Push(item);
    }
    public bool Push(GameObject gameObject)
    {
        if (index == ringArray.Length - 1)
            return false;
        else
        {
            index = index + 1;
            ringArray[index] = gameObject;
            gameObject.transform.SetParent(this.gameObject.transform);
            gameObject.transform.position = new Vector3(transform.position.x, 2 + index * 2, transform.position.z);
            return true;
        }
    }
    public GameObject Pop()
    {
        if (index == -1)
            return null;
        else
        {
            index = index - 1;
            ringArray[index + 1].transform.parent = null;
            return ringArray[index + 1];
        }
    }
    public bool SearchForGhost()
    {
        if (ringArray.Length <= 0)
            return true;
        foreach (var item in ringArray)
            if(item != null)
                if(item.name == "Ghost")
                return true;
        return false;
    }
    public bool isFull()
    {
        
        foreach (var item in ringArray)
            //makes it doesn't count ghosts as a ring
            if (item && item.name == "Ghost")
                return false;
        if (index == 2)
            return true;
        else
            return false;
    }
    IEnumerator Interact()
    {
        if (GetInteractable())
        {
            SetInteractable(false);
            yield return new WaitForSeconds(.2f);
            SetInteractable(true);
        }
    }
    public bool GetInteractable() { return interactable; }
    public void SetInteractable(bool value) { interactable = value; }
    public int GetArrayLenght() { return ringArray.Length; }
    public void CheckMeshes()
    {
        Transform[] firstActiveGameObject = new Transform[ringArray.Length];
        for (int j = 0; j < ringArray.Length; j++)
        {
            if (ringArray[j])
                if (ringArray[j].name != "Ghost")
                {
                    Transform meshes = ringArray[j].transform.GetChild(0);
                    for (int i = 0; i < meshes.transform.childCount; i++)
                        if (meshes.transform.GetChild(i).gameObject.activeSelf == true)
                            firstActiveGameObject[j] = meshes.transform.GetChild(i);
                }
        }
        if (firstActiveGameObject[0] && firstActiveGameObject[1] && firstActiveGameObject[2])
            if (firstActiveGameObject[0].name == firstActiveGameObject[1].name)
                if (firstActiveGameObject[0].name == firstActiveGameObject[2].name)
                    { SetInteractable(false); HoldRings.SetCompleteCount(); }
                    
    }
    public void PopGhosts() { if (SearchForGhost()) Destroy(Pop()); }
}
