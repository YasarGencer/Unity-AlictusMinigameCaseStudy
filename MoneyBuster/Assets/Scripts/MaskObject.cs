using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    GameObject[] objToMask;

    private void Start()
    {
        objToMask = GameObject.FindGameObjectsWithTag("Mask");
        foreach (var item in objToMask)
            item.GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
}
