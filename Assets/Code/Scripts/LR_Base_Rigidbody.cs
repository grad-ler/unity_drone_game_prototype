using UnityEngine;

namespace DroneGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class LR_Base_Rigidbody : MonoBehaviour
    {
        #region Variables

        [Header("Rigidbody Properties")]
    
        [SerializeField] private float weightInKgs = 1f;
    
        protected Rigidbody Rb;
    
        protected float StartDrag;
        protected float StartAngularDrag;
    
        #endregion
    
        void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            StartDrag = Rb.linearDamping;
            StartAngularDrag = Rb.angularDamping;
        }
    
        void FixedUpdate()
        {
            if (!Rb)
            {
                return;
            }

            HandlePhysics();
        }

        #region Custom Methods
    
        protected virtual void HandlePhysics()
        {
        
        }
    
        #endregion
    }
    
}
