using Miscs;
using PlayerBehaviors;
using UnityEngine;

namespace PlayerState
{
    public class RunningState : IState
    {
        private readonly Player _player;
        private UIManager.StateUISettings _uıSettings;

        RunningState(Player player,UIManager.StateUISettings uıSettings)
        {
            _player = player;
            _uıSettings = uıSettings;
        }
   
        public void EnterState()
        {
            _player.LineRenderer.enabled = false;
            _player.GetAnimator.Play("RUNState");


        }

        public void ExitState()
        {
            _uıSettings._gameInUI.SetActive(false);
        }

   

        public void FixedUpdate()
        {
        }

        public void Update()
        {
         
        }
    }

}

