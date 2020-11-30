using UnityEngine;

namespace PlayerBehaviors
{
    public class Player
    {
        readonly Rigidbody _rigidBody;


        public Player(Rigidbody rigidBody, Animator animator)
        {
            _rigidBody = rigidBody;
            GetAnimator  = animator;
        }


      

        public bool IsDead
        {
            get; set;
        }

        public Animator GetAnimator { get; }



        public Vector3 LookDir => -_rigidBody.transform.right;

        public Quaternion Rotation
        {
            get => _rigidBody.rotation;
            set => _rigidBody.rotation = value;
        }

        public Vector3 Position
        {
            get => _rigidBody.position;
            set => _rigidBody.position = value;
        }


        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }
        

        public Rigidbody RigidBody => _rigidBody;

      

       
    }
}
    

