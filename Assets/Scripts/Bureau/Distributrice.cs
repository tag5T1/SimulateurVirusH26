using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Distributrice : Mobilier
{
    [SerializeField] Material materialInfecté;
    Virus virus;
    public List<IAPersonne> fileDattente;
    public Vector3 distanceEnFile;



    private void Start()
    {
        distanceEnFile = positionInteraction - transform.position;
    }

    public void AttendreEnFile(IAPersonne personne)
    {
        fileDattente.Add(personne);
    }
    public void QuitterFile(IAPersonne personne)
    {
        fileDattente.Remove(personne);
    }

    // S'infecte et infecte lors d'une utilisation
    public Virus Utiliser(Virus virusUtilisateur)
    {
        if (virusUtilisateur != null)
            Infecter(virusUtilisateur);

        if (virus == null)
            return null;
        else
            return virus;
    }

    public void Infecter(Virus virus)
    {
        this.virus = virus;
        GetComponent<MeshRenderer>().material = materialInfecté;
    }
}
