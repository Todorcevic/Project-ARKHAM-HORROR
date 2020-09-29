using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverCluesAction : GameAction
{
    public InvestigatorComponent Investigator { get; set; }
    public int Amount { get; set; }
    public CardComponent Location { get; set; }

    /*****************************************************************************************/
    public DiscoverCluesAction(InvestigatorComponent investigator, int amount, CardComponent location = null)
    {
        Investigator = investigator;
        Amount = amount;
        Location = location ?? investigator.CurrentLocation;
    }

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (Location.CluesToken.Amount > 0)
            yield return new AddTokenAction(Investigator.InvestigatorCardComponent.CluesToken, Location.CluesToken.Amount < Amount ? Location.CluesToken.Amount : Amount, Location.CluesToken).RunNow();
    }
}
