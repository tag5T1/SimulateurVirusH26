using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderButton : MonoBehaviour
{
    public OfficeBuilderObjectScriptableObject objet;
    OfficeBuilderManager manager;

    public void Setup(OfficeBuilderObjectScriptableObject objet)
    {
        this.objet = objet;
        GetComponentInChildren<TextMeshProUGUI>().SetText(objet.nom);
        manager = OfficeBuilderManager.Instance;
        GetComponent<Button>().onClick.AddListener(() => OfficeBuilderManager.Instance.SetObjetSélectionné(objet));
    }
}
