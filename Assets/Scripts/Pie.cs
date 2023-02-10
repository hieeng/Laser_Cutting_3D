using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField] int _cutPiece = 1;
    public List<CombineInstance> _combine {get; set;}
    Combine _cb;
    Material _mat;
    public Material Mat {get => _mat;}
    public int CutPiece{get => _cutPiece;}
    Action _doDown;
    public event Action DoDown
    {
        add => _doDown += value;
        remove => _doDown -= value;
    }
    // Start is called before the first frame update
    void Start()
    {
        _cb = GetComponent<Combine>();
        _mat = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CutOff(out int cutPiece)
    {
        cutPiece = _cutPiece;
        if (_combine.Count <= 0) 
        {
            gameObject.SetActive(false);
            _doDown?.Invoke();
            cutPiece = 0;
            return false;
        }
        Debug.Log("cut");
        for (int i = 0; i < _cutPiece; i++)
        {
            if (_combine.Count <= 0) 
            {
                cutPiece = i;
                break;
            }
            _combine.RemoveAt(_combine.Count - 1);
        }
        _cb.Bake();
        return true;
    }
}
