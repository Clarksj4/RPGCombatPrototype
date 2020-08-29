using UnityEngine;
using System.Linq;

public class SpriteManager : Singleton<SpriteManager>
{
    private SpriteCatalogue spriteCatalogue;

    protected override void Awake()
    {
        base.Awake();

        spriteCatalogue = Resources.Load<SpriteCatalogue>("SpriteCatalogue");
    }

    public Sprite GetSpriteByName(string name)
    {
        return spriteCatalogue.Sprites.FirstOrDefault(s => s.name == name);
    }
}
