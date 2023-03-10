using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Camera MainCamera;
    public Camera ReplayCamera;
    private Game game;
    // Start is called before the first frame update
    void Start()
    {
        game = Game.GetInstance();
        MainCamera.enabled = true;
        ReplayCamera.enabled = false;
        game.ReplayEvent += OnReplay;
        game.ResetEvent += OnReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnReplay(ReplayEventArgs e)
    {
        MainCamera.enabled = false;
        ReplayCamera.enabled = true;
    }

    private void OnReset(ResetEventArgs e)
    {
        ReplayCamera.enabled = false;
        MainCamera.enabled = true;    
    }
    
}
