using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01053 : CardSkill
{
    protected override void BeginGameAction(GameAction gameAction)
    {
        base.BeginGameAction(gameAction);
        if (gameAction is CardEffectsFilterAction cardEffectFilter && CheckOnlyYourTest())
            cardEffectFilter.CardEffects.RemoveAll(c => c.Card.ID == ThisCard.ID);
    }

    bool CheckOnlyYourTest()
    {
        if (!(GameControl.CurrentInteractableAction is SkillTestAction)) return false;
        if (ThisCard.Owner == GameControl.ActiveInvestigator) return false;
        return true;
    }

    protected override bool CheckSkillTestWin(SkillTestActionComplete skillTestComplete)
    {
        if (skillTestComplete.SkillTest.TotalInvestigatorValue - skillTestComplete.SkillTest.TotalTestValue < 3) return false;
        return base.CheckSkillTestWin(skillTestComplete);
    }

    protected override IEnumerator ThisCardAnimation() => new AnimationCardAction(ThisCard, withReturn: false, audioClip: ThisCard.Effect1).RunNow();

    protected override IEnumerator ThisCardEffect() => new MoveCardAction(ThisCard, ThisCard.Owner.Hand, withPreview: false).RunNow();
}
