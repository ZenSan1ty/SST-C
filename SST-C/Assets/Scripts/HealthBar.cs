using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBarImage;
    public Player player;

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp((float)player.currentHealth / (float)player.maxHealth, 0, 1f);
    }
}
