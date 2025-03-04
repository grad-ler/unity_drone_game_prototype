using UnityEngine;

namespace DroneGame
{
    public class LR_Control_Scheme : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject controlSchemeUI;
        [SerializeField] private GameObject playerStatisticsUI;

        private bool _isVisible = false;

        void Start()
        {
            if (controlSchemeUI)
            {
                controlSchemeUI.SetActive(false); // Hide UI on start
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5) && LR_Game_Manager.Instance != null)
            {
                if (LR_Game_Manager.Instance.IsGamePlaying()) // Only allow toggling during gameplay
                {
                    ToggleControlScheme();
                }
            }
        }

        void ToggleControlScheme()
        {
            _isVisible = !_isVisible;
            
            if (controlSchemeUI)
            {
                controlSchemeUI.SetActive(_isVisible);
                playerStatisticsUI.SetActive(!_isVisible);
            }

            Time.timeScale = _isVisible ? 0f : 1f; // Pause/unpause the game
        }
    }
}