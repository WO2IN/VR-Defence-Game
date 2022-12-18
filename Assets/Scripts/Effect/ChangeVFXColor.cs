using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVFXColor : MonoBehaviour
{
    public ParticleSystem target;
    public float arrageRange = 0.5f;
    private void Awake()
    {
        target = GetComponent<ParticleSystem>();
    }

    public void Call(Color color)
    {
        // Environment Effect »ö»ó
        var main = target.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color, color * Random.Range(1f - arrageRange, 1f + arrageRange));
    }
}
