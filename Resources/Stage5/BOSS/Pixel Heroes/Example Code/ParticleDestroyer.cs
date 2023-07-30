using UnityEngine;

namespace PSO
{
    public class ParticleDestroyer : MonoBehaviour
    {
        public ParticleSystem Particle;

        void Update()
        {
            if (!Particle.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}