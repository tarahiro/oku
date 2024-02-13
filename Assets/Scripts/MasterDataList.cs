using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MasterDataList<T>
		: ScriptableObject
{// データの実体
    [SerializeField] protected T[] m_List = null;

    // Indexからデータを取得
    public T TryGetFromIndex(int index)
    {
        if (index >= 0 && index < m_List.Length)
        {
            return m_List[index];
        }
        return default;
    }

    // データの数を取得
    public int Count => m_List.Length;

    public IEnumerable<T> SetList { set => m_List = value.ToArray(); }
}
