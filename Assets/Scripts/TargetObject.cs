using System.Collections;
using UnityEngine;

namespace Shirahadori {
    public class TargetObject : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        private bool _isCatched = false;

        private void OnEnable()
        {
            gameManager.OnStartGame += OnStartGame;
        }

        private void OnStartGame()
        {
            _isCatched = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            _isCatched = true;
        }

        public void OnTriggerExit(Collider other)
        {
            _isCatched = false;
        }

        public bool IsCatched()
        {
            return _isCatched;
        }
    }
}
