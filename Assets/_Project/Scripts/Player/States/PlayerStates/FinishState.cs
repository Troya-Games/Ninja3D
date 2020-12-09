using System;
using Miscs;
using PlayerBehaviors;
using UniRx;
using UnityEngine;

namespace PlayerState
{
    public class FinishState : IState
    {
        private Player _player;
        private UIManager.StateUISettings _stateUI;
        private UIManager.ParticleEffects _particle;

        FinishState(Player player,UIManager.StateUISettings stateUI,UIManager.ParticleEffects particle)
        {
            _player = player;
            _stateUI = stateUI;
            _particle = particle;
        }
        public void EnterState()
        {
            _player.GetAnimator.Play("FINISHState");
            _player.Weapons[0].SetActive(false);
            _player.Weapons[1].transform.localPosition = new Vector3(8.4f, 8.5f, -1.7f);
            _player.Weapons[1].transform.localEulerAngles = new Vector3(169.983f, -113.502f, -309.758f);
            Observable.Timer(TimeSpan.FromSeconds(1.2)).Subscribe(x =>
            {
                _particle._finishParticle.Play();
                _stateUI._gameFinishUI.SetActive(true);

            });
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
