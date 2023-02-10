// 지오
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] ParticleSystem _rewardParticle;

    public void RewardParticle()
    {
        _rewardParticle.transform.position = transform.position;
        _rewardParticle.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
