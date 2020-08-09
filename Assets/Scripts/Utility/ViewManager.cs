using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Singleton;

namespace Utility
{
    public class ViewManager : MonoSingletonAuto<ViewManager>
    {
        public const string Front = "Foreground";
        public const string Middle = "Middleground";
        public const string Back = "Background";
        
        /// <summary>
        /// 存储所有已经加载进来的页面，
        /// 包含显示的和隐藏的
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="AbsBaseView"></typeparam>
        /// <returns></returns>
        private readonly Dictionary<string, AbsBaseView> _loadedViews = new Dictionary<string, AbsBaseView>();

        /// <summary>
        /// 存储正在显示的界面
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="AbsBaseView"></typeparam>
        /// <returns></returns>
        private readonly Dictionary<string, AbsBaseView> _showViews = new Dictionary<string, AbsBaseView>();

        /// <summary>
        /// 界面销毁的时间间隔
        /// </summary>
        private float _destroyInterval = 300;

        /// <summary>
        /// 画布，只要ViewManager的单列存在，就需要克隆出来画布
        /// 并且画布不能跟着场景的销毁而销毁
        /// </summary>
        public readonly Dictionary<string, GameObject> _canvas = new Dictionary<string, GameObject>();

        private GameObject root;

        [HideInInspector]
        public AbsBaseWindow currentWindow;

        /// <summary>
        /// 初始加载所有UI画布，并存储相应的画布信息
        /// </summary>
        private void Awake()
        {
            var canvasGroup = Resources.Load<GameObject>("Prefabs/UI/CanvasGroup");
            canvasGroup = Instantiate(canvasGroup, Vector3.zero, Quaternion.identity);
            canvasGroup.name = "CanvasGroup";
            for (var i = 0; i < canvasGroup.transform.childCount; i++)
            {
                var child = canvasGroup.transform.GetChild(i);
                if (child.TryGetComponent<Canvas>(out var canvas))
                {
                    _canvas.Add(canvas.name, canvas.gameObject);
                }
            }

            root = canvasGroup;
            
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvasGroup);
        }

        private void Start()
        {
            
        }

        public AbsBaseView Load(string viewName, string canvasName = null)
        {
            string fullName = $"Prefabs/UI/{viewName}";
            if (!_loadedViews.ContainsKey(fullName))
            {
                Debug.Log($"{fullName}, {canvasName}");
                var prefab = Resources.Load<GameObject>(fullName);
                AbsBaseView views = null;
                if (canvasName != null)
                    views = Instantiate(prefab, _canvas[canvasName].transform).GetComponent<AbsBaseView>();
                else
                    views = Instantiate(prefab).GetComponent<AbsBaseView>();
                views.name = fullName;
                views.hideTime = DateTime.Now;
                _loadedViews.Add(fullName, views);
            }

            return _loadedViews[fullName];
        }

        public void Remove(string fullName)
        {
            if (_showViews.ContainsKey(fullName))
            {
                _showViews.Remove(fullName);
            }

            if (_loadedViews.TryGetValue(fullName, out var view))
            {
                _loadedViews.Remove(fullName);
                Destroy(view.gameObject);
            }
        }

        public void RemoveAll()
        {
            foreach (var view in _loadedViews.Values)
            {
                Destroy(view.gameObject);
            }
            _loadedViews.Clear();
            _showViews.Clear();
        }

        public void Show(string viewName, string canvasName = null)
        {
            
            var view = Load(viewName, canvasName);
            if (!_showViews.ContainsKey(viewName))
            {
                //需要使当前要显示的界面层级是最高的
                //将当前物体在父物体下的索引变成最后一个
                view.transform.SetAsLastSibling();
                
                view.Show();
                _showViews.Add(view.name, view);
            }
        }

        public void ShowWindow(string windowName) 
        {
            string fullName = "Windows/" + windowName;

            var window = Load(fullName, Front);
            
            if (currentWindow != null) 
                if (currentWindow.name.Equals(window.name))
                    return;
                else
                    HideWindow();
            
            if (!_showViews.ContainsKey(window.name))
            {
                window.transform.SetAsLastSibling();
                window.Show();
                currentWindow = window as AbsBaseWindow;
                _showViews.Add(window.name, window);
            }
        }

        public void Hide(string viewName, bool isDestroy = false)
        {
            if (_showViews.TryGetValue(viewName, out var view))
            {
                _showViews.Remove(viewName);
                view.Hide(isDestroy);
                view.hideTime = DateTime.Now;
            }
        }

        public void HideWindow(string viewName = null, bool isDestroy = false)
        {
            if (viewName == null && _showViews.TryGetValue(currentWindow.name, out var window))
            {
                window.Hide();
                _showViews.Remove(window.name);
                currentWindow = null;
            }
        }

        /// <summary>
        /// 检测能被销毁的视图
        /// </summary>
        public void CheckDestroyableViews()
        {
            foreach (var viewName in _loadedViews.Keys.ToList())
            {
                var view = _loadedViews[viewName];
                if (view.state == AbsBaseView.ViewState.Hide)
                {
                    var interval = GetTimeSpan(DateTime.Now, view.hideTime);
                    if (interval >= _destroyInterval)
                    {
                        // 在foreach中不能对Dictionary中的值进行更改
                        // 使用for循环或者记录下要删除的viewName
                        Remove(view.name);
                    }
                }
            }
        }

        private long GetTimeSpan(DateTime t1, DateTime t2)
        {
            TimeSpan ts = t1 - t2;
            return Convert.ToInt32(ts.TotalSeconds);
        }
    }
}