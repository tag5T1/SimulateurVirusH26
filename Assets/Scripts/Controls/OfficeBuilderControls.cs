using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderControls : MonoBehaviour
{
    Manager manager;
    OfficeBuilderManager builderManager;


    public void Start() {
        manager = gameObject.GetComponent<Manager>();
        builderManager = gameObject.GetComponent<OfficeBuilderManager>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            manager.modeOfficeBuilderActivé = !manager.modeOfficeBuilderActivé;
        }
        if (Input.GetMouseButtonDown(0) && manager.modeOfficeBuilderActivé)
            builderManager.CréerBureau();
    }
}
