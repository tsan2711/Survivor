using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance { get; set; }
    public float resourceHealth;
    public float resourceMaxHealth;
    public Text healthText;
    void Start()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Update()
    {
        healthText.text = resourceHealth + "/" + resourceMaxHealth;
    }
}
