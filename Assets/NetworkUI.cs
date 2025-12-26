using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkUI : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;
    public Button changeColorButton;
    public Button rotateButton; // 연결 안 해도 무방
    public TMP_InputField ipInputField;

    void Start()
    {
        // 람다식을 사용하여 버튼 연결 간소화
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        clientButton.onClick.AddListener(StartClient);
        changeColorButton.onClick.AddListener(ChangeColor);
    }

    void StartClient()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null && !string.IsNullOrWhiteSpace(ipInputField.text))
        {
            transport.ConnectionData.Address = ipInputField.text.Trim();
        }
        NetworkManager.Singleton.StartClient();
    }

    void ChangeColor()
    {
        // UnityEngine.Object라고 명시하여 에러 해결
        // 씬에 있는 모든 벌을 찾아 색상 변경 요청 (가장 확실한 방법)
        var changers = UnityEngine.Object.FindObjectsByType<ModelChanger>(FindObjectsSortMode.None);
        foreach (var changer in changers)
        {
            changer.ChangeColorServerRpc();
        }
    }
}