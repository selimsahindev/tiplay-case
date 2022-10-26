using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Vector3 Modify(this Vector3 original, object x = null, object y = null, object z = null)
    {
        return new Vector3(
            (x == null ? original.x : (float)x),
            (y == null ? original.y : (float)y),
            (z == null ? original.z : (float)z));
    }

    public static void RemoveAtFast<T>(this List<T> list, int index)
    {
        int lastElementIndex = list.Count - 1;

        list[index] = list[lastElementIndex];
        list.RemoveAt(lastElementIndex);
    }
}
