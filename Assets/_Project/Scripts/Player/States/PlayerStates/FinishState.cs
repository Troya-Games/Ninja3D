using PlayerBehaviors;
using UnityEngine;

namespace PlayerState
{
    public class FinishState : IState
    {
        private Player _player;

        FinishState(Player player)
        {
            _player = player;
        }
        public void EnterState()
        {
            _player.GetAnimator.Play("FINISHState");
        }

        public void ExitState()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }

}
