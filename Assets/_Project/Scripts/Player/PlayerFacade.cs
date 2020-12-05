using PlayerState;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PlayerBehaviors
{
    public class PlayerFacade : MonoBehaviour
    {
   
        Player _model;
        
        [Inject]
        public void Construct(Player player)
        {
            _model = player;
            
        }

        public bool IsDead => _model.IsDead;

        public Vector3 Position
        {
            get => _model.Position;
            set =>_model.Position=value;
        }

        public Quaternion Rotation => _model.Rotation;

        public Animator Animator => _model.GetAnimator;
        
        public Rigidbody Rigidbody => _model.RigidBody;

        public LineRenderer Linerenderer => _model.LineRenderer;

        public BoxCollider Collider => _model.Collider;

        public void Addforce(Vector3 test) => _model.AddForce(test);

        public SkinnedMeshRenderer MeshRenderer => _model.MeshRenderer;

        public GameObject[] Weapons => _model.Weapons;

        public PlayerStateManager StateManager => _model.PlayerStateManager;
    }


}
