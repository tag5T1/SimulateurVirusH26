using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewFix : MonoBehaviour
{
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnRectTransformDimensionsChange()
    {
        Refresh();
    }

    public void Refresh()
    {
        var textes = GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach(TextMeshProUGUI t in textes)
        {
            t.ForceMeshUpdate();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
