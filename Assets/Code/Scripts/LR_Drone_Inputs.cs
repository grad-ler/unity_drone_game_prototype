using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class LR_Drone_Inputs : MonoBehaviour
    {
        #region Variables
    
        public Vector2 CyclicValue { get; private set; }
        public float CollectiveValue { get; private set; }
        public float PedalValue { get; private set; }
    
        #endregion
    
        #region Input Methods

        public void OnCyclic(InputAction.CallbackContext context)
        {
            CyclicValue = context.ReadValue<Vector2>();
        }

        public void OnPedals(InputAction.CallbackContext context)
        {
            PedalValue = context.ReadValue<float>();
        }

        public void OnCollective(InputAction.CallbackContext context)
        {
            CollectiveValue = context.ReadValue<float>();
        }
    
        #endregion
    }
}
