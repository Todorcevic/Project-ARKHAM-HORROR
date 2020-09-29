using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardManager
{
    public class Zones
    {
        public string zoneName;
        public Transform zoneTransform;
        public List<CardBaseComponent> zoneCards;

        public static Dictionary<string, Zones> zone = new Dictionary<string, Zones>();

        public static void CreateDictionaryZones()
        {
            GameObject[] zones = GameObject.FindGameObjectsWithTag("Zone");

            foreach (GameObject _zone in zones)
            { 
                Zones newZone = new Zones();
                newZone.zoneName = _zone.name;
                newZone.zoneTransform = _zone.transform;
                newZone.zoneCards = new List<CardBaseComponent>();

                zone.Add(_zone.name, newZone);
            }
        }
    }
}
