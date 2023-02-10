using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    [SerializeField] AnimationCurve _curve;
    float _time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //0.11 , -0.11
        transform.position = new Vector3(0, 0.11f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
