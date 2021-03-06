﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inspector;

namespace Map
{
    public class CTrack : MonoBehaviour
    {
        private const int TILE_SIZE = 18;

        public TrackType Type;
        private List<CTile> mInstTiles = new List<CTile>();
        public List<CTile> InstTileList
        {
            get
            {
                if (mInstTiles.Count == 0)
                {
                    SearchTiles();
                }
                return mInstTiles;
            }
        }
        public int TrackLength
        {
            get
            {
                if (mInstTiles.Count == 0)
                {
                    SearchTiles();
                }
                return mInstTiles.Count * TILE_SIZE;
            }
        }
        public int TileCount
        {
            get
            {
                if(mInstTiles.Count == 0)
                {
                    SearchTiles();
                }
                return mInstTiles.Count;
            }
        }
        public void DisableTiles()
        {
            foreach(var tile in InstTileList)
            {
                tile.gameObject.SetActive(false);
            }
        }
        private int mCurrentTileCount = 0;

        [Button]
        public void SearchTiles()
        {
            var children = this.transform.GetComponentsInChildren<Map.CTile>();
            mInstTiles.Clear();
            mInstTiles.AddRange(children);
            mCurrentTileCount = mInstTiles.Count;
        }

        public void CheckTileDestroy()
        {
            mCurrentTileCount--;
            if(mCurrentTileCount == 0)
            {
                Destroy(this.gameObject);
            }
        }

#if UNITY_EDITOR

        public static GUIStyle LabelStyleTrackType = null;
        private void OnDrawGizmos()
        {
            if(LabelStyleTrackType == null)
            {
                LabelStyleTrackType = new GUIStyle();
                LabelStyleTrackType.fontSize = 40;
            }
            UnityEditor.Handles.Label(this.transform.position, Type.ToString(),LabelStyleTrackType);
        }
#endif

    }
}
