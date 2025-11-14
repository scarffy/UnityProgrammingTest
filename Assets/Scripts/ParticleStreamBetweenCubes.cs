using UnityEngine;

namespace TestAssignment
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleStreamBetweenCubes : MonoBehaviour
    {
        [SerializeField] private Transform otherCube;
        private ParticleSystem ps;
        private ParticleSystem.MainModule mainModule;
        private ParticleSystem.VelocityOverLifetimeModule velModule;
        
        private void Awake()
        {
            if(ps == null)
                ps = GetComponent<ParticleSystem>();
            
            mainModule = ps.main;
            velModule = ps.velocityOverLifetime;
            mainModule.playOnAwake = true;
            ps.Stop();
        }


        private void Start()
        {
            ps.Play();
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
            
            velModule.space = ParticleSystemSimulationSpace.World;
            velModule.x = new ParticleSystem.MinMaxCurve(dir.x * mainModule.startSpeed.constant);
            velModule.y = new ParticleSystem.MinMaxCurve(dir.y * mainModule.startSpeed.constant);
            velModule.z = new ParticleSystem.MinMaxCurve(dir.z * mainModule.startSpeed.constant);
            
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }
}
