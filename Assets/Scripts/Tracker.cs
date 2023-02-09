using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] AnimationCurve _aniCurve;
    [SerializeField] Transform _camera;
    [SerializeField] Transform _Tracker;

    [SerializeField] float _shakeTime = 1.0f;
    [SerializeField] float _shakeSpeed = 2.0f;
    [SerializeField] float _shakeAmount = 1.0f;


    [SerializeField] float _offsetX, _offsetY, _offsetZ;

    [SerializeField] Transform[] _startPos;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void NextSession()
    {
        var next = _startPos[GameManager.Instance.CurrentSession + 1].position;
        _Tracker.transform.position = Vector3.Lerp(transform.position, next, 2f * Time.deltaTime);
        
    }

    void LateUpdate()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            CameraShake();
        }

    }

    public void CameraShake()
    {
        StartCoroutine(CS());
    }

    IEnumerator CS()
    {
        Vector3 position = _camera.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < _shakeTime)
        {
            Vector3 randomPoint = position + Random.insideUnitSphere;
            var a = _aniCurve.Evaluate(elapsedTime/_shakeTime) * _shakeAmount;
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, randomPoint * a, Time.deltaTime * _shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;

        }
        _camera.localPosition = position;
    }


}
