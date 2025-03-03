using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LR_Game_Over_UI : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI playerOnePositionText;
    [SerializeField] private TextMeshProUGUI playerTwoPositionText;
    [SerializeField] private TextMeshProUGUI playerOneTimeText;
    [SerializeField] private TextMeshProUGUI playerTwoTimeText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        LR_Game_Manager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        // Setup button listeners
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() =>
            {
                LR_Loader.Load(LR_Loader.Scene.Game_Scene);
            });
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                LR_Loader.Load(LR_Loader.Scene.Main_Menu_Scene);
            });
        }
        
        Hide();
    }
    
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (LR_Game_Manager.Instance.IsGameOver())
        {
            Show();
            UpdateResultsDisplay();
        }
        else
        {
            Hide();
        }
    }

    private void UpdateResultsDisplay()
    {
        // Retrieve race times and positions for both players
        float playerOneRaceTime = LR_Game_Manager.Instance.GetRaceTime(0);
        float playerTwoRaceTime = LR_Game_Manager.Instance.GetRaceTime(1);
        
        int playerOnePosition = LR_Game_Manager.Instance.GetPlayerPosition(0);
        int playerTwoPosition = LR_Game_Manager.Instance.GetPlayerPosition(1);
        
        // Display positions (1st, 2nd, etc.)
        playerOnePositionText.text = GetPositionText(playerOnePosition);
        playerTwoPositionText.text = GetPositionText(playerTwoPosition);
        
        // Display race times or DNF
        playerOneTimeText.text = FormatRaceTime(playerOneRaceTime);
        playerTwoTimeText.text = FormatRaceTime(playerTwoRaceTime);
    }
    
    private string GetPositionText(int position)
    {
        switch (position)
        {
            case 1:
                return "1st";
            case 2:
                return "2nd";
            default:
                return position + "th";
        }
    }
    
    private string FormatRaceTime(float time)
    {
        if (time < 0)
        {
            return "DNF";
        }
        else
        {
            return time.ToString("F2") + "s";
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}