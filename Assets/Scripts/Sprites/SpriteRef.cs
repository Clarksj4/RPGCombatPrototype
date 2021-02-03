using UnityEngine;
using System;

[Serializable]
public class SpriteRef
{
    /// <summary>
    /// Gets the defined name of the sprite name if it doesn't exist.
    /// </summary>
    public string Name { get { return !string.IsNullOrEmpty(name) ? name : Sprite.name; } }
    
    [SerializeField]
    private string name;
    public Sprite Sprite;
}
