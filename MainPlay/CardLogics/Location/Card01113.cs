using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01113 : CardLocation
{
    public override LocationSymbol MySymbol => LocationSymbol.Triangle;
    public override LocationSymbol MovePosibilities => LocationSymbol.Square;

    /*****************************************************************************************/
    protected override void EndGameAction(GameAction gameAction)
    {
        base.EndGameAction(gameAction);
        if (gameAction is MoveCardAction moveCardAction && MoveIntoLocation(moveCardAction))
            new EffectAction(() => TakeHorror(moveCardAction.ThisCard.Owner), TakeHorrorAnimation).AddActionTo();
    }

    bool MoveIntoLocation(MoveCardAction moveCardAction)
    {
        if (moveCardAction.ThisCard.CardType != CardType.PlayCard) return false;
        if (moveCardAction.Zone != ThisCard.MyOwnZone) return false;
        return true;
    }

    IEnumerator TakeHorrorAnimation() => new AnimationCardAction(ThisCard).RunNow();

    IEnumerator TakeHorror(InvestigatorComponent investigator) => new AssignDamageHorror(investigator, horrorAmount: 1).RunNow();

}
