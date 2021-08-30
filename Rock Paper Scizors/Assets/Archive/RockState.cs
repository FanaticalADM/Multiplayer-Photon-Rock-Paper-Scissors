using UnityEngine;
using System;

[System.Serializable]
public class RockState : FormState
{
    public RockState(CharacterForm characterForm) : base(characterForm)
    {
    }

    public override void OnStateEnter()
    {
        //enemyType = typeof(PaperState);
        OtherStates.Add(new ScissorsState(characterForm));
        OtherStates.Add(new PaperState(characterForm));
        color = Color.black;
        characterForm.SetCharacterColor(color);
        _FormStateEnum = FormStateEnum.Rock;
        _EnemyFormStateEnum = FormStateEnum.Paper;
        _PreyFormStateEnum = FormStateEnum.Scissors;
        characterForm.SetCharacterSprite(_FormStateEnum.GetHashCode());
    }

    public override void ForcedSetup()
    {
        _FormStateEnum = FormStateEnum.Rock;
        _EnemyFormStateEnum = FormStateEnum.Paper;
        _PreyFormStateEnum = FormStateEnum.Scissors;
    }
}
