using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    Camera mainCamera;
    TMP_Text prefabNormal;
    [SerializeField] GameObject dataPanel;
    TMP_Text dataText;



    private void Start()
    {
       mainCamera = Camera.main;
       dataPanel.SetActive(false);
       prefabNormal = Resources.Load<GameObject>("Prefabs/Textes/TextNormal").GetComponent<TMP_Text>();
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                var content = dataPanel.transform.GetChild(0).transform.GetChild(0).transform;

                if (hit.collider.gameObject.tag == "Personne")
                {
                    dataText = Instantiate(prefabNormal, content);                   
                    dataText.text = FormatListToString(
                        hit.collider.gameObject.GetComponent<IAPersonne>().personne.OnClick()
                    );

                    LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());

                    dataPanel.SetActive(true);
                }
                else 
                {
                    if (dataPanel.activeSelf) 
                    {
                        var x = content.childCount;
                        for (int i = x; i > 0; i--)
                            Destroy(content.GetChild(i-1).gameObject);
                        dataPanel.SetActive(false);                   
                    }

                }
              
            }
        }
    }
    

    string FormatListToString(List<Dictionary<string, string>> liste)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var dict in liste)
            foreach (var kvp in dict)
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");

        return sb.ToString();
    }
}
