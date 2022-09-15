using System.Collections;
using Objects;
using Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class MapController : Singleton<MapController>
    {
        private const int MAP_SIZE = 160;
        private const int WAVE_IN_ROW = 8;
        private const int WAVE_SIZE = 10;
        private const float RESPAWN_TIME = 1;
        
        [SerializeField] private Camera camera;
        
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

            Vector2 offset = Vector2.one;
            int astereoidCounter = 0;
        
            do
            {
                yield return new WaitForSeconds(1);
                SpawnWave(new Vector2(1,1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(-1,-1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(1,-1), offset, ref astereoidCounter);
                SpawnWave(new Vector2(-1,1), offset, ref astereoidCounter);

                offset.x++;
                if (offset.x>=WAVE_IN_ROW)
                {
                    offset.y++;
                    offset.x = 1;
                }
                

            } while (astereoidCounter<MAP_SIZE*MAP_SIZE);
        }

        private IEnumerator OnAstereoidDestroyed()
        {
            if (GameManager.Instance.GameState == GameState.waitForRestart)
                yield break;
            yield return new WaitForSeconds(RESPAWN_TIME);
            var astereoid = Pool<Astereoid>.Instance.GetObject();
            astereoid.gameObject.SetActive(false);
            Vector2 position = Vector2.zero;
            do
            {
                position = new Vector2(Random.Range(-MAP_SIZE / 2, MAP_SIZE / 2),
                    Random.Range(-MAP_SIZE / 2, MAP_SIZE / 2));
                yield return null;

            } while (Vector3.Distance(position, camera.transform.position) <= camera.orthographicSize*3);

            astereoid.gameObject.SetActive(true);
            astereoid.transform.position = position;
        }

        private void SpawnWave(Vector2 direction, Vector2 offset, ref int astereoidCounter)
        {
            for (int i = 1; i < WAVE_SIZE; i++)
            {
                for (int j = 1; j < WAVE_SIZE; j++)
                {
                    var astereoid = Pool<Astereoid>.Instance.GetObject();
                    astereoid.SetDirection(GetDirectionByPerlin(i+(int)offset.x*10,j+(int)offset.y*10));
                    astereoid.transform.position = new Vector3((i + offset.x * 10)*direction.x, (j + offset.y * 10)*direction.y);
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
    }
}
