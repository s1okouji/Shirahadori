using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Shirahadori {
    public class GameManager : MonoBehaviour
    {
        public UnityAction OnStartGame;
        public UnityAction OnStartReplay;        

        public UnityAction OnAction;
        public UnityAction OnEndReplay;
        public UnityAction OnEndGame;
        public UnityAction OnEndAction;
        public UnityAction OnClear;
        public UnityAction OnMiss;
        public UnityAction OnReset;
        public UnityAction OnStartAction;

        [SerializeField]
        private TargetObject target;                      

        public void StartGame()
        {
            OnStartGame?.Invoke();
            Debug.Log("StartGame");
        }

        public void EndAction()
        {
            OnEndAction?.Invoke();
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
            StartCoroutine(DelayCoroutine(1, () => { 
                OnEndGame();
            }));
            StartCoroutine(DelayCoroutine(1.5f, () => StartReplay()));
        }

        public void RestartGame()
        {
            Debug.Log("RestartGame");
            OnReset?.Invoke();
            StartCoroutine(DelayCoroutine(0.5f, () => { StartGame(); }));            
        }

        public void EndGame()
        {
            Debug.Log("End Game");
            OnEndGame?.Invoke();
        }

        public void StartReplay()
        {            
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
