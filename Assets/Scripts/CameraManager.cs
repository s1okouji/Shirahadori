using UnityEngine;

namespace Shirahadori
{
    public class CameraManager: MonoBehaviour
    {
        [SerializeField]
        private Camera MainCamera;
        [SerializeField]
        private Camera ReplayCamera;
        [SerializeField]
        private GameManager gameManager;
        
        void Start()
        {
            MainCamera.enabled = true;
            ReplayCamera.enabled = false;
            gameManager.OnEndGame += OnEndGame;
            gameManager.OnReset += OnReset;
        }        

        private void OnEndGame()
        {
            MainCamera.enabled = false;
            ReplayCamera.enabled = true;
        }

        private void OnReset()
        {
            ReplayCamera.enabled = false;
            MainCamera.enabled = true;
        }

    }
}