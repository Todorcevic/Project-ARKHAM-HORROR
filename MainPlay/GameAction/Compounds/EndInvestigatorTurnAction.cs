using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInvestigatorTurnAction : GameAction
{
    public InvestigatorComponent Investigator { get; set; }

    /*****************************************************************************************/
    public EndInvestigatorTurnAction(InvestigatorComponent investigator) => Investigator = investigator;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        GameControl.TurnInvestigator = null;
        yield return null;
    }
}
