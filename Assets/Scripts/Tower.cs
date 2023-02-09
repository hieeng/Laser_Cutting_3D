using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CustomBehaviour
{
    [SerializeField] float _floorSize = 0.24f;
    [SerializeField] float _fallTime = 0.5f;
    [SerializeField] Chest _chest = null;
    [SerializeField] Gem _gem = null;
    [SerializeField] Pie[] _pies = null;
    [SerializeField] Block[] _blocks = null;
    Combine[] _combines;
    int _currentHight = 0;
    public override void Init()
    {
        _combines = gameObject.GetComponentsInChildren<Combine>(); 
        for (int i = 0; i < _pies.Length; i++)
        {
            _combines[i].Init();
            _pies[i].DoDown += GoDown;
        }
        transform.localRotation = Quaternion.Euler(0, -90.0f, 0);
        _currentHight = _pies.Length;
    }

    void GoDown()
    {
        StartCoroutine(GoDownCo());
        GameManager.Instance.ShowFeedBack();
        _currentHight--;
        if (_currentHight <= 0)
            DestoryBlock();
    }
    IEnumerator GoDownCo()
    {
        float time = 0;
        Vector3 targetPos = transform.position - new Vector3(0, _floorSize, 0); 
        while(time < _fallTime)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, targetPos, time / _fallTime);
            time += Time.deltaTime;
        }
    }

    public void OpenChest(Action doWin)
    {
        _chest.OpenChest(doWin);
    }

    public void CrashGem()
    {
        _gem.RewardParticle();
    }

    void DestoryBlock()
    {
        for (int i = 0; i < _blocks.Length; i++)
        {
            _blocks[i].DestoryBlock();
            _blocks[i].isOver = true;
        }
    }
}
