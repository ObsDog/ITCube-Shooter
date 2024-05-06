using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    private PlayerController player;

    private void OnValidate()
    {
        player ??= GetComponent<PlayerController>();
    }

    private void Update()
    {
        healthText.text = "HP: " + player.health.ToString();
    }
}
