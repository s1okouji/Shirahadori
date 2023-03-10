using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/**
 * Gameのロジックなどを展開する
 */
public class Game
{

    private static Game instance;

    private int missCount = 0;

    public delegate void ResetEventHandler(ResetEventArgs e);
    public event ResetEventHandler ResetEvent;
    public delegate void MissEventHandler(MissEventArgs e);
    public event MissEventHandler MissEvent;
    public delegate void CatchEventHandler(CatchEventArgs e);
    public event CatchEventHandler CatchEvent;
    public delegate void ClearEventHandler(ClearEventArgs e);
    public event ClearEventHandler ClearEvent;
    public delegate void ReplayEventHandler(ReplayEventArgs e);
    public event ReplayEventHandler ReplayEvent;
    public delegate void ActionStartEventHandler(ActionStartEventArgs e);
    public event ActionStartEventHandler ActionStartEvent;
    public delegate void ActionEndEventHandler(ActionEndEventArgs e);
    public event ActionEndEventHandler ActionEndEvent;
    public delegate void StartGameEventHandler();
    public event StartGameEventHandler StartGameEvent;
    
    private void init()
    {
        MissEvent += Miss;        
    }
    
    /**
     * trueならばOK, falseならばアウト
     */
    public bool Judge(Judger judger)
    {
        bool judge = judger.isOK;
        Debug.Log($"Judge: {judge}");
        /*OnCatch();*/
        // TODO: UI以外の演出を止める。
        missCount++;
        if (!judge)
        {
            OnMiss();
            // 2秒後にリプレイを開始
            var context = SynchronizationContext.Current;
            new Thread(
                () =>
                {
                    Thread.Sleep(2000);
                    context.Post(_ =>
                    {
                        Debug.Log("リプレイを開始します。");
                        OnReplay(judger.isOut);
                    }, null);
                }
                ).Start();
        }
        else
        {
            OnClear();
        }
        /*OnReset();*/
        
        return false;
    }    

    private void Miss(MissEventArgs e)
    {
        Debug.Log($"お手付き! 現在のお手付き回数: {missCount}");        
    }

    public void OnStartGame()
    {
        StartGameEvent?.Invoke();
    }

    public void OnEnd()
    {
        ActionEndEvent?.Invoke(new ActionEndEventArgs());
    }

    public void OnStart()
    {
        ActionStartEvent?.Invoke(new ActionStartEventArgs());
    }

    public void OnReplay(bool isOut)
    {
        ReplayEvent?.Invoke(new ReplayEventArgs(isOut));
    }

    // キャッチモーションをしたときに発火
    // 結果は分からない
    public void OnCatch()
    {
        CatchEvent?.Invoke(new CatchEventArgs());
    }

    // クリアしたときに発火
    public void OnClear()
    {        
        ClearEvent?.Invoke(new ClearEventArgs());
    }

    public void OnReset()
    {        
        ResetEvent?.Invoke(new ResetEventArgs());
    }

    public void OnMiss()
    {
        MissEvent.Invoke(new MissEventArgs(missCount));
    }

    public static Game GetInstance()
    {
        if(null == instance)
        {
            instance = new Game();
            instance.init();
        }
        return instance;
    }
}