using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    public List<CombineInstance> _combine {get; set;}
    Combine _cb;
    Material _mat;
    public Material Mat {get => _mat;}
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

    public bool CutOff()
    {
        if (_combine.Count <= 0) 
        {
            gameObject.SetActive(false);
            _doDown?.Invoke();
            return false;
        }
        Debug.Log("cut");
        _combine.RemoveAt(_combine.Count - 1);
        _cb.Bake();
        return true;
    }
}
