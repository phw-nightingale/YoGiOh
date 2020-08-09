using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Singleton;

namespace Utility
{
    public class GameObjectPoolManager : MonoSingletonManual<GameObjectPoolManager>
    {
        // 使用Dictionary拓展对象池
        // private List<GameObject> pool = new List<GameObject>();

        private static readonly Dictionary<string, List<GameObject>>
            Pools = new Dictionary<string, List<GameObject>>();

        public int maxSize = 500;

        protected override void Awake()
        {
            instance = this;
            // Debug.Log("On " + name + " Awake and Initializing Component");
        }
        
        private void OnDestroy()
        {
            Clear();
            // Debug.Log("On " + name + " Destroy and Clear GameObjects");
        }

        /// <summary>
        /// 从缓存池中获取一个GameObject
        /// 如果缓存池中有，那么直接从缓存池中获取
        /// 如果缓存池中没有，那么实例化一个对象
        /// </summary>
        /// <returns>game object</returns>
        public GameObject GetGameObject(string prefabName)
        {
            GameObject obj = null;
            if (Pools.ContainsKey(prefabName))
            {
                List<GameObject> pool = Pools[prefabName];
                if (pool.Count > 0)
                {
                    obj = pool[0];
                    pool.RemoveAt(0);
                }
            }

            if (obj == null)
            {
                GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
                obj = Instantiate(prefab);
                obj.name = prefabName;
            }

            obj.SetActive(true);
            obj.transform.SetParent(null);

            return obj;
        }

        /// <summary>
        /// Same as GetGameObject()
        /// the difference is it can initialize object's transform prop
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public GameObject GetGameObject(string prefabName, Transform point)
        {
            GameObject obj = GetGameObject(prefabName);
            obj.transform.rotation = point.rotation;
            obj.transform.position = point.position;
            return obj;
        }

        /// <summary>
        /// Same as GetGameObject(string, Transform)
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject GetGameObject(string prefabName, Vector3 position, Quaternion rotation)
        {
            GameObject obj = GetGameObject(prefabName);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        /// <summary>
        /// 归还借用的GameObject对象
        /// TODO: 添加对象池的长度限制
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="obj">要归还的对象</param>
        public void ReturnGameObject(string prefabName, GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);

            List<GameObject> pool = null;

            if (!Pools.ContainsKey(prefabName))
            {
                // 创建池子
                pool = new List<GameObject>();
                Pools.Add(prefabName, pool);
            }

            pool = Pools[prefabName];
            // 对象池已满，销毁对象
            if (pool.Count >= maxSize)
                Destroy(obj);
            else
                pool.Add(obj);

            obj.SetActive(false);
            obj.transform.SetParent(transform);
        }

        /// <summary>
        /// Same as ReturnGameObject(string, GameObject)
        /// IF prefab name same as game object name
        /// </summary>
        /// <param name="obj"></param>
        public void ReturnGameObject(GameObject obj)
        {
            ReturnGameObject(obj.name, obj);
        }

        /// <summary>
        /// 清理字典
        /// </summary>
        private void Clear()
        {
            // 清理字典
            foreach (var pool in Pools.Values)
                foreach (var obj in pool)
                    if (obj != null)
                        Destroy(obj);
            
            // 将容器空间清理
            Pools.Clear();
            // 手动GC
            GC.Collect();
        }
    }
}