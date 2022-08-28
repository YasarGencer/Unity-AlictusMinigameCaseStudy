using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMask : MonoBehaviour
{
    [SerializeField]
    Transform maskPositioner;
    Vector3 startPos;
    [SerializeField]
    ParticleSystem cutParticles;
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if (maskPositioner.position.x < transform.position.x)
        {
            transform.position = maskPositioner.position;
            if (!cutParticles.isPlaying)
                cutParticles.Play();
        }
        else if (cutParticles.isPlaying)
            cutParticles.Stop();
    }
    public void StartPos()
    {
        transform.position = startPos;
    }

}
