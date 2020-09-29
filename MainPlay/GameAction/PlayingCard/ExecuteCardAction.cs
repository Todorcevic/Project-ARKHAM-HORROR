﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class ExecuteCardAction : GameAction
{
    public override GameActionType GameActionType => GameActionType.Basic;
    public CardEffect CardEffect { get; }
    public InvestigatorComponent Investigator { get; }

    /*****************************************************************************************/
    public ExecuteCardAction(CardEffect cardEffect)
    {
        CardEffect = cardEffect;
        Investigator = cardEffect.PlayOwner;
    }

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (!CardEffect.IsCancel && !Investigator.IsDefeat)
        {
            yield return PayingCost();
            if (!CardEffect.IsCancel && !Investigator.IsDefeat)
                yield return new EffectAction(CardEffect.Effect, CardEffect.AnimationEffect).RunNow();
            if (CardEffect.IsCancel)
                yield return Canceling();
            else
                yield return new UpdateActionsLeft(Investigator, -CardEffect.ActionCost).RunNow();
        }
        yield return ReturningCenterCards(); //Si no hay warning eliminar
    }

    IEnumerator PayingCost()
    {
        if (CardEffect.ResourceCost != 0)
        {
            if (CardEffect.Card.IsInCenterPreview) CardEffect.Card.MoveFast(CardEffect.Card.CurrentZone);
            yield return new AddTokenAction(Investigator.InvestigatorCardComponent.ResourcesToken, -CardEffect.ResourceCost).RunNow();
        }
        if (CardEffect.NeedExhaust) yield return new ExaustCardAction(CardEffect.Card).RunNow();
        yield return CardEffect.PayCostEffect?.Invoke();
    }

    IEnumerator Canceling()
    {
        yield return CardEffect.CancelEffect?.Invoke();
        if (CardEffect.NeedExhaust) yield return new ExaustCardAction(CardEffect.Card, false).RunNow();
        yield return new AddTokenAction(Investigator.InvestigatorCardComponent.ResourcesToken, CardEffect.ResourceCost).RunNow();
    }

    IEnumerator ReturningCenterCards()
    {
        foreach (CardComponent card in AllComponents.Table.CenterPreview.ZoneBehaviour.GetComponentsInChildren<CardComponent>())
        {
            Debug.Log("Warnign Returning Center Card: " + card.ID);
            card.MoveFast(card.CurrentZone);
        }
        yield return new WaitWhile(() => DOTween.IsTweening("MoveFast"));
    }
}
