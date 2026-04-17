using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;

public class Personne
{
    public Virus virus;
    public Immunite immunite;
    public EspaceDeTravail espaceDeTravail;
    public BureauDeTravail bureauDeTravail;
    public List<Coroutine> listeActions;
    public bool estInfecté { get; private set; }

    public Personne(EspaceDeTravail espace, GameObject objetPersonne)
    {
        espaceDeTravail = espace;
        bureauDeTravail = espace.bureau.GetComponent<BureauDeTravail>();
        estInfecté = false;
        immunite = new Immunite(objetPersonne);
    }




    public Vector3 GetPositionBureau()
    {
        return bureauDeTravail.positionInteraction;
    }

    public void DevientInfecté(GameObject objetPersonne, Virus virus)
    {
        if (!immunite.immune)
        {
            this.virus = new Virus(objetPersonne, virus);
            if (!estInfecté)
                Actions.InvokeNewOnInfection();
            Actions.InvokeOnInfection();

            estInfecté = true;
        }
    }

    public void DevientGueri()
    {
        immunite.GainImmunite();
        virus = null;

        Actions.InvokeOnGueri();

        estInfecté = false;
    }



    public List<Dictionary<string, string>> OnClick()
    {
        DataEspaceDeTravail dataEspaceDeTravail = new DataEspaceDeTravail();
        DataVirus dataVirus = new DataVirus();
        DataInfection dataInfection = new DataInfection();
        DataImmunite dataImmunite = new DataImmunite();
        List<Dictionary<string, string>> listeData = new List<Dictionary<string, string>>();

        dataEspaceDeTravail.Add(espaceDeTravail);
        listeData.Add(dataEspaceDeTravail.données);

        dataInfection.Add(estInfecté);
        listeData.Add(dataInfection.données);

        dataImmunite.Add(immunite);
        listeData.Add(dataImmunite.données);

        if (estInfecté)
        {
            dataVirus.Add(virus);
            listeData.Add(dataVirus.données);
        }

        return listeData;
    }
}
