using UnityEngine;

public abstract class IRouletteItem : MonoBehaviour {

    public abstract void ApplyResult();
    public abstract string GetTitle();
    public abstract string GetDescription();
    public abstract bool IsOneTime();
}
