using System.Collections;
using UnityEngine;

namespace Shirahadori {
    public class CPUController : BaseCharacterController
    {

        private bool playing = false;
        private Animator animator;
        private State _state;

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

        private void Start()
        {
        }

        public override void OnStartGame()
        {
            playing = true;
            _Start();
        }

        public override void OnStartReplay()
        {            
            _Start();
        }

        public override void OnEndGame()
        {            
            animator.speed = 1;
            _Reset();
        }

        public override void OnAction()
        {            
            animator.speed = 0;
        }

        public override void OnReset()
        {            
            _Reset();
            animator.speed = 1;
        }

        private void OnChangeVelocity()
        {

        }

        private void OnEnd()
        {

        }

        private void _Reset()
        {
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
