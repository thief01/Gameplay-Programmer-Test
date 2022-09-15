using Game;
using Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    public class Astereoid : MonoBehaviour
    {
        [SerializeField] private float speed;
        private SpriteRenderer spriteRenderer;
        private Vector2 velocity;
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            GameManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnEnable()
        {
            rigidbody.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized *
                                 Random.Range(0.01f, speed);
        }
    
        public void SetDirection(Vector2 direction)
        {
            rigidbody.velocity = direction * speed;
        }
    
        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Bullet")
            {
                GameManager.Instance.AddScore();
            }
            DestroyPooledObject();
        }

        public void DestroyPooledObject()
        {
            rigidbody.velocity = Vector2.zero;
            Pool<Astereoid>.Instance.BackToPool(this);
        }

        private void OnGameStateChanged()
        {
            if (GameManager.Instance.GameState == GameState.waitForRestart)
            {
                DestroyPooledObject();
            }
        }
    }
}
