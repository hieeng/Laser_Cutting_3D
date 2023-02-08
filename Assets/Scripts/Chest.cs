using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] AnimationCurve curve;
    [SerializeField] bool isHit;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeAmount;
    [SerializeField] float openTime;
    Vector3 shakePostion;
    float currentTime = 0;
    bool isOpen = false;

    Animator anim;
    Coroutine _coroutineShake = null;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        Open();
        Shake();
    }

    public void Open()
    {
        if (!isHit)
            return;

        currentTime += Time.deltaTime;
        if (currentTime >= openTime)
        {
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
            particle.SetActive(true);
        }
    }

    public void Shake()
    {
        if (!isHit)
            return;
        if (isOpen)
            return;

        var originPos = transform.localPosition;
        shakePostion = new Vector3(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));
        transform.localPosition = shakePostion * shakeAmount + originPos;
    }

}
