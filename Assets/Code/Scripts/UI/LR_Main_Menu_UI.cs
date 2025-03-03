using UnityEngine;
using UnityEngine.UI;

public class LR_Main_Menu_UI : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            LR_Loader.Load(LR_Loader.Scene.Game_Scene);
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
