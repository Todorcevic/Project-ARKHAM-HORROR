using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill : CardSkill
{
    protected override void BeginGameAction(GameAction gameAction)
    {
        base.BeginGameAction(gameAction);
        if (gameAction is CardEffectsFilterAction cardEffectFilter && CheckOnlyOneCard())
            cardEffectFilter.CardEffects.RemoveAll(c => c.Card.ID == ThisCard.ID && c.Card.CurrentZone != AllComponents.Table.SkillTest);
    }

    bool CheckOnlyOneCard()
    {
        if (!(GameControl.CurrentInteractableAction is SkillTestAction)) return false;
        if (!(AllComponents.Table.SkillTest.ListCards.Exists(c => c.ID == ThisCard.ID))) return false;
        return true;
    }

    protected override IEnumerator ThisCardEffect()
    {
        yield return base.ThisCardEffect();
        yield return new DrawAction(ThisCard.Owner).RunNow();
    }
}
