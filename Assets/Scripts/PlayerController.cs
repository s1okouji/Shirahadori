using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shirahadori
{

    [RequireComponent(typeof(Animator))]
    public class PlayerController : BaseCharacterController
    {

        private bool playing = false;
        private Animator animator;

        private enum State
        {
            Wait,
            Catch
        }

        private State _state;

        new private void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            _state = State.Wait;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (playing)
                {
                    gameManager.OnAction();
                }
            }
        }

        public override void OnStartGame()
        {
            Debug.Log("OnStartGame");
            playing = true;
        }

        public override void OnAction()
        {
            base.OnAction();
            animator.SetTrigger("Catch");
            _state = State.Catch;
        }

        public override void OnStartReplay()
        {
            base.OnStartReplay();
        }

        public override void OnReset()
        {
            base.OnReset();
            if (_state == State.Catch)
            {
                animator.ResetTrigger("Catch");
                animator.SetTrigger("Wait");
                _state = State.Wait;
            }
        }

        public override void OnEndGame()
        {
            base.OnEndGame();
            if (_state == State.Catch)
            {
                animator.ResetTrigger("Catch");
                animator.SetTrigger("Wait");
                _state = State.Wait;
            }
        }

        // TODO: End Catch Motion
        public void OnEndCatchMotion()
        {
            Debug.Log("OnEndCatchMotion");
            if (playing)
            {
                gameManager.Judge();
                playing = false;
            }
        }

        private bool IsCatchingTarget()
        {
            return true;
        }
    }
}
