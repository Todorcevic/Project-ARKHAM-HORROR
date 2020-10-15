using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Michsky.UI.Dark;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArkhamShared;

namespace ArkhamMenu
{
    public class CheckReady : MonoBehaviour
    {
        [SerializeField] PanelManager panelManager;
        [SerializeField] Button readyButton;
        [SerializeField] Button backButton;
        [SerializeField] ModalWindowManager backModalWindows;
        [SerializeField] InvestigatorSelectorComponent investigatorSelector;

        public void CheckingAllReady()
        {
            if (PanelManager.IsInInvestigatorPanel)
                readyButton.SwitchON(investigatorSelector.ListActiveInvCard.Count > 0);
            else readyButton.SwitchON(!investigatorSelector.ListActiveInvCard.Exists(i => !i.IsFull));
        }

        public void ReadyPressed()
        {
            if (PanelManager.IsInInvestigatorPanel) panelManager.GoToDeckPanel();
            else
            {
                Debug.Log("ReadyPressed");
                foreach (InvestigatorData investigatorD in InvestigatorData.AllInvestigatorsData)
                {
                    investigatorD.DeckPosition = investigatorSelector.ListActiveInvCard.IndexOf(investigatorSelector.ListActiveInvCard.Find(i => i.ID == investigatorD.Id)) + 1;
                    investigatorD.IsPlaying = true;
                }
                JsonDataManager.SaveJsonInvestigatorData();
                JsonDataManager.SaveJsonGameData();
                SceneManager.LoadScene(1);
                //Application.Quit();
            }
        }

        public void BackPressed()
        {
            if (!PanelManager.IsInInvestigatorPanel) panelManager.GoToInvestigatorPanel();
            else backModalWindows.ModalWindowIn();
        }
    }
}