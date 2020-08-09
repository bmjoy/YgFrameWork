﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AppState
{
    None,
    IsPlaying,
    Exiting,
}
public class AppConst 
{
    public static bool DebugMode = true;                        //调试模式-用于内部测试
    public static bool LogMode = true;                          //日志模式
    public static bool LuaByteMode = false;                     //Lua字节码模式-默认关闭 
    public static AppState AppState = AppState.None;            //APP的状态

    public static int GameFrameRate = 30;                       //帧频数
    public const uint BatchProcCount = 5;                       //ZIP批处理次数
    public const uint TotalCreateMapBatch = 2;                  //创建地图总批次
    public const int TimerInterval = 1;
    public const int NetMessagePoolMax = 100;

    public const int DefaultSortLayer = 0;                      //默认层渲染排序值
    public const int MapTileSortLayer = 0;                      //地图层渲染排序值
    public const int TilemapSortLayer = 1;
    public const int RoleSortLayer = 3;                         //角色层渲染排序值
    public const int BattleTempSortingOrder = 100;              //战斗时临时排序

    public const string AppName = "FirSango";                   //应用程序名称
    public const string AppPrefix = AppName + "_";              //应用程序前缀
    public const string ExtName = ".unity3d";                   //素材扩展名
    public const string LuaTempDir = "LuaTemp/";                //临时目录
    public const string ABDir = "Assets/StreamingAssets/res";
    public const string ResIndexFile = "res";
    public const string GameSettingPath = "GameSettings";       //游戏设置
    public const string ResUrl = "https://fishbird-1251004655.cos.ap-shanhai.myqcloud.com/firsango/";
    public const string PatchUrl = ResUrl + "patchs/";           //测试更新地址

    public const ushort SocketPort = 15940;                     //Socket服务器端口
    public const string SocketAddress = "39.98.224.112";        //Socket服务器地址

    public static string TablePath = Application.dataPath + "/res/Tables/";
    public static string[] DataPrefixs = { "datas_", "scripts_", "tables_" };
    public static string[] AssetPaths = { "/res/Datas/", "/res/Tables/", "/StreamingAssets/res/" };

    public static readonly WaitForSeconds WaitForSeconds_01 = new WaitForSeconds(0.1f);
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
}
