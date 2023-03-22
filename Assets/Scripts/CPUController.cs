using System.Collections;
using UnityEngine;

namespace Shirahadori {
    public class CPUController : BaseCharacterController
    {
        
        private Animator animator;
        private State _state;
        private float speed = 1;

        private enum State
        {
            Action,
            Idle
        }

        new private void Awake()
        {
            base.Awake();

        }

        public void StartAction()
        {
            gameManager.StartAction();
        }

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            _state = State.Idle;
        }        

        public override void OnStartGame()
        {            
            _Start();
            var randomValue = Random.value;
            speed = 0.8f + randomValue;      // 0.8f <= speed <= 1.8f
        }

        public override void OnStartReplay()
        {            
            _Start();
        }

        public override void OnEndGame()
        {            
            _Reset();
        }

        public override void OnAction()
        {
            StopAnimation();
        }

        public override void OnReset()
        {            
            _Reset();            
        }

        public void ChangeVelocity()
        {
            animator.speed = speed;
        }

        public void EndAction()
        {
            gameManager.OnEndAction();
        }

        public override void OnEndAction()
        {
            StopAnimation();
        }

        private void StopAnimation()
        {
            animator.speed = 0;
        }

        private void _Reset()
        {
            animator.speed = 1;
            animator.ResetTrigger("Action");
            animator.SetTrigger("Idle");
            _state = State.Idle;
        }

        private void _Start()
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Action");
            _state = State.Action;
        }
    }
}
