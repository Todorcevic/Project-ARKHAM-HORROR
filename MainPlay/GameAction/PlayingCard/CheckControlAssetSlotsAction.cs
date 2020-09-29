using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckControlAssetSlotsAction : GameAction
{
    InvestigatorComponent investigator;
    List<string> allSlots = new List<string> { "Hand", "Ally", "Arcane", "Body", "Accessory" };

    /*****************************************************************************************/
    public CheckControlAssetSlotsAction(InvestigatorComponent investigator) => this.investigator = investigator;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (investigator.IsDefeat) yield break;
        foreach (string slotType in allSlots.FindAll(s => investigator.Slots.AmountFreeSlot(s) < 0))
        {
            List<CardComponent> allAssetsForThisSlot = investigator.Assets.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains(slotType));
            allAssetsForThisSlot.AddRange(investigator.Threat.ListCards.FindAll(c => c.Info.Slot != null && c.Info.Slot.Contains(slotType)));
            List<CardEffect> cardEffects = new List<CardEffect>();
            foreach (CardComponent assetToDiscard in allAssetsForThisSlot.FindAll(c => c.CanBeDiscard))
                cardEffects.Add(new CardEffect(assetToDiscard, () => new DiscardAction(assetToDiscard).RunNow(), EffectType.Choose, name: "Descartar: " + assetToDiscard.Info.Name));
            if (cardEffects.Count > 0) yield return new ChooseCardAction(cardEffects, isOptionalChoice: false).RunNow();
        }
    }
}
