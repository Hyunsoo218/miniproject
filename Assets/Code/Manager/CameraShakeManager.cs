using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] GameObject _objCantTouch;
    public void CameraShake(RectTransform tTarget, float fRange, ShakeMode eSM)
    {
        _objCantTouch.SetActive(true);
        switch (eSM)
        {
            case ShakeMode.Nomal: StartCoroutine(Nomal(tTarget, fRange)); break;
        }
    }
    IEnumerator Nomal(RectTransform tTarget, float fRange)
    {
        Vector3 temp = tTarget.localPosition;
        for (int i = 0; i < 20; i++)
        {
            float x = temp.x + Random.Range(-fRange, fRange);
            float y = temp.y + Random.Range(-fRange, fRange);
            tTarget.localPosition = new Vector3(x, y, 0);
            yield return new WaitForSeconds(1f / 20f); 
        }
        tTarget.localPosition = temp;
        _objCantTouch.SetActive(false);
    }
}
public enum ShakeMode
{
    Nomal
}
