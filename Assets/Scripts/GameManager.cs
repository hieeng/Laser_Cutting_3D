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

    public static int Gem{get; set;} = 0;
    public int Score{get; set;} = 0;
    private bool _isEnd = false;
    public bool IsEnd{get => _isEnd;}

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
        _player.Init();
        _uiManager.SetGemText(Gem);
        _uiManager.OnStartPanel();
        for (int i = 0; i < _towers.Length; i++)
        {
            _towers[i].Init();
        }
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
    }

    public void ShowFeedBack()
    {
        _uiManager.ShowFeedBack();
    }
}
