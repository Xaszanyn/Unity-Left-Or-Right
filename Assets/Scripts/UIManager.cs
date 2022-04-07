using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject start;

    [SerializeField] CanvasGroup gameplay;

    [SerializeField] Transform tap;

    [SerializeField] CanvasGroup pauseScreen;
    [SerializeField] CanvasGroup failScreen;
    [SerializeField] TextMeshProUGUI failed;
    [SerializeField] CanvasGroup successScreen;

    [SerializeField] GameObject finalText;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI levelName;

    public void Initialize(string name)
    {
        gameplay.alpha = 0;
        pauseScreen.alpha = 0;
        failScreen.alpha = 0;
        successScreen.alpha = 0;

        scoreText.text = "0";

        levelName.text = name;

        tap.DOScale(1.2F, .5F).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetTarget(tap);
    }

    public void StartGame()
    {
        start.SetActive(false);

        DOTween.Kill(tap);
        tap.DOScale(0, .5F)
            .OnComplete(() => tap.gameObject.SetActive(false));

        gameplay.DOFade(1, 1).SetDelay(1);

        title.DOFade(0, .5F)
            .OnComplete(() => title.gameObject.SetActive(false));
        levelName.DOFade(0, .5F)
            .OnComplete(() => levelName.gameObject.SetActive(false));
    }

    public void ClickDown(Transform button)
    {
        DOTween.Kill(button);
        button.DOScale(1.2F, .05F).SetTarget(button).SetUpdate(true);
    }

    public void ClickUp(Transform button)
    {
        DOTween.Kill(button);
        button.DOScale(1, .15F).SetTarget(button).SetUpdate(true);
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        pauseScreen.DOFade(1, .5F).SetUpdate(true);
    }

    public void Resume()
    {
        pauseScreen.DOFade(0, .5F).SetUpdate(true)
            .OnComplete(() => pauseScreen.gameObject.SetActive(false));
    }

    public void Fail()
    {
        failScreen.gameObject.SetActive(true);
        failScreen.DOFade(1, .5F).SetDelay(.75F);

        failed.DOFade(1, .25F).SetDelay(1);
        failed.transform.localScale = new Vector3(15, 15, 1);
        failed.transform.DOScale(1, .75F).SetDelay(1).SetEase(Ease.OutQuart);
    }

    public void Success(bool final = false)
    {
        gameplay.DOFade(0, .5F)
            .OnComplete(() => gameplay.gameObject.SetActive(false));

        successScreen.gameObject.SetActive(true);
        successScreen.DOFade(1, .5F).SetDelay(1);

        if (final) finalText.SetActive(true);
    }

    public void Score(int score)
    {
        DOTween.Kill(scoreText);

        scoreText.transform.DOScale(1.2F, .15F).SetTarget(scoreText)
            .OnComplete(() => {
                scoreText.text = score + "";
                scoreText.transform.DOScale(1, .15F).SetTarget(scoreText);
            });
    }
}
