using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestAssignment.prototype
{
    /// <summary>
    /// Legacy particle system
    /// </summary>
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

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].position = Random.insideUnitSphere * boundaryRadius;
                particles[i].velocity = Random.insideUnitSphere * 0.5f;
                particles[i].startSize = 0.05f;
                particles[i].remainingLifetime = 1000f;
            }
            ps.SetParticles(particles, particles.Length);
        }

        void Update()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Vector3 pos = particles[i].position;
                Vector3 vel = particles[i].velocity;
                
                pos += vel * Time.deltaTime;
                
                if (pos.magnitude > boundaryRadius)
                {
                    pos = pos.normalized * boundaryRadius;
                    vel = Vector3.Reflect(vel, pos.normalized);
                }

                if (spinner != null)
                {
                    Vector3 toParticle = pos - spinner.position;
                    float dist = toParticle.magnitude;
                    float radius = 1f;
                    if (dist < radius)
                    {
                        vel += toParticle.normalized * 5f;
                    }
                }

                particles[i].position = pos;
                particles[i].velocity = vel;
            }

            ps.SetParticles(particles, particles.Length);
        }
    }
}
