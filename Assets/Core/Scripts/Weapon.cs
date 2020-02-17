using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particle;
    public GameObject firePos;
    public GameObject bullet;
    public GameObject focus;
    public void Fire()
    {
        particle.Play();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(focus.transform.position);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // TODO: Fire Effect
            GameObject b = Instantiate(bullet, firePos.transform.position, Quaternion.identity);
            b.transform.LookAt(hit.point);

            Destroy(b, 3);
        }
    }


}
