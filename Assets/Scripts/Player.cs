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
    [SerializeField] ParticleSystem _deadParticle;
    [SerializeField] GameObject _laser;
    [SerializeField] GameObject _pointer;
    [SerializeField] Material _gemMat;
    [SerializeField] Color _deadColor = Color.red;
    ParticleSystemRenderer _particleRenderer;
    LineRenderer _laserRenderer;
    ParticleSystem[] _laserParitcle;
    float _shootStart = 0;
    float _rotY = 0;
    int _layerMask;
    int _gemMask;
    int _blockMask;
    
    Action _doStart;
    public event Action DoStart
    {
        add => _doStart += value;
        remove => _doStart -=value;
    }
    Action _doStop;
    public event Action DoStop
    {
        add => _doStop += value;
        remove => _doStop -=value;
    }
    Action _doEnd;
    public event Action DoEnd
    {
        add => _doEnd += value;
        remove => _doEnd -=value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsEnd) return;
        Shoot();
    }

    public override void Init()
    {
        _particleRenderer = _particle.GetComponent<ParticleSystemRenderer>();
        _pointer.gameObject.SetActive(true);
        _gemMask = LayerMask.GetMask("Gem");
        _blockMask = LayerMask.GetMask("Block");
        _layerMask = LayerMask.GetMask("Tower") | _gemMask | _blockMask;
        _laserRenderer = _laser.GetComponent<LineRenderer>();
        _laserParitcle = _laser.GetComponentsInChildren<ParticleSystem>();
    }

    void Shoot()
    {
       
        if (Input.GetMouseButton(0))
        {
            _doStart?.Invoke();
            if (Time.time - _shootStart < _shootInterval) return;
            RaycastHit hit;
           _laser.gameObject.SetActive(true);
            Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.red);
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, _layerMask))
                return;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
            {
                InteractionBlock();
                return;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Gem"))
            {
                hit.collider.gameObject.SetActive(false);
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

    void InteractionBlock()
    {
        _laserRenderer.startColor = _deadColor;
        _laserRenderer.endColor = _deadColor;
        for (int i = 0; i < _laserParitcle.Length; i++)
        {
            var main = _laserParitcle[i].main;
            main.startColor = _deadColor;
        }
        gameObject.SetActive(false);
        _deadParticle.transform.position = transform.position;
        _deadParticle.gameObject.SetActive(true);
        _doEnd?.Invoke();
        //TODO : GameOver넣어야함
    }
}
