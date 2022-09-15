using System.Collections;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private Image frog;
        private TextMeshProUGUI score;

        private void Awake()
        {
            score = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            score.text = "Score: " + GameManager.Instance.Points;
            if (GameManager.Instance.Points == 70)
            {
                StartCoroutine(FrogDelay());
            }
        }

        private IEnumerator FrogDelay()
        {
            frog.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            frog.gameObject.SetActive(false);
        }
    }
}
