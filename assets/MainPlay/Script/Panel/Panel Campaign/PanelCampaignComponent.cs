using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

namespace ArkhamGamePlay
{
    public class PanelCampaignComponent : ControlPanelComponent, IButtonClickable
    {
        [SerializeField] List<InvestigatorTraumasComponent> investigatorTraumas;
        [SerializeField] List<Image> victoryCards;
        [SerializeField] TextMeshProUGUI campaignRegister;

        /*****************************************************************************************/
        public void SetPanelCampaign()
        {
            SetInvestigators();
        }

        void SetInvestigators()
        {
            investigatorTraumas.ForEach(i => i.gameObject.SetActive(false));
            int n = 0;
            foreach (InvestigatorComponent investigator in GameControl.AllInvestigators)
            {
                investigatorTraumas[n].gameObject.SetActive(true);
                investigatorTraumas[n].SetInvestigatorTraumas(investigator);
                n++;
            }
        }

        public void AddVictoryCard(string idCard)
        {
            Image victoryCard = victoryCards.Find(v => !v.gameObject.activeInHierarchy);
            victoryCard.gameObject.SetActive(true);
            victoryCard.sprite = AllComponents.CardBuilder.GetSprite(idCard);
        }

        public void RegisterText(string text) => campaignRegister.text += "* " + text + Environment.NewLine;

        public void SetButton() => SetReadyButton(this, state: ButtonState.Ready);

        public void ReadyClicked(ReadyButton button) => StartCoroutine(HideThisPanel());
    }
}