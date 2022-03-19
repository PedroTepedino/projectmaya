using UnityEngine;

public abstract class AMover
{
    private readonly Transform _moverTransform;

    protected AMover(Transform moverTransform)
    {
        _moverTransform = moverTransform;
    }
    
    public abstract void Tick();
}
