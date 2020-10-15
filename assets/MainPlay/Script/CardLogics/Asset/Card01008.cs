using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class Card01008 : CardAsset, IBuffable
    {
        readonly int extraHandSlots = 2;

        /*****************************************************************************************/
        protected override void BeginGameAction(GameAction gameAction)
        {
            base.BeginGameAction(gameAction);
            if (gameAction is CheckDiscardAssetsAction choosediscard && CheckSlotsAddTomeAssets(choosediscard))
                if (choosediscard.CardToPlay.KeyWords.Contains("Tome"))
                    choosediscard.NeedThisAmountSlots -= extraHandSlots;
                else
                {
                    int tomesAmount = ThisCard.VisualOwner.Assets.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains("Hand") && c.KeyWords.Contains("Tome")).Count
                        + ThisCard.VisualOwner.Threat.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains("Hand") && c.KeyWords.Contains("Tome")).Count;
                    choosediscard.NeedThisAmountSlots -= tomesAmount > extraHandSlots ? extraHandSlots : tomesAmount;
                    if (tomesAmount <= extraHandSlots)
                    {
                        choosediscard.AllAssetsForThisSlot = ThisCard.Owner.Assets.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains("Hand") && !c.KeyWords.Contains("Tome"));
                        choosediscard.AllAssetsForThisSlot.AddRange(ThisCard.Owner.Threat.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains("Hand") && !c.KeyWords.Contains("Tome")));
                    }
                }
        }

        bool CheckSlotsAddTomeAssets(CheckDiscardAssetsAction choosediscard)
        {
            if (!ThisCard.IsInPlay) return false;
            if (choosediscard.Investigator != ThisCard.Owner) return false;
            if (choosediscard.SlotType != "Hand") return false;
            return true;
        }

        public bool ActiveBuffCondition(GameAction gameAction) => ThisCard.IsInPlay;

        public void BuffEffect() => ThisCard.VisualOwner.InvestigatorCardComponent.CardTools.ShowBuff(ThisCard);

        public void DeBuffEffect() => ThisCard.VisualOwner.InvestigatorCardComponent.CardTools.HideBuff(ThisCard.UniqueId.ToString());
    }
}