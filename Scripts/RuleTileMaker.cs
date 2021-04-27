using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class RuleTileMaker : EditorWindow
{
    [MenuItem("Tools/RuleTileMaker")]
    public static void ShowWindow() 
    {
        GetWindow<RuleTileMaker>("RuleTileMaker");
    }

    public int Padding;
    public Color FillColor = Color.clear;
    public string Results = "RuleTiles";

    [Obsolete]
    private void OnGUI()
    {
        if (beginTexture == null)
        {
            beginTexture = EditorGUILayout.ObjectField(beginTexture, typeof(Texture2D), GUILayout.Width(240), GUILayout.Height(144)) as Texture2D;
        }
        else 
        {
            beginTexture = EditorGUILayout.ObjectField(beginTexture, typeof(Texture2D), GUILayout.Width(beginTexture.width + beginTexture.width / 2), GUILayout.Height(beginTexture.height + beginTexture.height / 2)) as Texture2D;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Padding", GUILayout.Width(114));
        Padding = EditorGUILayout.IntField(Padding);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Image Fill Color", GUILayout.Width(114));
        FillColor = EditorGUILayout.ColorField(FillColor);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Results folder name", GUILayout.Width(114));
        Results = EditorGUILayout.TextField(Results);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Build"))
        {
            Build();
        }

        // If u want to change Rules or Tiles in your project

        /*if (GUILayout.Button("Build XML"))
        {
            XMLCreator xml = new XMLCreator();
            xml.Build();
        }*/
    }

    List<Texture2D> btl = new List<Texture2D>();
    Texture2D beginTexture;

    List<Texture2D> etl = new List<Texture2D>();
    Texture2D endTexture;

    string root = "null";
    string MainFolder = "nll";
    string SpritesFolder = "nll";

    Rect[] rects;

    int frames;

    public void Build()
    {
        frames = beginTexture.height / 32;
        rects = new Rect[47 * frames];

        int width = (9 * (32 + Padding) + Padding);
        int height = (6 * (32 + Padding) + Padding) * frames;

        endTexture = new Texture2D(width, height);

        CreateFolders();
        CreateBeginTiles();
        CreateEndTiles();
        CreateEndTexture();
        CraeateRuleTile();

        byte[] bytes = endTexture.EncodeToPNG();
        File.WriteAllBytes("Assets/" + Results + "/" + beginTexture.name + "/Image.png", bytes);
    }

    #region Folders
    void CreateFolders()
    {
        root = "Assets/" + Results;
        MainFolder = root + "/" + beginTexture.name;
        SpritesFolder = MainFolder + "/sprites";

        if (!AssetDatabase.IsValidFolder("Assets/" + Results))
        {
            AssetDatabase.CreateFolder("Assets", Results);
        }
        if (!AssetDatabase.IsValidFolder(MainFolder))
        {
            AssetDatabase.CreateFolder(root, beginTexture.name);
        }
        if (!AssetDatabase.IsValidFolder(SpritesFolder))
        {
            AssetDatabase.CreateFolder(MainFolder, "sprites");
        }
    }
    #endregion

    #region List of Begin texturies
    void CreateBeginTiles()
    {
        btl.Clear();

        for (int y = beginTexture.height - 16; y >= 0; y -= 16)
        {
            for (int x = 0; x < beginTexture.width; x += 16)
            {
                RectInt rect = new RectInt(x, y, 16, 16);
                Texture2D t = new Texture2D(16, 16);

                t.SetPixels(beginTexture.GetPixels(rect.x, rect.y, rect.width, rect.height));
                t.filterMode = FilterMode.Point;

                t.filterMode = FilterMode.Point;
                t.Apply();

                btl.Add(t);
            }
        }
    }
    #endregion

    #region List of End texturies
    void CreateEndTiles()
    {
        etl.Clear();

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/RuleTileMaker/Scripts/Data.xml");
        XmlElement root = xDoc.DocumentElement;
        XmlNode tiles = root.SelectSingleNode("tiles");

        //Fill EndTexture
        for (int Y = 0; Y < endTexture.height; Y++)
        {
            for (int X = 0; X < endTexture.width; X++)
            {
                endTexture.SetPixel(X, Y, FillColor);
            }
        }

        //Load id's of the four tiles that are need to create the final sprite
        foreach (XmlNode tile in tiles)
        {
            for (int i = 0; i < frames; i++)
            {
                List<int> t = new List<int>();
                foreach (XmlNode id in tile)
                {
                    t.Add(Convert.ToInt32(id.InnerText));
                }
                CreateTile(t, i * 20);
            }
        }
    }

    void CreateTile(List<int> t, int bias)
    {
        Texture2D T = new Texture2D(32, 32);

        for (int x = 0; x < 32; x++)
        {
            for (int y = 0; y < 32; y++)
            {
                T.SetPixel(x, y, Color.clear);
            }
        }

        for (int x = 0; x < 16; x++)
        {
            for (int y = 16; y < 32; y++)
            {
                T.SetPixel(x, y, btl[t[0] + bias].GetPixel(x, y - 16));
            }
        }

        for (int x = 16; x < 32; x++)
        {
            for (int y = 16; y < 32; y++)
            {
                T.SetPixel(x, y, btl[t[1] + bias].GetPixel(x - 16, y - 16));
            }
        }

        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                T.SetPixel(x, y, btl[t[2] + bias].GetPixel(x, y));
            }
        }

        for (int x = 16; x < 32; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                T.SetPixel(x, y, btl[t[3] + bias].GetPixel(x - 16, y));
            }
        }

        T.filterMode = FilterMode.Point;
        T.Apply();

        etl.Add(T);
    }
    #endregion

    #region Final Texture
    void CreateEndTexture() 
    {
        int y = endTexture.height - 32 - Padding;
        for (int i = 0; i < frames; i++)
        {
            int it = i;
            DrawLine(ref it, 9, ref y);
            DrawLine(ref it, 9, ref y);
            DrawLine(ref it, 8, ref y);
            DrawLine(ref it, 7, ref y);
            DrawLine(ref it, 7, ref y);
            DrawLine(ref it, 7, ref y);
        }
        endTexture.filterMode = FilterMode.Point;
        endTexture.Apply();

        AssetDatabase.CreateAsset(endTexture, root + "/" + beginTexture.name + "/Texture.asset");
    }

    void DrawLine(ref int it, int tileCount, ref int y)
    {
        for (int x = Padding; x < tileCount * (32 + Padding); x += 32 + Padding)
        {
            rects[it] = new Rect(x, y, 32, 32);

            for (int X = 0; X < 32; X++)
            {
                for (int Y = 0; Y < 32; Y++)
                {
                    endTexture.SetPixel(x + X, y + Y, etl[it].GetPixel(X, Y));
                }
            }
            it += frames;
        }
        y -= 32 + Padding;
        endTexture.Apply();
    }
    #endregion

    #region Rule Tile
    void CraeateRuleTile()
    {
        RuleTile tile = CreateInstance<RuleTile>();

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Assets/RuleTileMaker/Scripts/Data.xml");
        XmlElement root = xDoc.DocumentElement;
        XmlNode rules = root.SelectSingleNode("rules");

        int id = 0;

        //LoadRles From XML and Add new Rule to tile
        foreach (XmlNode rule in rules)
        {
            List<int> r = new List<int>();
            foreach (XmlNode value in rule)
            {
                r.Add(Convert.ToInt32(value.InnerText));
            }
            CreateRule(r, ref id, ref tile);
        }

        //Add default sprite for RuleTile
        string path = SpritesFolder + "/sprite_" + Convert.ToString(32 * frames) + ".asset";
        Sprite s = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
        tile.m_DefaultSprite = s;

        AssetDatabase.CreateAsset(tile, this.root + "/" + beginTexture.name + "_RuleTile.asset");
    }

    void CreateRule(List<int> r, ref int id, ref RuleTile tile)
    {
        RuleTile.TilingRule rule = new RuleTile.TilingRule();

        Dictionary<Vector3Int, int> dic = new Dictionary<Vector3Int, int>();

        if (r[0] != 0)
            dic.Add(new Vector3Int(-1, 1, 0), r[0]);
        if (r[1] != 0)
            dic.Add(new Vector3Int(0, 1, 0), r[1]);
        if (r[2] != 0)
            dic.Add(new Vector3Int(1, 1, 0), r[2]);
        if (r[3] != 0)
            dic.Add(new Vector3Int(-1, 0, 0), r[3]);
        if (r[4] != 0)
            dic.Add(new Vector3Int(1, 0, 0), r[4]);
        if (r[5] != 0)
            dic.Add(new Vector3Int(-1, -1, 0), r[5]);
        if (r[6] != 0)
            dic.Add(new Vector3Int(0, -1, 0), r[6]);
        if (r[7] != 0)
            dic.Add(new Vector3Int(1, -1, 0), r[7]);

        rule.ApplyNeighbors(dic);

        if (frames > 1)
        {
            rule.m_Output = RuleTile.TilingRuleOutput.OutputSprite.Animation;
            rule.m_Sprites = new Sprite[frames];
        }

        //Load And Create Sprite from EndTexture
        for (int i = 0; i < frames; i++)
        {
            string TexturePath = root + "/" + beginTexture.name + "/Texture.asset";
            string SpritePath = SpritesFolder + "/sprite_" + Convert.ToString(id + i) + ".asset";

            Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(TexturePath, typeof(Texture2D));

            Rect rect = rects[id + i];
            Sprite s = Sprite.Create(t, rect, Vector2.one * 0.5f);
            AssetDatabase.CreateAsset(s, SpritePath);

            s = (Sprite)AssetDatabase.LoadAssetAtPath(SpritePath, typeof(Sprite));
            s.name = "sprite_" + Convert.ToString(id + i);
            rule.m_Sprites[i] = s;
        }

        tile.m_TilingRules.Add(rule);

        id += frames;
    }
    #endregion


}
