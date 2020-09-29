using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class JsonDataManager
{
    public T CreateListFromJson<T>(string pathAndNameJsonFile)
    {
        string JsonText = File.ReadAllText(pathAndNameJsonFile, System.Text.Encoding.Default);
        return JsonConvert.DeserializeObject<T>(JsonText);
    }

    public void CreateJsonFromList<T>(T deck, string pathAndNameJsonFile)
    {
        var jsonText = JsonConvert.SerializeObject(deck, Formatting.Indented);
        File.WriteAllText(GameFiles.RESOURCE_PATH + pathAndNameJsonFile, jsonText);
    }

    public void JointJsonFiles(string pathFolder)
    {
        List<Card> allCards = new List<Card>();
        foreach (string fileName in Directory.GetFiles(pathFolder))
        {
            if (new FileInfo(fileName).Extension == ".json")
                foreach (Card card in CreateListFromJson<Card[]>(fileName))
                    allCards.Add(card);
        }
        CreateJsonFromList(allCards, "allData.json");
        Debug.Log("allData.json is Done!");
    }
}
