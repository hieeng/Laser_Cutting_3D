using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CustomBehaviour
{
    [SerializeField] float _floorSize = 0.24f;
    [SerializeField] float _fallTime = 0.5f;
    [SerializeField] Pie[] _pies;
    Combine[] _combines;

    public override void Init()
    {
        _combines = gameObject.GetComponentsInChildren<Combine>(); 
        for (int i = 0; i < _pies.Length; i++)
        {
            _combines[i].Init();
            _pies[i].DoDown += GoDown;
        }
        transform.localRotation = Quaternion.Euler(0, -90.0f, 0);
    }

    void GoDown()
    {
        StartCoroutine(GoDownCo());
        GameManager.Instance.ShowFeedBack();
    }
    IEnumerator GoDownCo()
    {
        float time = 0;
        Vector3 targetPos = transform.position - new Vector3(0, _floorSize, 0); 
        while(time < _fallTime)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, targetPos, time/_fallTime);
            time += Time.deltaTime;
        }
    }
}
