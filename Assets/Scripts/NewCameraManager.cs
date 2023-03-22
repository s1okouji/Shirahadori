using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shirahadori
{
    public class NewCameraManager : MonoBehaviour
    {
        [SerializeField]
        private Camera MainCamera;
        [SerializeField]
        private Camera ReplayCamera;
        [SerializeField]
        private GameManager gameManager;
        // Start is called before the first frame update
        void Start()
        {
            MainCamera.enabled = true;
            ReplayCamera.enabled = false;
            gameManager.OnEndGame += OnEndGame;
            gameManager.OnReset += OnReset;
        }

        // Update is called once per frame
        void Update()
        {

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