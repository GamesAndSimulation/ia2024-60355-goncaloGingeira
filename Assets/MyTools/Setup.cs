using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;

public static class Setup  {
    
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders() {
        Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "Prefabs", "ScriptableObjects", "Scripts", "Settings");
        Refresh();
    }


    static class Folders {
        public static void CreateDefault(string root, params string[] folders) {
            var fullpath = Path.Combine(Application.dataPath, root);
            foreach(var folder in folders) {
                var path = Combine(fullpath, folder);
                if (!Exists(path)) {
                    CreateDirectory(path);
                }
            }
        }


    }
}
