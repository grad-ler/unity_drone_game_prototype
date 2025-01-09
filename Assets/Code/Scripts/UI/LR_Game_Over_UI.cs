using System;
using TMPro;
using UnityEngine;

public class LR_Game_Over_UI : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI raceTimeText;

    private void Start()
    {
        LR_Game_Manager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        Hide();
    }
    
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (LR_Game_Manager.Instance.IsGameOver())
        {
            Show();
            
            // Retrieve and display the final race time
            float raceTime = LR_Game_Manager.Instance.GetRaceTime();
            raceTimeText.text = raceTime.ToString("F2") + "s";
        }
        else
        {
            Hide();
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
