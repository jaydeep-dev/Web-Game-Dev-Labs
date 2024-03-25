using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour, IExp
{
    [field:SerializeField] public int Exp {get; private set;}

    public int ItemWorth => Exp;

    public void OnCollect()
    {
        Destroy(gameObject);
    }
}
