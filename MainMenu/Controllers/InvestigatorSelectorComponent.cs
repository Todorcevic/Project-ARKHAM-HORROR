using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardManager
{
    public class InvestigatorSelectorComponent : MonoBehaviour
    {
        [SerializeField] InvestigatorImageComponent imageComponent;

        static public List<InvNCardComponent> listActiveInvCard = new List<InvNCardComponent>();

        int investigatorQuantity;
        public int InvestigatorQuantity { get => investigatorQuantity; set => investigatorQuantity = value; }

        bool isAllReady;

        InvNCardComponent currentInvestigator;
        public InvNCardComponent CurrentInvestigator
        {
            get => currentInvestigator;
            set
            {
                if (currentInvestigator != null)
                    currentInvestigator.IsSelected = false;
                if (value != null)
                {
                    value.IsSelected = true;
                    imageComponent.CurrentImage = value.imageComponent.sprite;
                    value.UpdateTextNumberCards();
                }
                else
                    imageComponent.CurrentImage = null;
                currentInvestigator = value;
            }
        }

        public void AddInvestigator(string idCard)
        {
            InvNCardComponent invCard = TakeNextInvCard(false);
            invCard.SetInvCard(idCard);
            CurrentInvestigator = invCard;
            listActiveInvCard.Add(invCard);
        }

        public void RemoveInvestigator(InvNCardComponent invCard)
        {
            listActiveInvCard.Remove(invCard);
            invCard.CleanInvCard();
            CurrentInvestigator = TakeNextInvCard(true);
        }

        public InvNCardComponent TakeNextInvCard(bool iWantActive)
        {
            foreach (InvNCardComponent invNCard in GetComponentsInChildren<InvNCardComponent>())
                if (invNCard.IsActive == iWantActive)
                    return invNCard;
            return null;
        }

        public void CleanAllInvCard()
        {
            foreach (InvNCardComponent invCard in listActiveInvCard)
                invCard.CleanInvCard();
            listActiveInvCard.Clear();
            CurrentInvestigator = null;
        }
    }
}
