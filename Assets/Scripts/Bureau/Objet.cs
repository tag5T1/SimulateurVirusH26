using UnityEngine;

public class Objet : MonoBehaviour
{
    [SerializeField] Transform indicateurDePosition;
    public Vector3 positionInteraction { get; private set; }

    private void Start()
    {
        positionInteraction = indicateurDePosition.position;
        indicateurDePosition.gameObject.SetActive(false);
    }
}
