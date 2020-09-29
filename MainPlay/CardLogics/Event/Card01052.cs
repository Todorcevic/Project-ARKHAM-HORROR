using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01052 : CardEvent
{
    protected override bool CheckPlayedFromHand()
    {
        if (!ThisCard.VisualOwner.AllEnemiesInMyLocation.Exists(c => c.IsExausted)) return false;
        return base.CheckPlayedFromHand();
    }

    protected override IEnumerator LogicEffect()
    {
        List<CardEffect> listCardEffects = new List<CardEffect>();
        foreach (CardComponent enemy in ThisCard.VisualOwner.AllEnemiesInMyLocation.FindAll(c => c.IsExausted))
            listCardEffects.Add(new CardEffect(
                card: enemy,
                effect: () => new DamageEnemyAction(enemy, 2).RunNow(),
                animationEffect: ((CardEnemy)enemy.CardLogic).BadEffectForEnemyAnimation,
                type: EffectType.Choose,
                name: "Inflingir daño a " + enemy.Info.Name,
                investigatorImageCardInfoOwner: ThisCard.VisualOwner));
        yield return new ChooseCardAction(listCardEffects, cancelableCardEffect: ref playFromHand).RunNow();
    }
}
