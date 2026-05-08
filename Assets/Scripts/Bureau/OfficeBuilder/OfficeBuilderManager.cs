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
    public bool modeRotationActivee = false;
    [Header("Éléments UI")]
    public LayerMask layersOffice;
    public GameObject builderMenu;
    public GameObject buttonPrefab;
    public Transform viewContent;
    private OfficeBuilderObjectScriptableObject builderObjetSelectionne;
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
            if (builderObjetSelectionne != null) {
                CastRay(builderObjetSelectionne.layersOuPlaçable, out valid, out hit);

                if (valid && !modeRotationActivee)
                {
                    currentGhost.transform.position = hit.point + Vector3.up * currentGhost.transform.localScale.y / 2;
                }
            }
        }
    }



    public void ToggleMenuBuilder()
    {
        modeRotationActivee = false;
        modePlacementObjet = false;
        builderObjetSelectionne = null;
        GameObject.Destroy(currentGhost);
        builderMenu.SetActive(!builderMenu.activeSelf);
    }

    public void Click()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && builderMenu.activeSelf)
        {
            if (!modePlacementObjet)
            {
                CastRay(layersOffice, out bool valide, out RaycastHit hit);
            }
            else if (!modeRotationActivee)
            {
                ToggleRotation();
            }
            else
            {
                ToggleRotation();
                CréerObjet();
            }
        }
    }

    public void ToggleRotation()
    {
        modeRotationActivee = !modeRotationActivee;
    }

    public void TournerGhost(float degrés)
    {
        currentGhost.transform.Rotate(Vector3.up, -degrés * 10);
    }

    public void CréerObjet()
    {
        var objet = GameObject.Instantiate(builderObjetSelectionne.prefab, currentGhost.transform.position, currentGhost.transform.rotation);
        if (builderObjetSelectionne.nom == "Bureau")
        {
            manager.CreerEspaceDeTravail(objet);
        }
        else if (builderObjetSelectionne.nom == "Crayon") {
            manager.FindPickups();
        }
        else if (builderObjetSelectionne.nom == "Distributrice")
        {
            manager.FindDistributrices();
        }
        else if (builderObjetSelectionne.nom == "Poubelle")
        {
            manager.FindPoubelles();
        }
        else if (builderObjetSelectionne.nom == "Personne")
        {
            objet.GetComponent<IAPersonne>().Creation(manager.TrouverEspaceDeTravailLibre());
        }
        manager.BuildNavMesh();
    }

    public void SetObjetSélectionné(OfficeBuilderObjectScriptableObject objet)
    {
        if (objet == builderObjetSelectionne)
        {
            DeselectObjet();
        }
        else
        {
            modePlacementObjet = true;
            builderObjetSelectionne = objet;
            GameObject.Destroy(currentGhost);
            currentGhost = GameObject.Instantiate(objet.ghostPrefab);
            modeRotationActivee = false;
        }
            
    }
    public void DeselectObjet()
    {
        modePlacementObjet = false;
        modeRotationActivee = false;
        builderObjetSelectionne = null;
        GameObject.Destroy(currentGhost);
    }

    public void CastRay(LayerMask mask, out bool valid, out RaycastHit hit)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        valid = Physics.Raycast(ray, out hit, 100, mask);
    }
}
