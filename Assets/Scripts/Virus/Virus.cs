using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class Virus
{
    public GameObject personne { get; private set; }
    List<Symptome> symptomes;
    public string nom = "virus 1";

    /// <summary>
    /// Valeur de 1 à 10 déterminant la force des symptomes
    /// </summary>
    public float force { get; private set; }

    /// <summary>
    /// La angular velocity du RigidBody des particules de toux
    /// </summary>
    public  float décceleration { get; private set; }
    public float gravité { get; private set; }
    public float duréeVie { get; private set; }
    public float puissanceMutation { get; private set; } // % possible de changement
    public float maxSpread = 40;

    /// <summary>
    /// Constructeur qui copie le Virus
    /// </summary>
    /// <param name="vir"> Le Virus à copier </param>
    public Virus(GameObject personne, Virus vir)
    {
        this.personne = personne;
        this.force = vir.force;
        this.duréeVie = vir.duréeVie;
        this.décceleration = vir.décceleration;
        this.gravité = vir.gravité;
        this.puissanceMutation = vir.puissanceMutation;

        this.symptomes = DupliquerSymptomes(vir.symptomes);
        Muter();
    }

    /// <summary>
    /// Constructeur à paramètres aléatoires aléatoire
    /// </summary>
    /// <param name="personne"> Le Transform de la personne qui possède le Virus </param>
    public Virus(GameObject personne)
    {
        this.personne = personne;
        this.force = Random.Range(1f, 10f);
        this.duréeVie = 15;
        this.décceleration = 1.2f;
        this.gravité = Random.Range(0.1f, 0.4f);
        this.puissanceMutation = 10f;

        this.symptomes = new() {
            new Vomissements(this),
            new Toux(this),
            new Fièvre(this)
        };
        InitialiserSymptomes(this);
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
    public Virus(GameObject personne, List<Symptome> symptomes, float force, float duréeVie, float decceleration, float gravité, int puissanceMutation)
    {
        this.personne = personne;
        this.force = force;
        this.duréeVie = duréeVie;
        this.décceleration = decceleration;
        this.gravité = gravité;
        this.puissanceMutation = puissanceMutation;

        this.symptomes = symptomes;
        InitialiserSymptomes(this);
    }



    /// <summary>
    /// Change légèrement les paramètres du Virus selon sa force de mutation
    /// </summary>
    public void Muter()
    {
        float min = 1f - puissanceMutation/100;
        float max = 1f + puissanceMutation/100;
        // Modifie légèrement les paramètres
        this.force *= Random.Range(min, max);
        this.décceleration *= Random.Range(min, max);
        this.gravité *= Random.Range(min, max);
        this.duréeVie *= Random.Range(min, max);
        this.puissanceMutation += Random.Range(-0.1f, 0.1f); // 10% flat de mutation de base sur la mutation

        InitialiserSymptomes(this);
    }



    /// <summary>
    /// Applique les Symptomes
    /// </summary>
    public void EffectuerSymptomes()
    {
        foreach (Symptome s in symptomes)
        {
            s.EffectuerSymptome();
        }
    }

    public List<Symptome> DupliquerSymptomes(List<Symptome> symptomes)
    {
        var list = new List<Symptome>();
        foreach (Symptome s in symptomes)
        {
            list.Add(s.Dupliquer());
        }
        return list;
    }

    /// <summary>
    /// Initialise les symptomes avec le virus
    /// </summary>
    public void InitialiserSymptomes(Virus virus)
    {
        foreach (Symptome s in symptomes)
        {
            s.Initialiser(virus);
        }
    }
}
