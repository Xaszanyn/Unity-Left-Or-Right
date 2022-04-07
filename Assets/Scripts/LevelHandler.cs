using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] Transform levelSection;

    [SerializeField] Transform floor;

    Transform level;

    public void StartLevel(GameObject levelPrefab)
    {
        MoveFloor();

        MoveLevel();
        
        void MoveFloor()
        {
            floor.position = new Vector3(floor.position.x, floor.position.y, 0);
            
            floor.DOMoveZ(-16, 16 / GM.speed).SetEase(Ease.Linear).SetTarget(floor)
            .OnComplete(MoveFloor);
        }

        void MoveLevel()
        {
            level = Instantiate(levelPrefab, levelSection).transform;

            level.DOMoveZ(-1000, GM.speed).SetSpeedBased().SetEase(Ease.Linear).SetTarget(level);

            Rotater[] rotaters = level.GetComponentsInChildren<Rotater>();

            for (int i = 0; i < rotaters.Length; i++)
            {
                Rotate(rotaters[i].transform);
            }
        }
    }

    public void StopLevel()
    {
        DOTween.Kill(floor);
        DOTween.Kill(level);
    }

    public void Rotate(Transform item)
    {
        switch(item.tag)
        {
            case "Left":
                item.DOLocalRotate(new Vector3(0, 360, 30), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(item)
                    .OnComplete(() => Rotate(item));
                break;
            case "Right":
                item.DOLocalRotate(new Vector3(0, -360, 330), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(item)
                    .OnComplete(() => Rotate(item));
                break;
            case "Coin":
                item.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(item)
                    .OnComplete(() => Rotate(item));
                break;
            case "Left Big":
                item.DOLocalRotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(item)
                    .OnComplete(() => Rotate(item));
                break;
            case "Right Big":
                item.DOLocalRotate(new Vector3(0, -360, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetTarget(item)
                    .OnComplete(() => Rotate(item));
                break;
        }
    }
}
