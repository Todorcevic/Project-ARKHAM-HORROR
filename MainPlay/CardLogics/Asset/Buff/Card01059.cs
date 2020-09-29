using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01059 : CardAsset, IBuffable
{
    bool IBuffable.ActiveBuffCondition(GameAction gameAction) => ThisCard.IsInPlay;

    void IBuffable.BuffEffect()
    {
        ThisCard.VisualOwner.InvestigatorCardComponent.CardTools.ShowBuff(ThisCard);
        ThisCard.VisualOwner.WillpowerBonus++;
    }

    void IBuffable.DeBuffEffect()
    {
        ThisCard.VisualOwner.InvestigatorCardComponent.CardTools.HideBuff(ThisCard.UniqueId.ToString());
        ThisCard.VisualOwner.WillpowerBonus--;
    }
}
