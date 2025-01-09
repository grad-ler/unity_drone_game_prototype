using System;
using TMPro;
using UnityEngine;

public class LR_Player_Statistics_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceTimerText;

    private void Start()
    {
        LR_Game_Manager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        Hide();
    }

    private void Update()
    {
        raceTimerText.text = LR_Game_Manager.Instance.GetRaceTime().ToString("0.00");
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (LR_Game_Manager.Instance.IsGamePlaying())
        {
            Show();
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
