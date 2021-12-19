using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {

        slider.value = health;
        if (health <= 0)
        {
            Destroy(player);
            StartCoroutine(youLose());
        }
    }

    public void Heal(float health)
    {
        float oldHealth = slider.value;
        if (oldHealth + health > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
        else
        {
            slider.value += health;
        }
    }

    IEnumerator youLose()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("YouLose");
    }

}
