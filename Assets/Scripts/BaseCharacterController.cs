using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Diagnostics;

namespace Shirahadori {

    public class BaseCharacterController: MonoBehaviour
    {
    
        [SerializeField]
        protected GameManager gameManager;
        [SerializeField]
        protected GameObject target;

        protected bool replaying = false;
        private float time = 0f;
        private Record _replay;
        private Stopwatch stopwatch;

        protected void Awake()
        {        
            gameManager.OnStartGame += OnStartGame;
            gameManager.OnStartReplay += OnStartReplay;
            gameManager.OnAction += OnAction;
            gameManager.OnEndReplay += OnEndReplay;
            gameManager.OnReset += OnReset;
            gameManager.OnEndGame += OnEndGame;
            gameManager.OnStartAction += OnStartAction;
            stopwatch = new Stopwatch();
        }

        void Update()
        {
            /*Replay();*/
        }

        // Have to override
        public virtual void OnStartGame()
        {
        
        }

        public virtual void OnEndGame()
        {

        }

        public virtual void OnStartReplay()
        {
            replaying = true;
            /*_replay = record;
            Debug.Log($"{record.actionTiming}, {record.endTiming}");
            Replay();        */
        }

        public virtual void OnEndReplay()
        {
            replaying = false;
            time = 0;
        }

        // This method will be called in replay and in game.
        // In replay, this method called by Replay()
        // In game, this method called by gameManager.OnAction();
        public virtual void OnAction()
        {
            /*if (!replaying)
            {
                stopwatch.Stop();
                _replay.actionTiming = (float)stopwatch.Elapsed.TotalSeconds;
                _replay.endTiming = _replay.actionTiming + 1;
            }*/
        }

        public virtual void OnStartAction()
        {
            /*if (!replaying)
            {
                stopwatch.Start();
                _replay = new Record();
            }
            else
            {
                StartCoroutine(DelayCoroutine(_replay.actionTiming, () => { OnAction(); }));
                StartCoroutine(DelayCoroutine(_replay.endTiming, () => { gameManager.EndReplay(); }));
            }*/
        }

        public virtual void OnReset()
        {
            /*_replay = null;
            stopwatch.Reset();*/
        }

        protected void Replay()
        {
            if (!replaying) return;
            /*var lastTime = time;
            time += Time.deltaTime;*/
            StartCoroutine(DelayCoroutine(_replay.actionTiming, () =>
            {            
                OnAction();
            }));
            /*if (time >= _replay.actionTiming && _replay.actionTiming > lastTime)
            {
                OnAction();
            }*/

            /*if (time >= _replay.endTiming && _replay.endTiming > lastTime)
            {
                gameManager.EndReplay();
            }*/
            StartCoroutine(DelayCoroutine(_replay.endTiming, () =>
            {                        
                gameManager.EndReplay();
            }));
        }

        protected IEnumerator DelayCoroutine(float seconds, UnityAction action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
    }
}