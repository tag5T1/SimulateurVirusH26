using UnityEngine;
using Unity.Collections;
using System.Collections;
using Unity.VisualScripting;

public class GrilleInfection : MonoBehaviour
{
    [SerializeField] GameObject objetCaseInfection;
    [SerializeField] int taille = 20;
    CaseInfection[,] casesInfection = null;
    // CaseInfection[,] matriceAdjacence = new CaseInfection[3,3]; IDK IF USEFUL
    int tickSpeed = 10;
    const int TICK_RATE = 6; // Sommeil - deplacement - travail - deplacement - libre - deplacement
    Coroutine coroutine;

    private void Awake()
    {
        CreerGrille();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreerGrille();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var camera = Camera.main;
            var pos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos.x = Mathf.Floor(pos.x);
            pos.y = Mathf.Floor(pos.y);
            foreach (var o in casesInfection) {
                Debug.Log((Vector2)o.transform.position + "  vs  " + (Vector2)pos);
                if ((Vector2)o.transform.position == (Vector2)pos)
                {
                    o.Infecter(o.population / 10);
                    break;
                }
            }
        }
    }


    private IEnumerator Logique()
    {
        while (true)
        {
            float time = Time.time;

            // Infecte les cases
            for (int y = 0; y < taille; y++) {
                for (int x = 0; x < taille; x++) {
                    var caseVise = casesInfection[y, x];
                    float infectés = caseVise.nbInfectés;

                    float nbInfection = (int)(infectés * 0.05f);

                    // Infecte soi-meme
                    caseVise.Infecter(nbInfection);

                    // Infecte en haut
                    if (y > 0)
                    {
                        caseVise = casesInfection[y - 1, x];
                        caseVise.Infecter(nbInfection);
                    }

                    // Infecte en bas
                    if (y < taille-1)
                    {
                        caseVise = casesInfection[y + 1, x];
                        caseVise.Infecter(nbInfection);
                    }

                    // Infecte à gauche
                    if (x > 0)
                    {
                        caseVise = casesInfection[y, x - 1];
                        caseVise.Infecter(nbInfection);
                    }

                    // Infecte à droite
                    if (x < taille-1)
                    {
                        caseVise = casesInfection[y, x + 1];
                        caseVise.Infecter(nbInfection);
                    }
                }
            }

            // Applique les nouvelles infection
            foreach (var o in casesInfection) 
                o.FinTick();

            float tempsRestant = (1f/tickSpeed) - (Time.time - time);
            Debug.Log("TICK");
            yield return new WaitForSeconds(tempsRestant);
        }
    }


    private void CreerGrille()
    {
        if (casesInfection != null)
        {
            foreach (CaseInfection o in casesInfection)
                GameObject.Destroy(o.GameObject());
            casesInfection = null;
        }

        casesInfection = new CaseInfection[taille, taille];
        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                var o = GameObject.Instantiate(objetCaseInfection, this.transform);
                o.transform.position = new Vector2(-taille/2 + j, taille/2 - i);
                o.GetComponent<CaseInfection>().Initialiser();
                casesInfection[i, j] = o.GetComponent<CaseInfection>();
            }
        }

        for (int i = 0; i < Random.value * 3; i++)
        {
            int x = (int)(Random.value * taille);
            int y = (int)(Random.value * taille);
            casesInfection[y, x].Infecter(100);
        }
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Logique());
    }
}
