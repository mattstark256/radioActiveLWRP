using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    [Range(0.0f, 1.0f)] public float lookSmoothing = 0.25f;
    [Range(0.0f, 10.0f)] public float lookSpeed = 2.0f;
    [Range(0.0f, 100.0f)] public float gravity = 20.0f;
    [Range(0.0f, 100.0f)] public float jumpSpeed = 10.0f;
    [Range(0.0f, 100.0f)] public float moveSpeed = 10.0f;

    [NonSerialized] public bool inputEnabled = true;
}
