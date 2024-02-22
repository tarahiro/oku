using MagicTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectInstance : MonoBehaviour
{
    public virtual void Act(EffectHandler effectHandler)
    {
    }

    protected Action EndEffect(EffectHandler effectHandler)
    {
        return () => { 
            effectHandler.EndEffect(this);
            Destroy(gameObject);
        };
    }

}
