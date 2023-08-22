using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    Rigidbody2D rb;
   // public Attractor[] attractors;
    const float G = 667.4f;
    public static List<Attractor> Attractors;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }

    }
    void OnEnable()
    {
        if(Attractors == null)
           Attractors = new List<Attractor>();
            
        Attractors.Add(this);
    }
    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract (Attractor objtoAttract)
    {
        Rigidbody2D rbtoattract = objtoAttract.rb;

        Vector3 direction = rb.position - rbtoattract.position;
        float distance = direction.magnitude;

        float forcemegnitude = G *(rb.mass * rbtoattract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forcemegnitude;

        rbtoattract.AddForce(force);
    }
}
