using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Open()
    {
        Debug.Log("Upgrade UI ‚ğŠJ‚«‚Ü‚µ‚½i‰¼j");
    }
}
