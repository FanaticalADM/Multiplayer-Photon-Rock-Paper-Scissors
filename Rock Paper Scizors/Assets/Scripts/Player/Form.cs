using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Forms/Basic Form")]
public class Form : ScriptableObject
{
    public string formName;
    public FormStateEnum _formStateEnum;
    public FormStateEnum _enemyFormStateEnum;
    public FormStateEnum _preyFormStateEnum;
}
