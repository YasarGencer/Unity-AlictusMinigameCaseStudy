using UnityEngine;

public class Vacuum : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cheese"))
            Gamemanager.gamemanager.DestroyCheese(other.gameObject);
    }
}
