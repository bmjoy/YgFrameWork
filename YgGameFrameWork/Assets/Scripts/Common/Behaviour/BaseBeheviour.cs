﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseBeheviour
{
    static Dictionary<string, BaseManager> Managers = new Dictionary<string, BaseManager>();
    static Dictionary<string, BaseObject> ExtManagers = new Dictionary<string, BaseObject>();

    private Canvas _uiCanvas;

    protected Canvas uiCanvas
    {
        get
        {
            if (_uiCanvas == null)
            {
                _uiCanvas = GameObject.Find("/MainGame/UICanvas").GetComponent<Canvas>();
            }
            return _uiCanvas;
        }
    }

    private Camera _battleCamera;
    protected Camera battleCamera
    {
        get
        { 
            if(_battleCamera == null)
            {
                _battleCamera = Camera.main;
            }
            return _battleCamera;
        }
    }

    public T Instantiate<T>(T original) where T : UnityEngine.Object
    {
        return GameObject.Instantiate<T>(original);
    }

    public static void Destroy(UnityEngine.Object obj, float t)
    {
        if(obj != null)
        {
            GameObject.Destroy(obj, t);
        }
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return ManagementCenter.main.StartCoroutine(routine);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public static void Initialize()
    {
        InitManagers();
        InitExtManager();
        InitComponent();
    }
    /// <summary>
    /// 初始化管理器
    /// </summary>
    private static void InitManagers()
    {
        AddManager<UIManager>();
        AddManager<GameManager>();
        AddManager<CSceneManager>();
        AddManager<ResourceManager>();
    }
    /// <summary>
    /// 初始化扩展管理器
    /// </summary>
    private static void InitExtManager()
    {
        ExtManagers.Add("ConfigManager", configMgr);
        ExtManagers.Add("TimerManager", timerMgr);
        ExtManagers.Add("TableManager", tableMgr);
    }
    /// <summary>
    /// 初始化组件
    /// 如摄像机跟随等..
    /// </summary>
    private static void InitComponent()
    {

    }
    
    /// <summary>
    /// 添加管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static T AddManager<T>() where T : BaseManager, new()
    {
        var type = typeof(T);
        var obj = new T();
        Managers.Add(type.Name, obj);

        return obj;
    }
    /// <summary>
    /// 获取管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetManager<T>() where T : class
    {
        var type = typeof(T);
        if(!Managers.ContainsKey(type.Name))
        {
            return null;
        }
        return Managers[type.Name] as T;
    }

    public static BaseManager GetManager(string managerName)
    {
        if(!Managers.ContainsKey(managerName))
        {
            return null;
        }
        return Managers[managerName];
    }
    /// <summary>
    /// 获取扩展管理器
    /// </summary>
    /// <param name="componentName"></param>
    /// <returns></returns>
    public static object GetExtManager(string componentName)
    {
        if (!ExtManagers.ContainsKey(componentName))
        {
            return null;
        }
        return ExtManagers[componentName];
    }

    //===============================游戏管理器==============================

    private static GameManager _gameMgr;
    public static GameManager gameMgr
    {
        get
        {
            if(_gameMgr == null)
            {
                _gameMgr = GetManager<GameManager>();
            }
            return _gameMgr;
        }
    }

    private static CSceneManager _sceneMgr;
    public static CSceneManager sceneMgr
    {
        get
        {
            if(_sceneMgr == null)
            {
                _sceneMgr = GetManager<CSceneManager>();
            }
            return _sceneMgr;
        }
    }
    private static UIManager _uiMgr;
    public static UIManager uiMgr
    {
        get
        {
            if (_uiMgr == null)
            {
                _uiMgr = GetManager<UIManager>();
            }
            return _uiMgr;
        }
    }
    private static ResourceManager _resMgr;
    public static ResourceManager resMgr
    {
        get
        {
            if (_resMgr == null)
            {
                _resMgr = GetManager<ResourceManager>();
            }
            return _resMgr;
        }
    }
    //==========================扩展管理器=============================
    private static ConfigManager _configMgr;
    public static ConfigManager configMgr
    {
        get
        {
            if (_configMgr == null)
            {
                _configMgr = ConfigManager.Create();
            }
            return _configMgr;
        }
    }
    private static CTimer _timerMgr;
    public static CTimer timerMgr
    {
        get
        {
            if (_timerMgr == null)
            {
                _timerMgr = CTimer.Create();
            }
            return _timerMgr;
        }
    }

    private static TableManager _tableMgr;
    public static TableManager tableMgr
    {
        get
        {
            if(_tableMgr == null)
            {
                _tableMgr = TableManager.Create();
            }
            return _tableMgr;
        }
    }


    /// <summary>
    /// 控制器更新
    /// </summary>
    /// <param name="deltaTime"></param>
    public static void OnUpdate(float deltaTime)
    {
        //驱动所有管理器
        foreach(var mgr in Managers)
        {
            if(mgr.Value != null && mgr.Value.isOnUpdate)
            {
                mgr.Value.OnUpdate(deltaTime);
            }
        }
        //驱动所有组件
        foreach(var com in ExtManagers)
        {
            if(com.Value != null && com.Value.isOnUpdate)
            {
                com.Value.OnUpdate(deltaTime);
            }
        }
    }
}
