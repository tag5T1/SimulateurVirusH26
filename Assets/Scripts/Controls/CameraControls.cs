using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] GameObject point;
    float rotateSpeed = 3.0f;
    float moveSpeed = 5.0f;
    float distance = 50.0f;

    private void LateUpdate()
    {
        //Tourner la caméra autour du point
        if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.RotateAround(point.transform.position, Vector3.up, mouseX);

            if (transform.localEulerAngles.x >= 85 && transform.localEulerAngles.x <= 95)
            {
                if (Mathf.Sign(mouseY) == 1f)
                {
                    transform.RotateAround(point.transform.position, transform.right, -mouseY);
                }
            }

            else if (transform.localEulerAngles.x >= 265 && transform.localEulerAngles.x <= 275)
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
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            point.transform.RotateAround(transform.position, Vector3.up, mouseX);

            if (transform.localEulerAngles.x >= 85 && transform.localEulerAngles.x <= 95)
            {
                if (Mathf.Sign(mouseY) == -1f)
                {
                    point.transform.RotateAround(transform.position, transform.right, mouseY);
                }
            }

            else if (transform.localEulerAngles.x >= 265 && transform.localEulerAngles.x <= 275)
            {
                if (Mathf.Sign(mouseY) == 1f)
                {
                    point.transform.RotateAround(transform.position, transform.right, mouseY);
                }
            }
            else
            {
                point.transform.RotateAround(transform.position, transform.right, mouseY);
            }
        }

        //Avant
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftControl)) { transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * moveSpeed; point.transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * moveSpeed; }
        //Gauche
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftControl)) {transform.position -= transform.right * Time.deltaTime * moveSpeed; point.transform.position -= transform.right * Time.deltaTime * moveSpeed; }
        //Derrière
        if (Input.GetKey(KeyCode.S)) {transform.position -= new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * moveSpeed; point.transform.position -= new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * moveSpeed; }
        //Droite
        if (Input.GetKey(KeyCode.D)) {transform.position += transform.right * Time.deltaTime * moveSpeed; point.transform.position += transform.right * Time.deltaTime * moveSpeed; }
        //Haut
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W)) { transform.position += Vector3.up * Time.deltaTime * moveSpeed; point.transform.position += Vector3.up * Time.deltaTime * moveSpeed; }
        //Bas
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.S)) { transform.position -= Vector3.up * Time.deltaTime * moveSpeed; point.transform.position -= Vector3.up * Time.deltaTime * moveSpeed; }

        transform.LookAt(point.transform.position);
        //Avancer et reculer
        distance = distance + Input.mouseScrollDelta.y;
        transform.position = point.transform.position - transform.forward * distance;

        
    }
}
