// 지오
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject gemPartcile;
    [SerializeField] GameObject fanfareParticle;
    [SerializeField] AnimationCurve curve;
    [SerializeField] bool isHit;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeAmount;
    [SerializeField] float openTime;
    Vector3 shakePostion;
    float currentTime = 0;
    bool isOpen = false;

    Animator anim;
    public bool IsHit
    {
        get => isHit;
        set => isHit = value;
    }
    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }
    public void Open(Action doWin)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= openTime)
        {
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
            gemPartcile.SetActive(true);
            fanfareParticle.SetActive(true);
            GameManager.Instance.IsWin = true;
            doWin?.Invoke();
        }
    }

    public void Shake()
    {
        if (isOpen)
            return;

        var originPos = transform.localPosition;
        shakePostion = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 0f, UnityEngine.Random.Range(-0.1f, 0.1f));
        transform.localPosition = shakePostion * shakeAmount + originPos;
    }

    //츄이
    public void OpenChest(Action doWin)
    {
        Open(doWin);
        Shake();
    }
}
