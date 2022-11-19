using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUseQueue
{
    SkillUseQueueNode m_cHead;
    int m_nCount, m_nMaxCount;
    float n_fTimer;
    SkillUseQueueNode[] m_cNode;
    int m_nNodeCount;
    public SkillUseQueue(int nMaxCount)
    {
        m_cHead = null;
        m_nCount = 0;
        m_nNodeCount = 0;
        n_fTimer = 100f;
        m_nMaxCount = nMaxCount;
        m_cNode = new SkillUseQueueNode[nMaxCount];
        for (int i = 0; i < m_cNode.Length; i++)
            m_cNode[i] = new SkillUseQueueNode();
    }
    public void Add(Card cCard)
    {
        if (m_nCount == m_nMaxCount) {  }
        else if (Search(cCard) == false)
        {
            m_cNode[m_nNodeCount].SetCard(cCard);

            if (GetLast() == null) m_cHead = m_cNode[m_nNodeCount];
            else GetLast().SetNext(m_cNode[m_nNodeCount]);

            m_nCount++;
            m_nNodeCount++;

            if (m_nNodeCount == m_nMaxCount) m_nNodeCount = 0;
        }
    }
    public void Update()
    {
        n_fTimer += Time.deltaTime;

        if (n_fTimer >= 0.1f)
        {
            Use();
            n_fTimer = 0;
        }
    }
    void Use()
    {
        if (m_nCount == 0) return;

        m_nCount--;
        m_cHead.GetCard().Use();

        SkillUseQueueNode temp = m_cHead;
        m_cHead = m_cHead.GetNext();
        temp.ReSet();
    }
    public void Clear()
    {
        m_cHead = null;
        m_nCount = 0;
        m_nNodeCount = 0;
        n_fTimer = 100;
        for (int i = 0; i < m_cNode.Length; i++)
            m_cNode[i].ReSet();
    }
    bool Search(Card cCard)
    {
        if (m_cHead == null) return false;
        SkillUseQueueNode cTemp = m_cHead;
        while (cTemp.GetNext() != null)
        {
            if (cTemp.GetCard().Equals(cCard)) return true;
            cTemp = cTemp.GetNext();
        }
        if (GetLast().GetCard().Equals(cCard)) return true;
        return false;
    }
    SkillUseQueueNode GetLast()
    {
        if (m_cHead == null) return m_cHead;
        SkillUseQueueNode cTemp = m_cHead;
        while (cTemp.GetNext() != null)
        {
            cTemp = cTemp.GetNext();
        }
        return cTemp;
    }
}
public class SkillUseQueueNode
{
    Card m_cCard;
    SkillUseQueueNode m_cNext;

    public SkillUseQueueNode()                      { ReSet(); }
    public void ReSet()                             { m_cCard = null; m_cNext = null; }
    public void SetCard(Card cCard)                 { m_cCard = cCard; }
    public void SetNext(SkillUseQueueNode cNext)    { m_cNext = cNext; }
    public Card GetCard()                           { return m_cCard; }
    public SkillUseQueueNode GetNext()              { return m_cNext; }
}