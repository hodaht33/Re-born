using UnityEngine;

public static class Extension
{
    public static Transform RecursiveFind(this Transform root, string name)
    {
        foreach (Transform child in root.transform)
        {
            if (child.gameObject.name == name) return child;
        }

        foreach (Transform child in root)
        {
            var result = RecursiveFind(child, name);
            if (result) return result;
        }

        return null;
    }
}
