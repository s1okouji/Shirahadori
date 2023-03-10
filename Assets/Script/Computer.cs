using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Computer: MonoBehaviour
{
    private Animator anim;
    private Game game;
    private Replay replay;
    private float speed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        game = Game.GetInstance();
        replay = Replay.GetInstance();
        game.CatchEvent += OnCatch;
        game.ResetEvent += OnReset;
        game.StartGameEvent += StartGame;
        replay.CatchEvent += OnCatch;
        replay.ResetEvent += OnReset;
        anim.speed = 0;
        speed = 1;
    }
    private void Update()
    {        
    }

    public void ChangeVelocity()
    {                
        anim.speed = 0.8f + speed;
        Debug.Log(anim.speed);
    }

    public void OnStart()
    {
        if (!replay.replaying)
        {
            game.OnStart();
            Debug.Log("Compute: OnStart");
            // 速度を決める
            speed = UnityEngine.Random.value;
            anim.speed = 1;
        }
    }

    private void StartGame()
    {
        anim.speed = 1;
    }

    public void OnEnd()
    {
        if (!replay.replaying) game.OnEnd();
        anim.speed = 0;
        anim.ResetTrigger("Action");
    }

    // アニメーションを止める
    private void OnCatch(CatchEventArgs e)
    {
        anim.speed = 0;
        anim.ResetTrigger("Action");
        // Get Animation Clip
        /*var clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;*/
        /*Debug.Log($"Clip Name: {clip.name} Frames: {clip.frameRate} Length: {clip.length}");*/
    }

    private void OnReset(ResetEventArgs e)
    {
        Debug.Log("Computer: Reset");        
        anim.SetTrigger("Action");
        anim.speed = 1;
    }
}
