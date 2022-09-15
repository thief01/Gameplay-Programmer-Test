using System.Collections;
using Objects;
using Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class MapController : Singleton<MapController>
    {
        private const int ASTEREOIDS_OFFSET = 10;
        private const int MAP_SIZE = 160;
        private const float RESPAWN_TIME = 1;

        [SerializeField] private Transform player;
        [SerializeField] private Camera camera;

        private Vector2 cameraSize
        {
            get
            {
                return new Vector2(camera.orthographicSize*2, camera.orthographicSize)*1.25f;
            }
        }
    
        private void Start()
        {
            Pool<Astereoid>.Instance.OnObjectDestroyed.AddListener(() => StartCoroutine(OnAstereoidDestroyed()));
            StartCoroutine(SpawningAstereoids());
            GameManager.Instance.OnGameStateChanged.AddListener(OnGameStatechanged );
        }

        private IEnumerator SpawningAstereoids()
        {
            yield return new WaitForSeconds(1);
            int x = -MAP_SIZE;
            int y = -MAP_SIZE;

            int offset = 1;
            int astereoidCounter = 0;
        
            do
            {
                yield return new WaitForSeconds(1);
                SpawnWave(new Vector2(1,1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(-1,-1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(1,-1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(-1,1), offset, ref astereoidCounter);

                offset++;

            } while (astereoidCounter<MAP_SIZE*MAP_SIZE);
        }

        private IEnumerator OnAstereoidDestroyed()
        {
            if (GameManager.Instance.GameState == GameState.waitForRestart)
                yield break;
            yield return new WaitForSeconds(RESPAWN_TIME);
            var astereoid = Pool<Astereoid>.Instance.GetObject();

            Vector2 position = Vector2.zero;
            do
            {
                position = new Vector2(Random.Range(-MAP_SIZE / 2, MAP_SIZE / 2),
                    Random.Range(-MAP_SIZE / 2, MAP_SIZE / 2));
            } while (IsClosestToPlayer(position));

            astereoid.transform.position = position;
        }

        private void SpawnWave(Vector2 direction, int offset, ref int astereoidCounter)
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    var astereoid = Pool<Astereoid>.Instance.GetObject();
                    astereoid.SetDirection(GetDirectionByPerlin(i+offset*10,j+offset*10));
                    astereoid.transform.position = new Vector3((i + offset * 10)*direction.x, (j + offset * 10)*direction.y);
                    astereoidCounter++;
                }
            }
        }

        private void OnGameStatechanged()
        {
            if (GameManager.Instance.GameState == GameState.playing)
            {
                StartCoroutine(SpawningAstereoids());
            }
            if (GameManager.Instance.GameState == GameState.waitForRestart)
            {
                StopAllCoroutines();
            }
        }

        private Vector2 GetDirectionByPerlin(int x, int y)
        {
            float angle = Mathf.PerlinNoise((float)x / MAP_SIZE, (float)y / MAP_SIZE)*2 % 1;
            return new Vector2(Mathf.Cos(360*angle*Mathf.Deg2Rad) , Mathf.Sign(360*angle*Mathf.Deg2Rad));
        }

        private bool IsClosestToPlayer(Vector2 point)
        {
            return point.x < player.position.x - cameraSize.x && point.y < player.position.y - cameraSize.y &&
                   point.x > player.position.x + cameraSize.x && point.y > player.position.y + cameraSize.y;
        }
    }
}
