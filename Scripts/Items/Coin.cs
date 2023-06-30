using UnityEngine;

public class Coin : MonoBehaviour {
    public float ScoreValue;
    public ParticleSystem particle;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out ScoreCheck scoreCheck)){
            scoreCheck.Score += this.ScoreValue;
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}