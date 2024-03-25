using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectHandler : MonoBehaviour
{
    public int playerScore;

    public static event Action<int> OnPlayerScoreChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IExp item))
        {
            playerScore += item.Exp;
            OnPlayerScoreChanged?.Invoke(playerScore);
            item.OnCollect();
        }
    }
}
