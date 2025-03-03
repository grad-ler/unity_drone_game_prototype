using System;
using TMPro;
using UnityEngine;

public class LR_Player_Statistics_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceTimerText;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private GameObject timerBackground;

    private void Start()
    {
        if (LR_Game_Manager.Instance != null)
        {
            LR_Game_Manager.Instance.OnStateChanged += GameManager_OnStateChanged;
        }
        else
        {
            Debug.LogError("Game Manager instance not found!");
        }

        HideAll();
    }

    private void Update()
    {
        if (LR_Game_Manager.Instance == null || !LR_Game_Manager.Instance.IsGamePlaying())
            return;

        float raceTime = LR_Game_Manager.Instance.GetRaceTime(0); // Player index 0
        float remainingTime = LR_Game_Manager.Instance.GetTimeLeftAfterFirstFinish();

        if (remainingTime > 0)
        {
            // When remaining time starts, switch UI
            HideRaceTime();
            ShowRemainingTime(remainingTime);
        }
        else
        {
            // Normal race time display
            ShowRaceTime(raceTime);
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (LR_Game_Manager.Instance != null && LR_Game_Manager.Instance.IsGamePlaying())
        {
            ShowRaceTime(0); // Show race time initially
        }
        else
        {
            HideAll();
        }
    }

    private void ShowRaceTime(float raceTime)
    {
        if (raceTimerText != null)
        {
            raceTimerText.gameObject.SetActive(true);
            raceTimerText.text = $"{raceTime:0.00}s";
        }

        if (timerBackground != null)
        {
            timerBackground.SetActive(true);
        }
    }

    private void HideRaceTime()
    {
        if (raceTimerText != null)
        {
            raceTimerText.gameObject.SetActive(false);
        }
    }

    private void ShowRemainingTime(float remainingTime)
    {
        if (remainingTimeText != null)
        {
            remainingTimeText.gameObject.SetActive(true);
            remainingTimeText.text = $"{remainingTime:0.0}s";
        }
    }

    private void HideRemainingTime()
    {
        if (remainingTimeText != null)
        {
            remainingTimeText.gameObject.SetActive(false);
        }
    }

    private void HideAll()
    {
        HideRaceTime();
        HideRemainingTime();

        if (timerBackground != null)
        {
            timerBackground.SetActive(false);
        }
    }
}
