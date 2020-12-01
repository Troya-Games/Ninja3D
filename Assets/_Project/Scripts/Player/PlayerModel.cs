using UnityEngine;

namespace PlayerBehaviors
{
    public class Player
    {
        
        public Player(Rigidbody rigidBody, Animator animator,LineRenderer lineRenderer)
        {
            RigidBody = rigidBody;
            GetAnimator  = animator;
            LineRenderer = lineRenderer;
        }

        

        public bool IsDead
        {
            get; set;
        }

        public Animator GetAnimator { get; }

        
        public Vector3 LookDir => -RigidBody.transform.right;

        public Quaternion Rotation
        {
            get => RigidBody.rotation;
            set => RigidBody.rotation = value;
        }

        public Vector3 Position
        {
            get => RigidBody.position;
            set => RigidBody.position = value;
        }


        public void AddForce(Vector3 force)
        {
            RigidBody.AddForce(force);
        }
        

        public Rigidbody RigidBody { get; }
        
        public LineRenderer LineRenderer { get; }
 
        
       

       
    }
}
    

