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
        private Record record;

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
            record = new Record(gameManager);
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
            playing = true;
        }

        public override void OnAction()
        {            
            animator.SetTrigger("Catch");
            _state = State.Catch;
        }

        public override void OnStartReplay()
        {
            StartCoroutine(DelayCoroutine(record.actionTiming, () => gameManager.OnAction()));
            StartCoroutine(DelayCoroutine(record.endTiming, () => gameManager.OnEndReplay()));
        }

        public override void OnReset()
        {
            _Reset();
        }

        public override void OnEndGame()
        {
            _Reset();
            playing = false;
        }
        
        public void OnEndCatchMotion()
        {            
            if (playing)
            {
                gameManager.Judge();                
            }
        }

        private void _Reset()
        {
            if (_state == State.Catch)
            {
                animator.ResetTrigger("Catch");
                animator.SetTrigger("Wait");
                _state = State.Wait;
            }
        }
    }
}
