// 츄이
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    [SerializeField] Player _player;
    [SerializeField] Tower[] _towers;
    [SerializeField] Block[] _blocks;
    [SerializeField] Tracker _tracker;
    [SerializeField] StageManager _stageManager;
    [SerializeField] int _currentSession = 0;
    public static int Gem{get; set;} = 0;
    public int Score{get; set;} = 0;
    private bool _isEnd = false;
    public bool IsEnd{get => _isEnd; set => _isEnd = value;}
    public bool IsWin{get; set;} = false;
    public bool IsCamMove{get; set;} = false;
    public int CurrentSession
    {
        get => _currentSession;
        set => _currentSession = value;
    }
    public Tracker Tracker
    {
        get => _tracker;
    }
    static public GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        _player.DoEnd += Lose;
        _player.DoStart += GameStart;
        _player.DoRotateTower += DoRotateTower;
        _player.DoOpenChest += DoOpenChest;
        _player.DoCrashGem += DoCrashGem;
        _player.Init();
        _uiManager.Init();
        _uiManager.SetGemText(Gem);
        _uiManager.OnStartPanel();
        for (int i = 0; i < _towers.Length; i++)
        {
            _towers[i].Init();
        }
        _uiManager.ShowCurrentSession();
        //...
    }
    // 지오
    public int GetCurrentStageLevel()
    {
        return _stageManager.stage;
    }
    void Lose()
    {
        StartCoroutine(LoseCo());
    }
    IEnumerator LoseCo()
    {
        _isEnd = true;
        yield return new WaitForSeconds(0.8f);
        _uiManager.OnLosePanel();
    }


    void GameStart()
    {
        if (!_uiManager.StartPanel.activeSelf) return;
        _uiManager.OffStartPanel();
        _uiManager.OnInGamePanel();
    }

    // 지오
    public void ShowFeedBack()
    {
        _uiManager.ShowFeedBack();
    }

    void DoRotateTower(float rotY)
    {
        int idx = _currentSession;
        _towers[idx].transform.Rotate(0, rotY, 0);
    }

    void DoOpenChest()
    {
        int idx = _currentSession;
        _towers[idx].OpenChest(DoWin);
    }

    void DoCrashGem()
    {
        int idx = _currentSession;
        _towers[idx].CrashGem();
        _currentSession++;
        Gem += 1;
        _uiManager.SetGemText(Gem);
        //카메라 애니메이션
        _player.MoveBackAni();
       StartCoroutine(NextSessionCo(_player.MoveAni));
    }

    IEnumerator NextSessionCo(Action move)
    {
        float time = 0.0f;
        while(time < 0.8f)
        {
            yield return null;
            time += Time.deltaTime;
        }
        _tracker.NextSession(move);
        _uiManager.ShowCurrentSession();
        _uiManager.ShowClearSession(_currentSession - 1);
    }

    void DoWin()
    {
        Gem += 8;
        _uiManager.ShowClearSession(_currentSession);
        _uiManager.SetGemText(Gem);
        StartCoroutine(DoWinCo());
    }

    IEnumerator DoWinCo()
    {
        float time = 0.0f;
        while(time < 1.5f)
        {
            yield return null;
            time += Time.deltaTime;
        }
        _uiManager.OnWinPanel();
    }
    // 지오
    public void ScoreUp()
    {
        Score++;
        _uiManager.SetInGameScoreText();
    }
}
