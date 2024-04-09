using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract class StatusBar : MonoBehaviour
{
    protected int max;

    protected Slider slider;
    public Text stateText;
    public abstract void set(ref int current, int amount);
    public abstract void decrease(ref int current, int amount);
    public abstract void increase(ref int current, int amount);
    public abstract void updateSlider(int current);
    public abstract void setStatusText(int current);
    public abstract void setMaxValue(int amount);
    public abstract int getMaxValue();
    protected void Initalize()
    {
        updateSlider(max);
        setStatusText(max);
    }
}
