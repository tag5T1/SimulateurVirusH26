using UnityEngine;
using UnityEngine.UIElements;
using XCharts.Runtime;

public class ControlGeneral:MonoBehaviour
{
    static bool pauseActive = false;
    static float vitessePrecedente;
    [SerializeField] GameObject graphs;
    [SerializeField] GameObject menu;
    bool graphsActive;

    private void Awake()
    {
        graphsActive = graphs.activeSelf;
    }
    private void LateUpdate()
    {
        // Input pour le contrôle sur le temps
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!OfficeBuilderManager.Instance.builderMenu.activeSelf) //Si le builder n'est pas activer
            {
                Pause();
                menu.SetActive(!menu.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale += 0.1f;
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftShift)) 
        { 
            if (Time.timeScale-0.1f >= 0f)
            Time.timeScale -= 0.1f;
            else
            {
                Time.timeScale = 0f;
                pauseActive = true;
            }
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow))
        {
            Time.timeScale += 0.1f * Time.unscaledDeltaTime;
            pauseActive = false;
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.DownArrow))
        {
            if (Time.timeScale - 0.1f * Time.unscaledDeltaTime >= 0f)
                Time.timeScale -= 0.1f * Time.unscaledDeltaTime;
            else
            {
                Time.timeScale = 0f;
                pauseActive = true;
            }
            Debug.Log(Time.timeScale);
        }

        // Input pour afficher les graphiques
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (graphsActive) 
            { 
                graphsActive = !graphsActive;
                graphs.SetActive(graphsActive);
            }
            else
            {
                graphsActive = !graphsActive;
                graphs.SetActive(graphsActive);
            }
        }

        // Input pour enlever toutes les particules
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            GameObject[] particules = GameObject.FindGameObjectsWithTag("Particules");

            if (particules.Length > 0)
            {
                foreach (GameObject particule in particules)
                {
                    Destroy(particule);
                }
            }
        }
    }

    public static void Pause()
    {
        if (!pauseActive)
        {
            vitessePrecedente = Time.timeScale;
            Time.timeScale = 0f;
            pauseActive = true;
        }
        else
        {
            Time.timeScale = vitessePrecedente;
            pauseActive = false;
        }
    }
}
