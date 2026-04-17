using UnityEngine;

public class PickUpObjet : Objet
{
    public bool utilisé = false;



    public void Utiliser(IAPersonne personne)
    {
        gameObject.transform.SetParent(personne.gameObject.transform, true);
        transform.position = GetComponent<Transform>().position + new Vector3(0.5f, 0, 0.5f);
        utilisé = true; 
    }

    public void Lacher()
    {
        gameObject.transform.SetParent(null, true);
        positionInteraction = indicateurDePosition.position;
        utilisé = false;
    }
}
