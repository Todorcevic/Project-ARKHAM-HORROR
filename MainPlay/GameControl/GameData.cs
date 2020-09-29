using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public const int INITIAL_RESOURCES = 5;
    public const float ANIMATION_TIME_DEFAULT = 0.4f;
    public const float INTERACTIVE_TIME_DEFAULT = 0.25f;
    public const float DELAY_TIME_DEFAULT = 0.05f;
    public const float CARD_THICK = 0.01f;

    public static int InvestigatorsStartingAmount { get; set; } = 2;
    public static int DrawInitialHand { get; set; } = 5;
    public static int MaxSizeHand { get; set; } = 8;
    public static string Chapter { get; set; } = "Core";
    public static Language Language { get; set; } = Language.ES;
    public static Difficulty Difficulty { get; set; } = Difficulty.Standard;
    public static string Scenario { get; set; } = "Scenario1";
    public static Card[] CardDataList { get; set; }
    public static Dictionary<string, Card> CardDataDictionary { get; set; }
    public static Color ExaustedColor { get => new Color(1f, 0.5f, 0.5f); }

}