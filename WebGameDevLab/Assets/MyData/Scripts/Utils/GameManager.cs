

using UnityEngine.SceneManagement;

public static class GameManager
{
    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

public enum Scenes
{
    Mainmenu,
    Gameplay
}

public enum PlayerEvents
{
    Alive,
    Dead,
}