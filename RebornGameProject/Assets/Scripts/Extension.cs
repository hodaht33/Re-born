using UnityEngine;

public static class Extension
{
    public static Transform RecursiveFind(this Transform root, string name)
    {
        Transform result = root.transform.Find(name);
        if (result) return result;

        foreach (Transform child in root)
        {
            result = RecursiveFind(child, name);
            if (result) return result;
        }

        return null;
    }
}
