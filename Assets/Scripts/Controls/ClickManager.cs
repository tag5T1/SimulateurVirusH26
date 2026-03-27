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
    [SerializeField] GameObject dataPanel;
    TMP_Text dataText;
    TMP_Text prefabNormal;
    bool pause;



    private void Start()
    {
       mainCamera = Camera.main;
       dataPanel.SetActive(false);
       prefabNormal = Resources.Load<GameObject>("Prefabs/Textes/TextNormal").GetComponent<TMP_Text>();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pause)
            {
                Time.timeScale = 1.0f;
                pause = false;
            }
            else
            {
                Time.timeScale = 0f;
                pause = true;   
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction*1000, Color.yellow, 1);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                Debug.Log("Som Hit");
                var content = dataPanel.transform.GetChild(0).GetChild(0);

                if (hit.collider.gameObject.tag == "Personne")
                {
                    Debug.Log("person found");
                                     
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
