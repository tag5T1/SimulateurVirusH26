using UnityEngine;

public class Personne : MonoBehaviour
{
    [SerializeField] GameObject particule;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.blue, 0.05f);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instance;
            var pos = transform.position + 0.6f * transform.forward;
            for (int i = 0; i < 10; i++)
            {
                instance = GameObject.Instantiate(particule, pos, Quaternion.identity);
                VirusParticule vir = null;
                while (vir == null)
                {
                    vir = instance.GetComponent<VirusParticule>();
                }
                vir.Creation(transform.forward, 5);
            }
        }

        if(Input.GetKey(KeyCode.Space))
            transform.Rotate(Vector3.up, 45 * Time.deltaTime);
    }
}
