using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class CardAgenda : CardLogic
{
    public abstract IEnumerator BackFace();

    public virtual bool CheckAgendaAdvance()
    {
        int doomTotal = 0;
        foreach (CardComponent card in GameControl.AllCardComponents.FindAll(c => c.DoomToken.Amount > 0))
            doomTotal += card.DoomToken.Amount;
        if (doomTotal >= ThisCard.Info.Doom) return true;
        return false;
    }

    public IEnumerator AgendaAdvanceAnimation()
    {
        ThisCard.CardTools.PlayOneShotSound(ThisCard.ClipType1);
        yield return new WaitWhile(() => ThisCard.CardTools.AudioSource.isPlaying);
    }

    public IEnumerator AgendaAdvance()
    {
        foreach (CardComponent card in GameControl.AllCardComponents.FindAll(c => c.DoomToken.Amount > 0))
            yield return new AddTokenAction(card.DoomToken, -card.DoomToken.Amount).RunNow();
        yield return new ShowCardAction(ThisCard, isBack: true, withReturn: true).RunNow();
        yield return BackFace();
        yield return new DiscardAction(ThisCard, outGame: true, withTopUp: false, withPreview: true).RunNow();
        yield return new ShowCardAction(GameControl.CurrentAgenda.ThisCard, withReturn: true).RunNow();
    }
}
