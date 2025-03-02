using System;
using UnityEngine;

namespace SimpleUI
{
    public abstract class BaseFadeUI : MonoBehaviour
    {
        public abstract void DoFadeIn(Action onComplete = null);
        public abstract void DoFadeOut(Action onComplete = null);
    }
}