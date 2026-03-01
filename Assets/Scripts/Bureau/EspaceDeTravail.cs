using UnityEngine;

public class EspaceDeTravail
{
    public GameObject bureau;

    public void RandomiserPositionBureau()
    {
        bureau.transform.position = new Vector3(Random.Range(-25, 25), -3.75f, Random.Range(-25, 25));
        bureau.GetComponent<Objet>().TrouverPositionIndicateur();
    }
}
