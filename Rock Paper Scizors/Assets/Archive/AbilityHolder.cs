using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;


public class AbilityHolder : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<Ability> abilities;
    [SerializeField] private Button abilityButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI keyName;
    [SerializeField] private KeyCode keyCode;
    private Ability nextAbility;
    private float cooldownTime;
    private bool isCooldownOn;
    private float currentCooldownTime;
    [SerializeField] private GameObject buttonFill;
    private Image fillImage;

    private void Awake()
    {

    }

    private void Start()
    {
        InitializeAbilities();
        abilityButton.onClick.AddListener(ActivateAbility);
        buttonFill.SetActive(false);
        SetupAbilityButton();
        fillImage = buttonFill.GetComponent<Image>();
        keyName.text = keyCode.ToString();
        
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (isCooldownOn)
            {
                currentCooldownTime -= Time.deltaTime;
                fillImage.fillAmount = currentCooldownTime / cooldownTime;
            }
            else if (Input.GetKey(keyCode))
            {
                ActivateAbility();
            }
        }
    }

    private void InitializeAbilities()
    {
        for (int abilityIndex = 0; abilityIndex < abilities.Count; abilityIndex++)
        {
            abilities[abilityIndex].Initialize();
        }
    }

    private void SetupAbilityButton()
    {
        nextAbility = abilities[UnityEngine.Random.Range(0,abilities.Count)];
        nextAbility.InGameInitialize();
        if(photonView.IsMine)
            buttonText.text = nextAbility.AbilityName;
    }

    public void ActivateAbility()
    {
        Debug.Log($"Player {photonView.ViewID} has activated ability {nextAbility.AbilityName}.");
        nextAbility.Activate();
        StartCoroutine(CooldownActivationCoroutine());
        SetupAbilityButton();
    }

    IEnumerator CooldownActivationCoroutine()
    {
        isCooldownOn = true;
        cooldownTime = nextAbility.Cooldown;
        currentCooldownTime = cooldownTime;
        abilityButton.enabled = false;
        buttonFill.SetActive(true);
        yield return new WaitForSeconds(cooldownTime);
        isCooldownOn = false;
        abilityButton.enabled = true;
        buttonFill.SetActive(false);
    }

    public void StopAllAbilitiesCoroutines()
    {
        for (int abilityIndex = 0; abilityIndex < abilities.Count; abilityIndex++)
        {
            abilities[abilityIndex].StopAllCoroutines();
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (photonView.IsMine)
        {
            isCooldownOn = false;
            abilityButton.enabled = true;
            buttonFill.SetActive(false);
            SetupAbilityButton();
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (photonView.IsMine)
        {
            StopAllCoroutines();
        }
    }

}
