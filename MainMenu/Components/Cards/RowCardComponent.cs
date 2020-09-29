using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace CardManager
{
    public class RowCardComponent : CardBaseComponent
    {
        public TextMeshProUGUI cardNameComponent;
        public override string ID
        {
            get => base.ID;
            set
            {
                base.ID = value;
                cardNameComponent.text = AllCards.DataCardDictionary[value].name;
            }
        }

        public override int Quantity
        {
            get => base.Quantity;
            set
            {
                if (quantity == value) return;
                quantity = value;
                quantityText.text = (quantity == 1) ? "" : "x" + value;
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            auraComponent.DOFillAmount(1f, 0.25f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            deckBuildManager.HidePreviewCard(this);
            auraComponent.DOFillAmount(0f, 0.25f).OnComplete(DeSelect);
        }

        void DeSelect()
        {
            IsSelected = false;
        }

        public override void DoubleClickCard(CardBaseComponent cardBase)
        {
            if (!IsDisable)
                deckBuildManager.DechooseCard(this);
        }
    }
}
