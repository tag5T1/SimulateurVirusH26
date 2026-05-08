using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OfficeBuilderControls : MonoBehaviour
{
    OfficeBuilderManager builderManager;


    public void Start() {
        StartCoroutine(TrouverManager());
    }

    public void Update() {
        if (builderManager != null)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                builderManager.ToggleMenuBuilder();
            }
            if (Input.GetMouseButtonDown(0))
            {
                builderManager.Click();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && builderManager.modeRotationActivee)
            {
                builderManager.ToggleRotation();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && builderManager.modePlacementObjet && !builderManager.modeRotationActivee)
                builderManager.DeselectObjet();
            if (builderManager.modeRotationActivee)
            {
                builderManager.TournerGhost(Input.GetAxis("Mouse X"));
            }
        }
    }

    private IEnumerator TrouverManager()
    {
        while (builderManager == null)
        {
            builderManager = OfficeBuilderManager.Instance;
            yield return new WaitForEndOfFrame();
        }
    }
}
