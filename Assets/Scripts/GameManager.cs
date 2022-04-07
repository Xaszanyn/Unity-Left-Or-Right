using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] LevelHandler LH;
    [SerializeField] PuzzleManager PM;
    [SerializeField] UIManager UI;

    [SerializeField] List<Level> levels;

    int score = 0;

    bool failed = false;
    bool successed = false;

    public float speed;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    
    void Start()
    {
        PC.Lock();
        PC.Idle();

        UI.Initialize(levels[PlayerPrefs.GetInt("level", 0) % levels.Count].name);
    }

    public void StartGame()
    {
        Level level = levels[PlayerPrefs.GetInt("level", 0) % levels.Count];

        UI.StartGame();

        PC.Unlock();
        PC.Run();

        LH.StartLevel(level.level);
        PM.StartPuzzle(level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.DeleteAll();
    }

    public void Fail()
    {
        if (failed || successed) return;

        failed = true;

        PC.Lock();

        LH.StopLevel();
        PC.Fail();

        UI.Fail();
    }

    public void Success()
    {
        if (failed || successed) return;

        int level = PlayerPrefs.GetInt("level", 0);

        if (level < 6)
        {
            PlayerPrefs.SetInt("level", level + 1);
            UI.Success();
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
            UI.Success(true);
        }

        successed = true;

        PC.Lock();
        
        LH.StopLevel();

        PC.Success();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        if (successed || failed) return;

        DOTween.To(() => Time.timeScale, scale => Time.timeScale = scale, 0, .5F).SetUpdate(true);

        UI.Pause();

        PC.Lock();
    }

    public void Resume()
    {
        DOTween.To(() => Time.timeScale, scale => Time.timeScale = scale, 1, .5F).SetUpdate(true);

        UI.Resume();

        PC.Unlock();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void IncrementScore(int amount)
    {
        score += amount;

        UI.Score(score);
    }
}
