using System.Collections;
using UnityEngine;

public class DelayHandler : MonoBehaviour
{
    private static MonoBehaviour mono = null;

    private static MonoBehaviour GetMono {
        get {
            if (mono == null)
            {
                var obj = new GameObject("DelayHandler");
                DontDestroyOnLoad(obj);
                mono = obj.AddComponent<DelayHandler>();
            }
            return mono;
        }
    }

    public static void WaitAndInvoke(System.Action callback, float t = 1f, bool realtime = false)
    {
        if (callback == null || t <= 0) return;
        GetMono.StartCoroutine(WaitAndInvokeCoroutine(callback, t, realtime));
    }

    private static IEnumerator WaitAndInvokeCoroutine(System.Action callback, float t, bool realtime)
    {
        yield return realtime ? new WaitForSecondsRealtime(t) : new WaitForSeconds(t);
        callback.Invoke();
    }

    public static void WaitAndInvoke<T>(System.Action<T> callback, T parameterValue, float t = 1f, bool realtime = false)
    {
        if (callback == null || t <= 0) return;
        GetMono.StartCoroutine(WaitAndInvokeCoroutine(callback, parameterValue, t, realtime));
    }

    private static IEnumerator WaitAndInvokeCoroutine<T>(System.Action<T> callback, T parameterValue, float t, bool realtime)
    {
        yield return realtime ? new WaitForSecondsRealtime(t) : new WaitForSeconds(t);
        callback.Invoke(parameterValue);
    }
}