using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CustomBehaviour
{
    [SerializeField] float _floorSize = 0.24f;
    [SerializeField] float _fallTime = 0.5f;
    [SerializeField] Pie[] _pies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _pies.Length; i++)
        {
            _pies[i].DoDown += GoDown;
        }
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public override void Init()
    {

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
