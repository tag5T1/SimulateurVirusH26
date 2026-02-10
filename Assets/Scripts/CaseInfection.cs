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

    public void Initialiser(float resistance)
    {
        this.resistance = resistance;
        population = 10000;
        couleur = new CouleurInfection();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Infecter(float nbInfection)
    {
        nextNbInfectés += (int)(nbInfection * (1f - resistance));
    }

    public void FinTick()
    {
        nbInfectés += nextNbInfectés;
        if (nbInfectés > population)
            nbInfectés = population;
        nextNbInfectés = 0;
        ChangerCouleur();
    }
    public void ChangerCouleur()
    {
        var tauxInfecté = (float)nbInfectés / (float)population;
        color = couleur.CalculerCouleurCase(tauxInfecté, resistance);
        spriteRenderer.color = color;
    }

}
