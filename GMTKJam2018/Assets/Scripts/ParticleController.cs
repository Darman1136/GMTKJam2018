using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    private ParticleSystem ps;

    // Use this for initialization
    void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    void Update() {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int numAlive = ps.GetParticles(particles);

        for (int i = 0; i < numAlive; i++) {
            Vector3 pos = particles[i].position;
            pos.x = 0;
            particles[i].position = pos;
        }

        ps.SetParticles(particles, numAlive);
    }
}
