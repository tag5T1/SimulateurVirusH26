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
}
