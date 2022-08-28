using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonSwipedCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
