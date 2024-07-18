using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Health}
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curHp = Monster_AI.instance.health;
                float maxHp = Monster_AI.instance.maxHealth;
                //bool PlayerIsIn = Monster_AI.instance.isPlayerInRange;
                mySlider.value = (curHp/maxHp);
                // if(PlayerIsIn)
                // {
                //     mySlider.gameObject.SetActive(true);
                // }                
                break;
            case InfoType.Health:
                float curHealth = Player_Health.instance.health;
                float maxHealth = Player_Health.instance.maxHealth;
                mySlider.value = (curHealth/maxHealth);
                break;
        }
    }
}
