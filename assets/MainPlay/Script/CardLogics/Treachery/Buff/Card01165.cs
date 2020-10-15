using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class Card01165 : CardTreachery, IBuffable
    {
        InvestigatorComponent InvestigatorAffected;
        public override bool IsDiscarted => false;

        /*****************************************************************************************/
        protected override void EndGameAction(GameAction gameAction)
        {
            if (gameAction is CardEffectsFilterAction cardEffectsFilter && CheckAction())
                cardEffectsFilter.CardEffects.FindAll(c => (CardType.Asset | CardType.Event).HasFlag(c.Card.CardType) && (EffectType.Play | EffectType.Fast).HasFlag(c.Type))
                .ForEach(c => cardEffectsFilter.CardEffects.Remove(c));
            if (gameAction is UpkeepPhase && Forced())
                new DiscardAction(ThisCard).AddActionTo();
        }

        bool CheckAction()
        {
            if (GameControl.ActiveInvestigator != InvestigatorAffected) return false;
            if (!ThisCard.IsInPlay) return false;
            return true;
        }

        bool Forced()
        {
            if (!ThisCard.IsInPlay) return false;
            return true;
        }

        public override IEnumerator Revelation() =>
            new MoveCardAction(ThisCard, GameControl.ActiveInvestigator.Threat, withPreview: false).RunNow();

        public bool ActiveBuffCondition(GameAction gameAction) => ThisCard.IsInPlay;

        public void BuffEffect()
        {
            InvestigatorAffected = GameControl.ActiveInvestigator;
            InvestigatorAffected.InvestigatorCardComponent.CardTools.ShowBuff(ThisCard);
        }

        public void DeBuffEffect()
        {
            InvestigatorAffected.InvestigatorCardComponent.CardTools.HideBuff(ThisCard.UniqueId.ToString());
            InvestigatorAffected = null;
        }
    }
}