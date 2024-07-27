using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(Vector2 currentPosition, Vector2 targetPosition, float speed, float detectionRadius, ref Vector2 randomDirection, ref float changeDirectionTimer, float changeDirectionTime, bool isAttacking);
}
