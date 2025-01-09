using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LR_Checkpoint_Single : MonoBehaviour
{
    private LR_Track_Checkpoints _trackCheckpoints;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // private void Start()
    // {
    //     Hide();
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerInput>(out PlayerInput player))
        {
            _trackCheckpoints.DroneThroughCheckpoint(this, other.transform);
        }
    }

    public void SetTrackCheckpoints(LR_Track_Checkpoints trackCheckpoints)
    {
        this._trackCheckpoints = trackCheckpoints;
    }

    public void Show()
    {
        _meshRenderer.enabled = true;
    }

    public void Hide()
    {
        _meshRenderer.enabled = false;
    }
}
