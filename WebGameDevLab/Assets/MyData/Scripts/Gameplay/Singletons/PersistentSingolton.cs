using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingolton<T> : Singleton<T> where T : Component
{
    public bool AutoUnparentOnAwake = true;

    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();

        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}