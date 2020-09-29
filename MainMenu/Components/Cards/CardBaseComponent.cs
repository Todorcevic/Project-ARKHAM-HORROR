using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

namespace CardManager
{
    public class CardBaseComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public string NameCard { get; set; }
        public Image auraComponent;
        public Image imageComponent;
        public Sprite imageBack;
        public TextMeshProUGUI quantityText;
        public Card Info => AllCards.DataCardDictionary[ID];
        [Inject] public DeckBuildManager deckBuildManager;

        protected string id;
        public virtual string ID
        {
            get => id;
            set
            {
                id = value;
                SetNameCard(value);
                SetImageCard(value);

                void SetNameCard(string idCard)
                {
                    gameObject.name = idCard;
                    NameCard = AllCards.DataCardDictionary[idCard].name;
                }

                void SetImageCard(string idCard)
                {
                    imageComponent.sprite = StaticUtility.cardsImage.Find(img => img.name == idCard) ?? null;
                    imageBack = StaticUtility.cardsImage.Find(img => img.name == idCard + "b") ?? null;
                }
            }
        }

        public bool IsInvestigator { get; set; }

        protected int quantity;
        public virtual int Quantity
        {
            get => quantity;
            set
            {
                if (quantity == value) return;
                quantityText.text = (value < 2) ? "" : "x" + value;
                quantity = value;
            }
        }

        protected bool isSelected;
        public virtual bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected == value) return;
                isSelected = auraComponent.enabled = value ? true : false;
            }
        }

        protected bool isDisable;
        public virtual bool IsDisable
        {
            get => isDisable;
            set
            {
                if (isDisable == value) return;
                imageComponent.color = value ? new Color(0.789f, 0.789f, 0.789f, 0.5f) : Color.white;
                isDisable = value;
            }
        }

        protected bool isVisible;
        public virtual bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible == value) return;
                gameObject.SetActive(value);
                isVisible = value;
            }
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            IsSelected = true;
            deckBuildManager.ShowingPreviewCard(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            IsSelected = false;
            deckBuildManager.HidePreviewCard(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!IsDisable && StaticUtility.DoubleClicked(eventData.clickTime, eventData.pointerPress))
                DoubleClickCard(this);
        }

        public virtual void DoubleClickCard(CardBaseComponent cardBase)
        {
            if (IsInvestigator)
                deckBuildManager.ChooseInvestigatorCard(this);
            else
                deckBuildManager.ChooseCard(this);
        }
    }
}
