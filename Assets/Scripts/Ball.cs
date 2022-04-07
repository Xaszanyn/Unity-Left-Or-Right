using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] PuzzleManager PM;

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Log": PM.Select(collision.transform); break;
            case "Floor": StartCoroutine(Countdown()); break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Success")) GM.Success();
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2);

        GM.Fail();
    }
}
