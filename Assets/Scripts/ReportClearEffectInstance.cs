using MagicTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportClearEffectInstance : EffectInstance
{
    public override void Act(EffectHandler effectHandler)
    {
        transform.TweenRotation(Quaternion.Euler(0, 0, 180f), .5f).OnComplete(EndEffect(effectHandler));
    }
}
