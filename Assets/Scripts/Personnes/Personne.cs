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
    public bool estInfecté { get; private set; }



    public Personne(EspaceDeTravail espace)
    {
        espaceDeTravail = espace;
        bureauDeTravail = espace.bureau.GetComponent<BureauDeTravail>();
    }

    public Vector3 GetPositionBureau()
    {
        return bureauDeTravail.positionInteraction;
    }

    public void DevientInfecté(Virus virus)
    {
        this.virus = virus.Muter();
        estInfecté = true;
    }



    public List<Dictionary<string, string>> OnClick()
    {
        DataEspaceDeTravail dataEspaceDeTravail = new DataEspaceDeTravail();
        DataVirus dataVirus = new DataVirus();
        DataInfection dataInfection = new DataInfection();
        List<Dictionary<string, string>> listeData = new List<Dictionary<string, string>>();

        dataEspaceDeTravail.Add(espaceDeTravail);
        listeData.Add(dataEspaceDeTravail.données);

        dataInfection.Add(estInfecté);
        listeData.Add(dataInfection.données);

        if (estInfecté)
        {
            dataVirus.Add(virus);
            listeData.Add(dataVirus.données);
        }

        return listeData;
    }
}
