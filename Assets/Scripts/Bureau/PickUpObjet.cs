using UnityEngine;

public class PickUpObjet : Objet
{
    public bool utilisé = false;
    public void Utiliser(IAPersonne personne)
    {
        gameObject.transform.SetParent(personne.gameObject.transform, true);
        utilisé = true; 
    }

    public void Lacher(IAPersonne personne)
    {
        gameObject.transform.SetParent(null, true);
        positionInteraction = indicateurDePosition.position;
        utilisé = false;
    }
}
