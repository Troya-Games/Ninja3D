using DG.Tweening;
using Miscs;
using PlayerBehaviors;
using UnityEngine;

namespace PlayerState
{
    public class DeadState : IState
    {
        private Player _player;
        private UIManager.StateUISettings _uıSettings;

        DeadState(Player player,UIManager.StateUISettings uıSettings)
        {
            _player = player;
            _uıSettings = uıSettings;
        }
        public void EnterState()
        {

            DOTween.KillAll();
            _player.GetAnimator.Play("DEATHState");
            _player.RigidBody.ResetVelocity();
            _uıSettings._gameDeadUI.SetActive(true);
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

