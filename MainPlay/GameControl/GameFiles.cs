using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFiles
{
    public const string INVESTIGATOR_BACK_IMAGE = "InvestigatorBack";
    public const string ENCOUNTER_BACK_IMAGE = "EncounterBack";

    public const string RESOURCE_PATH = "Assets/Resources/";
    public const string ALL_CARD_DATA_JSON = RESOURCE_PATH + "AllCardData.json";
    public static string INVESTIGATOR_PATH = RESOURCE_PATH + "Investigator/";
    const string HISTORIES_PATH = "History/histories.json";
    const string CHAOS_BAG_DIRECTORY = "ChaosBag/";
    public const string ENCOUNTER_JSON = "Encounter.json";
    public const string ACT_JSON = "Act.json";
    public const string AGENDA_JSON = "Agenda.json";
    public const string LOCATION_JSON = "Location.json";
    public const string SCENARIO_JSON = "Scenario.json";
    public const string SPECIAL_JSON = "Special.json";

    public static string ScenarioPath { get; set; } = RESOURCE_PATH + "/" + GameData.Chapter + "/" + GameData.Scenario + "/";
    public static string HistoriesPath { get; set; } = RESOURCE_PATH + "/" + GameData.Chapter + "/" + GameData.Scenario + "/" + HISTORIES_PATH;
    public static string ChaosBagPath { get; set; } = RESOURCE_PATH + "/" + GameData.Chapter + "/" + CHAOS_BAG_DIRECTORY;
}
