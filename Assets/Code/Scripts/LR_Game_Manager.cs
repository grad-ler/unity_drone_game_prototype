using System;
using DroneGame;
using UnityEngine;
using UnityEngine.InputSystem;

public class LR_Game_Manager : MonoBehaviour
{
    public static LR_Game_Manager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    [SerializeField] private LR_Track_Checkpoints[] trackCheckpoints;
    [SerializeField] private int totalCheckpoints;
    [SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float timeAfterFirstFinish = 5f;
    
    private bool[] _goalReached;
    private int[] _currentCheckpoints;
    private float[] _raceTime;
    private bool _firstPlayerFinished = false;
    private float _timeAfterFirstFinishTimer = 0f;
    private int _firstFinisherIndex = -1;
    private PlayerInput[] _playerInputs;
    
    private State _state;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
        _goalReached = new bool[trackCheckpoints.Length];
        _currentCheckpoints = new int[trackCheckpoints.Length];
        _raceTime = new float[trackCheckpoints.Length];
        _playerInputs = new PlayerInput[trackCheckpoints.Length];

        // Initialize all race times to -1 (indicating DNF)
        for (int i = 0; i < _raceTime.Length; i++)
        {
            _raceTime[i] = -1f;
        }
    }

    private void Start()
    {
        // Get PlayerInput components for each player
        for (int i = 0; i < trackCheckpoints.Length; i++)
        {
            int playerIndex = i;
            if (trackCheckpoints[i].droneTransform != null)
            {
                PlayerInput playerInput = trackCheckpoints[i].droneTransform.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    _playerInputs[i] = playerInput;
                }
                else
                {
                    Debug.LogError($"PlayerInput component NOT found on droneTransform for Track_Checkpoints at index {i}!");
                }
            }
            else
            {
                Debug.LogError($"droneTransform is NOT assigned in Inspector for Track_Checkpoints at index {i}!");
            }

            trackCheckpoints[i].OnPlayerCorrectCheckpoint += (sender, EventArgs) => GameManager_OnPlayerCorrectCheckpoint(sender, EventArgs, playerIndex);
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
                
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    _state = State.GamePlaying;
                    ResetRace();
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
                
            case State.GamePlaying:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    LR_Loader.Load(LR_Loader.Scene.Main_Menu_Scene);
                }
                
                // Track race time for each active player
                for (int i = 0; i < _raceTime.Length; i++)
                {
                    if (!_goalReached[i]) // Only update time if player hasn't finished
                    {
                        _raceTime[i] += Time.deltaTime;
                    }
                }

                // Check if first player finished
                if (!_firstPlayerFinished)
                {
                    CheckForFirstFinisher();
                }
                else
                {
                    // Update timer after first player finished
                    _timeAfterFirstFinishTimer += Time.deltaTime;
                    
                    // Time's up - end the race
                    if (_timeAfterFirstFinishTimer >= timeAfterFirstFinish)
                    {
                        // Mark all unfinished players as DNF
                        MarkUnfinishedPlayersAsDNF();
                        
                        _state = State.GameOver;
                        DisableAllPlayerControls();
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        // Check if all players finished before time ran out
                        bool allFinished = true;
                        for (int i = 0; i < _goalReached.Length; i++)
                        {
                            if (!_goalReached[i])
                            {
                                allFinished = false;
                                break;
                            }
                        }
                        
                        if (allFinished)
                        {
                            _state = State.GameOver;
                            DisableAllPlayerControls();
                            OnStateChanged?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                break;
                
            case State.GameOver:
                // No update needed in game over state
                break;
        }
    }


    private void MarkUnfinishedPlayersAsDNF()
    {
        for (int i = 0; i < _goalReached.Length; i++)
        {
            if (!_goalReached[i])
            {
                Debug.Log($"Player {i + 1} did not finish within time limit - marking as DNF");
                _raceTime[i] = -1f; // Set to -1 to indicate DNF
            }
        }
    }

    private void CheckForFirstFinisher()
    {
        for (int i = 0; i < _goalReached.Length; i++)
        {
            if (_goalReached[i])
            {
                _firstPlayerFinished = true;
                _firstFinisherIndex = i;
                _timeAfterFirstFinishTimer = 0f;
                DisablePlayerControls(i); // Disable controls for the first finisher
                Debug.Log($"Player {i + 1} finished first! Other players have {timeAfterFirstFinish} seconds left.");
                break;
            }
        }
    }

    private void DisableAllPlayerControls()
    {
        for (int i = 0; i < _playerInputs.Length; i++)
        {
            DisablePlayerControls(i);
        }
    }

    private void DisablePlayerControls(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < _playerInputs.Length && _playerInputs[playerIndex] != null)
        {
            _playerInputs[playerIndex].DeactivateInput();
        
            // Shut down engines
            LR_Drone_Controller droneController = _playerInputs[playerIndex].GetComponent<LR_Drone_Controller>();
            if (droneController != null)
            {
                droneController.ShutdownEngines();
            }
        }
    }



    private void ResetRace()
    {
        for (int i = 0; i < _goalReached.Length; i++)
        {
            _goalReached[i] = false;
            _currentCheckpoints[i] = 0;
            _raceTime[i] = 0f; // Start with 0 time
        }
        _firstPlayerFinished = false;
        _firstFinisherIndex = -1;
        _timeAfterFirstFinishTimer = 0f;
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public float GetRaceTime(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < _raceTime.Length)
        {
            return _raceTime[playerIndex];
        }
        Debug.LogError("Invalid player index in GetRaceTime: " + playerIndex);
        return -1f;
    }

    public int GetPlayerPosition(int playerIndex)
    {
        // If player didn't finish, return last position
        if (_raceTime[playerIndex] < 0)
            return _playerInputs.Length;
            
        // Calculate position based on finish times
        int position = 1;
        for (int i = 0; i < _raceTime.Length; i++)
        {
            // Skip comparing against self
            if (i == playerIndex)
                continue;
                
            // If another player has a better (lower) time and finished
            if (_raceTime[i] > 0 && _raceTime[i] < _raceTime[playerIndex])
                position++;
        }
        
        return position;
    }

    public float GetTimeLeftAfterFirstFinish()
    {
        if (_firstPlayerFinished)
        {
            return timeAfterFirstFinish - _timeAfterFirstFinishTimer;
        }
        return 0f;
    }

    private void GameManager_OnPlayerCorrectCheckpoint(object sender, EventArgs e, int playerIndex)
    {
        _currentCheckpoints[playerIndex]++;
        
        // Check if player completed all checkpoints
        if (_currentCheckpoints[playerIndex] >= totalCheckpoints)
        {
            _goalReached[playerIndex] = true;
            // Race time is already tracked correctly, no need to update it
            Debug.Log($"Player {playerIndex + 1} finished with time: {_raceTime[playerIndex]:F2}s");
        }
    }
}