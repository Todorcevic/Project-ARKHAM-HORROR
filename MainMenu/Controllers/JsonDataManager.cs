using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace CardManager
{
    public class JsonDataManager : MonoBehaviour
    {
        static string filePath;

        void Awake()
        {
            filePath = "Assets/CardManager/Resources/";
            CreateDataCard(filePath + "deckbuilding.json");
            AllInvestigator.CreateDictionaryInvestigator();
            Zones.CreateDictionaryZones();
        }

        void Start()
        {
            /********************   TOOLS   *****************************/

            //CreateUrlImageToText(AllCards.DataCard, "url2");
        }

        static void CreateDataCard(string fileJson)
        {         
            string allJsonText = File.ReadAllText(fileJson);
            AllCards.DataCard = JsonConvert.DeserializeObject<Card[]>(allJsonText);
            AllCards.DataCardDictionary = AllCards.DataCard.ToDictionary(c => c.code);
            AllCards.MultiplyX2CoreSetQuantity();
        }

        static void CreateUrlImageToText(Card[] listCards ,string nameFile)
        {
            List<string> url = new List<string>();
            var sql = from card in listCards
                      where card.imagesrc != null
                      select "https://es.arkhamdb.com" + card.imagesrc;
            //var sql = listCards.Select(card =>"https://es.arkhamdb.com" + card.backimagesrc ).Where(backimagesrc => backimagesrc != null); //Lambda expression for practice
            url = sql.ToList();

            File.WriteAllLines(filePath + nameFile + ".txt", url);
        }

        public static void CreateJsonDecks(List<string> deck, string nameDeck)
        {
            var jsonString = JsonConvert.SerializeObject(deck, Formatting.Indented);
            File.WriteAllText(filePath + nameDeck + ".json", jsonString);
        }
    }
}
