using UnityEngine;

namespace TestAssignment
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleStreamBetweenCubes : MonoBehaviour
    {
        [SerializeField] private Transform otherCube;
        private ParticleSystem particlesSystem;
        private ParticleSystem.MainModule mainModule;
        private ParticleSystem.VelocityOverLifetimeModule velocityModule;
        
        private void Awake()
        {
            if(particlesSystem == null)
                particlesSystem = GetComponent<ParticleSystem>();
            
            mainModule = particlesSystem.main;
            velocityModule = particlesSystem.velocityOverLifetime;
            mainModule.playOnAwake = true;
            particlesSystem.Stop();
        }


        private void Start()
        {
            particlesSystem.Play();
        }


        private void Update()
        {
            if (otherCube == null) return;
            Vector3 dir = (otherCube.position - transform.position);
            float dist = dir.magnitude;
            if (dist > 0.001f)
            {
                dir /= dist;
            }

            mainModule.startSpeed = Mathf.Max(1f, dist * 5f);
            
            velocityModule.space = ParticleSystemSimulationSpace.World;
            velocityModule.x = new ParticleSystem.MinMaxCurve(dir.x * mainModule.startSpeed.constant);
            velocityModule.y = new ParticleSystem.MinMaxCurve(dir.y * mainModule.startSpeed.constant);
            velocityModule.z = new ParticleSystem.MinMaxCurve(dir.z * mainModule.startSpeed.constant);
            
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }
}
