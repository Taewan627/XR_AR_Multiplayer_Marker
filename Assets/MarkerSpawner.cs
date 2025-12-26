using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.Netcode;

public class MarkerSpawner : MonoBehaviour
{
    public GameObject modelPrefab;

    // 씬에 있는 벌을 기억할 변수
    private NetworkObject spawnedBee;

    // 벌이 정면을 보게 만드는 보정값 (필요하면 숫자 조절)
    private Quaternion offsetRotation = Quaternion.Euler(-90f, 180f, 0f);

    void Start()
    {
        var manager = GetComponent<ARTrackedImageManager>();
        if (manager != null) manager.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        // 1. 벌 생성 (오직 서버만 할 수 있음)
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            foreach (var newImage in args.added)
            {
                if (spawnedBee == null)
                {
                    GameObject go = Instantiate(modelPrefab, newImage.transform.position, newImage.transform.rotation * offsetRotation);
                    spawnedBee = go.GetComponent<NetworkObject>();
                    spawnedBee.Spawn(true); // 네트워크에 소환!
                }
            }
        }

        // 2. 위치 업데이트 (서버든 클라든 '내 눈앞의 마커' 위치로 벌을 강제 이동)
        // 핵심: NetworkTransform의 위치 동기화를 껐기 때문에, 여기서 각자 움직여줘야 함!

        // 아직 벌 변수가 비어있다면, 씬에서 찾아본다 (클라이언트용)
        if (spawnedBee == null && NetworkManager.Singleton != null)
        {
            // 태그나 컴포넌트로 찾기
            var found = FindFirstObjectByType<ModelChanger>();
            if (found != null) spawnedBee = found.GetComponent<NetworkObject>();
        }

        // 벌을 찾았으면, 마커 위치로 이동시킨다
        if (spawnedBee != null)
        {
            foreach (var updatedImage in args.updated)
            {
                if (updatedImage.trackingState == TrackingState.Tracking)
                {
                    // 내 폰 카메라가 보고 있는 마커 위치로 벌을 순간이동
                    spawnedBee.transform.position = updatedImage.transform.position;
                    spawnedBee.transform.rotation = updatedImage.transform.rotation * offsetRotation;
                }
            }
        }
    }
}