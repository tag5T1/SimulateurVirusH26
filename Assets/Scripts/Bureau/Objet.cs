using UnityEngine;
using UnityEngine.Rendering;

public class Objet : MonoBehaviour
{
    Virus virus;
    [SerializeField] protected Transform indicateurDePosition;
    [SerializeField] Material materialInfecté;
    public Vector3 positionInteraction { get; protected set; }



    private void Awake()
    {
        TrouverPositionIndicateur();
    }

    public void TrouverPositionIndicateur()
    {
        positionInteraction = indicateurDePosition.position;
        indicateurDePosition.gameObject.SetActive(false);
    }

    public Virus Infecter(Virus virusUtilisateur)
    {
        if (virusUtilisateur != null)
        {
            this.virus = virusUtilisateur;
            GetComponent<MeshRenderer>().material = materialInfecté;
        }

        if (virus == null)
            return null;
        else
            return virus;
    }
}
