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
        if (other.TryGetComponent(out CollectibleItem item))
        {
            playerScore += item.ItemWorth;
            OnPlayerScoreChanged?.Invoke(playerScore);
            Destroy(item.gameObject);
        }
    }
}
