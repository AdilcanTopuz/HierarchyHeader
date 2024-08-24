using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class HierarchyGroupHeader
{
    static HierarchyGroupHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && gameObject.name.StartsWith("#", System.StringComparison.Ordinal))
        {
            var colorName = gameObject.name.Substring(1).Split(' ')[0];
            var color = Color.gray;

            //hex or name
            if (ColorUtility.TryParseHtmlString("#" + colorName, out var hexColor))
            {
                color = hexColor;
            }
            else if (ColorUtility.TryParseHtmlString(colorName.ToLower(), out var namedColor))
            {
                color = namedColor;
            }

            EditorGUI.DrawRect(selectionRect, color);
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("#" + colorName + " ", "").ToUpperInvariant());
        }
    }
}