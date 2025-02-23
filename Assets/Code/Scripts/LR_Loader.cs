using UnityEngine;
using UnityEngine.SceneManagement;

public static class LR_Loader
{
    public enum Scene
    {
        Main_Menu_Scene,
        Game_Scene,
        Loading_Scene
    }
    
    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        LR_Loader.targetScene = targetScene;
        
        SceneManager.LoadScene(Scene.Loading_Scene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
