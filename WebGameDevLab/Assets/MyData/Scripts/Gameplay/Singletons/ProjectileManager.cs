using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : GenericPoolManager<Projectile>
{
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    // When Loading New Scene
    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
    {
        pool.Clear();
    }
}
