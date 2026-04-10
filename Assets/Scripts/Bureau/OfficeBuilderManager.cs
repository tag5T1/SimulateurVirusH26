using NUnit.Framework.Internal.Execution;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderManager : MonoBehaviour
{
    Manager manager;
    Camera mainCamera;
    private GameObject currentGhost;
    public bool modeBuilderActivé;
    public bool rotationActivée;
    private OfficeBuilderObjectScriptableObject builderObjetSelectionné;
    public OfficeBuilderObjectScriptableObject bureauBuilderObjet;
    public OfficeBuilderObjectScriptableObject crayonBuilderObjet;
    public OfficeBuilderObjectScriptableObject distributriceBuilderObjet;
    public OfficeBuilderObjectScriptableObject poubelleBuilderObjet;
    private OfficeBuilderObjectScriptableObject[] objets;
    private int idObjetSelectionné;



    public void Start() {
        manager = GetComponent<Manager>();
        mainCamera = Camera.main;
        // Base selection
        idObjetSelectionné = 0;
        builderObjetSelectionné = bureauBuilderObjet;
        currentGhost = GameObject.Instantiate(bureauBuilderObjet.ghostPrefab);
        currentGhost.SetActive(false);
        objets = new OfficeBuilderObjectScriptableObject[] {
            bureauBuilderObjet,
            crayonBuilderObjet,
            distributriceBuilderObjet,
            poubelleBuilderObjet
        };
    }

    private void Update()
    {
        if (modeBuilderActivé)
        {
            RaycastHit hit;
            bool valid;
            CastRay(builderObjetSelectionné.layersOùPlaçable, out valid, out hit);

            if (valid && !rotationActivée)
            {
                currentGhost.transform.position = hit.point + Vector3.up * currentGhost.transform.localScale.y / 2;
            }
        }
    }



    public void ToggleBuilder()
    {
        if (modeBuilderActivé)
        {
            modeBuilderActivé = false;
            currentGhost.SetActive(false);
        }
        else
        {
            modeBuilderActivé = true;
            currentGhost.SetActive(true);
        }
    }
    public void ToggleRotation()
    {
        if (rotationActivée)
        {
            rotationActivée = false;
        }
        else
        {
            rotationActivée = true;
        }
    }

    public void Click()
    {
        if (!rotationActivée)
            ToggleRotation();
        else
        {
            ToggleRotation();
            CréerObjet();
        }
    }

    public void TournerGhost(float degrés)
    {
        currentGhost.transform.Rotate(Vector3.up, -degrés * 10);
    }

    public void CréerObjet()
    {
        GameObject.Instantiate(builderObjetSelectionné.prefab, currentGhost.transform.position, currentGhost.transform.rotation);
        if (builderObjetSelectionné == crayonBuilderObjet) {
            manager.FindPickups();
        }
        else if (builderObjetSelectionné == distributriceBuilderObjet)
        {
            manager.FindDistributrices();
        }
        else if (builderObjetSelectionné == poubelleBuilderObjet)
        {
            manager.FindPoubelles();
        }
        manager.BuildNavMesh();
    }

    public void CycleObjet()
    {
        Debug.Log(builderObjetSelectionné.name);

        idObjetSelectionné = (idObjetSelectionné + 1) % objets.Length;
        builderObjetSelectionné = objets[idObjetSelectionné];
        Debug.Log(builderObjetSelectionné.name);

        GameObject.Destroy(currentGhost);
        currentGhost = GameObject.Instantiate(builderObjetSelectionné.ghostPrefab);
    }

    public void CastRay(LayerMask mask, out bool valid, out RaycastHit hit)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        valid = Physics.Raycast(ray, out hit, 100, mask);
    }
}
