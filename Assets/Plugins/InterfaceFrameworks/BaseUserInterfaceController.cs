using System;
using UnityEngine;
using SimpleUI.Interfaces;

namespace SimpleUI
{
    public class BaseUserInterfaceController : MonoBehaviour, IUserInterfaceControllable
    {
        [Tooltip("The FadeUI component that will be used to fade the panel in and out (Optional)")]
        [SerializeField] private BaseFadeUI fadeUI;

        protected virtual void SetData()
        {

        }

        public virtual void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            SetData();

            if (fadeUI == null)
            {
                callback.Invoke();
                return;
            }

            fadeUI.DoFadeIn(callback);
        }

        public virtual void Hide(Action callback = null)
        {
            if (fadeUI == null)
            {
                gameObject.SetActive(false);
                callback?.Invoke();
                return;
            }

            fadeUI.DoFadeOut(() =>
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        public void Hide()
        {
            Hide(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}