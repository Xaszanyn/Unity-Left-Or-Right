using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] GameObject ball;

    [SerializeField] List<Transform> logSack;
    [SerializeField] Transform puzzleLevel;

    [SerializeField] GameObject empty;

    Transform selectedNode;
    MeshRenderer selectedRenderer;

    [SerializeField] Material materialPuzzle;
    [SerializeField] Material materialSelected;
    [SerializeField] Material materialTemporary;
    
    [SerializeField] Color colorPuzzle;
    [SerializeField] Color colorSelected;

    [SerializeField] GameObject successTemplate;

    [SerializeField] MeshRenderer left;
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorLeftActive;
    [SerializeField] MeshRenderer right;
    [SerializeField] Color colorRight;
    [SerializeField] Color colorRightActive;

    public void StartPuzzle(Level puzzle)
    {
        for(int i = 0; i < puzzle.logs.Count; i++)
        {
            Level.Log data = puzzle.logs[i];

            Transform log = logSack[0];

            logSack.RemoveAt(0);
            
            log.SetParent(puzzleLevel);

            log.localPosition = data.position;

            if (data.vertical) log.localScale = new Vector3(1, data.length, 1);
            else log.localScale = new Vector3(data.length, 1, 1);
            

            Transform node = Instantiate(empty, log).transform;
            node.name = "Node";

            switch(data.node)
            {
                case Level.Log.Node.left:
                    node.localPosition = new Vector3(-((data.length / 2) - .5F) / data.length, 0, 0);
                    break;
                case Level.Log.Node.right:
                    node.localPosition = new Vector3(((data.length / 2) - .5F) / data.length, 0, 0);
                    break;
                case Level.Log.Node.uncontrollable:
                    log.tag = "Uncontrollable Log";
                    break;
            }

            node.SetParent(puzzleLevel);
            log.SetParent(node);
        }

        Transform success = Instantiate(successTemplate, puzzleLevel).transform;

        success.localPosition = new Vector3(puzzle.success.x, -7.5F, .05F);
        success.localScale = new Vector3(puzzle.success.y, 1, 1);

        ball.SetActive(true);
        ball.transform.localPosition = puzzle.ball;
    }

    public void Select(Transform newLog)
    {
        if (selectedNode == newLog.parent) return;

        MeshRenderer oldRenderer = selectedRenderer;

        selectedNode = newLog.parent;

        materialSelected.DOColor(colorPuzzle, .5F)
            .OnComplete(() => materialSelected.color = colorSelected);

        selectedRenderer = newLog.GetComponent<MeshRenderer>();

        selectedRenderer.material = materialTemporary;
        
        materialTemporary.color = colorPuzzle;
        materialTemporary.DOColor(colorSelected, .5F)
            .OnComplete(() => {
                selectedRenderer.material = materialSelected;
                oldRenderer.material = materialPuzzle;
            });
    }

    public void Left(bool big = false)
    {
        if (selectedNode == null) return;

        DOTween.Kill(selectedNode);

        if (!big) selectedNode.DOLocalRotate(new Vector3(0, 0, selectedNode.localRotation.eulerAngles.z + 10), .5F).SetTarget(selectedNode);
        else selectedNode.DOLocalRotate(new Vector3(0, 0, selectedNode.localRotation.eulerAngles.z + 30), .1F).SetTarget(selectedNode);

        DOTween.Kill(left);

        left.material.DOColor(colorLeftActive, .25F).SetTarget(left)
            .OnComplete(() => left.material.DOColor(colorLeft, .25F).SetTarget(left));
    }

    public void Right(bool big = false)
    {
        if (selectedNode == null) return;

        DOTween.Kill(selectedNode);

        if (!big) selectedNode.DOLocalRotate(new Vector3(0, 0, selectedNode.localRotation.eulerAngles.z - 10), .5F).SetTarget(selectedNode);
        else selectedNode.DOLocalRotate(new Vector3(0, 0, selectedNode.localRotation.eulerAngles.z - 30), .1F).SetTarget(selectedNode);

        DOTween.Kill(right);

        right.material.DOColor(colorRightActive, .25F).SetTarget(right)
            .OnComplete(() => right.material.DOColor(colorRight, .25F).SetTarget(right));
    }
}