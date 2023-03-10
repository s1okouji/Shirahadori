using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{

    private Animator anim;
    private bool isCatching;
    private bool isWaiting;
    private bool isOut;
    private bool isStart;
    private Game game;
    private Replay replay;
    public GameObject target;
    private Judger judger;
    private string catchMotionName = "Character1_Reference_catch";
    private Stopwatch stopwatch;    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        game = Game.GetInstance();
        replay = Replay.GetInstance();
        isCatching = false;
        isWaiting = false;
        isOut = false;
        game.ResetEvent += Reset;
        game.MissEvent += Miss;
        game.ClearEvent += Clear;
        game.ActionStartEvent += OnStart;
        game.ActionEndEvent += OnEnd;
        replay.InputEvent += OnInput;
        replay.ResetEvent += Reset;        
        judger = target.GetComponent<Judger>();
        stopwatch = new Stopwatch();
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart)
        {
            return;
        }

        if (replay.replaying)
        {
            return;
        }

        if (isOut && Input.GetKey(KeyCode.Mouse0) && !isCatching)
        {
            isCatching = true; // ˆê“x‚¾‚¯ŒÄ‚Ño‚·‚½‚ß
            game.Judge(judger);
        }
        if (Input.GetKey(KeyCode.Mouse0) && !isCatching && !isOut)
        {
            anim.SetTrigger("Catch");
            UnityEngine.Debug.Log("Catch!");
            stopwatch.Stop();            
            Replay.GetInstance().timespan = stopwatch.Elapsed;
            game.OnCatch();
            isCatching = true;            
            var context = SynchronizationContext.Current;
            new Thread(() =>
            {
                Thread.Sleep(200);
                context.Post(_ =>
                {
                    game.Judge(judger);
                }, null);
            }).Start();
        }        
    }    

    private void OnStart(ActionStartEventArgs e)
    {
        isStart = true;
        anim.ResetTrigger("Wait");
        UnityEngine.Debug.Log("Player: StartAction");
        stopwatch.Start();
    }

    private void OnEnd(ActionEndEventArgs e)
    {
        isOut = true;
        stopwatch.Stop();
        Replay.GetInstance().timespan = stopwatch.Elapsed;      
        UnityEngine.Debug.Log("Player: EndAction");
    }

    private void OnInput(InputEventArgs e)
    {
        UnityEngine.Debug.Log("Player: OnInput");
        anim.SetTrigger("Catch");
        replay.OnCatch();
        isCatching = true;
    }

    private void Miss(MissEventArgs e)
    {

    }

    private void Clear(ClearEventArgs e)
    {
        UnityEngine.Debug.Log("Player: Clear");        
    }

    private void Reset(ResetEventArgs e)
    {
        UnityEngine.Debug.Log("Player: Wait");
        anim.ResetTrigger("Catch");
        anim.SetTrigger("Wait");
        isCatching = false;
        stopwatch.Reset();        
        isOut = false;
    }
}
