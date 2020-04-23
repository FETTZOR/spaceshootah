using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    Player player;
    void Start()
    {
       healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}

