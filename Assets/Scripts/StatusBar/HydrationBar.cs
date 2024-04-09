using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class HydrationBar : StatusBar
{
    public static HydrationBar Instance;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        slider = GetComponent<Slider>();
    }

    public override void decrease(ref int current, int amount)
    {
        if (current - amount >= 0)
        {
            current -= amount;
        }
        if (current <= 0)
        {
            current = 0;
            Debug.Log("Player is Dead");
        }
        updateSlider(current);
        setStatusText(current);
    }
    public override void increase(ref int current, int amount)
    {
        if (current + amount <= getMaxValue())
        {
            current += amount;
        }
        if (current > getMaxValue())
        {
            current = getMaxValue();
        }
        updateSlider(current);
        setStatusText(current);
    }

    public override void updateSlider(int current)
    {
        slider.value = (float)current / getMaxValue();
    }

    public override void setStatusText(int current)
    {
        stateText.text = current + "%";
    }
    public override void set(ref int currentHydation, int amount)
    {
        currentHydation = amount;
        updateSlider(currentHydation);
        setStatusText(currentHydation);
    }

    public override void setMaxValue(int amount)
    {
        max = amount;
    }
    public override int getMaxValue()
    {
        return max;
    }
}
