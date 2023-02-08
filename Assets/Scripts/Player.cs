using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomBehaviour
{
    [SerializeField] float _power = 0.01f;
    [SerializeField] float _shootInterval = 0.1f;
    [SerializeField] Transform _tower;
    float _shootStart = 0;
    float _rotY = 0;

    Action _doStop;

    public event Action DoStop
    {
        add => _doStop += value;
        remove => _doStop -=value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public override void Init()
    {

    }

    void Shoot()
    {
       
        if (Input.GetMouseButton(0))
        {
            if (Time.time - _shootStart < _shootInterval) return;
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.red);
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, LayerMask.GetMask("Tower")))
                return;
            //ChangeScale(hit);
            Pie pie = hit.collider.gameObject.GetComponent<Pie>();
            Debug.Log("shoot");
            if (!pie.CutOff()) return;
            _rotY = 360.0f/101;
            _tower.Rotate(0, _rotY, 0);
            _shootStart = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _doStop?.Invoke();
        }
    }


    void ChangeScale(RaycastHit hit)
    {
        Vector3 scale;
        scale = hit.transform.lossyScale;
        scale.x -= _power * Time.deltaTime;
        scale.z -= _power * Time.deltaTime;
        if(scale.x <= 0)
        {
            scale.x = 0;
            scale.z = 0;
        }
        hit.transform.localScale = scale;
    }
}
