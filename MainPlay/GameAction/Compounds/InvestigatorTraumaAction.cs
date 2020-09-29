using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorTraumaAction : GameAction
{
    readonly bool isPhysical;
    readonly string textToCampaign;
    readonly InvestigatorComponent investigator;
    readonly CardComponent cardToCampaign;
    readonly AudioClip audioAnimation;

    /*****************************************************************************************/
    public InvestigatorTraumaAction(InvestigatorComponent investigator, string textToCampaign, bool isPhysical, CardComponent cardToCampaign = null)
    {
        this.isPhysical = isPhysical;
        this.investigator = investigator;
        this.textToCampaign = textToCampaign;
        this.cardToCampaign = cardToCampaign;
        audioAnimation = isPhysical ? investigator.InvestigatorCardComponent.Effect3 : investigator.InvestigatorCardComponent.Effect7;
    }

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (cardToCampaign != null)
            AllComponents.PanelCampaign.AddVictoryCard(cardToCampaign.ID);
        AllComponents.PanelCampaign.RegisterText(textToCampaign);
        if (isPhysical) investigator.PhysicalTraumas++;
        else investigator.MentalTraumas++;
        yield return null;
    }

    protected override IEnumerator Animation() =>
        new AnimationCardAction(investigator.InvestigatorCardComponent, audioClip: audioAnimation).RunNow();
}
