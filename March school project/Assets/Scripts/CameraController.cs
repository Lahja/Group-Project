using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    public GameObject player;
    
   

    // Start is called before the first frame update
    void Start()
    {
        currentView = views[0];
    }
  


    // Update is called once per frame
     void Update()
    {
      if (Input.GetKeyDown(KeyCode.E))
    {
        currentView = views[0];
    }
    if (Input.GetKeyDown(KeyCode.R))
    {
      currentView = views[1];
    }
    if (Input.GetKeyDown(KeyCode.F))
    {
      currentView = views[2];
    }
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentView = views[3];
        }
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }
}
