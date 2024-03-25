using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryGrass : AbstractFactoryTile
{

    public override void CreateTile()
    {
        Debug.Log("Grass Tile Created");
    }
}
