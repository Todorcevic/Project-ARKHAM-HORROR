using Michsky.UI.Dark;
using UnityEngine.UI;

namespace CardManager
{
    public static class StaticsComponents
    {
        public static Languaje Languaje = Languaje.ES;
        public static int MaxUniqueCardsSelected { get; set; } = 2;

        public static void ButtonsOnOff(Button button, bool state)
        {
            button.interactable = state;
            button.GetComponent<UIElementSound>().enabled = state;
        }
    }
}

public enum Languaje
{
    EN,
    ES
}
