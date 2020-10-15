using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class EnemiesAttack : GameAction
    {
        readonly bool isOportunityAttack;
        readonly InvestigatorComponent investigator;
        readonly List<CardComponent> enemys;
        readonly List<CardEffect> cardEffects = new List<CardEffect>();
        public override string GameActionInfo => "Ataque de los Enemigos sobre " + investigator.InvestigatorCardComponent.Info.Name + ".";
        /*****************************************************************************************/
        public EnemiesAttack(InvestigatorComponent investigator, List<CardComponent> enemys = null, bool isOportunityAttack = false)
        {
            this.investigator = investigator;
            this.enemys = enemys ?? this.investigator.Threat.ListCards.FindAll(c => c.CardType == CardType.Enemy && !c.IsExausted);
            this.isOportunityAttack = isOportunityAttack;
        }

        /*****************************************************************************************/
        protected override IEnumerator ActionLogic()
        {
            if (investigator.IsDefeat) yield break;
            if (enemys.Count > 0)
            {
                foreach (CardComponent enemy in enemys)
                {
                    cardEffects.Add(new CardEffect(
                        card: enemy,
                        effect: EnemyAttack,
                        animationEffect: EnemyAttackAnimation,
                        type: EffectType.Choose,
                        name: "Recibir ataque de " + enemy.Info.Name,
                        investigatorImageCardInfoOwner: investigator));

                    IEnumerator EnemyAttack()
                    {
                        yield return new SettingEnemyAttackAction(enemy, GameControl.ActiveInvestigator).RunNow();
                        if (!isOportunityAttack) yield return new ExaustCardAction(enemy, true).RunNow();
                        enemys.Remove(enemy);
                    }

                    IEnumerator EnemyAttackAnimation()
                    {
                        yield return new AnimationCardAction(enemy, audioClip: enemy.Effect7).RunNow();
                    }
                }
                yield return new SelectInvestigatorAction(investigator).RunNow();
                yield return new ChooseCardAction(cardEffects, isOptionalChoice: false, isFastAction: !isOportunityAttack).RunNow();
                yield return new EnemiesAttack(investigator, enemys, isOportunityAttack).RunNow();
            }
        }
    }
}