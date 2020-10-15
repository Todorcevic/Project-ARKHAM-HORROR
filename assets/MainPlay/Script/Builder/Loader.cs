using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using ArkhamShared;

namespace ArkhamGamePlay
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] bool isTestMode;
        [SerializeField] AllComponents allComponents;

        void Awake()
        {
            Screen.SetResolution(1280, 720, true, 60); //for Compilation
            GameControl.IsTestMode = isTestMode;
            JsonDataManager.LoadDataCards();
            allComponents.BuildingComponents();
            AllComponents.CardBuilder.LoadTexture(GameData.Instance.Language);
            AllComponents.Table.SettingTableZones();
            AllComponents.PanelHistory.SettingPanelPosition();
            AllComponents.TokenStacks.SettingTokenStack();
            AllComponents.InvestigatorManagerComponent.BuildInvestigators();
            AllComponents.CardBuilder.BuildScenario();
        }

        void Start()
        {
            GameControl.CurrentAction = new VoidAction();
            if (isTestMode) new TestAction().AddActionTo();
            else new SettingGame().AddActionTo();
            StartCoroutine(GameControl.CurrentAction.RunNow());
        }
    }
}