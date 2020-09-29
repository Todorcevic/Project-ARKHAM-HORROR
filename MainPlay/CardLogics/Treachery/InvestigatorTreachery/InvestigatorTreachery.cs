using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorTreachery : CardTreachery
{
    protected InvestigatorComponent Investigator => ThisCard.VisualOwner;

    /*****************************************************************************************/
    public override IEnumerator Revelation() => null;
}
