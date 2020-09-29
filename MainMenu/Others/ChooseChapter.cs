using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace CardManager
{
    public class ChooseChapter : MonoBehaviour, IPointerClickHandler
    {
        public string jsonNameChapter;
        public TextMeshProUGUI nameChapter;
        public TextMeshProUGUI modalDescription;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            modalDescription.GetComponent<TextMeshProUGUI>().SetText("¿Estás seguro que deseás jugar " + nameChapter.text + "? Esto eliminará cualquier progreso anterior.");
        }
    }
}
