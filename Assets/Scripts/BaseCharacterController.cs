using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Shirahadori {

    public class BaseCharacterController: MonoBehaviour
    {
    
        [SerializeField]
        protected GameManager gameManager;
        [SerializeField]
        protected GameObject target;            

        protected void Awake()
        {        
            gameManager.OnStartGame += OnStartGame;
            gameManager.OnStartReplay += OnStartReplay;
            gameManager.OnAction += OnAction;
            gameManager.OnEndReplay += OnEndReplay;
            gameManager.OnReset += OnReset;
            gameManager.OnEndGame += OnEndGame;
            gameManager.OnStartAction += OnStartAction;            
        }        
        
        public virtual void OnStartGame()
        {
        
        }

        public virtual void OnEndGame()
        {

        }

        public virtual void OnStartReplay()
        {
            
        }

        public virtual void OnEndReplay()
        {
            
        }
        
        public virtual void OnAction()
        {
           
        }

        public virtual void OnStartAction()
        {
           
        }

        public virtual void OnReset()
        {
           
        }

        protected IEnumerator DelayCoroutine(float seconds, UnityAction action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
    }
}