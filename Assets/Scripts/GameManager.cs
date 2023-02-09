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
    public bool IsEnd{get => _isEnd;}
    public int CurrentSession
    {
        get => _currentSession;
        set => _currentSession = value;
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
        _player.Init();
        _uiManager.SetGemText(Gem);
        _uiManager.OnStartPanel();
        for (int i = 0; i < _towers.Length; i++)
        {
            _towers[i].Init();
        }
        //_uiManager.ShowCurrentSession();
        //...
    }
    public int GetCurrentStageLevel()
    {
        return _stageManager.stage;
    }
    void Lose()
    {
        Gem += 10;
        _uiManager.SetGemText(Gem);
        StartCoroutine(LoseCo());
    }
    IEnumerator LoseCo()
    {
        _isEnd = true;

        yield return new WaitForSeconds(0.5f);
        _uiManager.OnLosePanel();
    }


    void GameStart()
    {
        if (!_uiManager.StartPanel.activeSelf) return;
        _uiManager.OffStartPanel();
        _uiManager.OnInGamePanel();
    }

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
        _towers[idx].OpenChest();
    }

    void DoCrashGem()
    {
        int idx = _currentSession;
        //_towers[idx].CrashGem();
        var pos = _tracker.transform.position;
        pos.y = 0;
        pos.z += 5;
        _player.transform.position = pos;
        //애니메이션
    }
}
