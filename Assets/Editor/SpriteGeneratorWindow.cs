// Поместите этот файл в папку Assets/Editor, например Assets/Editor/SpriteGeneratorWindow.cs
using UnityEngine;
using UnityEditor;
using System.IO;

public class SpriteGeneratorWindow : EditorWindow
{
    GameObject prefab;
    int textureSize = 512;

    [MenuItem("Window/Sprite Generator")]
    static void OpenWindow()
    {
        GetWindow<SpriteGeneratorWindow>("Sprite Generator");
    }

    void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        textureSize = EditorGUILayout.IntField("Resolution", textureSize);

        if (GUILayout.Button("Generate Sprite") && prefab != null)
            GenerateSprite();
    }

    void GenerateSprite()
    {
        // 1) Создаём временный экземпляр префаба и камеру
        var tempGO = Instantiate(prefab);
        tempGO.transform.position = Vector3.zero;
        var camGO = new GameObject("TempCamera");
        var cam = camGO.AddComponent<Camera>();
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.orthographic = true;

        // 2) Рассчитываем границы модели, чтобы камера охватила всю область
        Bounds bounds = CalculateBounds(tempGO);
        cam.orthographicSize = Mathf.Max(bounds.extents.x, bounds.extents.y);
        cam.transform.position = bounds.center + Vector3.back * 10;
        cam.transform.LookAt(bounds.center);

        // 3) Рендерим в RenderTexture
        var rt = new RenderTexture(textureSize, textureSize, 24);
        cam.targetTexture = rt;
        var prev = RenderTexture.active;
        RenderTexture.active = rt;
        cam.Render();

        // 4) Копируем пиксели в Texture2D
        var tex = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
        tex.Apply();

        // 5) Убираем временные объекты
        cam.targetTexture = null;
        RenderTexture.active = prev;
        DestroyImmediate(rt);
        DestroyImmediate(camGO);
        DestroyImmediate(tempGO);

        // 6) Сохраняем PNG и импортируем как Sprite
        string path = $"Assets/{prefab.name}_Sprite.png";
        File.WriteAllBytes(path, tex.EncodeToPNG());
        AssetDatabase.ImportAsset(path);

        var ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.alphaIsTransparency = true;
        ti.SaveAndReimport();

        Debug.Log($"Sprite created: {path}");
    }

    Bounds CalculateBounds(GameObject go)
    {
        var rends = go.GetComponentsInChildren<Renderer>();
        var b = rends[0].bounds;
        foreach (var r in rends) b.Encapsulate(r.bounds);
        return b;
    }
}
