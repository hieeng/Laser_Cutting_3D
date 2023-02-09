using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeLength;
    [SerializeField] float fallDownSize;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Transform center;
    float elapsed = 0.0f;

    private void Update() 
    {
        if (GameManager.Instance.IsEnd) return;
        Rotation();
        if (Input.GetKeyDown("space"))
            DestoryBlock();
    }

    private void Rotation()
    {
        if (gameObject == null)
            return;
        if (elapsed < timeLength)
        {
            elapsed += Time.deltaTime;
            var rSpeed = curve.Evaluate(elapsed / timeLength) * speed;
            gameObject.transform.RotateAround(center.position, Vector3.up, rSpeed * Time.deltaTime);
        }
        else
            elapsed = 0;
    }

    public void DestoryBlock()
    {
        StartCoroutine(CoroutineDestoryBlock());
    }

    IEnumerator CoroutineDestoryBlock()
    {
        var time = 0.0f;
        Vector3 downPos = transform.position - new Vector3(0, fallDownSize, 0);
        Debug.Log(downPos);
        while (time <= 1)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, downPos, time / 1);
 
            time += Time.deltaTime;
        }
        gameObject.SetActive(false);
    }
}
