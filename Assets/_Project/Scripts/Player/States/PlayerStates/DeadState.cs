using PlayerBehaviors;
using UnityEngine;

namespace PlayerState
{
    public class DeadState : IState
    {
        private Player _player;

        DeadState(Player player)
        {
            _player = player;
        }
        public void EnterState()
        {

            _player.GetAnimator.Play("DEATHState");
            Debug.Log("PlayerDeadStateEnter");
        }

        public void ExitState()
        {
            Debug.Log("PlayerDeadStateExit");
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}

