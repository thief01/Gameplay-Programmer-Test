using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIScore : MonoBehaviour
    {
        private TextMeshProUGUI score;

        private void Awake()
        {
            score = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            score.text = "Score: " + GameManager.Instance.Points.ToString();
        }
    }
}
