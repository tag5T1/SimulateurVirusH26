using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderManager : MonoBehaviour
{
    Manager manager;
    Camera mainCamera;
    GameObject personnePrefab;
    public OfficeBuilderObjectScriptableObject bureauPrefab;
    GameObject distributricePrefab;


    public void Start() {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        mainCamera = Camera.main;

        // SCRIPTABLE OBJECTS A LA PLACE
        personnePrefab = Resources.Load<GameObject>("Prefabs/Personne");
        distributricePrefab = Resources.Load<GameObject>("Prefabs/Distributrice");
    }



    public void CréerBureau() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) ;
            GameObject.Instantiate(bureauPrefab.prefab, hit.point + Vector3.up*bureauPrefab.hauteurPrefab, Quaternion.identity);
    }
}
