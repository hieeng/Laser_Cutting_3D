using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeLength;
    [SerializeField] float fallDownSize;
    [SerializeField] Vector3 ScaleAdd;
    [SerializeField] float timeAdd;
    [SerializeField] float timeDestory;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Transform center;
    float elapsed = 0.0f;
    public bool isOver = false;

    private void Update() 
    {
        if (GameManager.Instance.IsEnd) return;
        Rotation();
/*         if (Input.GetKeyDown("space"))
        {
            isOver =true;
            DestoryBlock();
        } */
    }

    private void Rotation()
    {
        if (isOver)
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

    public void AddBlock()
    {
        gameObject.SetActive(true);
        StartCoroutine(CoruotineAddBlock());
    }

    IEnumerator CoruotineAddBlock()
    {
        var time = 0.0f;

        while (time < timeAdd)
        {
            yield return null;

            transform.localScale = Vector3.Lerp(transform.localScale, ScaleAdd, time/timeAdd);
            time += Time.deltaTime;
        }
    }

    public void DestoryBlock()
    {
        StartCoroutine(CoroutineDestoryBlock());
    }

    IEnumerator CoroutineDestoryBlock()
    {
        var time = 0.0f;

        while (time < timeDestory)
        {
            yield return null;

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, time/timeDestory);
            time += Time.deltaTime;
        }
        gameObject.SetActive(false);
    }
}
