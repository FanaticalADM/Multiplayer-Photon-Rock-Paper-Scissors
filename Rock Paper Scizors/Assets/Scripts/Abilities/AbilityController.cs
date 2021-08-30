using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityController : MonoBehaviour
{
    public Image buttonFill;
    public TMP_Text abilityName;
    public TMP_Text keyName;
    public Button button;
    public PlayerController playerController;
    public Ability ability;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if(buttonFill.fillAmount >= 0)
            buttonFill.fillAmount = ability.CooldownTime / ability.Cooldown;
    }

    public void Initialize(Ability _ability, KeyCode key)
    {
        ability = _ability;
        keyName.text = key.ToString();
        abilityName.text = ability.AbilityName;
    }

    public void Initialize(Ability _ability)
    {
        ability = _ability;
        abilityName.text = ability.AbilityName;
    }
}
