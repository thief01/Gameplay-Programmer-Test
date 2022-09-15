using Objects;
using Patterns;
using UnityEngine;

namespace Game
{
    public class InitPool : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int bulletPoolCount=10;

        [SerializeField] private GameObject astereoidPrefab;
        [SerializeField] private int astereoidPoolCount = 25600;

        private void Awake()
        {
            Pool<Bullet>.Instance.InitPool(bulletPoolCount, bulletPrefab);
            Pool<Astereoid>.Instance.InitPool(astereoidPoolCount, astereoidPrefab);
        }
    }
}
