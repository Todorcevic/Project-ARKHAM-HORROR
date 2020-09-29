using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace CardManager
{
    public class StaticUtility : MonoBehaviour
    {
        [SerializeField] ScriptableSprite englishSprites;
        [SerializeField] ScriptableSprite spanishSprites;
        public static List<Sprite> cardsImage;

        void Awake()
        {
            CreateImageCards();
        }

        static GameObject lastObjectClicked = null;
        static float doubleClickTimeLimit = 0.5f;
        static float lastClickTime = 0f;

        public static bool DoubleClicked(float clickTime, GameObject objectClicked)
        {
            if (clickTime - lastClickTime < doubleClickTimeLimit && objectClicked == lastObjectClicked)
                return true;
            lastClickTime = clickTime;
            lastObjectClicked = objectClicked;
            return false;
        }

        public static T TakeRandomCard<T>(List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static IEnumerator MoveScrollBar(string nameScroll, float moveToValue)
        {
            yield return new WaitForSeconds(0.04f);
            if (GameObject.Find(nameScroll) != null)
                GameObject.Find(nameScroll).GetComponent<Scrollbar>().value = moveToValue;
        }

        void CreateImageCards()
        {
            switch (StaticsComponents.Languaje)
            {
                default:
                case Languaje.EN:
                    {
                        cardsImage = englishSprites.cardsSprite;
                        break;
                    }
                case Languaje.ES:
                    {
                        cardsImage = spanishSprites.cardsSprite;
                        break;
                    }
            }
        }
    }
}
