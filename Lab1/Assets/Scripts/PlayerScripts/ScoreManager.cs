using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI text;

    private int scoreSouls;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int changeSouls)
    {
        scoreSouls += changeSouls;
        text.text = $"Souls: {scoreSouls.ToString()}";
    }
}
