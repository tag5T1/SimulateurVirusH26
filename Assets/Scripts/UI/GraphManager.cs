using UnityEngine;
using XCharts.Runtime;
using System;

public class GraphManager : MonoBehaviour
{
    [SerializeField] LineChart graphInfecte;
    [SerializeField] LineChart graphForceVirus;
    [SerializeField] LineChart graphMutationVirus;

    private int infecte;

    private void Awake()
    {
        infecte = 0;
    }

    private void OnEnable()
    {
        Actions.NewOnInfection += graphiqueInfection;
        Actions.OnInfection += graphiqueForceVirus;
        Actions.OnInfection += graphiqueMutationVirus;
    }
    private void OnDisable()
    {
        Actions.NewOnInfection -= graphiqueInfection;
        Actions.OnInfection -= graphiqueForceVirus;
        Actions.OnInfection -= graphiqueMutationVirus;
    }

    public void graphiqueInfection()
    {
        infecte++;
        graphInfecte.AddData(0, Time.time, infecte);
    }

    public void graphiqueForceVirus()
    {
        graphiqueMoyenne(graphForceVirus);
    }
    public void graphiqueMutationVirus()
    {
        graphiqueMoyenne(graphMutationVirus);
    }

    public void graphiqueMoyenne(LineChart graph)
    {
        GameObject[] personnes = GameObject.FindGameObjectsWithTag("Personne");

        float parametre = 0;
        float nbInfecte = 0;
        foreach (GameObject person in personnes)
        {
            if (person.GetComponent<IAPersonne>().personne.estInfecté)
            {
                switch (graph.name)
                {
                    case "GraphForceVirus": parametre += GetForce(person.GetComponent<IAPersonne>().personne);
                        break;

                    case "GraphPuissanceMutation": parametre += GetMutation(person.GetComponent<IAPersonne>().personne);
                        break;
                    default: Debug.Log(graph.name);
                        break;
                }
                nbInfecte++;
            }
        }
        parametre = parametre / nbInfecte;
        graph.AddData(0, Time.time, parametre);
    }
    private float GetForce(Personne pers)
    {
        return pers.virus.force;
    }
    private float GetMutation(Personne pers)
    {
        return pers.virus.puissanceMutation;
    }
}
