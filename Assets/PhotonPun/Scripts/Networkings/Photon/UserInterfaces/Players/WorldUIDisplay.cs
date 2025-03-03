using UnityEngine;
using TMPro;

namespace Photon.Pun.Interfaces
{
    public class WorldUIDisplay : MonoBehaviourPun
    {
        [SerializeField]
        private TMP_Text _textPrefab;

        protected TMP_Text textName;

        protected virtual void Awake()
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            textName = Instantiate(_textPrefab, canvas.transform);
        }

        private void OnDestroy()
        {
            if (textName != null)
                Destroy(textName.gameObject);
        }

        private void Update()
        {
            textName.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}