using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] Transform effectInstanceParent;
    [SerializeField] List<EffectInstance> m_effectPrefabList = new List<EffectInstance>();
    List<EffectInstance> m_effectInstanceList = new List<EffectInstance>();

    float m_time;


    public void CallEffect()
    {
        foreach(EffectInstance effect in m_effectPrefabList)
        {
            m_effectInstanceList.Add(Instantiate(effect, effectInstanceParent));
            m_effectInstanceList[m_effectInstanceList.Count - 1].Act(this);
        }
    }

    public void EndEffect(EffectInstance effectInstance)
    {
        m_effectInstanceList.Remove(effectInstance);
    }
}
