using Unity.Netcode;
using UnityEngine;

public class ModelChanger : NetworkBehaviour
{
    private Renderer rend;
    // 읽기는 모두 가능, 쓰기는 서버만 가능
    private NetworkVariable<Color> beeColor = new NetworkVariable<Color>(Color.white, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        rend = GetComponentInChildren<Renderer>();
        // 접속 시 현재 색상 적용
        ApplyColor(beeColor.Value);
        // 색상이 변할 때마다 실행될 함수 연결
        beeColor.OnValueChanged += OnColorChanged;
    }

    public override void OnNetworkDespawn()
    {
        beeColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        ApplyColor(newColor);
    }

    private void ApplyColor(Color color)
    {
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        if (rend != null) rend.material.color = color;
    }

    // [중요] RequireOwnership = false가 있어야 클라이언트 폰에서도 버튼이 먹힙니다!
    [ServerRpc(RequireOwnership = false)]
    public void ChangeColorServerRpc()
    {
        // UnityEngine.Random으로 명시하여 에러 해결
        beeColor.Value = UnityEngine.Random.ColorHSV();
    }
}