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
            animator.SetTrigger("Action");
            _state = State.Action;
        }

        public override void OnStartReplay()
        {
            base.OnStartReplay();
            animator.SetTrigger("Action");
            _state = State.Action;
        }

        public override void OnEndGame()
        {
            animator.ResetTrigger("Action");
            animator.SetTrigger("Idle");
            _state = State.Idle;
            animator.speed = 1;
        }

        public override void OnAction()
        {
            base.OnAction();
            animator.speed = 0;
        }

        public override void OnReset()
        {
            base.OnReset();
            animator.ResetTrigger("Action");
            animator.SetTrigger("Idle");
            _state = State.Idle;
            animator.speed = 1;
        }

        private void OnChangeVelocity()
        {

        }

        private void OnEnd()
        {

        }
    }
}
