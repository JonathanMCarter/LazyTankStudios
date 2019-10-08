using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Item Editor Window Script
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 6/10/19 - To add this comment bit in (nothing else was changed)

    This script makes the entire item editor window found under "Tools/Item Editor" 
    There aren't many comments here as editor script are a lot of lines just to make the windows look right

*/

public class ItemEditor : EditorWindow
{

    public static Item Type;
    public Item ItemToEdit;
    public int ToolbarValue;

    // Values to make a new item
    public string NewItemFileName;

    public struct ItemProps
    {
        public string NewItemName;
        public string NewItemDesc;
        public Sprite NewItemSprite;
        public ITEM_TYPE ItemType;
        public int ItemStack;
        public int ItemMinDamage;
        public int ItemMaxDamage;
    }

    public ItemProps NewItemProps = new ItemProps();

    // Values for editing exsisting items
    private Vector2 ScrollPos;

    private readonly List<Item> Read = new List<Item>();
    private Rect DeselectWindow;


    [MenuItem("Tools/Item Editor")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("Item Editor");
    }


    private void OnEnable()
    {
        ReadItems();
    }

    private void OnGUI()
    {
        DeselectWindow = new Rect(0, 0, position.width, position.height);

        ToolbarValue = GUILayout.Toolbar(ToolbarValue, new string[] { "Overview", "New Item", "Edit Item" }, GUILayout.MaxHeight(30));


        switch (ToolbarValue)
        {
            case 0:

                EditorGUILayout.LabelField("Item Editor");

                if (GUILayout.Button("Update Results"))
                {
                    ReadItems();
                    Debug.Log(Read.Count);
                }

                if (GUILayout.Button("Clear Results"))
                {
                    Read.Clear();
                }

                ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Width(position.width), GUILayout.ExpandHeight(true));

                for (int i = 0; i < Read.Count; i++)
                {
                    //if ((i % 2) == 0)
                    //{
                    //    GUI.color = Color.white;
                    //}
                    //else
                    //{
                    //    GUI.color = Color.grey;
                    //}
                    
                    EditorGUILayout.BeginHorizontal();

                    if (Read[i].icon)
                    {
                        if (GUILayout.Button(Read[i].icon.texture, GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                        {
                            GUI.FocusControl(null);
                        }
                    }

                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Item Name: ", EditorStyles.boldLabel, GUILayout.MaxWidth(80));
                    EditorGUILayout.LabelField(Read[i].itemName);
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Item Type: ", EditorStyles.boldLabel, GUILayout.MaxWidth(80));
                    EditorGUILayout.LabelField(Read[i].type.ToString());
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    if (GUILayout.Button("Edit Item", GUILayout.MaxWidth(100)))
                    {
                        ItemToEdit = Read[i];
                        ToolbarValue = 2;
                    }

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Separator();
                }

                EditorGUILayout.EndScrollView();

                //GUI.color = Color.white;

                break;
            case 1:

                EditorGUILayout.LabelField("Make a new item");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item File Name: ", GUILayout.MaxWidth(100));
                NewItemFileName = EditorGUILayout.TextField(NewItemFileName, GUILayout.MaxWidth(position.width - 25));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(30);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item Name: ", GUILayout.MaxWidth(75));
                NewItemProps.NewItemName = EditorGUILayout.TextField(NewItemProps.NewItemName, GUILayout.MaxWidth(position.width - 25));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("Item Description: ", GUILayout.MaxWidth(100));
                NewItemProps.NewItemDesc = EditorGUILayout.TextArea(NewItemProps.NewItemDesc, GUILayout.MaxWidth(position.width - 25), GUILayout.MaxHeight((position.width - 25) / 4));

                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item Icon: ", GUILayout.MaxWidth(75));
                NewItemProps.NewItemSprite = (Sprite)EditorGUILayout.ObjectField(NewItemProps.NewItemSprite, typeof(Sprite), false, GUILayout.MaxWidth(position.width - 25));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item Type: ", GUILayout.MaxWidth(75));
                NewItemProps.ItemType = (ITEM_TYPE)EditorGUILayout.EnumPopup(NewItemProps.ItemType, GUILayout.MaxWidth(position.width - 25));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (NewItemProps.ItemType == ITEM_TYPE.WEAPON)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Min Damage: ", GUILayout.MaxWidth(75));
                    NewItemProps.ItemMinDamage = EditorGUILayout.IntField(NewItemProps.ItemMinDamage, GUILayout.MaxWidth(position.width - 25));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Max Damage: ", GUILayout.MaxWidth(75));
                    NewItemProps.ItemMaxDamage = EditorGUILayout.IntField(NewItemProps.ItemMaxDamage, GUILayout.MaxWidth(position.width - 25));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }

                GUI.color = Color.green;

                if (GUILayout.Button("Create Item"))
                {
                    CreateNewItem();
                }

                GUI.color = Color.white;

                break;
            case 2:

                EditorGUILayout.LabelField("NOTE: You can't currently edit weapons damage stats here");

                if (ItemToEdit.type != ITEM_TYPE.WEAPON)
                {
                    ItemToEdit = (Item)EditorGUILayout.ObjectField(ItemToEdit, typeof(Item), false);
                }
                else
                {
                    ItemToEdit = (ItemOneHanded)EditorGUILayout.ObjectField(ItemToEdit, typeof(ItemOneHanded), false);
                }

                if (ItemToEdit)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Item Name: ", GUILayout.MaxWidth(75));
                    ItemToEdit.itemName = EditorGUILayout.TextField(ItemToEdit.itemName, GUILayout.MaxWidth(position.width - 25));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("Item Description: ", GUILayout.MaxWidth(100));
                    ItemToEdit.itemDescription = EditorGUILayout.TextArea(ItemToEdit.itemDescription, GUILayout.MaxWidth(position.width - 25), GUILayout.MaxHeight((position.width - 25) / 4));

                    GUILayout.Space(10);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Item Icon: ", GUILayout.MaxWidth(75));
                    ItemToEdit.icon = (Sprite)EditorGUILayout.ObjectField(ItemToEdit.icon, typeof(Sprite), false, GUILayout.MaxWidth(position.width - 25));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Item Type: ", GUILayout.MaxWidth(75));
                    ItemToEdit.type = (ITEM_TYPE)EditorGUILayout.EnumPopup(ItemToEdit.type, GUILayout.MaxWidth(position.width - 25));
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }


                //if (ItemToEdit.type == ITEM_TYPE.WEAPON)
                //{
                //    EditorGUILayout.BeginHorizontal();
                //    EditorGUILayout.LabelField("Min Damage: ", GUILayout.MaxWidth(75));
                //    ItemToEdit.damageMin = EditorGUILayout.IntField(NewItemProps.ItemMinDamage, GUILayout.MaxWidth(position.width - 25));
                //    GUILayout.FlexibleSpace();
                //    EditorGUILayout.EndHorizontal();

                //    EditorGUILayout.BeginHorizontal();
                //    EditorGUILayout.LabelField("Max Damage: ", GUILayout.MaxWidth(75));
                //    NewItemProps.ItemMaxDamage = EditorGUILayout.IntField(NewItemProps.ItemMaxDamage, GUILayout.MaxWidth(position.width - 25));
                //    GUILayout.FlexibleSpace();
                //    EditorGUILayout.EndHorizontal();
                //}

                break;
            default:
                break;
        }

        // Makes it so you can deselect elements in the window by adding a button the size of the window that you can't see under everything
        //make sure the following code is at the very end of OnGUI Function
        if (GUI.Button(DeselectWindow, "", GUIStyle.none))
        {
            GUI.FocusControl(null);
        }
    }


    private List<Item> ReadItems()
    {
        // Makes a new lsit the size of the amount of objects in the path
        List<string> AllFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/items"));

        // Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
        if (AllFiles.Count > 0)
        {
            Item Source;

            foreach (string Thingy in AllFiles)
            {
                string Path = "Assets" + Thingy.Replace(Application.dataPath, "").Replace('\\', '/');

                if (AssetDatabase.LoadAssetAtPath(Path, typeof(Item)))
                {
                    Source = (Item)AssetDatabase.LoadAssetAtPath(Path, typeof(Item));
                    Read.Add(Source);
                }
            }
        }
        else
        {
            // IF this happens, it broken :( ......... or there is nothing to read
        }

        return Read;
    }


    private void CreateNewItem()
    {
        if (NewItemProps.ItemType != ITEM_TYPE.WEAPON)
        {
            Item asset = ScriptableObject.CreateInstance<Item>();

            asset.itemName = NewItemProps.NewItemName;
            asset.itemDescription = NewItemProps.NewItemDesc;
            asset.icon = NewItemProps.NewItemSprite;
            asset.type = NewItemProps.ItemType;
            asset.stackSize = 1;

            AssetDatabase.CreateAsset(asset, "Assets/Items/" + NewItemFileName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }
        else
        {
            ItemOneHanded asset = ScriptableObject.CreateInstance<ItemOneHanded>();

            asset.itemName = NewItemProps.NewItemName;
            asset.itemDescription = NewItemProps.NewItemDesc;
            asset.icon = NewItemProps.NewItemSprite;
            asset.type = NewItemProps.ItemType;
            asset.stackSize = 1;
            asset.damageMin = NewItemProps.ItemMinDamage;
            asset.damageMax = NewItemProps.ItemMaxDamage;

            AssetDatabase.CreateAsset(asset, "Assets/Items/" +NewItemFileName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
        }

        NewItemFileName = "";
        NewItemProps = new ItemProps();
    }
}
