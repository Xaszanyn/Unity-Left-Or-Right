using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
