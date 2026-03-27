using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Distributrice : Mobilier
{
    public List<IAPersonne> fileDattente;
    public Vector3 distanceEntrePersonnesEnFile { get; private set; }



    private void Start()
    {
        distanceEntrePersonnesEnFile = positionInteraction - transform.position;
    }

    public void AttendreEnFile(IAPersonne personne)
    {
        fileDattente.Add(personne);
    }
    public void QuitterFile(IAPersonne personne)
    {
        fileDattente.Remove(personne);
    }
}
