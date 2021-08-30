using UnityEngine;

public class PaperState : FormState
{
    
    public PaperState(CharacterForm characterForm) : base(characterForm)
    {
    }

    public override void OnStateEnter()
    {
        //enemyType = typeof(ScissorsState);
        OtherStates.Add(new ScissorsState(characterForm));
        OtherStates.Add(new RockState(characterForm));
        color = Color.white;
        characterForm.SetCharacterColor(color);
        _FormStateEnum = FormStateEnum.Paper;
        _EnemyFormStateEnum = FormStateEnum.Scissors;
        _PreyFormStateEnum = FormStateEnum.Rock;
        characterForm.SetCharacterSprite(_FormStateEnum.GetHashCode());
    }

    public override void ForcedSetup()
    {
        _FormStateEnum = FormStateEnum.Paper;
        _EnemyFormStateEnum = FormStateEnum.Scissors;
        _PreyFormStateEnum = FormStateEnum.Rock;
    }
}
