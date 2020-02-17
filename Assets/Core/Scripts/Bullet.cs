using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Parameter")]
    public int damage;
    int fireSpeed = 2000;
    public GameObject bloodEffect;
    public GameObject bulletEffect;

    private void Start()
    {
        GetComponentInChildren<Rigidbody>().velocity = transform.forward * fireSpeed * Time.deltaTime;
        Destroy(this.gameObject, 3);
    }
    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == Parameter.Tag.Monster)
        {
            GameObject obj = Instantiate(bloodEffect);
            obj.transform.position = transform.position;
            Destroy(obj, 3);

        }
        //Destroy(other.gameObject);
        Destroy(this.gameObject);
        //Instantiate(bulletEffect, transform.position, Quaternion.identity);
    }
}
