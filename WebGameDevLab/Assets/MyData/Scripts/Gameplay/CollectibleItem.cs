using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private int itemWorth;

    public int ItemWorth => itemWorth;
}
