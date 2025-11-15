using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAssignment
{
    public class ParticleManager : MonoBehaviour
    {
        public ParticleSystem ps;
        private ParticleSystem.Particle[] particles;
        public int particleCount = 1000000;
        public float boundaryRadius = 10f;
        public Transform spinner;

        void Start()
        {
            particles = new ParticleSystem.Particle[particleCount];
            ps.maxParticles = particleCount;
            ps.Emit(particleCount);
            ps.GetParticles(particles);

            // Initialize particles inside a sphere
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].position = Random.insideUnitSphere * boundaryRadius;
                particles[i].velocity = Random.insideUnitSphere * 0.5f;
                particles[i].startSize = 0.05f;
                particles[i].remainingLifetime = 1000f; // Long lifetime
            }
            ps.SetParticles(particles, particles.Length);
        }

        void Update()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Vector3 pos = particles[i].position;
                Vector3 vel = particles[i].velocity;

                // Move particle
                pos += vel * Time.deltaTime;

                // Keep inside sphere
                if (pos.magnitude > boundaryRadius)
                {
                    pos = pos.normalized * boundaryRadius;
                    vel = Vector3.Reflect(vel, pos.normalized);
                }

                // Spinner deflection
                if (spinner != null)
                {
                    Vector3 toParticle = pos - spinner.position;
                    float dist = toParticle.magnitude;
                    float radius = 1f; // spinner radius
                    if (dist < radius)
                    {
                        vel += toParticle.normalized * 5f; // deflect
                    }
                }

                particles[i].position = pos;
                particles[i].velocity = vel;
            }

            ps.SetParticles(particles, particles.Length);
        }
    }
}
