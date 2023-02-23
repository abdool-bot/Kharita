using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Gpm.Common.Util;

namespace Gpm.Common
{
    public class ManagedCoroutine : CustomYieldInstruction
    {
        public class EnumeratorStack
        {
            public EnumeratorStack(IEnumerator enumerator) { this.enumerator = enumerator; }
            public int count = 0;
            public IEnumerator enumerator;
        }

        private Coroutine coRoroutine = null;
#if UNITY_EDITOR
        private EditorCoroutine coEditorCoroutine = null;
#endif

        public System.Type type;

        public string name;

        public ManagedCoroutine parent;
#if RECORD_CORUTINE_STACK
        public System.Diagnostics.StackTrace stackTrace = null;
#endif
        public Stack<EnumeratorStack> enumeratorStack = new Stack<EnumeratorStack>();

        public IEnumerator currentEnumerator;

        static private Stack<ManagedCoroutine> currentManagedCoroutine = new Stack<ManagedCoroutine>();

        public override bool keepWaiting
        {
            get
            {
                if (coRoroutine != null)
                {
                    return true;
                }
#if UNITY_EDITOR
                if (coEditorCoroutine != null)
                {
                    return true;
                }
#endif
                return false;
            }
        }

        public ManagedCoroutine(IEnumerator routine, int skipFrames = 1)
        {
#if ENABLE_MANAGED_CORUTINE
            type = routine.GetType();
            name = type.ToString();
#endif

#if RECORD_CORUTINE_STACK

#if UNITY_EDITOR
            stackTrace = new System.Diagnostics.StackTrace(skipFrames, true);
#else
            stackTrace = new System.Diagnostics.StackTrace(skipFrames, false);
#endif

#endif
            if (currentManagedCoroutine.Count > 0)
            {
                parent = currentManagedCoroutine.Peek();
            }
#if RECORD_CORUTINE_STACK
            if(parent != null)
            {
                foreach(var frame in stackTrace.GetFrames())
                {
                    if(frame.GetMethod().GetType() == parent.currentEnumerator.GetType())
                    {
                        break;
                    }
                
                }
            
            }
#endif

            StartCoroutine(routine);
        }

        public static ManagedCoroutine Start(IEnumerator routine)
        {
            return new ManagedCoroutine(routine, 2);
        }

        public void StartCoroutine(IEnumerator routine)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                coEditorCoroutine = EditorCoroutine.Start(routine);
                return;
            }
#endif

            coRoroutine = ManagedCoroutineInstance.StartCoroutine(Run(routine));
        }

        public void StopCoroutine()
        {
#if UNITY_EDITOR
            if (coEditorCoroutine != null)
            {
                coEditorCoroutine.Stop();
                coEditorCoroutine = null;
            }
#endif
            if (coRoroutine != null)
            {
                ManagedCoroutineInstance.StopCoroutine(coRoroutine);
                coRoroutine = null;
            }
        }

        private IEnumerator Run(IEnumerator routine)
        {
#if ENABLE_MANAGED_CORUTINE
            ManagedCoroutineInstance.managedList.Add(this);

            enumeratorStack.Push(new EnumeratorStack(routine));

            while (enumeratorStack.Count > 0)
            {
                var currentEnumeratorStatck = enumeratorStack.Peek();

                object currentYieldedObject;
                try
                {
                    currentEnumerator = currentEnumeratorStatck.enumerator;
                    currentManagedCoroutine.Push(this);

                    if (currentEnumerator.MoveNext() == false)
                    {
                        enumeratorStack.Pop();

                        continue;
                    }

                    currentYieldedObject = currentEnumerator.Current;
                }
                catch(System.Exception ex)
                {
#if RECORD_CORUTINE_STACK
                    throw new ManagedCoroutineException(this, ex);
#else
                    throw ex;
#endif

                    break;
                }
                finally
                {
                    currentEnumerator = null;
                    currentManagedCoroutine.Pop();
                }

                currentEnumeratorStatck.count++;

                IEnumerator currentYieldedEnumerator = currentYieldedObject as IEnumerator;
                if(currentYieldedEnumerator != null)
                {
                    enumeratorStack.Push(new EnumeratorStack(currentYieldedEnumerator));
                }
                else
                {
                    yield return currentYieldedObject;
                }
            }

#if UNITY_EDITOR
            coEditorCoroutine = null;
#endif
            coRoroutine = null;

            ManagedCoroutineInstance.managedList.Remove(this);
#else
            yield return routine;
#endif
        }
    }


    internal class ManagedCoroutineInstance : MonoBehaviour
    {
        static private ManagedCoroutineInstance instance;
        static private bool initialized;
        static private bool isQuitting = false;

        public static List<ManagedCoroutine> managedList = new List<ManagedCoroutine>();

        internal static ManagedCoroutineInstance Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }

        internal static void Initialize()
        {
            if (initialized == false)
            {
                ManagedCoroutineInstance manager = null;

                try
                {
                    manager = GameObject.FindObjectOfType<ManagedCoroutineInstance>();
                }
                catch
                {
                    // Throw exception when calling from a worker thread.
                    var ex = new System.Exception("ManagedCoroutineInstance component created on the main thread. Make sure it is added to the scene before calling ManagedCoroutineInstance from a worker thread.");
                    UnityEngine.Debug.LogException(ex);
                    throw ex;
                }

                if (isQuitting == true)
                {
                    // don't create new instance after quitting
                    // avoid "Some objects were not cleaned up when closing the scene find target" error.
                    return;
                }

                if (manager == null)
                {
                    new GameObject("ManagedCoroutineInstance").AddComponent<ManagedCoroutineInstance>();
                }
                else
                {
                    manager.Awake(); // force awake
                }
            }
        }

        new internal static Coroutine StartCoroutine(IEnumerator routine)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                EditorCoroutine.Start(routine); return null;
            }
#endif

            var dispatcher = Instance;
            if (dispatcher != null)
            {
                return (dispatcher as MonoBehaviour).StartCoroutine(routine);
            }
            else
            {
                return null;
            }
        }

        new internal static void StopCoroutine(Coroutine routine)
        {
            var dispatcher = Instance;
            if (dispatcher != null)
            {
                (dispatcher as MonoBehaviour).StopCoroutine(routine);
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                initialized = true;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (this != instance)
                {
                    Debug.LogWarning("There is already a CorutineManager in the scene.");
                }
            }
        }

        private void OnDestroy()
        {
            instance = null;
            initialized = false;

            managedList.Clear();
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }
}