using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this.GetComponent<T>();
        }

        if (Instance.gameObject != this.gameObject)
        {
            Destroy(this.gameObject);
            return;
        }


        AfterSingletonCheck();
    }

    protected virtual void AfterSingletonCheck() {}
}