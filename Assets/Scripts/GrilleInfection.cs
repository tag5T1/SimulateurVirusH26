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
    int tickSpeed = 2;
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
            Debug.Log("SPACE");
        }
            
    }


    private IEnumerator Logique()
    {
        while (true)
        {
            var time = Time.time;

            for (int i = 0; i < taille; i++) {
                for (int j = 0; j < taille; j++) {
                    var caseVise = casesInfection[i, j];
                    var infectés = caseVise.nbInfectés;
                    var population = caseVise.population;

                    int nouveauxInfectés = (int)(infectés * 0.25f);

                    // Infection sur soi-même
                    caseVise.Infecter(nouveauxInfectés);
                    caseVise.ChangerCouleur();

                    // Infection en haut
                    if (i > 0)
                    {
                        caseVise = casesInfection[i - 1, j];
                        caseVise.Infecter(nouveauxInfectés);
                        caseVise.ChangerCouleur();
                    }

                    // Infection en bas
                    if (i < taille-1)
                    {
                        caseVise = casesInfection[i + 1, j];
                        caseVise.Infecter(nouveauxInfectés);
                        caseVise.ChangerCouleur();
                    }

                    // Infection à gauche
                    if (j > 0)
                    {
                        caseVise = casesInfection[i, j - 1];
                        caseVise.Infecter(nouveauxInfectés);
                        caseVise.ChangerCouleur();
                    }

                    // Infection à droite
                    if (j < taille-1)
                    {
                        caseVise = casesInfection[i, j + 1];
                        caseVise.Infecter(nouveauxInfectés);
                        caseVise.ChangerCouleur();
                    }
                }
            }

            var tempsRestant = (1/tickSpeed) - (Time.time - time);
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
