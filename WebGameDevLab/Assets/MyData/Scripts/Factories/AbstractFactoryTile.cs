using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactoryTile : MonoBehaviour
{
    [SerializeField] protected GameObject tilePrefab;

    public abstract void CreateTile();
}
