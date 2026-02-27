using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Personne
{
    public Virus virus;
    public EspaceDeTravail espaceDeTravail;
    public BureauDeTravail bureauDeTravail;
    public List<Coroutine> listeActions;
    public bool infecté { get; private set; }
    public float niveauToux;


    public Personne(EspaceDeTravail espace)
    {
        espaceDeTravail = espace;
        bureauDeTravail = espace.bureau.GetComponent<BureauDeTravail>();
    }

    public Vector3 GetPositionBureau()
    {
        return bureauDeTravail.positionInteraction;
    }

    public void Infecter(Virus virus)
    {
        this.virus = virus.Muter();
        infecté = true;
    }

    public List<Dictionary<string, string>> OnClick()
    {
        DataEspaceDeTravail dataEspaceDeTravail = new DataEspaceDeTravail();
        DataVirus dataVirus = new DataVirus();
        DataInfection dataInfection = new DataInfection();
        List<Dictionary<string, string>> listeData = new List<Dictionary<string, string>>();

        dataEspaceDeTravail.Add(espaceDeTravail);
        listeData.Add(dataEspaceDeTravail.données);

        dataInfection.Add(infecté);
        listeData.Add(dataInfection.données);

        if (infecté)
        {
            dataVirus.Add(virus);
            listeData.Add(dataVirus.données);
        }

        return listeData;
    }
}
