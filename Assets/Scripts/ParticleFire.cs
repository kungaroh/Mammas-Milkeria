using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFire : MonoBehaviour
{
    [SerializeField] private Material[] milkMat;
    [SerializeField] private MilkManager _milkManager;
    
    // Start is called before the first frame update
    public void PlayParticle()
    {
        int prestigeLevel = _milkManager.prestigeLevel;
        ParticleSystem _pSys = GetComponentInChildren<ParticleSystem>();
        if (prestigeLevel > 3)
        {
            prestigeLevel = 3;
        }
        _pSys.GetComponent<ParticleSystemRenderer>().material = milkMat[prestigeLevel];
        _pSys.Play();
        GetComponent<AudioSource>().Play();
    }
}
