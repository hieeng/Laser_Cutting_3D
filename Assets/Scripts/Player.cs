using System.Dynamic;
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
    [SerializeField] float _aniMoveFactor = 1.95f;
    [SerializeField] float _aniMoveTime = 0.5f;
    [SerializeField] float _rayDistance = 20.0f;
    ParticleSystemRenderer _particleRenderer;
    LineRenderer _laserRenderer;
    ParticleSystem[] _laserParitcle;
    float _shootStart = 0;
    float _rotY = 0;
    int _layerMask;
    int _gemMask;
    int _blockMask;
    int _chestMask;
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
    Action<float> _doRotateTower;
    public event Action<float> DoRotateTower
    {
        add => _doRotateTower += value;
        remove => _doRotateTower -=value;
    }
    Action _doOpenChest;
    public event Action DoOpenChest
    {
        add => _doOpenChest += value;
        remove => _doOpenChest -=value;
    }

    Action _doCrashGem;
    public event Action DoCrashGem
    {
        add => _doCrashGem += value;
        remove => _doCrashGem -=value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere);
        if (GameManager.Instance.IsEnd) return;
        if (GameManager.Instance.IsWin || GameManager.Instance.IsCamMove)
        {
            _laser.SetActive(false);
            _pointer.SetActive(false);
            return;
        }
        Shoot();
    }

    public override void Init()
    {
        _particleRenderer = _particle.GetComponent<ParticleSystemRenderer>();
        _pointer.gameObject.SetActive(true);
        _gemMask = LayerMask.GetMask("Gem");
        _blockMask = LayerMask.GetMask("Block");
        _chestMask = LayerMask.GetMask("Chest");
        _layerMask = LayerMask.GetMask("Tower") | _gemMask | _blockMask | _chestMask;
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
            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);
            if (!Physics.Raycast(transform.position, transform.forward, out hit, _rayDistance, _layerMask))
                return;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Block"))
            {
                InteractionBlock();
                return;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Chest"))
            {
                InterActionChest();
                return;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Gem"))
            {
                InterActionGem();
                return;
            }
            //ChangeScale(hit);
            Pie pie = hit.collider.gameObject.GetComponent<Pie>();
            var cutPiece = 1;
            if (!pie.CutOff(out cutPiece)) 
            {
                HideParticle();
                return;
            }
            GameManager.Instance.ScoreUp();
            ShowParticle(pie.Mat, hit.point);
            _rotY = 360.0f/100 * cutPiece;
            _doRotateTower?.Invoke(_rotY);
            _shootStart = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _laser.gameObject.SetActive(false);
            var main = _particle.main;
            main.loop = false;
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
        var main = _particle.main;
        main.loop = true;
        _particle.Play();
        _particleRenderer.material = material;
        _particle.transform.position = pos;
        _particle.gameObject.SetActive(true);
    }

    void HideParticle()
    {
        var main = _particle.main;
        main.loop = false;
    }

    void InteractionBlock()
    {
        StartCoroutine(DeadCo());
    }

    IEnumerator DeadCo()
    {
        GameManager.Instance.IsEnd = true;
        HideParticle();
         _laserRenderer.startColor = _deadColor;
        _laserRenderer.endColor = _deadColor;
        for (int i = 0; i < _laserParitcle.Length; i++)
        {
            var main = _laserParitcle[i].main;
            main.startColor = _deadColor;
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        _deadParticle.transform.position = transform.position;
        _deadParticle.gameObject.SetActive(true);
        _doEnd?.Invoke();
    }

    void InterActionChest()
    {
        _doOpenChest?.Invoke();
    }

    void InterActionGem()
    {
        _pointer.SetActive(false);
        _doCrashGem?.Invoke();
    }

    public void MoveAni()
    {
         StartCoroutine(MoveAniCo());
    }

    IEnumerator MoveAniCo()
    {
        //pos.z += _aniMoveFactor;
        float time = 0.0f;
        var pos = GameManager.Instance.Tracker.transform.position;
        pos.y = 0.26f;
        pos.z += 1;
        transform.position = pos;
        pos.z += _aniMoveFactor;
        while(time < _aniMoveTime)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, pos, time / _aniMoveTime);
            time += Time.deltaTime;
        }
        _pointer.SetActive(true);
        GameManager.Instance.IsCamMove = false;
    }

    public void MoveBackAni()
    {
        GameManager.Instance.IsCamMove = true;
        StartCoroutine(MoveBackAniCo());
    }
    IEnumerator MoveBackAniCo()
    {
        var pos = transform.position;
        pos.z -=2;
        var time = 0.0f;
        while(time < _aniMoveTime)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, pos, time / _aniMoveTime);
            time += Time.deltaTime;
        }
    }
}
