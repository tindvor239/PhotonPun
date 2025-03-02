using System;
using System.Collections;
using UnityEngine;
namespace SimpleUI
{
    public class FadeUI : BaseFadeUI
    {
        [SerializeField] CanvasGroup canvasGroup;
        // [SerializeField] private float duration = 0.5f;
        private float _duration = .1f;

        public override void DoFadeIn(Action onComplete = null)
        {
            StartCoroutine(FadeToAlpha(onComplete, 1));
        }

        // DoFade method that you can call for fading in or fading out
        public override void DoFadeOut(Action onComplete = null)
        {
            StartCoroutine(FadeToAlpha(onComplete, 0));
        }

        // Coroutine that fades the alpha value of the CanvasGroup to the target value
        private IEnumerator FadeToAlpha(Action onCompleted, float targetAlpha)
        {
            float startAlpha = canvasGroup.alpha;  // The current alpha value
            float elapsedTime = 0f;  // Time elapsed in the fade process

            // Fade from the current alpha value to the target value
            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _duration);
                yield return null;
            }

            // Ensure the final alpha value is set
            canvasGroup.alpha = targetAlpha;
            onCompleted?.Invoke();
        }
    }
}
