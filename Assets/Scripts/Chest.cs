using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] bool isHit;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeAmount;


    Coroutine _coroutineShake = null;

    private void Update() 
    {
        Shake();
    }

    public void Shake()
    {
        if (!isHit)
            return;
        if (null != _coroutineShake)
            StopCoroutine(_coroutineShake);
        _coroutineShake = StartCoroutine(CoroutineShake());
    }

    IEnumerator CoroutineShake()
    {
        float timer = 0.0f;
        var originPos = transform.position;
        var shakePostion = new Vector3(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));

        while(timer <= shakeTime)
        {
            transform.localPosition = shakePostion * shakeAmount + originPos;
    
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }
}
