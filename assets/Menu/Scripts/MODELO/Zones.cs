﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamMenu
{
    public class Zones
    {
        public string zoneName;
        public Transform zoneTransform;
        public List<CardBaseComponent> zoneCards;

        public static List<Zones> listZones = new List<Zones>();
        public static Dictionary<string, Zones> dictionaryZones = new Dictionary<string, Zones>();

        public static void CreateDictionaryZones()
        {
            if (listZones.Count > 0) return;
            foreach (GameObject _zone in GameObject.FindGameObjectsWithTag("Zone"))
            {
                Zones newZone = new Zones();
                newZone.zoneName = _zone.name;
                newZone.zoneTransform = _zone.transform;
                newZone.zoneCards = new List<CardBaseComponent>();
                listZones.Add(newZone);
                dictionaryZones.Add(_zone.name, newZone);
            }
        }
    }
}
