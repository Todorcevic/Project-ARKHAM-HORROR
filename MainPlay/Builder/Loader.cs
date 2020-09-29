using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class Loader : MonoBehaviour
{
    [SerializeField] bool testMode;
    [SerializeField] AllComponents allComponents;
    readonly JsonDataManager jsonDataManager = new JsonDataManager();

    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
        GameControl.IsTestMode = testMode;
        if (testMode)
        {
            GameFiles.INVESTIGATOR_PATH = GameFiles.RESOURCE_PATH + "InvestigatorTest/";
            GameData.InvestigatorsStartingAmount = 2;
        }

        allComponents.BuildingComponents();
        LoadDataCards();
        AllComponents.CardBuilder.LoadTexture(GameData.Language);
        AllComponents.Table.SettingTableZones();
        AllComponents.PanelHistory.SettingPanelPosition();
        AllComponents.TokenStacks.SetTokenStack();
        AllComponents.InvestigatorManagerComponent.BuildInvestigators();
        AllComponents.CardBuilder.BuildScenario(GameFiles.ScenarioPath);
    }

    void Start()
    {
        GameControl.CurrentAction = new VoidAction();
        if (testMode) new TestAction().AddActionTo();
        else new SettingGame().AddActionTo();
        StartCoroutine(GameControl.CurrentAction.RunNow());
    }

    void LoadDataCards()
    {
        GameData.CardDataList = jsonDataManager.CreateListFromJson<Card[]>(GameFiles.ALL_CARD_DATA_JSON);
        GameData.CardDataDictionary = GameData.CardDataList.ToDictionary(c => c.Code);
    }
}


