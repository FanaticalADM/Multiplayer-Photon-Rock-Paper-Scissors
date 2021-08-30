using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Ability : MonoBehaviourPunCallbacks
{

    public float CooldownTime = 0;
    public bool activeAbility;

    protected PlayerController playerController;

    [field: SerializeField] public string AbilityName { get; set; }
    [field: SerializeField] public float Cooldown { get; protected set; }

    public abstract void Initialize();
    public abstract void InGameInitialize();
    public abstract void Activate();

    public void Update()
    {
        if (activeAbility == false && CooldownTime > 0)
        {
            CooldownTime -= Time.deltaTime;
        }

        if( CooldownTime <= 0)
        {
            activeAbility = true;
        }
    }

    public IEnumerator WaitToActivateCoroutine()
    {
        activeAbility = false;
        yield return new WaitForSeconds(Cooldown);
    }
}
