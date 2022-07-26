using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinza : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void AnimateType(int _id)
    {
        animator.SetInteger("state",_id);
    }

    public void Walk()
    {        
        AnimateType(1);
    }
    public void Run()
    {
        AnimateType(2);
    }
    public void Roar()
    {
        AnimateType(3);
    }
    public void Angry()
    {
        AnimateType(4);
    }
    public void Idle()
    {
        AnimateType(0);
    }
}
