using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class Virus
{
    Transform personne;
    public SymptomesHandler symptomes;

    public string nom = "virus 1";
    public float niveauMin {  get; private set; }
    public float force {  get; private set; }
    public  float décceleration {  get; private set; }
    public float gravité { get; private set; }
    public float duréeVie { get; private set; }
    public float puissanceMutation { get; private set; } // % possible de changement
    public float maxSpread = 20;

    /// <summary>
    /// Constructeur qui copie le Virus
    /// </summary>
    /// <param name="vir"> Le Virus à copier </param>
    public Virus(Virus vir)
    {
        this.personne = vir.personne;
        this.symptomes = vir.symptomes;
        this.force = vir.force;
        this.duréeVie = vir.duréeVie;
        this.niveauMin = vir.niveauMin;
        this.décceleration = vir.décceleration;
        this.gravité = vir.gravité;
    }

    /// <summary>
    /// Constructeur qui crée un virus aléatoire
    /// </summary>
    /// <param name="personne"> Le Transform de la personne qui possède le Virus </param>
    public Virus(Transform personne)
    {
        this.personne = personne;
        this.symptomes = new SymptomesHandler(this, new List<Symptome>() { new Toux(personne.gameObject) });
        this.force = Random.Range(10f, 16f);
        this.duréeVie = 15;
        this.niveauMin = Random.Range(85, 105);
        this.décceleration = 1.2f;
        this.gravité = Random.Range(0.1f, 0.4f);
        this.puissanceMutation = 10f;
    }

    /// <summary>
    /// Constructeur à paramètres donnés
    /// </summary>
    /// <param name="personne"> Le Transform de la personne qui possède le virus </param>
    /// <param name="force"> La force de projection des particules </param>
    /// <param name="duréeVie"> La durée de vie des particules en secondes </param>
    /// <param name="niveauMin"> Le niveau minimum de symptome nécessaire pour en voir </param>
    /// <param name="decceleration"> La déccélération des particules </param>
    /// <param name="gravité"> La gravité que subisse les particules </param>
    /// <param name="puissanceMutation"> Le % qui détermine la valeur minimum et maximum de mutation </param>
    public Virus(Transform personne, SymptomesHandler symptomes, float force, float duréeVie, float niveauMin, float decceleration, float gravité, int puissanceMutation)
    {
        this.personne = personne;
        this.symptomes = symptomes;
        this.force = force;
        this.duréeVie = duréeVie;
        this.niveauMin = niveauMin;
        this.décceleration = decceleration;
        this.gravité = gravité;
        this.puissanceMutation = puissanceMutation;
    }



    /// <summary>
    /// Change légèrement les paramètres du Virus selon sa force de mutation
    /// </summary>
    /// <returns> Le nouveau Virus muté </returns>
    public Virus Muter()
    {
        // Copie le virus
        Virus virusMuté = new(this);
        float min = 1f - puissanceMutation/100;
        float max = 1f + puissanceMutation/100;
        // Modifie légèrement les paramètres
        virusMuté.niveauMin *= Random.Range(min, max);
        virusMuté.force *= Random.Range(min, max);
        virusMuté.décceleration *= Random.Range(min, max);
        virusMuté.gravité *= Random.Range(min, max);
        virusMuté.duréeVie *= Random.Range(min, max);
        virusMuté.puissanceMutation *= Random.Range(900, 1100) / 1000; // 10% de mutation de base sur la mutation
        return virusMuté;
    }



    /// <summary>
    /// Effectue les symptomes du SymptomesHandler
    /// </summary>
    public void EffectuerSymptomes()
    {
        symptomes.EffectuerSymptomes();
    }
}
