using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // if (Monster_AI.instance == null || !Monster_AI.instance.isLive || !Monster_AI.instance.isPlayerInRange)
        // {
        //     mySlider.gameObject.SetActive(false);
        //     return;
        // }
        // mySlider.gameObject.SetActive(true);
        switch (type)
        {
            case InfoType.Exp:
                float curHp = Monster_AI.instance.health;
                float maxHp = Monster_AI.instance.maxHealth;
                float attack = Player_Attack.instance.attackDamage;
                mySlider.value = (curHp/maxHp);                
                break;
            case InfoType.Health:

                break;
        }
    }
}
