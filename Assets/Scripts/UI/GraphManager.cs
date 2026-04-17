using UnityEngine;
using XCharts.Runtime;
using System;

public class GraphManager : MonoBehaviour
{
    [SerializeField] LineChart graphInfecte;
    [SerializeField] LineChart graphForceVirus;
    [SerializeField] LineChart graphMutationVirus;
    LineChart[] graphiques;

    private int infecte;

    private void Awake()
    {
        infecte = 0;
        graphiques = new LineChart[3] {graphInfecte, graphForceVirus, graphMutationVirus};
    }

    private void Update()
    {
        VerifierTailleGraphique();
    }

    private void OnEnable()
    {
        Actions.NewOnInfection += graphiqueInfectionPlus;
        Actions.OnInfection += graphiqueForceVirus;
        Actions.OnInfection += graphiqueMutationVirus;
        Actions.OnGueri += graphiqueInfectionMoins;
    }
    private void OnDisable()
    {
        Actions.NewOnInfection -= graphiqueInfectionPlus;
        Actions.OnInfection -= graphiqueForceVirus;
        Actions.OnInfection -= graphiqueMutationVirus;
        Actions.OnGueri -= graphiqueInfectionMoins;
    }

    public void graphiqueInfectionPlus()
    {
        infecte++;
        graphInfecte.AddData(0, Time.time, infecte);
    }

    public void graphiqueInfectionMoins()
    {
        infecte--;
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

    private void VerifierTailleGraphique()
    {
        foreach(var g in graphiques)
        {
            var serie = g.GetSerie(0);
            int nb = serie.dataCount;
            if(nb >= 200)
            {
                for (int i = 1; i > nb; i+=2)
                { 
                    serie.RemoveData(i);
                }
            }
        }
    }
}
