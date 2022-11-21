using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin : MonoBehaviour
{
    virtual public void Open() { gameObject.SetActive(true); }
    virtual public void Open(Stage cStage) { gameObject.SetActive(true); }
    virtual public void Open(string strData) { gameObject.SetActive(true); }
    virtual public void Open(string strData, Card cCard) { gameObject.SetActive(true); }
    virtual public void Open(string strData, List<Card> cCard) { gameObject.SetActive(true); }
    virtual public void Open(string strData, Member cMember) { gameObject.SetActive(true); }
    virtual public void Close() { gameObject.SetActive(false); }
}
