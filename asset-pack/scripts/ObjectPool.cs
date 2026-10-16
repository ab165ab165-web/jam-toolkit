// ObjectPool.cs — JamKit
// 用途: 泛型 GameObject 对象池（子弹、特效、敌人、NPC、平台等）。
//       预生成 + SetActive(false) 回收；Get() 取出一个空闲对象；Release() 归还。
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#，无第三方依赖。
// 用法:
//   public class BulletPool : ObjectPool<Bullet> { }
//   var bullet = bulletPool.Get();
//   bullet.transform.position = ...;
//   bullet.OnExpire(); bulletPool.Release(bullet);

using System.Collections.Generic;
using UnityEngine;

namespace JamKit
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField, Min(0)] private int initialSize = 16;
        [SerializeField] private bool autoExpand = true;

        private readonly Stack<T> _free = new Stack<T>();
        private readonly HashSet<T> _live = new HashSet<T>();

        protected virtual void Awake()
        {
            if (prefab == null)
            {
                Debug.LogError($"[{nameof(ObjectPool<T>)}] prefab not assigned on '{name}'");
                return;
            }
            // Pre-instantiate
            for (int i = 0; i < initialSize; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.gameObject.SetActive(false);
                _free.Push(obj);
            }
        }

        /// <summary>取出一个对象；如全部在用且允许扩容则新建一个。</summary>
        public virtual T Get(Vector3 position, Quaternion rotation)
        {
            if (prefab == null) return null;
            T obj = null;
            if (_free.Count > 0)
            {
                obj = _free.Pop();
            }
            else if (autoExpand)
            {
                obj = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning($"[{nameof(ObjectPool<T>)}] Pool exhausted and autoExpand=false");
                return null;
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.gameObject.SetActive(true);
            _live.Add(obj);
            OnGet(obj);
            return obj;
        }

        public T Get() => Get(Vector3.zero, Quaternion.identity);

        /// <summary>归还对象。</summary>
        public virtual void Release(T obj)
        {
            if (obj == null) return;
            if (!_live.Remove(obj))
            {
                // Not from this pool — silently ignore to avoid double-release bugs.
                return;
            }
            OnRelease(obj);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform, false);
            _free.Push(obj);
        }

        /// <summary>回收所有在用对象。</summary>
        public virtual void ReleaseAll()
        {
            // copy to avoid mutating during enumeration
            var liveSnapshot = new List<T>(_live);
            for (int i = 0; i < liveSnapshot.Count; i++)
                Release(liveSnapshot[i]);
        }

        // Hooks for subclasses
        protected virtual void OnGet(T obj) { }
        protected virtual void OnRelease(T obj) { }

        public int FreeCount => _free.Count;
        public int LiveCount => _live.Count;
    }
}