using UnityEngine;

using System.Collections.Generic;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.data;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class FurnitureSpriteObserver : IFurnitureObserver, IOrientationObserver {

        GameObject furnitureHolder;

        Dictionary<ITile, GameObject> gameobjects;

        public FurnitureSpriteObserver() {
            furnitureHolder = new GameObject("Furniture");

            gameobjects = new Dictionary<ITile, GameObject>();

            OrientationManager.RegisterObserver(this);
        }

        public void StopObserving() {
            OrientationManager.UnregisterObserver(this);
        }

        public GameObject CreateSprite(ITile tile) {
            GameObject furniture_go = new GameObject();
            furniture_go.name = "Furni [" + tile.X.ToString() + "," + tile.Y.ToString() + "]";
            furniture_go.transform.SetParent(furnitureHolder.transform, true);

            SpriteRenderer sr = furniture_go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";

            gameobjects.Add(tile, furniture_go);
            UpdateSprite(tile);

            return furniture_go;
        }

        void UpdateSprite(ITile tile) {
            if (gameobjects.ContainsKey(tile) == false) {
                Debug.LogError("Trying to update a furni without a gameobject created.");
                return;
            }

            GameObject furni_go = gameobjects[tile];

            furni_go.transform.position = (Vector3)TileTransformer.CoordToWorld(tile.X, tile.Y);

            SpriteRenderer sr = furni_go.GetComponent<SpriteRenderer>();
            sr.sprite = DataManager.furnitureSpriteData.GetDataById(tile.GetPlacedFurniture().GetSpriteIndex()).GetSprite(tile.GetPlacedFurniture());
            sr.sortingOrder = SortingOrders.TileOrder(tile.X, tile.Y, TileSubLayer.Furniture);
        }

        void UpdateAllSprites() {
            foreach (KeyValuePair<ITile, GameObject> entry in gameobjects) {
                UpdateSprite(entry.Key);
            }
        }

        public void BindLevel(Level level) {
            for (int x = 0; x < level.Width; x++) {
                for (int y = 0; y < level.Height; y++) {
                    CreateSprite(level.GetTileAt(x, y));
                }
            }

            level.SubscribeToFurnitureChanges(this);
        }

        public void RemoveFurniture() {

            foreach (KeyValuePair<ITile, GameObject> entry in gameobjects) {
                GameObject.Destroy(entry.Value);
            }
            gameobjects = new Dictionary<ITile, GameObject>();
        }

        public void NotifyFurnitureTypeChanged(ITile tile) {
            UpdateSprite(tile);
        }

        public void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation) {
            UpdateAllSprites();
        }
    }
	
}