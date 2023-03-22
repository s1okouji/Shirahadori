using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Shirahadori {
    public class GameManager : MonoBehaviour
    {
        public UnityAction OnStartGame;
        public UnityAction OnStartReplay;
        /*public delegate void onStartReplay(Record record);
        public onStartReplay OnStartReplay;*/

        public UnityAction OnAction;
        public UnityAction OnEndReplay;
        public UnityAction OnEndGame;
        public UnityAction OnClear;
        public UnityAction OnMiss;
        public UnityAction OnReset;
        public UnityAction OnStartAction;

        [SerializeField]
        private TargetObject target;

        private float oneGameTime = 0f;
        private bool playing = false;
        private Stopwatch stopwatch;

        private void Awake()
        {
            OnStartGame += _OnStartGame;
            stopwatch = new Stopwatch();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {           
        }        

        private void _OnStartGame()
        {
            stopwatch.Reset();
            playing = true;
            stopwatch.Start();
        }

        public void StartGame()
        {
            OnStartGame?.Invoke();
            Debug.Log("StartGame");
        }

        public void StartAction()
        {
            OnStartAction?.Invoke();
        }

        public void Action()
        {
            OnAction?.Invoke();
        }

        public void Judge()
        {
            var result = target.IsCatched();
            Debug.Log("Judge");
            if (result)
            {
                Clear();
            }
            else
            {
                Miss();
            }
            var record = new Record();
            record.actionTiming = (float)stopwatch.Elapsed.TotalSeconds;
            StartCoroutine(DelayCoroutine(1, () => {
                Debug.Log("Start Replay");
                playing = false;
                stopwatch.Stop();
                record.endTiming = (float)stopwatch.Elapsed.TotalSeconds;
                OnEndGame();
            }));
            StartCoroutine(DelayCoroutine(1.5f, () => StartReplay(record)));
        }

        public void RestartGame()
        {
            Debug.Log("RestartGame");
            OnReset?.Invoke();
            StartCoroutine(DelayCoroutine(0.5f, () => { StartGame(); }));
            /*StartGame();*/
        }

        public void EndGame()
        {
            Debug.Log("End Game");
            OnEndGame?.Invoke();
        }

        public void StartReplay(Record record)
        {
            /*OnStartReplay?.Invoke(record);*/
            OnStartReplay?.Invoke();
        }

        public void EndReplay()
        {
            OnEndReplay?.Invoke();
        }

        public void Clear()
        {
            OnClear?.Invoke();
        }

        public void Miss()
        {
            OnMiss?.Invoke();
        }

        public void Reset()
        {
            OnReset?.Invoke();
        }

        private IEnumerator DelayCoroutine(float seconds, UnityAction action)
        {
            yield return new WaitForSecondsRealtime(seconds);
            action?.Invoke();
        }
    }
}
