using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomBehaviour
{
    [SerializeField] float _power = 0.01f;
    [SerializeField] float _shootInterval = 0.1f;
    [SerializeField] Transform _tower;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] GameObject _laser;
    [SerializeField] GameObject _pointer;
    [SerializeField] Material _gemMat;
    ParticleSystemRenderer _particleRenderer;
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
        _particleRenderer = _particle.GetComponent<ParticleSystemRenderer>();
        _pointer.gameObject.SetActive(true);
        Debug.Log(_particleRenderer);
    }

    void Shoot()
    {
       
        if (Input.GetMouseButton(0))
        {
            if (Time.time - _shootStart < _shootInterval) return;
            RaycastHit hit;
           _laser.gameObject.SetActive(true);
            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.red);
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, LayerMask.GetMask("Tower")|LayerMask.GetMask("Gem")))
                return;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Gem"))
            {
                hit.collider.gameObject.SetActive(false);
                //ShowParticle(_gemMat, hit.point);
                return;
            }
            //ChangeScale(hit);
            Pie pie = hit.collider.gameObject.GetComponent<Pie>();
            if (!pie.CutOff()) 
            {
                HideParticle();
                return;
            }
            ShowParticle(pie.Mat, hit.point);
            _rotY = 360.0f/101;
            _tower.Rotate(0, _rotY, 0);
            _shootStart = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _laser.gameObject.SetActive(false);
            HideParticle();
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

    void ShowParticle(Material material, Vector3 pos)
    {
        _particleRenderer.material = material;
        _particle.transform.position = pos;
        _particle.gameObject.SetActive(true);
    }

    void HideParticle()
    {
        _particle.gameObject.SetActive(false);
    }
}
