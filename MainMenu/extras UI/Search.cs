﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CardManager
{
    public class Search : MonoBehaviour
    {
        public ViewCardManager visivilityManager;
        public void searchText()
        {
            visivilityManager.TextSearch = GetComponent<TMP_InputField>().text;
        }
    }
}
