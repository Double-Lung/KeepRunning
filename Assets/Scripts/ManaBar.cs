using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxMana(float maxMana) {
        slider.maxValue = maxMana;
        slider.value = maxMana;
        fill.color = gradient.Evaluate(1);
    }
    public void SetMana(float mana) {
        slider.value = mana;
        fill.color = gradient.Evaluate(1);
    }
}
