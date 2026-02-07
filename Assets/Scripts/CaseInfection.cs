using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CaseInfection : MonoBehaviour
{
    CouleurInfection couleur;
    [SerializeField] Color color;
    SpriteRenderer spriteRenderer;
    public int population { get; private set; }
    public int nbInfectés { get; private set; }
    bool goingUp;
    private float time;

    public void Initialiser()
    {
        goingUp = true;
        time = Time.time;
        population = 400;
        couleur = new CouleurInfection();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangerCouleur()
    {
        var tauxInfecté = (float)nbInfectés / (float)population;
        color = couleur.CalculerCouleurCase(tauxInfecté);

        spriteRenderer.color = color;
    }


    public void Infecter(int nouveauxInfectés)
    {
        nbInfectés += nouveauxInfectés;
        if (nbInfectés > population)
            nbInfectés = population;
    }
}
