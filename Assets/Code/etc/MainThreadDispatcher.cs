using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    public static MainThreadDispatcher Instance;

    private Queue<Action> QueuedActions = new Queue<Action>();

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        for (int i = 0; i < 75; i++)
        {
            if (QueuedActions.Count > 0)
            {
                QueuedActions.Dequeue().Invoke();
            }
            
        }
    }
    public void Enqueue(Action act)
    {
        QueuedActions.Enqueue(act);
    }
    public void Clear()
    {
        QueuedActions.Clear();
    }
}
