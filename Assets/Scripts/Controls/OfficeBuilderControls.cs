using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderControls : MonoBehaviour
{
    OfficeBuilderManager builderManager;


    public void Start() {
        builderManager = gameObject.GetComponent<OfficeBuilderManager>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            builderManager.ToggleBuilder();
        }
        if (Input.GetMouseButtonDown(0) && builderManager.modeBuilderActivé) {
            builderManager.Click();
        }
        if (Input.GetMouseButtonDown(1) && builderManager.rotationActivée)
        {
            builderManager.ToggleRotation();
        }
        if (builderManager.rotationActivée) {
            builderManager.TournerGhost(Input.GetAxis("Mouse X"));
        }
        if (Input.GetKeyDown(KeyCode.G) && builderManager.modeBuilderActivé) {
            builderManager.CycleObjet();
        }
    }
}
