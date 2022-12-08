using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    [SerializeField] TextMesh _cGetGoldTextMesh;
    Animator _cAnimator;
    [SerializeField] NeglectSin sin;

    private void Start()
    {
        _cAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        _cGetGoldTextMesh.transform.Translate(Vector3.up * 5f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoldBox")
        {
            _cAnimator.SetTrigger("ItemGet");
            //
            string strGold = "";
            if (sin._nBuf1Ea > 0)
            {
                strGold = GameManager.GM.GoldToStr(GameManager.GM.m_cPlayer.m_nBangchiGold*2);
            }
            else
            {
                strGold = GameManager.GM.GoldToStr(GameManager.GM.m_cPlayer.m_nBangchiGold);
            }
            
            _cGetGoldTextMesh.text = "+" + strGold;
            _cGetGoldTextMesh.transform.localPosition = new Vector3(-2.65f, 1f, -1.54f);
        }
    }
}
