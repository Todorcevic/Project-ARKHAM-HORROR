﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Card01000 : CardTreachery
{
    static List<string> basicWeaknessList;

    /*****************************************************************************************/
    protected override void EndGameAction(GameAction gameAction)
    {
        if (gameAction is MoveDeck moveDeck && moveDeck.ListCards.Contains(ThisCard)) SearchBasicWeakness();
    }

    void SearchBasicWeakness()
    {
        if (basicWeaknessList == null)
        {
            basicWeaknessList = new List<string>();
            foreach (var card in GameData.CardDataList.Where(c => c.Subtype_code == "basicweakness" && c.Pack_code == "core" && c.Code != "01000").Select(c => new { Id = c.Code, c.Quantity }))
                for (int i = 0; i < card.Quantity; i++)
                    basicWeaknessList.Add(card.Id);
        }

        int rndCard = Random.Range(0, basicWeaknessList.Count - 1);
        CardComponent basicWeaknessCard = AllComponents.CardBuilder.BuildCard(basicWeaknessList[rndCard], GameFiles.INVESTIGATOR_BACK_IMAGE);
        basicWeaknessCard.Owner = ThisCard.Owner;
        GameControl.CurrentInvestigator.ListCardComponent.Remove(ThisCard);
        GameControl.CurrentInvestigator.ListCardComponent.Add(basicWeaknessCard);
        basicWeaknessList.RemoveAt(rndCard);

        new EffectAction(DrawRandomBasicWeakness).AddActionTo();

        IEnumerator DrawRandomBasicWeakness()
        {
            yield return ThisCard.transform.DOLocalMoveX(-1, GameData.ANIMATION_TIME_DEFAULT).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return new MoveCardAction(ThisCard, AllComponents.CardBuilder.Zone, isBack: false).RunNow();
            yield return new ShowCardAction(basicWeaknessCard).RunNow();
            yield return new MoveCardAction(basicWeaknessCard, GameControl.CurrentInvestigator.InvestigatorDeck, withPreview: false, isBack: true).RunNow();
        }
    }

    public override IEnumerator Revelation() => null;
}
