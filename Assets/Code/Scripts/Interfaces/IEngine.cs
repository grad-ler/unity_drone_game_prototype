using UnityEngine;

namespace DroneGame
{
    public interface IEngine
    {
        void InitEngine();
        void UpdateEngine(Rigidbody rb, LR_Drone_Inputs input);
    }
   
}