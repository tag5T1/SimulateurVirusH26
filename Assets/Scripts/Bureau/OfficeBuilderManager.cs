using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderManager : MonoBehaviour
{
    Manager manager;
    Camera mainCamera;
    private OfficeBuilderObjectScriptableObject builderObjetSelectionné;
    public OfficeBuilderObjectScriptableObject bureauBuilderObjet;
    public OfficeBuilderObjectScriptableObject crayonBuilderObjet;
    private OfficeBuilderObjectScriptableObject[] objets;
    private int idObjetSelectionné;



    public void Start() {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        mainCamera = Camera.main;
        idObjetSelectionné = 0;
        builderObjetSelectionné = bureauBuilderObjet;
        objets = new OfficeBuilderObjectScriptableObject[] {
            bureauBuilderObjet,
            crayonBuilderObjet
        };
    }



    public void CréerObjet() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            GameObject.Instantiate(builderObjetSelectionné.prefab, hit.point + Vector3.up * builderObjetSelectionné.hauteurPrefab, Quaternion.identity);
    }


    public void CycleObjet()
    {
        idObjetSelectionné = (idObjetSelectionné + 1) % objets.Length;
        builderObjetSelectionné = objets[idObjetSelectionné];
    }
}
