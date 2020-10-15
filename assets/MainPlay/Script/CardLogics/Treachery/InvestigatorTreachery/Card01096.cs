using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class Card01096 : InvestigatorTreachery
    {
        public override IEnumerator Revelation()
        {
            int totalCardsToDiscard = Investigator.Hand.ListCards.Count - 1;
            for (int i = 0; i < totalCardsToDiscard; i++)
                yield return new InvestigatorDiscardHand(Investigator).RunNow();
        }
    }
}