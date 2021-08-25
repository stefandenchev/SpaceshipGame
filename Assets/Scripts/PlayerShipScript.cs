using UnityEngine;
using System.Collections;

public class PlayerShipScript : ShipScript 
{
    float speed = 0.1f;
    float minX = -4.3f;
    float maxX = 4.3f;
    float minZ = -10f;
    float maxZ = 1.4f;
    float horizontalAxis;
    float verticalAxis;
    float planeRotateTime = 1f;
    float planeReturnRotationSpeed = 0f;
    float planeReturnDampTime = 0.2f;
    private PlayerLogicScript playerLogicScript;

    private void Start()
    {
        playerLogicScript = GetComponent<PlayerLogicScript>();
    }
    void Update ()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && Time.frameCount % 6 == 0)
        {
            Fire();
        }

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, -45f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (horizontalAxis < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 45f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (transform.rotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0f, ref planeReturnRotationSpeed, planeReturnDampTime));
        }

        //transform.Translate(horizontalAxis * speed, 0f, verticalAxis * speed);
        transform.position += Vector3.right * horizontalAxis * speed;
        transform.position += Vector3.forward * verticalAxis * speed;

	}

    private void LateUpdate()
    {
        var newPosition = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, Mathf.Clamp(transform.position.z, minZ, maxZ));
        transform.position = newPosition;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            playerLogicScript.Health -= 5;
            col.gameObject.GetComponent<EnemyScript>().HideMe();
        }
    }

}