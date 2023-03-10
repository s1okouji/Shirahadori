using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{

    private Game game;
    private UIDocument ui;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        game = Game.GetInstance();
        ui = GetComponent<UIDocument>();
        root = ui.rootVisualElement;

        game.ClearEvent += OnClear;
        game.MissEvent += OnMiss;
        game.ResetEvent += OnReset;
        var button = root.Q<Button>("Restart");
        var startButton = root.Q<Button>("Start");
        button.style.display = DisplayStyle.None;
        button.RegisterCallback<MouseUpEvent>(Restart);
        startButton.RegisterCallback<MouseUpEvent>(Start);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Start(MouseUpEvent e)
    {
        if(e.button == 0)
        {
            var button = root.Q<Button>("Start");
            button.style.display = DisplayStyle.None;
            game.OnStartGame();
        }
    }

    private void Restart(MouseUpEvent e)
    {
        Debug.Log("Callback");
        if(e.button == 0)
        {            
            game.OnReset();
        }
    }

    private void OnMiss(MissEventArgs e)
    {
        var otetsuki = root.Q<VisualElement>("Otetsuki");
        otetsuki.visible = true;
    }
    
    private void OnReset(ResetEventArgs e)
    {
        var otetsuki = root.Q<VisualElement>("Otetsuki");
        var clear = root.Q<VisualElement>("Clear");
        otetsuki.visible = false;
        clear.visible = false;
        var button = root.Q<Button>("Restart");
        button.style.display = DisplayStyle.None;
    }

    private void OnClear(ClearEventArgs e)
    {
        var clear = root.Q<VisualElement>("Clear");
        var button = root.Q<Button>("Restart");
        button.style.display = DisplayStyle.Flex;
        clear.visible = true;
    }
}
