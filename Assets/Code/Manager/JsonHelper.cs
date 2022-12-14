using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.info;
    }
    private class Wrapper<T>
    {
        public T[] info;
    }
}