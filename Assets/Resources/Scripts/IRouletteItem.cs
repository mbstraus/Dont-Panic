using UnityEngine;

public abstract class IRouletteItem : MonoBehaviour {

    public abstract void ApplyResult();
    public abstract string Description();
    public abstract bool IsOneTime();
}
