using UnityEngine;

namespace DroneGame
{
    
    [RequireComponent(typeof(BoxCollider))]
    public class LR_Drone_Engine : MonoBehaviour, IEngine
    {

        #region Variables

        [Header("Engine Properties")] 
        [SerializeField] private float maxPower =4f;

        [Header("Propeller Properties")] 
        [SerializeField] private Transform propeller;
        [SerializeField] private float propRotSpeed = 3000f;
        
        #endregion
        
        #region Interface Methods

        public void InitEngine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEngine(Rigidbody rb, LR_Drone_Inputs input)
        {
            //Debug.Log("Running Engine: " +  gameObject.name);
            Vector3 upVec = transform.up;
            upVec.x = 0;
            upVec.z = 0;
            float diff = 1 - upVec.magnitude;
            float finalDiff = Physics.gravity.magnitude * diff;
            
            Vector3 engineForce = Vector3.zero;
            
            // divided by 4 because of 4 engines
            engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude + finalDiff) + (input.CollectiveValue * maxPower)) / 4f;
            rb.AddForce(engineForce, ForceMode.Force);
            
            HandlePropellers();
        }

        void HandlePropellers()
        {
            if (!propeller)
            {
                return;
            }
            
            propeller.Rotate(new Vector3(0,0,1), propRotSpeed);
        }
        
        #endregion
        
    }
}
