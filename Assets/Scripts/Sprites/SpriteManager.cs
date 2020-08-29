using UnityEngine;
using System.Linq;

public class SpriteManager : Singleton<SpriteManager>
{
    private SpriteCatalogue SpriteCatalogue
    {
        get
        {
            if (spriteCatalogue == null)
                spriteCatalogue = Resources.Load<SpriteCatalogue>("SpriteCatalogue");
            return spriteCatalogue;
        }
    }
    private SpriteCatalogue spriteCatalogue;

    public Sprite GetSpriteByName(string name)
    {
        return SpriteCatalogue.Sprites.FirstOrDefault(s => s.name == name);
    }
}
