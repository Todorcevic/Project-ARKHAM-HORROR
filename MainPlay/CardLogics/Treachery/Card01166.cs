using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01166 : CardTreachery
{
    CardAgenda CurrentAgenda => GameControl.CurrentAgenda;

    /*****************************************************************************************/
    public override IEnumerator Revelation()
    {
        yield return new AddTokenAction(CurrentAgenda.ThisCard.DoomToken, 1).RunNow();
        yield return new CheckDoomThreshold(CurrentAgenda).RunNow();
    }
}
