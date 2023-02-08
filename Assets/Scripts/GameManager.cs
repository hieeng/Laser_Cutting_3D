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

    public int Gem{get; set;} = 0;
    public int Score{get; set;} = 0;

    static public GameManager Instance;
    private void Awake() 
    {
        Instance = this;
        _player.Init();
        //...
    }

    public int GetCurrentStageLevel()
    {
        return _stageManager.stage;
    }
}
