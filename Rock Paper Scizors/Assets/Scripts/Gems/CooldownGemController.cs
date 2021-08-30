using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CooldownGemController : MonoBehaviourPunCallbacks, ICooldownReduction
{
    public float ReductionProcentage { get ; set ; }

    void Start()
    {
        ReductionProcentage = 100.0f;
    }

    [PunRPC] public void DestroyGem(float time)
    {
        Destroy(gameObject, time);
    }
}
