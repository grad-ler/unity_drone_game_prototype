using System;
using UnityEngine;

public class LR_Game_Manager : MonoBehaviour
{
    public static LR_Game_Manager Instance{ get; private set; }
    
    public event EventHandler OnStateChanged;
    
    [SerializeField] private LR_Track_Checkpoints trackCheckpoints;
    [SerializeField] private int totalCheckpoints;
    
    private bool _goalReached = false;
    private int _currentCheckpoints = 0;
    private float _raceTime;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    
    private State _state;

    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;

    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
    }

    private void Start()
    {
        trackCheckpoints.OnPlayerCorrectCheckpoint += GameManager_OnPlayerCorrectCheckpoint;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer <= 0)
                {
                    _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer <= 0)
                {
                    _state = State.GamePlaying;
                    _raceTime = 0f; // Reset race time when the game starts
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                // Track race time as long as the game is playing
                _raceTime += Time.deltaTime;
                
                if (_goalReached)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
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
        return _countdownToStartTimer;
    }

    public float GetRaceTime()
    {
        return _raceTime;
    }
    
    private void GameManager_OnPlayerCorrectCheckpoint(object sender, EventArgs e)
    {
        _currentCheckpoints++;
        Debug.Log("Current checkpoint: " + _currentCheckpoints);
        if (_currentCheckpoints == totalCheckpoints)
        {
            _goalReached = true;
        }
    }
}
