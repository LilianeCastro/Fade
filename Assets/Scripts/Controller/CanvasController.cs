using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoSingleton<CanvasController>
{
    public Animator fadeAnim;
    public Animator hitAnim;

    public void FadeIn()
    {
        fadeAnim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnim.SetTrigger("FadeOut");
    }

    public void HitAnim()
    {
        hitAnim.SetTrigger("Hit");
    }

    public void HitDefault()
    {
        hitAnim.SetTrigger("Default");
    }
}
