using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public ParticleSystem particle;
    bool isRun = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Bullet" && !isRun)
        {
            particle.Play();
            isRun = true;
        }

    }
}
