using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MasterDataList<T>
		: ScriptableObject
{// �f�[�^�̎���
    [SerializeField] protected T[] m_List = null;

    // Index����f�[�^���擾
    public T TryGetFromIndex(int index)
    {
        if (index >= 0 && index < m_List.Length)
        {
            return m_List[index];
        }
        return default;
    }

    // �f�[�^�̐����擾
    public int Count => m_List.Length;

    public IEnumerable<T> SetList { set => m_List = value.ToArray(); }
}
