using UnityEngine;

public class ScissorsState : FormState
{
    public ScissorsState(CharacterForm characterForm) : base(characterForm)
    {
    }

    public override void OnStateEnter()
    {
        //enemyType = typeof(RockState);
        OtherStates.Add(new PaperState(characterForm));
        OtherStates.Add(new RockState(characterForm));
        color = Color.grey;
        characterForm.SetCharacterColor(color);
        _FormStateEnum = FormStateEnum.Scissors;
        _EnemyFormStateEnum = FormStateEnum.Rock;
        _PreyFormStateEnum = FormStateEnum.Paper;
        characterForm.SetCharacterSprite(_FormStateEnum.GetHashCode());
    }

    public override void ForcedSetup()
    {
        _FormStateEnum = FormStateEnum.Scissors;
        _EnemyFormStateEnum = FormStateEnum.Rock;
        _PreyFormStateEnum = FormStateEnum.Paper;
    }
}
