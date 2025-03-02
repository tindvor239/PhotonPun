using System;
using System.Collections.Generic;
using Singletons;
using SimpleUI.Interfaces;
using UnityEngine;

namespace SimpleUI
{
    public class PanelManager : ScopedSingleton<PanelManager>
    {
        private const string RESOURCES_PATH = "UserInterfaces/";
        private Dictionary<string, IUserInterfaceControllable> _panelDictionary = new();

        public T ShowPanelFromResource<T>(string panelId, Action onShownCallBack = null) where T : MonoBehaviour, IUserInterfaceControllable
        {
            if (_panelDictionary.TryGetValue(panelId, out IUserInterfaceControllable p))
            {
                p.Show(onShownCallBack);
                ((T)p).transform.SetAsLastSibling();
                return (T)p;
            }

            var loadedUI = Resources.Load<T>(RESOURCES_PATH + panelId);
            T newUI = Instantiate(loadedUI, transform);
            newUI.Show(onShownCallBack);
            _panelDictionary.Add(panelId, newUI);
            return newUI;
        }

        public T Get<T>(string panelId) where T : BaseUserInterfaceController
        {
            if (_panelDictionary.TryGetValue(panelId, out IUserInterfaceControllable panel))
            {
                return (T)panel;
            }
            return null;
        }

        public void HidePanel(string panelId, Action onHideCallBack = null)
        {
            if (_panelDictionary.ContainsKey(panelId))
            {
                _panelDictionary[panelId].Hide(onHideCallBack);
            }
        }
    }
}