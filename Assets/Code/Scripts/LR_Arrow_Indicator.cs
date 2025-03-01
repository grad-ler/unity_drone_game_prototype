using System;
using UnityEngine;

public class LR_Arrow_Indicator : MonoBehaviour
{
    [SerializeField] private Transform droneTransform; // The player's drone
    [SerializeField] private LR_Track_Checkpoints trackCheckpoints; // Reference to the checkpoint manager
    
    private Transform _currentTargetCheckpoint;
    
    private void Start()
    {
        UpdateTargetCheckpoint();
        trackCheckpoints.OnPlayerCorrectCheckpoint += OnCheckpointPassed;
    }
    
    private void Update()
    {
        if (_currentTargetCheckpoint != null)
        {
            Vector3 directionToCheckpoint = (_currentTargetCheckpoint.position - droneTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCheckpoint);
            transform.rotation = lookRotation;
        }
    }
    
    private void OnCheckpointPassed(object sender, EventArgs e)
    {
        UpdateTargetCheckpoint();
    }
    
    private void UpdateTargetCheckpoint()
    {
        _currentTargetCheckpoint = trackCheckpoints.GetCheckpointTransform();
    }
}