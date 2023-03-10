using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Utility 
{ 
    public static float GetCurrentFrame(AnimatorStateInfo stateInfo, AnimationClip clip)
    {
        return stateInfo.normalizedTime * stateInfo.length * clip.frameRate;
    }
    
}
