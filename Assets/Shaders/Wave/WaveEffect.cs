using UnityEngine;
using UnityEngine.Rendering;

public static class WaveEffect {

    private static float time = 2f;
    private static Vector2 position;

    public static void Render(CommandBuffer commandBuffer, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material waveMaterial) {
        waveMaterial.SetFloat("_MyTime", time);
        waveMaterial.SetVector("_Pos", position);
        commandBuffer.Blit(source, destination, waveMaterial);
    }

    public static void Update() {
        if (time < 2f) {
            time += Time.deltaTime;
        }
    }

    public static void Play(Vector2 position) {
        WaveEffect.position = Camera.main.WorldToViewportPoint(position);
        time = 0;
    }

}