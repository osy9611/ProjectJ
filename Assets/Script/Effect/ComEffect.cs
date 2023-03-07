using Module.Unity.Addressables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComEffect : MonoBehaviour
{
    ParticleSystem particle;
    Poolable poolable;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!particle.isPlaying)
        {
            if(poolable != null )
                Managers.Pool.Push(poolable);
            else
                poolable = GetComponent<Poolable>();
        }
    }
}
