using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttract : MonoBehaviour
{
    const float G = 6.6674f;

    public static List<GravityAttract> Attractors;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        foreach(GravityAttract attractor in Attractors)
        {
            if(attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<GravityAttract>();

        Attractors.Add(this);
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(GravityAttract target)
    {
        Rigidbody2D targetRb = target.rb;

        //Vector3 direction = targetRb.position - rb.position;
        Vector3 direction = rb.position - targetRb.position;
        direction.z = 0; // In 2d games we dont want to move on the Z axis
        float distance = direction.magnitude;

        if (distance == 0)
            return;
        else if(distance < 0.5f)
        {
            distance = 0.5f;
        }

        float forceMagnitude = (rb.mass * targetRb.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        //rb.AddForce(force);
        targetRb.AddForce(force);
    }
}
