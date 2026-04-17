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
            if (Input.GetKeyDown(KeyCode.H))
            {
                builderManager.ToggleMenuBuilder();
            }
            if (Input.GetMouseButtonDown(0))
            {
                builderManager.Click();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && builderManager.modeRotationActivée)
            {
                builderManager.ToggleRotation();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && builderManager.modePlacementObjet && !builderManager.modeRotationActivée)
                builderManager.DeselectObjet();
            if (builderManager.modeRotationActivée)
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
