using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] GameObject point;
    float rotateSpeed = 3.0f;
    float moveSpeed = 15.0f;
    float distance = 25.0f;

    private void LateUpdate()
    {
        Vector3 angles = transform.localEulerAngles;
        float sinX = Mathf.Sin(Mathf.Deg2Rad * angles.x);
        Debug.Log(sinX);

        //Tourner la caméra autour du point
        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            // Tourne en X sans limite
            transform.RotateAround(point.transform.position, Vector3.up, mouseX);

            // Tourne en Y seulement si 
            if (sinX > 0.99)
            {
                if (Mathf.Sign(mouseY) == 1f)
                {
                    transform.RotateAround(point.transform.position, transform.right, -mouseY);
                }
            }
            else if (sinX < -0.99)
            {
                if (Mathf.Sign(mouseY) == -1f)
                {
                    transform.RotateAround(point.transform.position, transform.right, -mouseY);
                }
            }
            else
            {
                transform.RotateAround(point.transform.position, transform.right, -mouseY);
            }
        }

        //Tourner le point autour de la caméra
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;
            Debug.Log(mouseY);

            point.transform.RotateAround(transform.position, Vector3.up, mouseX);

            // Si ne regarde pas en bas, permet de tourner vers le bas
            if (sinX > 0.995)
            {
                if (Mathf.Sign(mouseY) == 1f)
                {
                    point.transform.RotateAround(transform.position, transform.right, -mouseY);
                }
            }

            else if (sinX < -0.995)
            {
                if (Mathf.Sign(mouseY) == -1f)
                {
                    point.transform.RotateAround(transform.position, transform.right, -mouseY);
                }
            }
            else
            {
                point.transform.RotateAround(transform.position, transform.right, -mouseY);
            }
        }

        // 3D movement
        // Avant
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)) {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            point.transform.position += transform.forward * Time.deltaTime * moveSpeed; 
        }
        // Arrière
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift)) { 
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
            point.transform.position -= transform.forward * Time.deltaTime * moveSpeed; 
        }
        // Gauche
        if (Input.GetKey(KeyCode.A)) {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
            point.transform.position -= transform.right * Time.deltaTime * moveSpeed;
        }
        // Droite
        if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
            point.transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
        //Haut
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
            point.transform.position += Vector3.up * Time.deltaTime * moveSpeed;
        }
        //Bas
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)) {
            transform.position -= Vector3.up * Time.deltaTime * moveSpeed;
            point.transform.position -= Vector3.up * Time.deltaTime * moveSpeed;
        }

        transform.LookAt(point.transform.position);


        // Zoom in et out
        var delta = Input.mouseScrollDelta.y;
        if (!(delta < 0 && distance < 1)) {
            distance = distance + Input.mouseScrollDelta.y;
        }
        transform.position = point.transform.position - transform.forward * distance;

        
    }
}
