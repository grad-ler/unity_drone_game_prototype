using System;
using UnityEngine;

public class LR_Track_Checkpoints_UI : MonoBehaviour
{
     [SerializeField] private LR_Track_Checkpoints trackCheckpoints;

     private void Start()
     { 
          trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnPlayerCorrectCheckpoint; 
          Hide();
     }

     private void TrackCheckpoints_OnPlayerCorrectCheckpoint(object sender, EventArgs e)
     {
          Hide();
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
