using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    GameObject boss;

    private void OnBecameVisible()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void Damage(float health)
    {

        slider.value -= health;
        if (slider.value <= 0)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            Destroy(boss);
            StartCoroutine(youWin());
        }
    }


    IEnumerator youWin()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("YouWin");
    }

}
