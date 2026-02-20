using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Personne
{
    public Virus virus;
    [SerializeField] int rayonPathfinding = 10;
    public float niveauToux;
    public Vector3 destination;
    public EspaceDeTravail espaceDeTravail;

    public Personne()
    {
        destination = new Vector3(Random.Range(-rayonPathfinding, rayonPathfinding), -5, Random.Range(-rayonPathfinding, rayonPathfinding));
    }

    public Vector3 G�n�rerPosition()
    {
        Vector3 position;
        position = espaceDeTravail.bureau.transform.position;
        return position;
    }

    public void Infecter(Virus virus)
    {
        this.virus = virus.Muter();
    }
}
