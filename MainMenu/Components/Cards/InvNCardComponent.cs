using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace CardManager
{
    public class InvNCardComponent : CardBaseComponent
    {
        public TextMeshProUGUI numberCardsSelected;
        public int RequerimentCardsQuantity { get; set; }
        public int XP { get; set; }
        public bool IsActive { get; set; }
        public bool IsFull { get; set; }

        public List<string> listCardsID = new List<string>();

        public void SetInvCard(string idCard)
        {
            ID = idCard;
            imageComponent.enabled = true;
            IsActive = true;
        }

        public void CleanInvCard()
        {
            RequerimentCardsQuantity = 0;
            listCardsID.Clear();
            imageComponent.enabled = false;
            IsActive = false;
            UpdateTextNumberCards();
        }

        public void UpdateListAdd_Remove(string idCard, bool action)
        {
            if (action)
                listCardsID.Add(idCard);
            else
                listCardsID.Remove(idCard);
            UpdateTextNumberCards();
        }

        public void UpdateTextNumberCards()
        {
            numberCardsSelected.SetText((listCardsID.Count > 0) ? $"({listCardsID.Count - RequerimentCardsQuantity}/{AllInvestigator.InvestigatorDictionary[ID].deckSize})" : "");
            IsFull = (listCardsID.Count - RequerimentCardsQuantity >= AllInvestigator.InvestigatorDictionary[ID].deckSize) ? true : false;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsDisable && StaticUtility.DoubleClicked(eventData.clickTime, eventData.pointerPress))
                DoubleClickCard(this);
            else
                deckBuildManager.InvestigatorSelect(this);
        }

        public override void DoubleClickCard(CardBaseComponent cardBase)
        {
            deckBuildManager.DeChooseInvestigatorCard(this);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.1f, 0.25f);
            deckBuildManager.ShowingPreviewCard(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.25f);
            deckBuildManager.HidePreviewCard(this);
        }
    }
}
