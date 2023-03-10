using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judger : MonoBehaviour
{

    public bool isOK;
    private Game game;
    public bool isOut { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        isOK = false;
        game = Game.GetInstance();
        game.CatchEvent += OnCatch;
        game.ActionEndEvent += OnEnd;
        game.ResetEvent += OnReset;
        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");        
        if (!isOK)
        {
            Debug.Log("change isOK = true");            
            isOK = true;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("change isOK = false");
        Debug.Log("Exit");
        isOK = false;
    }    

    private void OnCatch(CatchEventArgs e)
    {        
    }

    private void OnEnd(ActionEndEventArgs e)
    {
        isOut = true;
    }

    private void OnReset(ResetEventArgs e)
    {
        isOut = false;
    }    
}
