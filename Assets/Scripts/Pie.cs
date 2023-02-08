using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;
    public List<CombineInstance> _combine {get; set;}
    Combine _cb;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CutOff()
    {
        if (_combine.Count <= 0) 
        {
            Stop();
            gameObject.SetActive(false);
            _doDown?.Invoke();
            return false;
        }
        Debug.Log("cut");
        _combine.RemoveAt(_combine.Count - 1);
        _cb.Bake();
        _particle.Play();
        return true;
    }

    public void Stop()
    {
        _particle.Stop();
    }
}
