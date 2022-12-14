using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin : MonoBehaviour
{
    public Slot Slot
    {
        get => default;
        set
        {
        }
    }

    public MainThreadDispatcher MainThreadDispatcher
    {
        get => default;
        set
        {
        }
    }

    public RankData RankData
    {
        get => default;
        set
        {
        }
    }

    public MonsterData MonsterData
    {
        get => default;
        set
        {
        }
    }

    public MouseMove MouseMove
    {
        get => default;
        set
        {
        }
    }

    public SetBuffManager SetBuffManager
    {
        get => default;
        set
        {
        }
    }

    public SetBuff SetBuff
    {
        get => default;
        set
        {
        }
    }

    public MonsterSponManager MonsterSponManager
    {
        get => default;
        set
        {
        }
    }

    virtual public void Open() { gameObject.SetActive(true); }
    virtual public void Open(Act cAct) { gameObject.SetActive(true); }
    virtual public void Open(Stage cStage) { gameObject.SetActive(true); }
    virtual public void Open(string strData) { gameObject.SetActive(true); }
    virtual public void Open(string strData, Card cCard) { gameObject.SetActive(true); }
    virtual public void Open(string strData, List<Card> cCard) { gameObject.SetActive(true); }
    virtual public void Open(string strData, Member cMember) { gameObject.SetActive(true); }
    virtual public void Close() 
    {
        gameObject.SetActive(false); 
    }
}
