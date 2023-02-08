using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeLength;
    [SerializeField] AnimationCurve curve;
    float elapsed = 0.0f;

    private void Update() 
    {
        Rotation();
    }

    private void Rotation()
    {
        if (elapsed < timeLength)
        {
            elapsed += Time.deltaTime;
            var rSpeed = curve.Evaluate(elapsed / timeLength) * speed;
            gameObject.transform.RotateAround(Vector3.zero, Vector3.up, rSpeed * Time.deltaTime);
        }
        else
            elapsed = 0;
    }
}
