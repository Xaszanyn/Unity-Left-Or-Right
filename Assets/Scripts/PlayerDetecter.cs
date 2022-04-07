using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDetecter : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] PuzzleManager PM;

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Spike":
                Fail();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Left":
                ItemFade(other.transform);
                PM.Left();
                GM.IncrementScore(1);
                break;
            case "Right":
                ItemFade(other.transform);
                PM.Right();
                GM.IncrementScore(1);
                break;
            case "Coin":
                ItemFade(other.transform);
                GM.IncrementScore(10);
                break;
            case "Left Big":
                ItemFade(other.transform);
                PM.Left(true);
                GM.IncrementScore(5);
                break;
            case "Right Big":
                ItemFade(other.transform);
                PM.Right(true);
                GM.IncrementScore(5);
                break;
        }
    }

    void ItemFade(Transform item)
    {
        DOTween.Kill(item);

        item.DOScale(0, .1F) 
            .OnComplete(() => item.gameObject.SetActive(false));
    }

    void Fail()
    {
        GM.Fail();
    }
}
