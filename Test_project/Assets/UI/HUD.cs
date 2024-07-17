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
