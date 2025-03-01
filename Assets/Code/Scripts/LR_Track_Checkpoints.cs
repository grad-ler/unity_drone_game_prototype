using System;
using System.Collections.Generic;
using UnityEngine;

public class LR_Track_Checkpoints : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;

    public Transform droneTransform;
    [SerializeField] private List<LR_Checkpoint_Single> checkpointSingleList;

    private int _nextCheckpointIndex;

    private void Start()
    {
        foreach (LR_Checkpoint_Single checkpoint in checkpointSingleList)
        {
            checkpoint.SetTrackCheckpoints(this);
            checkpoint.Hide();
        }

        _nextCheckpointIndex = 0;
        checkpointSingleList[_nextCheckpointIndex].Show();
    }

    public void DroneThroughCheckpoint(LR_Checkpoint_Single checkpointSingle)
    {
        int checkpointIndex = checkpointSingleList.IndexOf(checkpointSingle);

        if (checkpointIndex == _nextCheckpointIndex)
        {
            checkpointSingleList[_nextCheckpointIndex].Hide();

            _nextCheckpointIndex = (_nextCheckpointIndex + 1) % checkpointSingleList.Count;
            checkpointSingleList[_nextCheckpointIndex].Show();

            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }

    public Transform GetCheckpointTransform()
    {
        return checkpointSingleList[_nextCheckpointIndex].transform;
    }
}
