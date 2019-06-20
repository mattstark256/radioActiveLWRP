using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationController", menuName = "Application Controller", order = 1)]
public class ApplicationController : ScriptableObject
{
    void OnEnable()
    {
        Cursor.visible = false;
    }
}
