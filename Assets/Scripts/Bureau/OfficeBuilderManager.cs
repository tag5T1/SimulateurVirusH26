using NUnit.Framework.Internal.Execution;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OfficeBuilderManager : MonoBehaviour
{
    public static OfficeBuilderManager Instance;

    Manager manager;
    Camera mainCamera;
    private GameObject currentGhost;
    [Header("Modes")]
    public bool modePlacementObjet;
    public bool modeRotationActivée = false;
    [Header("Éléments UI")]
    public LayerMask layersOffice;
    public GameObject builderMenu;
    public GameObject buttonPrefab;
    public Transform viewContent;
    private OfficeBuilderObjectScriptableObject builderObjetSelectionné;
    public OfficeBuilderObjectScriptableObject[] objets;



    public void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate OfficeBuilderManager in \"" + gameObject.name + "\"");
            GameObject.Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void Start() {
        manager = Manager.Instance;
        mainCamera = Camera.main;
        objets = Resources.LoadAll<OfficeBuilderObjectScriptableObject>("OfficeBuilderScriptableObjects");
        // Buttons
        foreach (var objet in objets)
        {
            var bouton = GameObject.Instantiate(buttonPrefab, viewContent);
            bouton.GetComponent<BuilderButton>().Setup(objet);
        }
        builderMenu.SetActive(false);
    }

    private void Update()
    {
        if (modePlacementObjet)
        {
            RaycastHit hit;
            bool valid;
            CastRay(builderObjetSelectionné.layersOùPlaçable, out valid, out hit);

            if (valid && !modeRotationActivée)
            {
                currentGhost.transform.position = hit.point + Vector3.up * currentGhost.transform.localScale.y / 2;
            }
        }
    }



    public void ToggleMenuBuilder()
    {
        builderObjetSelectionné = null;
        GameObject.Destroy(currentGhost);
        builderMenu.SetActive(!modePlacementObjet);
    }

    public void Click()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!modePlacementObjet)
            {
                CastRay(layersOffice, out bool valide, out RaycastHit hit);
                if (valide)
                    Debug.Log(hit.collider);
            }
            else if (!modeRotationActivée)
                ToggleRotation();
            else
            {
                ToggleRotation();
                CréerObjet();
            }
        }
    }

    public void ToggleRotation()
    {
        modeRotationActivée = !modeRotationActivée;
    }

    public void TournerGhost(float degrés)
    {
        currentGhost.transform.Rotate(Vector3.up, -degrés * 10);
    }

    public void CréerObjet()
    {
        GameObject.Instantiate(builderObjetSelectionné.prefab, currentGhost.transform.position, currentGhost.transform.rotation);
        if (builderObjetSelectionné.nom == "Crayon") {
            manager.FindPickups();
        }
        else if (builderObjetSelectionné.nom == "Distributrice")
        {
            manager.FindDistributrices();
        }
        else if (builderObjetSelectionné.nom == "Poubelle")
        {
            manager.FindPoubelles();
        }
        manager.BuildNavMesh();
    }

    public void SetObjetSélectionné(OfficeBuilderObjectScriptableObject objet)
    {
        if (objet == builderObjetSelectionné)
        {
            DeselectObjet();
        }
        else
        {
            modePlacementObjet = true;
            builderObjetSelectionné = objet;
            GameObject.Destroy(currentGhost);
            currentGhost = GameObject.Instantiate(objet.ghostPrefab);
            modeRotationActivée = false;
        }
            
    }
    public void DeselectObjet()
    {
        modePlacementObjet = false;
        modeRotationActivée = false;
        builderObjetSelectionné = null;
        GameObject.Destroy(currentGhost);
    }

    public void CastRay(LayerMask mask, out bool valid, out RaycastHit hit)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        valid = Physics.Raycast(ray, out hit, 100, mask);
    }
}
