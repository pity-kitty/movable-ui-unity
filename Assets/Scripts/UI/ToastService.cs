using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate.UI
{
    public class ToastService : MonoBehaviour
    {
        [SerializeField] private Text toastText;
        [SerializeField] private float animationSpeed;
        [SerializeField] private float animationDelay;
        [SerializeField] private Transform toastAnchor;

        private float initialYPosition;
        private float endYPosition;

        private void Start()
        {
            initialYPosition = transform.position.y;
            endYPosition = toastAnchor.position.y;
        }
        
        public void ShowToast(string message)
        {
            StopAllCoroutines();
            toastText.text = message;
            StartCoroutine(AnimateToast());
        }
        
        private IEnumerator AnimateToast()
        {
            yield return ToastAnimation(initialYPosition, endYPosition);
            yield return new WaitForSeconds(animationDelay);
            yield return ToastAnimation(endYPosition, initialYPosition);
        }

        private IEnumerator ToastAnimation(float startValue, float endValue)
        {
            var transformPosition = transform.position;
            for (float i = 0; i < 1; i += Time.deltaTime / animationSpeed)
            {
                transformPosition.y = Mathf.Lerp(startValue, endValue, i);
                transform.position = transformPosition;
                yield return null;
            }
            transformPosition.y = endValue;
            transform.position = transformPosition;
        }
    }
}