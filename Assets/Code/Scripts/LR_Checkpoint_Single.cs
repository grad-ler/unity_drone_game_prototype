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

    private void OnTriggerEnter(Collider other)
    {
        if (_trackCheckpoints == null) return;

        Debug.Log("other: " + other.name + " | gameObject: " + gameObject.name);
        
        if (other.TryGetComponent<PlayerInput>(out PlayerInput player))
        {
            _trackCheckpoints.DroneThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(LR_Track_Checkpoints trackCheckpoints)
    {
        _trackCheckpoints = trackCheckpoints;
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