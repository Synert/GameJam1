using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D level;
    public ColourToPrefab[] ColourMappings;

	void Start ()
    {
        LevelGeneration();
	}
	
    void LevelGeneration ()
    {
        for (int x = 0; x < level.width; x++)
        {
            for (int y = 0; y < level.height; y++)
            {
                TileGeneration(x, y);
            }
        }
    }

    void TileGeneration (int x, int y)
    {
        Color PixelColour = level.GetPixel(x, y);
        if (PixelColour.a == 0)
        {
            return;
        }

        foreach (ColourToPrefab colorMapping in ColourMappings)
        {
            if (colorMapping.colour.Equals(PixelColour))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
