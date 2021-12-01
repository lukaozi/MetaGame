using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BaseInput pi;

    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20.0f;
    public float cameraDampValue = 0.5f;

    private GameObject playerHandle;

    private GameObject cameraHandle;

    private GameObject model;

    private float tempEulerX;

    private GameObject camera;

    private Vector3 camaraDampVelocity;

    public bool IsAI = true;

    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;

        tempEulerX = 20.0f;

        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempEulerAngles = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);

        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        model.transform.eulerAngles = tempEulerAngles;

        if (!IsAI)
        {
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref camaraDampVelocity, cameraDampValue) ;
            camera.transform.eulerAngles = transform.eulerAngles;
        }
    }

    private void OnGUI()
    {
//        if (GUILayout.Button("远镜头"))
//        {
//            transform.localPosition = new Vector3(0, 6f, -14f);
//        }
//
//        if (GUILayout.Button("近镜头"))
//        {
//            transform.localPosition = new Vector3(0, 0.5f, -2f);
//        }
        
    }
}
