using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow : MonoBehaviour
{
    public void ClosePopup()
    {
        Destroy(gameObject);
    }
}
