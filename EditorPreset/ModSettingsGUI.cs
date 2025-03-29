using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityModManagerNet;

namespace EditorPreset
{
    public class css
    {
        public static GUIStyle Label_title()
        {
            var style = new GUIStyle();
            style.fontSize = 30;
            style.margin = new RectOffset(0, 0, 0, 10);
            style.normal.textColor = Color.white;
            return style;
        }
        public static GUIStyle Label_subtitle()
        {
            var style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 18;
            style.margin = new RectOffset(0, 0, 0, 5);
            return style;
        }
        public static GUIStyle MarginBottomSmall()
        {
            var style = new GUIStyle();
            style.margin = new RectOffset(0, 0, 0, 12);
            return style;
        }
    }

    public class ModSettingsGUI
    {
        static string settingsZoom = Main.setting.cameraZoom.ToString();
        static string settingsPositionX = Main.setting.cameraPosX.ToString();
        static string settingsPositionY = Main.setting.cameraPosY.ToString();
        static string settingsBeatsAhead = Main.setting.trackBeatsAhead.ToString();
        static string settingsBeatsBehind = Main.setting.trackBeatsBehind.ToString();
        static string settingsAuthor = Main.setting.levelAuthor;
        static string settingsVolume = Main.setting.songVolume.ToString();

        public static string CurrentScreen = "MainScreen";
        public static string AnimationType_Caption = "";
        public static void GUI()
        {
            #region 곡 설정
            GUILayout.Label(RDString.Get("editor.SongSettings"), css.Label_title());

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label(RDString.Get("editor.volume"), css.Label_subtitle());
            settingsVolume = GUILayout.TextField(settingsVolume);
            try
            {
                int converted = Convert.ToInt32(settingsVolume);
                if (converted > 100)
                {
                    throw new Exception();
                }
                Main.setting.songVolume = converted;
            }
            catch
            {
                settingsVolume = "0";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            #endregion

            #region 레벨 설정
            GUILayout.Label(RDString.Get("editor.LevelSettings"), css.Label_title());

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label(RDString.Get("editor.publish.by"), css.Label_subtitle());
            settingsAuthor = GUILayout.TextField(settingsAuthor);
            if (!settingsAuthor.IsNullOrEmpty())
            {
                Main.setting.levelAuthor = settingsAuthor;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            #endregion

            #region 카메라 설정
            GUILayout.Label("카메라 설정", css.Label_title());
            GUILayout.Label("위치", css.Label_subtitle());

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label("X");
            settingsPositionX = GUILayout.TextField(settingsPositionX);
            try
            {
                float converted = Convert.ToSingle(settingsPositionX);
                Main.setting.cameraPosX = converted;
            }
            catch
            {
                settingsPositionX = "0";
            }

            GUILayout.Label("Y");
            settingsPositionY = GUILayout.TextField(settingsPositionY);
            try
            {
                float converted = Convert.ToSingle(settingsPositionY);
                Main.setting.cameraPosY = converted;
            }
            catch
            {
                settingsPositionY = "0";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Label("줌", css.Label_subtitle());
            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            settingsZoom = GUILayout.TextField(settingsZoom, 3);
            try
            {
                float converted = Convert.ToSingle(settingsZoom);
                Main.setting.cameraZoom = converted;
            }
            catch
            {
                settingsZoom = "100";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            #endregion

            #region 길 애니메이션
            GUILayout.Label("길 설정", css.Label_title());

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label("길 출현 애니메이션", css.Label_subtitle());
            GUILayout.Label(Main.setting.trackAnimation.ToString());
            if (GUILayout.Button("선택"))
            {
                AnimationType_Caption = "길 출현 애니메이션 선택";
                CurrentScreen = "SelectTrackAnimation";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label("애니메이션 이전 비트", css.Label_subtitle());
            settingsBeatsAhead = GUILayout.TextField(settingsBeatsAhead);
            try
            {
                float converted = Convert.ToSingle(settingsBeatsAhead);
                Main.setting.trackBeatsAhead = converted;
            }
            catch
            {
                settingsBeatsAhead = "0";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label("길 퇴장 애니메이션", css.Label_subtitle());
            GUILayout.Label(Main.setting.trackDisappearAnimation.ToString());
            if (GUILayout.Button("선택"))
            {
                AnimationType_Caption = "길 퇴장 애니메이션 선택";
                CurrentScreen = "SelectTrackAnimationD";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(css.MarginBottomSmall());
            GUILayout.Label("애니메이션 이후 비트", css.Label_subtitle());
            settingsBeatsBehind = GUILayout.TextField(settingsBeatsBehind);
            try
            {
                float converted = Convert.ToSingle(settingsBeatsBehind);
                Main.setting.trackBeatsBehind = converted;
            }
            catch
            {
                settingsBeatsBehind = "0";
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            #endregion
        }

        public static void SelectAnimationType(string caption = "길 애니메이션 선택", string description = "", string TargetScreen = "MainScreen", bool isDisappear = false)
        {
            GUILayout.Label(caption, css.Label_title());

            List<string> TrackAniNames = new List<string>();
            TrackAniNames.AddRange(Enum.GetNames(typeof(TrackAnimationType)));

            foreach (string TrackAniName in TrackAniNames)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(TrackAniName))
                {
                    if (isDisappear)
                    {
                        Main.setting.trackDisappearAnimation = (TrackAnimationType)Enum.Parse(typeof(TrackAnimationType), TrackAniName);
                    }
                    else
                    {
                        Main.setting.trackAnimation = (TrackAnimationType)Enum.Parse(typeof(TrackAnimationType), TrackAniName);
                    }
                    CurrentScreen = TargetScreen;
                    AnimationType_Caption = string.Empty;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}
