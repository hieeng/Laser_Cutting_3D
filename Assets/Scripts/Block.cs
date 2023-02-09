using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeLength;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Transform center;
    float elapsed = 0.0f;

    private void Update() 
    {
        if (GameManager.Instance.IsEnd) return;
        Rotation();
    }

    private void Rotation()
    {
        if (elapsed < timeLength)
        {
            elapsed += Time.deltaTime;
            var rSpeed = curve.Evaluate(elapsed / timeLength) * speed;
            gameObject.transform.RotateAround(center.position, Vector3.up, rSpeed * Time.deltaTime);
        }
        else
            elapsed = 0;
    }
}
