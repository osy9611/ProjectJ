using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager 
{
    public void Play(Animator ani, string name)
    {
        if (ani == null)
            return;

        var info = ani.GetCurrentAnimatorClipInfo(0);

        if (info.Length == 0 || ani.GetCurrentAnimatorStateInfo(0).IsName(name))
            return;

        ani.CrossFade(name, 0.03f);
    }

    public void SetEnableAni(Animator ani, bool enable)
    {
        ani.enabled = enable;
    }

    public bool CheckPlayAniName(Animator ani, string name)
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(name))
            return true;

        return false;
    }

    public void CheckAndPlay(Animator ani,string name)
    {
        if (CheckPlayAniName(ani, name))
            return;
       
        Managers.Ani.Play(ani, name);
    }

    public bool CheckAniName(Animator ani, string name)
    {
        foreach (AnimationClip clip in ani.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return true;
            }
        }
        return false;
    }
}
