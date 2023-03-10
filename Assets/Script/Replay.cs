using System;
using System.Threading;
using UnityEngine;

public class Replay
{
    public delegate void ResetEventHandler(ResetEventArgs e);
    public event ResetEventHandler ResetEvent;
    public delegate void MissEventHandler(MissEventArgs e);
    public event MissEventHandler MissEvent;
    public delegate void CatchEventHandler(CatchEventArgs e);
    public event CatchEventHandler CatchEvent;
    public delegate void ClearEventHandler(ClearEventArgs e);
    public event ClearEventHandler ClearEvent;
    public delegate void InputEventHandler(InputEventArgs e);
    public event InputEventHandler InputEvent;
    public delegate void ActionEndEventHandler(ActionEndEventArgs e);
    public event ActionEndEventHandler ActionEndEvent;

    public TimeSpan timespan;

    private Game game;
    public bool replaying { get; set; }

    private static Replay instance;
    public static Replay GetInstance()
    {
        if(instance == null)
        {
            instance = new Replay();
            instance.init();
        }
        return instance;
    }    

    private void init()
    {
        game = Game.GetInstance();
        CatchEvent += Catch;
        game.ReplayEvent += OnReplay;
        ActionEndEvent += End;
        ResetEvent += TurnOffReplay;
        game.ResetEvent += TurnOffReplay;
    }

    public void OnReset()
    {
        ResetEvent?.Invoke(new ResetEventArgs());
    }

    public void OnCatch()
    {
        CatchEvent?.Invoke(new CatchEventArgs());
    }

    private void Catch(CatchEventArgs e)
    {
        var context = SynchronizationContext.Current;
        new Thread(() =>
        {
            Thread.Sleep(2000);
            context.Post(_ =>
            {
                Debug.Log("リセットを開始します");
                game.OnReset();
            }, null);
        }).Start();
    }

    private void End(ActionEndEventArgs e)
    {
        var context = SynchronizationContext.Current;
        new Thread(() =>
        {
            Thread.Sleep(2000);
            context.Post(_ =>
            {
                Debug.Log("リセットを開始します");
                game.OnReset();
            }, null);
        }).Start();
    }

    private void OnReplay(ReplayEventArgs e)
    {
        OnReset();
        replaying = true;
        var context = SynchronizationContext.Current;        
        new Thread(() =>
        {
            Thread.Sleep((int)timespan.TotalMilliseconds);
            context.Post(_ =>
            {                               
                if (!e.isOut)
                {
                    OnInput();
                }
                else
                {
                    OnEnd();
                }
            }, null);
        }).Start();           
    }

    private void OnEnd()
    {
        ActionEndEvent?.Invoke(new ActionEndEventArgs());
    }

    private void OnInput()
    {
        InputEvent?.Invoke(new InputEventArgs());
    }

    private void TurnOffReplay(ResetEventArgs e)
    {
        replaying = false;
    }
}
