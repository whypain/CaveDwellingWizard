using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    /// <summary> 
    /// Invoked when health changes. Parameters: current health, max health. 
    /// </summary>
    public Action<int, int> OnChanged; 

    /// <summary> 
    /// Invoked when health changes. Parameter: normalized health (0 to 1). 
    /// </summary>
    public Action<float> OnChanged01; 

    /// <summary>
    /// Invoked when current value decreases.
    /// </summary>
    public Action OnDecreased;

    /// <summary>
    /// Invoked when current value increases.
    /// </summary>
    public Action OnIncreased;

    /// <summary>
    /// Invoked when current value reaches zero.
    /// </summary>
    public Action OnDepleted;

    /// <summary>
    /// Invoked when current value reaches max value.
    /// </summary>
    public Action OnRecovered;

    public int Max => max;
    public int Current => current;
    public float Normalized => (float)current / max;

    protected bool isInitialized;


    [SerializeField] int max = 100;
    int current;


    void Awake()
    {
        current = max;
        LateAwake();
    }


    /// <summary>
    /// Called at the end of Awake().
    /// </summary>
    protected virtual void LateAwake() { }

    public virtual void Initialize() { isInitialized = true; }


    /// <summary>
    /// Decreases the current value by the specified amount.
    /// </summary>
    /// <param name="amount"></param>
    public void Decrease(int amount)
    {
        Set(current - amount);
        OnDecreased?.Invoke();

        if (current <= 0)
        {
            OnDepleted?.Invoke();
        }
    }


    /// <summary>
    /// Increases the current value by the specified amount.
    /// </summary>
    /// <param name="amount"></param>
    public void Increase(int amount)
    {
        Set(current + amount);
        OnIncreased?.Invoke();

        if (current >= max)
        {
            OnRecovered?.Invoke();
        }
    }


    /// <summary>
    /// Sets the current value to the specified value.
    /// </summary>
    /// <param name="value"></param>
    public void Set(int value)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Resource not initialized!");
            return;
        }

        current = Mathf.Clamp(value, 0, max);
        OnChanged?.Invoke(current, max);
        OnChanged01?.Invoke(Normalized);
    }
}
