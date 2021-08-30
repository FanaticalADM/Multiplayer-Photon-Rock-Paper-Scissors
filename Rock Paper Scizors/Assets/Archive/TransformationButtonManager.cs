using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransformationButtonManager : MonoBehaviour
{
    //[SerializeField] Button transformationButton;
    //[SerializeField] TextMeshProUGUI buttonText;
    //[SerializeField] CharacterForm characterForm;

    //private void Start()
    //{
    //    characterForm.OnFormChanged += SetupTransformButton;
    //}

    //private void SetupTransformButton(FormState obj)
    //{
    //    transformationButton.onClick.RemoveAllListeners();
    //    FormState nextForm = characterForm.CurrentForm.OtherStates[UnityEngine.Random.Range(0, 2)];
    //    buttonText.text = nextForm.ToString();        
    //    transformationButton.onClick.AddListener(delegate () { ChangeForm(nextForm); });
        
    //}

    //public void ChangeForm(FormState formState)
    //{
    //    characterForm.SetForm(formState);
    //}
}
