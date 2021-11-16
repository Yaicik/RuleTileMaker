using UnityEngine;
using System.Xml.Linq;
using System;

public class XMLCreator
{
    XDocument xDoc = new XDocument();
    XElement root = new XElement("root");
    XElement tiles = new XElement("tiles");
    XElement rules = new XElement("rules");

    public void Build() 
    {
        CreateTiles();
        CreateRules();

        root.Add(tiles);
        root.Add(rules);

        xDoc.Add(root);
        xDoc.Save("Assets/RuleTileMaker/Scripts/Data1.xml");
    }

    #region Rules
    void CreateRules()
    {
        int id = 0;
        CreateRule(new int[] {
            0,2,0,
            2,  1,
            0,1,2
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  2,
            2,1,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  1,
            2,1,2
        }, ref id);
        CreateRule(new int[] {
            2,1,2,
            1,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  2,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,1,0,
            2,  2,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            2,  1,
            0,1,1
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  2,
            1,1,0
        }, ref id);
        CreateRule(new int[] {
            0,1,2,
            2,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            2,1,0,
            1,  2,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,1,0,
            2,  2,
            0,1,0
        }, ref id);
        CreateRule(new int[] {
            0,1,2,
            2,  1,
            0,1,2
        }, ref id);
        CreateRule(new int[] {
            2,1,0,
            1,  2,
            2,1,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            2,  2,
            0,1,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            2,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,1,1,
            2,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            1,1,0,
            1,  2,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            1,1,1,
            1,  1,
            1,1,2
        }, ref id);
        CreateRule(new int[] {
            1,1,1,
            1,  1,
            2,1,1
        }, ref id);
        CreateRule(new int[] {
            1,1,1,
            1,  1,
            2,1,2
        }, ref id);
        CreateRule(new int[] {
            2,1,2,
            1,  1,
            1,1,1
        }, ref id);
        CreateRule(new int[] {
            0,1,1,
            2,  1,
            0,1,1
        }, ref id);
        CreateRule(new int[] {
            1,1,0,
            1,  2,
            1,1,0
        }, ref id);
        CreateRule(new int[] {
            1,1,1,
            1,  1,
            1,1,1
        }, ref id);
        CreateRule(new int[] {
            2,1,2,
            1,  1,
            2,1,2
        }, ref id);
        CreateRule(new int[] {
            1,1,2,
            1,  1,
            1,1,1
        }, ref id);
        CreateRule(new int[] {
            2,1,1,
            1,  1,
            1,1,1
        }, ref id);
        CreateRule(new int[] {
            1,1,2,
            1,  1,
            1,1,2
        }, ref id);
        CreateRule(new int[] {
            2,1,1,
            1,  1,
            2,1,1
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  1,
            1,1,1
        }, ref id);
        CreateRule(new int[] {
            1,1,1,
            1,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            2,  2,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            2,1,2,
            1,  1,
            2,1,1
        }, ref id);
        CreateRule(new int[] {
            2,1,2,
            1,  1,
            1,1,2
        }, ref id);
        CreateRule(new int[] {
            0,1,2,
            2,  1,
            0,1,1
        }, ref id);
        CreateRule(new int[] {
            2,1,0,
            1,  2,
            1,1,0
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  1,
            2,1,1
        }, ref id);
        CreateRule(new int[] {
            0,2,0,
            1,  1,
            1,1,2
        }, ref id);
        CreateRule(new int[] {
            1,1,2,
            1,  1,
            2,1,1
        }, ref id);
        CreateRule(new int[] {
            2,1,1,
            1,  1,
            2,1,2
        }, ref id);
        CreateRule(new int[] {
            1,1,2,
            1,  1,
            2,1,2
        }, ref id);
        CreateRule(new int[] {
            0,1,1,
            2,  1,
            0,1,2
        }, ref id);
        CreateRule(new int[] {
            1,1,0,
            1,  2,
            2,1,0
        }, ref id);
        CreateRule(new int[] {
            2,1,1,
            1,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            1,1,2,
            1,  1,
            0,2,0
        }, ref id);
        CreateRule(new int[] {
            2,1,1,
            1,  1,
            1,1,2
        }, ref id);
    }

    void CreateRule(int[] r, ref int id)
    {
        XElement rule = new XElement("rule");

        for (int i = 0; i < r.Length; i++)
        {
            XElement value = new XElement("value", Convert.ToString(r[i]));
            rule.Add(value);
        }
        rules.Add(rule);
    }
    #endregion

    #region Tiles
    void CreateTiles()
    {
        CreateTile(6, 5, 12, 19);
        CreateTile(4, 7, 18, 13);
        CreateTile(4, 5, 14, 15);
        CreateTile(4, 5, 18, 19);
        CreateTile(8, 9, 14, 15);
        CreateTile(4, 7, 14, 17);
        CreateTile(2, 3, 16, 17);
        CreateTile(6, 5, 12, 0);
        CreateTile(4, 7, 0, 13);
        CreateTile(2, 9, 16, 15);
        CreateTile(8, 3, 14, 17);
        CreateTile(2, 3, 12, 13);
        CreateTile(2, 9, 12, 19);
        CreateTile(8, 3, 18, 13);
        CreateTile(6, 7, 12, 13);
        CreateTile(6, 5, 16, 15);
        CreateTile(2, 0, 16, 15);
        CreateTile(0, 3, 14, 17);
        CreateTile(0, 0, 0, 19);
        CreateTile(0, 0, 18, 0);
        CreateTile(0, 0, 18, 19);
        CreateTile(8, 9, 0, 0);
        CreateTile(2, 0, 12, 0);
        CreateTile(0, 3, 0, 13);
        CreateTile(0, 0, 0, 0);
        CreateTile(8, 9, 18, 19);
        CreateTile(0, 9, 0, 0);
        CreateTile(8, 0, 0, 0);
        CreateTile(0, 9, 0, 19);
        CreateTile(8, 0, 18, 0);
        CreateTile(4, 5, 0, 0);
        CreateTile(0, 0, 14, 15);
        CreateTile(6, 7, 16, 17);
        CreateTile(8, 9, 18, 0);
        CreateTile(8, 9, 0, 19);
        CreateTile(2, 9, 12, 0);
        CreateTile(8, 3, 0, 13);
        CreateTile(4, 5, 18, 0);
        CreateTile(4, 5, 0, 19);
        CreateTile(0, 9, 18, 0);
        CreateTile(8, 0, 18, 19);
        CreateTile(0, 9, 18, 19);
        CreateTile(2, 0, 12, 19);
        CreateTile(0, 3, 18, 13);
        CreateTile(8, 0, 14, 15);
        CreateTile(0, 9, 14, 15);
        CreateTile(8, 0, 0, 19);
    }

    void CreateTile(int t0, int t1, int t2, int t3)
    {
        XElement tile = new XElement("tile");
        XElement i0 = new XElement("id", Convert.ToString(t0));
        tile.Add(i0);
        XElement i1 = new XElement("id", Convert.ToString(t1));
        tile.Add(i1);
        XElement i2 = new XElement("id", Convert.ToString(t2));
        tile.Add(i2);
        XElement i3 = new XElement("id", Convert.ToString(t3));
        tile.Add(i3);

        tiles.Add(tile);
    }
    #endregion
}
