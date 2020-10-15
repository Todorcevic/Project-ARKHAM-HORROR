using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class Card01168 : CardTreachery, IBuffable
    {
        CardComponent LocationAffected;
        public override bool IsDiscarted => false;

        /*****************************************************************************************/
        protected override void EndGameAction(GameAction gameAction)
        {
            if (gameAction is InvestigateLocation investigateLocation && Discard(investigateLocation))
                new DiscardAction(ThisCard).AddActionTo();
        }

        bool Discard(InvestigateLocation investigateLocation)
        {
            if (LocationAffected != investigateLocation.Location) return false;
            if (!investigateLocation.SkillTest.IsWin) return false;
            return true;
        }

        public override IEnumerator Revelation()
        {
            if (GameControl.ActiveInvestigator.CurrentLocation.MyOwnZone.ListCards.Exists(c => c.CardLogic is Card01168))
                yield return new DiscardAction(ThisCard).RunNow();
            else yield return new MoveCardAction(ThisCard, GameControl.ActiveInvestigator.CurrentLocation.MyOwnZone, withPreview: false).RunNow();
        }

        bool IBuffable.ActiveBuffCondition(GameAction gameAction) => ThisCard.IsInPlay;

        void IBuffable.BuffEffect()
        {
            LocationAffected = ThisCard.CurrentZone.ThisCard;
            LocationAffected.CardTools.ShowBuff(ThisCard);
            ((CardLocation)LocationAffected?.CardLogic).ShroudBonus += 2;
        }

        void IBuffable.DeBuffEffect()
        {
            LocationAffected.CardTools.HideBuff(ThisCard.UniqueId.ToString());
            ((CardLocation)LocationAffected.CardLogic).ShroudBonus -= 2;
            LocationAffected = null;
        }
    }
}