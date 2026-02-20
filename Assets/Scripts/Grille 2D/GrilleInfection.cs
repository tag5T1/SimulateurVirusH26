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
    [SerializeField] int tickSpeed = 10;
    [SerializeField] int nbInfectionInitiale;
    const int TICK_RATE = 6; // Sommeil - deplacement - travail - deplacement - libre - deplacement
    bool pause;
    Coroutine coroutine;

    private void Awake()
    {
        CreerGrille(0);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreerGrille(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreerGrille(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tickSpeed++;
            Debug.Log(tickSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S) && tickSpeed > 1)
        {
            tickSpeed--;
            Debug.Log(tickSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            tickSpeed++;
            Debug.Log(tickSpeed);
        }
        if (Input.GetKey(KeyCode.A) && tickSpeed > 1)
        {
            tickSpeed--;
            Debug.Log(tickSpeed);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tickSpeed+=5;
            Debug.Log(tickSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D) && tickSpeed > 1)
        {
            tickSpeed-=5;
            if (tickSpeed < 1)
                tickSpeed = 1;
            Debug.Log(tickSpeed);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var camera = Camera.main;
            var pos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos.x = Mathf.Floor(pos.x);
            pos.y = Mathf.Floor(pos.y);
            foreach (var o in casesInfection) {
                if ((Vector2)o.transform.position == (Vector2)pos)
                {
                    o.Infecter(o.population / 10);
                    break;
                }
            }
        }
    }


    private IEnumerator LogiqueTick()
    {
        while (true)
        {
            float time = Time.time;

            if (!pause)
            {
                // Infecte les cases
                for (int y = 0; y < taille; y++)
                {
                    for (int x = 0; x < taille; x++)
                    {
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
                        if (y < taille - 1)
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
                        if (x < taille - 1)
                        {
                            caseVise = casesInfection[y, x + 1];
                            caseVise.Infecter(nbInfection);
                        }
                    }
                }

                // Applique les nouvelles infection
                foreach (var o in casesInfection)
                    o.FinTick();
            }

            float tempsRestant = (1f / tickSpeed) - (Time.time - time);
            yield return new WaitForSeconds(tempsRestant);
        }
    }


    private void CreerGrille(int type)
    {
        // Crée les résistances
        float[,] mapResistance = new float[taille, taille];
        if (type == 0)
            for (int y = 0; y < taille; y++)
                for (int x = 0; x < taille; x++)
                    mapResistance[y, x] = (float)Random.Range(0, 96) / 100;
        else if (type == 1)
            for (int y = 0; y < taille; y++)
                for (int x = 0; x < taille; x++)
                    mapResistance[x, y] = Random.Range(0, 2);


        // Réinitialise la grille si elle existait déjà
        if (casesInfection != null)
            {
                foreach (CaseInfection o in casesInfection)
                    GameObject.Destroy(o.GameObject());
                casesInfection = null;
            }

        // Crée une nouvelle caseInfection à chaque emplacement de la grille
        casesInfection = new CaseInfection[taille, taille];
        for (int y = 0; y < taille; y++)
        {
            for (int x = 0; x < taille; x++)
            {
                var o = GameObject.Instantiate(objetCaseInfection, this.transform);
                var nouvelleCase = o.GetComponent<CaseInfection>();
                o.transform.position = new Vector2(-taille/2 + x, taille/2 - y);
                nouvelleCase.Initialiser(mapResistance[y, x]);
                nouvelleCase.FinTick();
                casesInfection[y, x] = o.GetComponent<CaseInfection>();
            }
        }

        // Infecte un nombre aléatoire de cases
        for (int i = 0; i < nbInfectionInitiale; i++)
        {
            int x = (int)(Random.value * taille);
            int y = (int)(Random.value * taille);
            casesInfection[y, x].Infecter(Random.value / 3 * casesInfection[y,x].population);
        }

        // Commence la boucle de simulation
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(LogiqueTick());
    }
}
