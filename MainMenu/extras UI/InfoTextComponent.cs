using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CardManager
{
    public class InfoTextComponent : MonoBehaviour
    {
        CardBaseComponent card;
        public CardBaseComponent Card
        {
            get => card;
            set
            {
                if (value == null)
                    CleanInfoText();
                else
                    ShowingInfoText(value);

                card = value;
            }
        }
        void ShowingInfoText(CardBaseComponent card)
        {
            string textToDisplay = "\n";
            textToDisplay += AllCards.DataCardDictionary[card.ID].text + "\n\n";
            textToDisplay += AllCards.DataCardDictionary[card.ID].flavor + "\n\n";
            textToDisplay += AllCards.DataCardDictionary[card.ID].back_text + "\n\n";
            textToDisplay += AllCards.DataCardDictionary[card.ID].back_flavor + "\n\n";
            textToDisplay = textToDisplay
                .Replace("[reaction]", "<i><b>\u0059</b></i>")
                .Replace("[action]", "<i><b>\u0049</b></i>")
                .Replace("[free]", "<i><b>\u0055</b></i>")

                .Replace("[elder_sign]", "<i><b>\u0058</b></i>")
                .Replace("[skull]", "<i><b>\u006E</b></i>")
                .Replace("[tablet]", "<i><b>\u0056</b></i>")
                .Replace("[cultist]", "<i><b>\u0062</b></i>")
                .Replace("[auto_fail]", "<i><b>\u005A</b></i>")

                .Replace("[rogue]", "<i><b>\u0054</b></i>")
                .Replace("[survivor]", "<i><b>\u0052</b></i>")
                .Replace("[guardian]", "<i><b>\u0051</b></i>")
                .Replace("[mystic]", "<i><b>\u0057</b></i>")
                .Replace("[seeker]", "<i><b>\u0045</b></i>")

                .Replace("[willpower]", "<i><b>\u0041</b></i>")
                .Replace("[combat]", "<i><b>\u0044</b></i>")
                .Replace("[intellect]", "<i><b>\u0046</b></i>")
                .Replace("[agility]", "<i><b>\u0053</b></i>")
                .Replace("[wild]", "<i><b>\u0067</b></i>")

                .Replace("[[Tome]]", "<i>Tome</i>")
                .Replace("[[Tomo]]", "<i>Tomo</i>")
                .Replace("[Elite]", "<i>Elite</i>")
                .Replace("[[Elite]]", "<i>Elite</i>")
                .Replace("[[\u00c9lite]]", "<i>Élite</i>")
                .Replace("[[Monster]]", "<i>Monster</i>")
                .Replace("[[Primigenio]]", "<i>Primigenio</i>")
                .Replace("[[Monstruo]]", "<i>Monstruo</i>")
                .Replace("[[Ancient One]]", "<i>Ancient One</i>")
                .Replace("[[Locura]]", "<i>Locura</i>")
                .Replace("[[Madness]]", "<i>Madness</i>")
                .Replace("[[Hechizo]]", "<b>Hechizo</b>")
                .Replace("[[Arma de fuego]]", "<b>Arma de fuego</b>")
                .Replace("[[Firearm]]", "<b>Firearm</b>")
                ;
            GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
            StartCoroutine(StaticUtility.MoveScrollBar("ScrollbarInfo", 1f));
        }

        void CleanInfoText()
        {
            GetComponent<TextMeshProUGUI>().SetText("");
        }
    }
}
