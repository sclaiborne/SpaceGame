using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public bool held = false;
    [Range(0.1f, 60f)]
    public float forceConst;
    [Range(0.01f, 5f)]
    public float arrowMagConst;
    public Rigidbody2D rb;
    public GravityAttract ga;
    public int arrowZ = 1;
    public GameObject dragObject;

    Vector2 heldPosition;
    GameObject heldObj;

    

    // Start is called before the first frame update
    void Start()
    {
        rb.Sleep();
        ga.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!held)
        {
            held = true;
            heldPosition = Input.mousePosition;
            heldObj = Instantiate(dragObject, this.transform.position, Quaternion.identity);
        }
        Debug.Log("Mouse down");
    }

    private void OnMouseDrag()
    {
        // Draw vector
        if (held)
            drawLaunchVector(heldPosition, Input.mousePosition);

        Debug.Log("Mouse drag");
    }

    private void OnMouseUp()
    {
        if (held)
        {
            Destroy(heldObj);
            launch(heldPosition, Input.mousePosition);
            held = false;
        }

        Debug.Log("Mouse up");
    }

    private void drawLaunchVector(Vector2 start, Vector2 end)
    {
        Vector2 distance = start - end;
        heldObj.transform.localScale = new Vector3(dragObject.transform.localScale.x, distance.magnitude*forceConst*arrowMagConst, dragObject.transform.localScale.z);
        heldObj.transform.eulerAngles =  new Vector3(0, 0, Mathf.Rad2Deg*Mathf.Atan2(distance.y, distance.x)-90);

    }

    private void launch(Vector2 start, Vector2 end)
    {
        Vector2 force = (start - end) * forceConst;
        rb.WakeUp();
        ga.enabled = true;
        rb.AddForce(force);
    }
}
