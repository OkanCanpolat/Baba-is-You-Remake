using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    private bool eraseMode;
    private LevelCreator levelCreator;
    private KeyValuePair<Texture, GameObject> selectedItem;
    private Dictionary<Texture, GameObject> elementSpriteMap = new Dictionary<Texture, GameObject>();
    private Dictionary<Texture, GameObject> textSpriteMap = new Dictionary<Texture, GameObject>();

    private bool showObjects;
    private bool showTexts;
    private Vector2 scrollPosition;
    private string searchStr = "";
    private void OnEnable()
    {
        levelCreator = target as LevelCreator;

        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Rock/rock_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Rock/Rock.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Tile/tile_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Tile/Tile.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Baba/baba_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Baba/Baba.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Keke/keke_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Keke/Keke.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Flag/flag_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Flag/Flag.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Skull/skull_24_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Skull/Skull.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Flower/flower_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Flower/Flower.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Jelly/jelly_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Jelly/Jelly.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Crab/crab_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Crab/Crab.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Seastar/Seastar_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Seastar/Seastar.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Algae/algae_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Algae/Algae.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Love/love_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Love/Love.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Pillar/pillar_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Pillar/Pillar.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Key/key_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Key/Key.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Door/door_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Door/Door.prefab", typeof(GameObject)));

        #region Walls
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Wall/wall_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Wall/Wall_15.prefab", typeof(GameObject)));
        #endregion

        # region Lavas
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Lava/lava_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Lava/Lava_15.prefab", typeof(GameObject)));
        #endregion

        #region Grasses
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Grass/grass_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Grass/Grass_15.prefab", typeof(GameObject)));
        #endregion

        #region Waters
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Water/water_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Water/Water_15.prefab", typeof(GameObject)));
        #endregion

        #region Bricks
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Brick/brick_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Brick/Brick_15.prefab", typeof(GameObject)));
        #endregion

        #region Hedges
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Hedge/hedge_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Hedge/Hedge_15.prefab", typeof(GameObject)));
        #endregion

        #region Ices
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_3.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_4_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_4.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_5_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_5.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_6_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_6.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_7_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_7.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_8_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_8.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_9_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_9.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_10_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_10.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_11_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_11.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_12_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_12.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_13_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_13.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_14_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_14.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Ice/ice_15_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Ice/Ice_15.prefab", typeof(GameObject)));
        #endregion

        #region Bubbles
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Bubble/bubble_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Bubble/Bubble_0.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Bubble/bubble_1_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Bubble/Bubble_1.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Bubble/bubble_2_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Bubble/Bubble_2.prefab", typeof(GameObject)));
        elementSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Objects/Bubble/bubble_3_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Bubble/Bubble_3.prefab", typeof(GameObject)));

        #endregion

        #region Texts Noun
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Is/text_is_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/IsText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/And/text_and_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/AndText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Rock/text_rock_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/RockText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Flag/text_flag_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/FlagText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Baba/text_baba_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/BabaText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Skull/text_skull_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/SkullText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Wall/text_wall_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/WallText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Water/text_water_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/WaterText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Lava/text_lava_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/LavaText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Grass/text_grass_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/GrassText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Jelly/text_jelly_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/JellyText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Crab/text_crab_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/CrabText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Star/text_star_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/StarText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Keke/text_keke_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/KekeText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Algae/text_algae_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/AlgaeText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Love/text_love_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/LoveText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Pillar/text_pillar_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/PillarText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Door/text_door_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/DoorText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Key/text_key_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/KeyText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Hedge/text_hedge_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/HedgeText.prefab", typeof(GameObject)));
        #endregion

        #region Texts Verb
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/You/text_you_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/YouText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Win/text_win_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/WinText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Stop/text_stop_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/StopText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Push/text_push_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/PushText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Defeat/text_defeat_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/DefeatText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Sink/text_sink_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/SinkText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Hot/text_hot_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/HotText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Melt/text_melt_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/MeltText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Move/text_move_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/MoveText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Open/text_open_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/OpenText.prefab", typeof(GameObject)));
        textSpriteMap.Add((Texture)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Texts/Shut/text_shut_0_1.png", typeof(Texture)), (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Elements/Text/ShutText.prefab", typeof(GameObject)));
        #endregion
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();

        DrawCreateDeleteButtons();

        if (levelCreator.mapCreated)
        {
            DrawEraser();
            DrawGrid();
            DrawSearchBar();
            DrawElements();
            DrawTexts();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnClickGrid(int c, int r)
    {
        if (eraseMode && levelCreator.LevelDataHolder != null)
        {
            GridData gridData;

            if (levelCreator.LevelDataHolder.TryGetValue(new Vector2(c, r), out gridData))
            {
                levelCreator.LevelDataHolder.Remove(new Vector2(c, r));

                foreach (Element e in gridData.Elements)
                {
                    DestroyImmediate(e.gameObject);
                }
            }
        }

        else
        {
            Vector3 position = new Vector3(c, r, 0);
            Element e = ((GameObject)PrefabUtility.InstantiatePrefab(selectedItem.Value)).GetComponent<Element>();
            e.transform.position = position;
            //Element e = Instantiate(selectedItem.Value, position, Quaternion.identity).GetComponent<Element>();
            e.Column = c;
            e.Row = r;

            GridData gridData;

            if (levelCreator.LevelDataHolder.TryGetValue((Vector2)position, out gridData))
            {
                gridData.Elements.Add(e);
            }

            else
            {
                gridData = new GridData(selectedItem.Key, e);
                DataHolderEditor dataHolder = new DataHolderEditor((Vector2)position, gridData);
                levelCreator.LevelDataHolder.DataHolders.Add(dataHolder);
            }
        }
    }

    private void DrawCreateDeleteButtons()
    {

        if (GUILayout.Button("Create"))
        {
            levelCreator.Create();
        }

        if (GUILayout.Button("Delete"))
        {
            levelCreator.Delete();
        }
    }
    private void DrawElements()
    {
        showObjects = EditorGUILayout.Foldout(showObjects, "Objects");

        if (!showObjects) return;

        int count = 0;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);

        foreach (KeyValuePair<Texture, GameObject> keyValuePair in elementSpriteMap)
        {
            if (!keyValuePair.Key.name.Contains(searchStr))
            {
                continue;
            }


            if (GUILayout.Button(keyValuePair.Key, GUILayout.Width(40), GUILayout.Height(40)))
            {
                selectedItem = keyValuePair;
            }

            count++;

            if (count % 10 == 0)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                count = 0;
            }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void DrawTexts()
    {
        showTexts = EditorGUILayout.Foldout(showTexts, "Texts");

        if (!showTexts) return;

        int count = 0;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);

        foreach (KeyValuePair<Texture, GameObject> keyValuePair in textSpriteMap)
        {
            if (!keyValuePair.Key.name.Contains(searchStr))
            {
                continue;
            }
            if (GUILayout.Button(keyValuePair.Key, GUILayout.Width(40), GUILayout.Height(40)))
            {
                selectedItem = keyValuePair;
            }

            count++;

            if (count % 10 == 0)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                count = 0;
            }
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void DrawEraser()
    {
        if (eraseMode) GUI.backgroundColor = Color.red;

        if (GUILayout.Button("Erase Mode"))
        {
            eraseMode = !eraseMode;
        }

        GUI.backgroundColor = Color.white;
    }
    private void DrawGrid()
    {
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 8;
        style.fontStyle = FontStyle.Bold;

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, false);

        for (int r = levelCreator.height - 1; r >= 0; r--)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            for (int c = 0; c < levelCreator.width; c++)
            {
                Texture buttonImage = null;
                GridData gd;

                if (levelCreator.LevelDataHolder != null &&
                    levelCreator.LevelDataHolder.TryGetValue(new Vector2(c, r), out gd))
                {
                    buttonImage = gd.Texture;
                }

                if (buttonImage == null)
                {
                    if (GUILayout.Button(c + "_" + r, style, GUILayout.Width(35), GUILayout.Height(35)))
                    {
                        OnClickGrid(c, r);
                    }
                }

                else
                {
                    if (GUILayout.Button(buttonImage, style, GUILayout.Width(35), GUILayout.Height(35)))
                    {
                        OnClickGrid(c, r);
                    }
                }

            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();
    }
    private void DrawSearchBar()
    {

        searchStr = EditorGUILayout.TextField(searchStr, EditorStyles.toolbarSearchField);
        EditorGUILayout.Space();
    }
}
