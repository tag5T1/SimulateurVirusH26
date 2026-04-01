using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Office Builder Object", menuName = "Office Builder Objects/Office Builder Object")]
public class OfficeBuilderObjectScriptableObject : ScriptableObject
{
    public string nom;
    public GameObject prefab;
    public float hauteurPrefab;
}
