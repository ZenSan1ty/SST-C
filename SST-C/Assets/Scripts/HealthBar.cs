using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBarImage;
    public Player player;
    public SwordBoss swordBoss;
    public RockBoss rockBoss;

    public void UpdateHealthBar()
    {
        if (player != null)
        {
            healthBarImage.fillAmount = Mathf.Clamp((float)player.currentHealth / (float)player.maxHealth, 0, 1f);
        }
        else if (swordBoss != null)
        {
            healthBarImage.fillAmount = Mathf.Clamp((float)swordBoss.curHealth / (float)swordBoss.maxHealth, 0, 1f);
        }
        else if (rockBoss != null)
        {
            healthBarImage.fillAmount = Mathf.Clamp((float)rockBoss.curHealth / (float)rockBoss.maxHealth, 0, 1f);

        }
    }
}
