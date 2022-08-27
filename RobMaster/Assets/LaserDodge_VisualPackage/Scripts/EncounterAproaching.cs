using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterAproaching : MonoBehaviour
{
    [SerializeField] float speedValue;
    float speed;
    [SerializeField] bool done = false;

    private void Start()
    {
        SetSpeed(speedValue);
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
    }
    public float GetSpeedValue() { return speedValue; }
    public void SetSpeed(float value) { speed = value; }
    public void SetDone(bool value) { done = value; }
    public bool GetDone() { return done; }
}
