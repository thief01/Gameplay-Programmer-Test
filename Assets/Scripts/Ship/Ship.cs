using Game;
using UnityEngine;

namespace Ship
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float forceMultipliter;
        [SerializeField] private float rotationSpeed;
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    
        public void Move(Vector2 direction)
        {
            float rotation = direction.x * rotationSpeed;
            transform.eulerAngles += new Vector3(0, 0, rotation);
        
            float force = direction.y * forceMultipliter * Time.deltaTime;

            Vector3 flyingDirection = Vector3.zero;
            if (force < 0)
            {
                flyingDirection = transform.right;
            }
            else if(force > 0)
            {
                flyingDirection = -transform.right;
            }

            flyingDirection *= maxSpeed * Time.deltaTime;
        
            Vector2 velocity = rigidbody.velocity;
            if (((Vector2)flyingDirection + velocity).magnitude <= maxSpeed)
            {
                rigidbody.velocity += flyingDirection;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameManager.Instance.SetGameState(GameState.waitForRestart);
            rigidbody.velocity=Vector3.zero;
        }
    }
}
