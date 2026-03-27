using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Distributrice : Mobilier
{
    public List<IAPersonne> fileDattente;
  

    public void AttendreEnFile(IAPersonne personne)
    {
        fileDattente.Add(personne);
    }
    public void QuitterFile()
    {
        fileDattente.RemoveAt(0);
    }
}
