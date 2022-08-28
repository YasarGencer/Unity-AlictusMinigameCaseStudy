using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    Vector3 rotationH, rotationNH;

    public bool holding { get; set; } = false;

    void Update()
    {
        if(holding)
        transform.rotation = Quaternion.Euler(rotationH.x, rotationH.y, rotationH.z);
        else
            transform.rotation = Quaternion.Euler(rotationNH.x, rotationNH.y, rotationNH.z);
    }
}
