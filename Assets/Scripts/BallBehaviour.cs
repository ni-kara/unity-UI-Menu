using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public void OnMouseDown()=>    
        GameObject.Find("SFX").GetComponent<AudioSource>().Play();
}
