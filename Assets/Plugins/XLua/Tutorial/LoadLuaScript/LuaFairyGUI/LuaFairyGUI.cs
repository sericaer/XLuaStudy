/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using XLua;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;
using FairyGUI;

namespace Tutorial
{
    public class LuaFairyGUI : MonoBehaviour
    {
        LuaEnv luaenv = null;
        // Use this for initialization
        void Start()
        {
            Debug.Log("LuaFairyGUI Start");

            //UIPackage.SetDefaultLoadFunc((string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
            //{
            //    destroyMethod = DestroyMethod.Destroy;

            //    var path = Application.streamingAssetsPath  + "/" +  name + extension;

            //    if (!File.Exists(path))
            //    {
            //        Debug.LogWarning("Can not find file " + path);
            //        return null;
            //    }

            //    var text = System.IO.File.ReadAllText(path);

            //    TextAsset textAsset = new TextAsset(text);
            //    return textAsset;
            //});

            luaenv = new LuaEnv();

            luaenv.AddLoader((ref string filename) =>
            {
                filename = $"{Application.streamingAssetsPath}/{filename}.lua.txt";

                return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(filename));
            });

            luaenv.DoString("require 'lua_fairy_gui'");
        }

        // Update is called once per frame
        void Update()
        {
            if (luaenv != null)
            {
                luaenv.Tick();
            }
        }

        void OnDestroy()
        {
            luaenv.Dispose();
        }
    }
}
