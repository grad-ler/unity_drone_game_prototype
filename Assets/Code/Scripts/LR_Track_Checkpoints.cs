using System;
using System.Collections.Generic;
using UnityEngine;

public class LR_Track_Checkpoints : MonoBehaviour
{
    
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerFalseCheckpoint;
    
    [SerializeField] private List<Transform> droneTransformList;
    private List<LR_Checkpoint_Single> _checkpointSingleList;
    private List<int> _nextCheckpointSingleIndexList;
    
    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        _checkpointSingleList = new List<LR_Checkpoint_Single>();
    
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            LR_Checkpoint_Single checkpointSingle = checkpointSingleTransform.GetComponent<LR_Checkpoint_Single>();
            checkpointSingle.SetTrackCheckpoints(this);
        
            _checkpointSingleList.Add(checkpointSingle);

            // Make only the first checkpoint visible
            if (_checkpointSingleList.Count == 1)
            {
                checkpointSingle.Show();
            }
            else
            {
                checkpointSingle.Hide();
            }
        }
    
        _nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform droneTransform in droneTransformList)
        {
            _nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void DroneThroughCheckpoint(LR_Checkpoint_Single checkpointSingle, Transform droneTransform)
    {
        int nextCheckpointSingleIndex = _nextCheckpointSingleIndexList[droneTransformList.IndexOf(droneTransform)];
        if (_checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //correct checkpoint
            Debug.Log("Correct!");
            LR_Checkpoint_Single correctCheckpointSingle = _checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();
            
            _nextCheckpointSingleIndexList[droneTransformList.IndexOf(droneTransform)] = (nextCheckpointSingleIndex + 1) % _checkpointSingleList.Count;
            _checkpointSingleList[_nextCheckpointSingleIndexList[droneTransformList.IndexOf(droneTransform)]].Show();
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //false checkpoint
            Debug.Log("Wrong!");
            OnPlayerFalseCheckpoint?.Invoke(this, EventArgs.Empty);

            LR_Checkpoint_Single correctCheckpointSingle = _checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }
        
    }
    
    public int GetNextCheckpointIndex(Transform droneTransform)
    {
        return _nextCheckpointSingleIndexList[droneTransformList.IndexOf(droneTransform)];
    }

    public Transform GetCheckpointTransform(int index)
    {
        return _checkpointSingleList[index].transform;
    }
}
