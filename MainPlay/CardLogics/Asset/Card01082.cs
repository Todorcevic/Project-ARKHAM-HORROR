using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01082 : CardAsset
{
    CardEffect cardEffect;

    /*****************************************************************************************/
    protected override void BeginGameAction(GameAction gameAction)
    {
        base.BeginGameAction(gameAction);
        if (gameAction is EnemyAttackAction enemyAttack && ActivateEffect(enemyAttack))
            enemyAttack.ChooseCardOptionalAction.ChosenCardEffects.Add(cardEffect = new CardEffect(
                card: ThisCard,
                effect: () => ExhaustAndDoDamage(enemyAttack),
                animationEffect: ExhaustAndDoDamageAnimation,
                payEffect: PayCostEffect,
                cancelEffect: CancelEffect,
                needExhaust: true,
                type: EffectType.Reaction,
                name: "Activar " + ThisCard.Info.Name,
                investigatorImageCardInfoOwner: ThisCard.VisualOwner));
    }

    bool ActivateEffect(EnemyAttackAction enemyAttack)
    {
        if (!ThisCard.IsInPlay) return false;
        if (ThisCard.VisualOwner != enemyAttack.Investigator) return false;
        if (enemyAttack.EnemyDamage < 1) return false;
        if (ThisCard.VisualOwner.AllEnemiesInMyLocation.FindAll(c => c != enemyAttack.Enemy).Count < 1) return false;
        return true;
    }

    IEnumerator ExhaustAndDoDamageAnimation()
    {
        ThisCard.CardTools.PlayOneShotSound(ThisCard.Effect1);
        yield return new WaitWhile(() => ThisCard.CardTools.AudioSource.isPlaying);
    }

    IEnumerator ExhaustAndDoDamage(EnemyAttackAction enemyAttack)
    {
        List<CardEffect> allEnemies = new List<CardEffect>();
        foreach (CardComponent enemy in ThisCard.VisualOwner.AllEnemiesInMyLocation.FindAll(c => c != enemyAttack.Enemy))
            allEnemies.Add(new CardEffect(
                card: enemy,
                effect: () => DoDamage(enemy),
                animationEffect: ((CardEnemy)enemy.CardLogic).BadEffectForEnemyAnimation,
                type: EffectType.Choose,
                name: "Hacer daño a " + enemy.Info.Name,
                investigatorImageCardInfoOwner: ThisCard.VisualOwner));
        yield return new ChooseCardAction(allEnemies, cancelableCardEffect: ref cardEffect).RunNow();

        IEnumerator DoDamage(CardComponent enemy)
        {
            yield return new DamageEnemyAction(enemy, enemyAttack.EnemyDamage).RunNow();
            enemyAttack.EnemyDamage = 0;
        }
    }

    IEnumerator PayCostEffect() => new AddTokenAction(ThisCard.SanityToken, 1).RunNow();

    IEnumerator CancelEffect()
    {
        if (!ThisCard.IsInPlay)
        {
            yield return new MoveCardAction(ThisCard, ThisCard.VisualOwner.Assets).RunNow();
            yield return new AddTokenAction(ThisCard.SanityToken, (int)ThisCard.Info.Sanity - 1).RunNow();
        }
        else yield return new AddTokenAction(ThisCard.SanityToken, -1).RunNow();
    }
}
