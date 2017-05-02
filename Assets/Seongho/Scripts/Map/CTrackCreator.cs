﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Map
{
    public enum TrackType
    {
        NONE = 0,
        END,
        EMPTY,
        A, B, C, D, E, F, G,
        H, I, J, K, L, M, N,
        O, P, Q, R, S, T, U,
        V, W, X, Y, Z,
    }

    public class CTrackCreator
    {

        private int mTileCount = 70;
        public int TileCount
        {
            set
            {
                mTileCount = value;
            }
            get
            {
                return mTileCount;
            }
        }
        private int mSight = 6;


        private List<TrackType> mTrackData = new List<TrackType>();
        private Dictionary<TrackType, CTrack> CurrentPFTracks
        {
            get
            {
                return PFTrackList[CurrentPFTrackIndex];
            }
        }
        private int CurrentPFTrackIndex = 0;
        private List<Dictionary<TrackType, CTrack>> PFTrackList = new List<Dictionary<TrackType, CTrack>>();

        private List<CTile> mInstTileList = new List<CTile>();
        private Queue<CTile> mShowTileQueue = new Queue<CTile>();

        private Transform mParent = null;
        private int mCurrentPivot = -1;

        public float TrackProgress
        {
            get
            {
                if (mCurrentPivot >= 0)
                {
                    return (float)mCurrentPivot / mInstTileList.Count;
                }
                return 0.0f;
            }
        }

        private System.Action mOnShowEndTrack = null;

        public CTrackCreator(Transform tParent)
        {
            mParent = tParent;
            PFTrackList.Add(LoadThemePFTrack("Tracks/Theme1"));
            PFTrackList.Add(LoadThemePFTrack("Tracks/Theme2"));
            PFTrackList.Add(LoadThemePFTrack("Tracks/Theme3"));
            PFTrackList.Add(LoadThemePFTrack("Tracks/Theme4"));
        }
        private Dictionary<TrackType, CTrack> LoadThemePFTrack(string path)
        {
            Dictionary<TrackType, CTrack> storage = new Dictionary<TrackType, CTrack>();
            var prefabs = Resources.LoadAll<CTrack>(path);
            foreach (var track in prefabs)
            {
                if (storage.ContainsKey(track.Type) == false)
                {
                    storage.Add(track.Type, track);
                    //Debug.Log(string.Format("Load Prefab {0}", track.Type));
                }
            }
            return storage;
        }

        public void CreateTrackData()
        {
            //TrackType[] types = (TrackType[])Enum.GetValues(typeof(TrackType));

            List<TrackType> tTypes = new List<TrackType>();
            tTypes.Add(TrackType.E);

            mTrackData.Capacity = mTileCount;
            mTrackData.Clear();

            bool tIsStartRoad = false;
            bool tIsEndRoad = false;

            for (int i = 0; i < mTileCount - 1;)
            {
                TrackType addTrack = tTypes[UnityEngine.Random.Range(0, tTypes.Count)];
                mTrackData.Add(addTrack);

                if (CurrentPFTracks.ContainsKey(addTrack))
                {
                    i += CurrentPFTracks[addTrack].TileCount;
                }
                else
                {
                    i++;
                }

                if (tIsStartRoad == false && i >= 5)
                {
                    tIsStartRoad = true;

                    for (int tTrackValue = (int)TrackType.A; tTrackValue < (int)TrackType.Z; tTrackValue++)
                    {
                        if(CurrentPFTracks.ContainsKey((TrackType)tTrackValue))
                        {
                            tTypes.Add((TrackType)tTrackValue);
                            Debug.Log((TrackType)tTrackValue);
                        }
                    }

                }
                else if (tIsEndRoad == false && i >= 65)
                {
                    tIsEndRoad = true;
                    //tTypes.RemoveRange(1, 4);
                    tTypes.Clear();
                    tTypes.Add(TrackType.EMPTY);
                }
            }
            mTrackData.Add(TrackType.END);
            for (int i = 0; i < 3; i++)
            {
                mTrackData.Add(TrackType.EMPTY);
            }

        }

        public void PositionTracks()
        {
            Vector3 tPos = Vector3.zero;
            int tIndex = 0;
            foreach(var tTrackType in mTrackData)
            {
                if (CurrentPFTracks.ContainsKey(tTrackType))
                {
                    CTrack tTrack = GameObject.Instantiate<CTrack>(CurrentPFTracks[tTrackType], tPos, Quaternion.identity);
                    tTrack.transform.SetParent(mParent);
                    tPos += Vector3.forward * CurrentPFTracks[tTrackType].TrackLength;


                    tTrack.DisableTiles();
                    mInstTileList.AddRange(tTrack.InstTileList);

                    foreach (var tile in tTrack.InstTileList)
                    {
                        tile.Init(this, tIndex, tTrackType);
                        tIndex++;
                    }

                }
            }
            Debug.Log(mInstTileList.Count);

        }

        public void UpdateTrackTile(int pivot)
        {
            if(mCurrentPivot >= pivot)
            {
                return;
            }
            mCurrentPivot = pivot;

            int start = Mathf.Clamp(pivot - 1, 0, mInstTileList.Count - 1);
            int end = Mathf.Clamp(start + mSight, 0, mInstTileList.Count);

            for (int i = start; i < end; i++)
            {
                if(mShowTileQueue.Contains(mInstTileList[i]) == false)
                {
                    mInstTileList[i].Show();

                    if(mInstTileList[i].GetTrackType() == TrackType.END)
                    {
                        mOnShowEndTrack.SafeInvoke();
                        Debug.Log("Show End Track");
                    }

                    mShowTileQueue.Enqueue(mInstTileList[i]);
                    if(mShowTileQueue.Count > mSight)
                    {
                        mShowTileQueue.Dequeue().Hide();
                    }
                }
                else // 이미 보여지고 있음
                {

                }
            }
        }
        public CTile GetTile(int index)
        {
            CTile tTile = null;

            if (index >= 0 && mInstTileList.Count > index)
            {
                tTile = mInstTileList[index];
            }
            return tTile;
        }

        
    }
}