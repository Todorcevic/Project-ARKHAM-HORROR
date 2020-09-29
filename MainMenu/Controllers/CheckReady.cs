using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace CardManager
{
    public class CheckReady : MonoBehaviour
    {
        [SerializeField] Button readyButton;

        public bool CheckingAllReady()
        {
            foreach (InvNCardComponent invNCard in InvestigatorSelectorComponent.listActiveInvCard)
            {
                if (!invNCard.IsFull)
                {
                    StaticsComponents.ButtonsOnOff(readyButton, false);
                    return true;
                }
            }
            StaticsComponents.ButtonsOnOff(readyButton, true);
            return false;
        }

        public void ReadyPressed()
        {
            Debug.Log("ReadyPressed");
            int i = 1;        
            foreach (InvNCardComponent invCard in InvestigatorSelectorComponent.listActiveInvCard)
            {
                invCard.listCardsID.Insert(0, invCard.ID);
                JsonDataManager.CreateJsonDecks(invCard.listCardsID, "InvestigatorDeck" + i);
                i++;
            }
            //SceneManager.LoadScene("Inicio");
            Application.Quit();
        }
    }
}
