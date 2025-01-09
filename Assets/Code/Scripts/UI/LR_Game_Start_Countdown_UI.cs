using System;
using TMPro;
using UnityEngine;

public class LR_Game_Start_Countdown_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        LR_Game_Manager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update()
    {
        countdownText.text = Mathf.RoundToInt(LR_Game_Manager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (LR_Game_Manager.Instance.IsCountdownToStartActive())
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
