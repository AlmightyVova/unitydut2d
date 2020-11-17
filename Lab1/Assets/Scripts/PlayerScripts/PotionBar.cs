using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxShield(int maxShield)
    {
        slider.maxValue = maxShield;
    }

    public void SetShield(int shield)
    {
        slider.value = shield;
    }
}