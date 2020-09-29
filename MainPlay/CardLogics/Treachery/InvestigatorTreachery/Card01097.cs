using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01097 : InvestigatorTreachery
{
    public override IEnumerator Revelation() =>
      new AddTokenAction(Investigator.InvestigatorCardComponent.ResourcesToken, -Investigator.Resources).RunNow();
}
