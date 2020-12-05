using PlayerState;
using UnityEngine;
using Zenject;

namespace PlayerBehaviors
{
    public class Player
    {
        
        public Player(Rigidbody rigidBody, Animator animator
            ,LineRenderer lineRenderer,BoxCollider collider,SkinnedMeshRenderer meshRenderer,GameObject[] weapons)
        {
            RigidBody = rigidBody;
            GetAnimator  = animator;
            LineRenderer = lineRenderer;
            Collider = collider;
            MeshRenderer = meshRenderer;
            Weapons = weapons;

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
 
        public BoxCollider Collider { get; }
        
       
        public SkinnedMeshRenderer MeshRenderer { get; }

        public GameObject[] Weapons { get; }
        
      [Inject] public PlayerStateManager PlayerStateManager {get;}
    }
}
    

