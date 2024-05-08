using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [HideInInspector] public bool isCooldown;
    [SerializeField] private float coolDown;

    private Image shieldImage;
    private PlayerController player;

    private void OnValidate()
    {
        shieldImage ??= GetComponent<Image>();
        player ??= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        isCooldown = true;
    }

    private void Update()
    {
        if(isCooldown)
        {
            shieldImage.fillAmount -= 1 / coolDown * Time.deltaTime;
            if(shieldImage.fillAmount <= 0 )
            {
                shieldImage.fillAmount = 1;
                isCooldown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }

    public void ReduceTimer(float damage)
    {
        shieldImage.fillAmount += damage / 70f;
    }
}
