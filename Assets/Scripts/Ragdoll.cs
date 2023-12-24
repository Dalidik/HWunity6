using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    
    public List<Rigidbody> elements;
    public Animator animator;
  
    private void OnTriggerEnter(Collider other)
    {
        animator.enabled = false;
        foreach (var element in elements)
        {
            
            element.isKinematic = false;
        }
    }
}
