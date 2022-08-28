using System.Collections;
using UnityEngine;

public class SwipeAreaCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.SetSwipeable(true);
        GameManager.GetSwipableObject(other.gameObject);
        switch (other.transform.name.Split('-')[0])
        {
            case "LEFT":
                GameManager.DirectionToSwipe(0);
                break;
            case "RIGHT":
                GameManager.DirectionToSwipe(1);
                break;
            case "UP":
                GameManager.DirectionToSwipe(2);
                break;
            case "DOWN":
                GameManager.DirectionToSwipe(3);
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameManager.SetSwipeable(false);
    }
}
