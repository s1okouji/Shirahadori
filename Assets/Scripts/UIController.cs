using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shirahadori
{
    public class UIController : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("GameObject which manages the game.")]
        private GameManager gameManager;

        private UIDocument ui;
        private VisualElement root;

        private Button _buttonStart;
        private Button _buttonRestart;

#if UNITY_STANDALONE_WIN
        private Button _buttonQuit;
#endif

        // Start is called before the first frame update
        void Start()
        {
            ui = GetComponent<UIDocument>();
            root = ui.rootVisualElement;
            _buttonStart = root.Q<Button>("Start");
            _buttonRestart = root.Q<Button>("Restart");
            _buttonRestart.style.display = DisplayStyle.None;

            _buttonStart.clicked += StartGame;
            _buttonRestart.clicked += RestartGame;

#if UNITY_STANDALONE_WIN
            Debug.Log("Windows");
            _buttonQuit = root.Q<Button>("Quit");
            _buttonQuit.style.display = DisplayStyle.None;
            _buttonQuit.clicked += Quit;
#endif

            RegisterCallBack();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void RegisterCallBack()
        {
            gameManager.OnClear += OnClear;
            gameManager.OnMiss += OnMiss;
            gameManager.OnEndReplay += OnEndReplay;
            gameManager.OnReset += OnReset;
            Debug.Log("RegisterCallback");
        }

        private void StartGame()
        {
            _buttonStart.style.display = DisplayStyle.None;
            gameManager.StartGame();
        }

        private void RestartGame()
        {
            gameManager.RestartGame();
        }

        private void OnClear()
        {
            var clear = root.Q<VisualElement>("Clear");
            clear.visible = true;
        }

        private void OnMiss()
        {
            var otetsuki = root.Q<VisualElement>("Otetsuki");
            otetsuki.visible = true;
        }

        private void OnEndReplay()
        {
            var button = root.Q<Button>("Restart");
            button.style.display = DisplayStyle.Flex;
#if UNITY_STANDALONE_WIN
            _buttonQuit.style.display = DisplayStyle.Flex;
#endif
        }

        private void OnReset()
        {
            var otetsuki = root.Q<VisualElement>("Otetsuki");
            var clear = root.Q<VisualElement>("Clear");
            otetsuki.visible = false;
            clear.visible = false;
            var button = root.Q<Button>("Restart");
            button.style.display = DisplayStyle.None;
#if UNITY_STANDALONE_WIN
            _buttonQuit.style.display = DisplayStyle.None;
#endif

        }

#if UNITY_STANDALONE_WIN
        private void Quit()
        {
            Application.Quit();
        }
#endif
    }
}
