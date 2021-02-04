using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PG : MonoBehaviour
{
    public Vector2Int size;
    public float zoom;
    public Vector2 offset;
    public Tilemap tm;
    public Tile tile;
    public float intensivity;
    public float cutPlane;

    public void genTex()
    {
        var tex = new Texture2D(size.x, size.y);
        tex.filterMode = FilterMode.Point;
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var gr = Mathf.PerlinNoise((x + offset.x) / zoom, (y + offset.y) / zoom);
                tex.SetPixel(x,y, getColorByInt(gr));
            }
        }
        tex.Apply();
        var spr = Sprite.Create(tex, new Rect(0, 0, size.x, size.y), new Vector2(0, 0));
        GetComponent<SpriteRenderer>().sprite = spr;
    }

    public void genTileCave()
    {
        tm.ClearAllTiles();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = Mathf.PerlinNoise((x + offset.x) / zoom, (y + offset.y) / zoom) * intensivity;
                var gr = p;
                var t = tile;
                t.color = getColorByInt(gr);
                //t.color = new Color(gr, gr, gr, 1);
                if (gr > cutPlane)
                {
                    tm.SetTile(new Vector3Int(x, y, 0), t);
                }
            }
        }
    }

    void Update()
    {
        genTileCave();
    }

    public Color getColorByInt(float inp)
    {
        var outp = new Color(0, 0, 0, 1);
        if (inp <Mathf.Infinity)
        {
            outp = new Color(130F / 255F, 206F / 255F, 74F / 255F, 1);
        }
        if (inp < 0.8F)
        {
            outp = new Color(110F / 255F, 176F / 255F, 44F / 255F, 1);
        }
        if (inp < 0.6F)
        {
            outp = new Color(245F / 255F, 239F / 255F, 73F / 255F ,1);
        }

        if (inp < 0.5F)
        {
            outp = Color.Lerp(new Color(17F / 255F, 21F / 255F, 150F / 255F,1), new Color(31F / 255F, 86F / 255F, 196F / 255F,1), inp / 0.5F);
        }
        return outp;
    }
    
}
