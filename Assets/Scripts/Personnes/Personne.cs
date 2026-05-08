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
    public bool estInfecte { get; private set; }

    public Personne(EspaceDeTravail espace, GameObject objetPersonne)
    {
        espaceDeTravail = espace;
        bureauDeTravail = espace.bureau.GetComponent<BureauDeTravail>();
        estInfecte = false;
        immunite = new Immunite(objetPersonne);
    }




    public Vector3 GetPositionBureau()
    {
        return bureauDeTravail.positionInteraction;
    }

    public void DevientInfecte(GameObject objetPersonne, Virus virus)
    {
        if (!immunite.immune)
        {
            this.virus = new Virus(objetPersonne, virus);
            if (!estInfecte)
                Actions.InvokeNewOnInfection();
            Actions.InvokeOnInfection();

            estInfecte = true;
        }
    }

    public void DevientGueri()
    {
        immunite.GainImmunite();
        virus = null;

        Actions.InvokeOnGueri();

        estInfecte = false;
    }



    public List<Dictionary<string, string>> OnClick()
    {
        DataEspaceDeTravail dataEspaceDeTravail = new DataEspaceDeTravail();
        DataVirus dataVirus = new DataVirus();
        DataInfection dataInfection = new DataInfection();
        DataImmunite dataImmunite = new DataImmunite();
        List<Dictionary<string, string>> listeData = new List<Dictionary<string, string>>();

        dataEspaceDeTravail.Add(espaceDeTravail);
        listeData.Add(dataEspaceDeTravail.donnees);

        dataInfection.Add(estInfecte);
        listeData.Add(dataInfection.donnees);

        dataImmunite.Add(immunite);
        listeData.Add(dataImmunite.donnees);

        if (estInfecte)
        {
            dataVirus.Add(virus);
            listeData.Add(dataVirus.donnees);
        }

        return listeData;
    }
}
