using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CaseInfection : MonoBehaviour
{
    CouleurInfection couleur;
    Color color;
    SpriteRenderer spriteRenderer;
    public float population { get; private set; }
    public float nbInfectés;
    private float nextNbInfectés;
    public float resistance; // % de réduction du nombre d'infectés

    public void Initialiser()
    {
        resistance = (float)Random.Range(0, 101) / 100;

        population = 10000;
        couleur = new CouleurInfection();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangerCouleur()
    {
        var tauxInfecté = (float)nbInfectés / (float)population;
        color = couleur.CalculerCouleurCase(tauxInfecté);

        spriteRenderer.color = color;
    }


    public void Infecter(float nbInfection)
    {
        nextNbInfectés += (int)(nbInfection * (1f - resistance));
        if (nextNbInfectés > population)
            nextNbInfectés = population;

        ChangerCouleur();
    }

    public void FinTick()
    {
        nbInfectés += nextNbInfectés;
        nextNbInfectés = 0;
    }
}
