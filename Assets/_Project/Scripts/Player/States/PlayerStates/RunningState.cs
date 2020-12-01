using PlayerBehaviors;
using UnityEngine;

namespace PlayerState
{
    public class RunningState : IState
    {
        private Player _player;

        RunningState(Player player)
        {
            _player = player;
        }
   
        public void EnterState()
        {
            Debug.Log("RunnigStateEnter");
            _player.LineRenderer.enabled = false;


        }

        public void ExitState()
        {
        }

   

        public void FixedUpdate()
        {
        }

        public void Update()
        {
         
        }
    }

}

