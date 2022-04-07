using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Camera camera;

    [SerializeField] Transform player;

    [SerializeField] float edges;
    [SerializeField] float sensitivity;

    bool moveLock;

    bool dragging;
    float position;
    float lastPosition;

    public void Idle()
    {
        int random = Random.Range(1, 100);

        if (random <= 45) animator.SetTrigger("Bored");
        else if (random <= 75) animator.SetTrigger("Normal");
        else animator.SetTrigger("Moron");
    }

    public void Run()
    {
        animator.SetTrigger("Run");

        camera.transform.DOLocalMove(new Vector3(0, 12, 0), 1);
        camera.transform.DOLocalRotate(new Vector3(17.5F, 0, 0), 1);
    }

    void Update()
    {
        if (!moveLock) Drag();
    }

    void Drag()
    {
        if (Input.GetMouseButtonDown(0) && !dragging)
        {
            dragging = true;
            position = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            lastPosition = player.localPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float difference = Input.mousePosition.x - position;

            player.localPosition = new Vector3(Mathf.Clamp(lastPosition + (difference / sensitivity), -edges, edges), player.localPosition.y, player.localPosition.z);
        }
    }

    public void Lock()
    {
        moveLock = true;
    }

    public void Unlock()
    {
        moveLock = false;
    }

    public void Fail()
    {
        animator.SetTrigger("Fail");
        transform.DOLocalMoveY(-1, 1);
        transform.DOLocalMoveZ(transform.localPosition.z - 1.5F, 3);
    }

    public void Success()
    {
        int random = Random.Range(1, 100);

        if (random <= 35) animator.SetTrigger("Horon");
        else if (random <= 60) animator.SetTrigger("Clap");
        else if (random <= 80) animator.SetTrigger("Happy");
        else animator.SetTrigger("Cheer");
    }
}
