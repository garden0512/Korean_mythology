using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour
{
    public float playerMoveSpeed;

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(inputX, inputY)*Time.deltaTime * playerMoveSpeed);
    }
}
