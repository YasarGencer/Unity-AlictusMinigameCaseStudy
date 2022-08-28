using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] arrows;

    [SerializeField]
    private float timeBetween = 1f, speed;
    private float timeToSpawn;

    void Update()
    {

        if (Time.time >= timeToSpawn)
        {
            timeToSpawn = Time.time + timeBetween;
            Instantiate(arrows[Random.Range(0, arrows.Length)], transform.position, Quaternion.identity).GetComponent<Rigidbody>().velocity = Vector3.left * Time.fixedDeltaTime * speed;
        }
    }
}
