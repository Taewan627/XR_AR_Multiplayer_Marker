# XR AR Multiplayer Marker Project

---

## [EN] Overview
This project demonstrates a **marker-based AR application** with **real-time state synchronization** between multiple mobile devices using **Unity AR Foundation** and **Netcode for GameObjects**.

## [KR] 개요
본 프로젝트는 **Unity AR Foundation**과 **Netcode for GameObjects**를 활용하여  
**이미지 마커 기반 AR 환경**에서 **다중 모바일 디바이스 간 상태(색상) 동기화**를 구현한 프로젝트이다.

---

## [EN] Key Features
- Image marker tracking using ARTrackedImageManager  
- Server-authoritative network object spawning  
- Real-time color synchronization via NetworkVariable  
- Local marker-based placement per device (no NetworkTransform)  
- Verified on two physical mobile devices  

## [KR] 주요 기능
- ARTrackedImageManager를 이용한 이미지 마커 인식  
- 서버 권한 기반 네트워크 오브젝트 생성  
- NetworkVariable을 통한 색상 상태 실시간 동기화  
- NetworkTransform 없이 디바이스별 로컬 마커 기준 배치  
- 실제 모바일 기기 2대 테스트 완료  

---

## [EN] Tech Stack
- Unity  
- AR Foundation (ARCore)  
- Netcode for GameObjects  
- Android  

## [KR] 사용 기술
- Unity  
- AR Foundation (ARCore)  
- Netcode for GameObjects  
- Android  

---

## [EN] Test Scenario
1. Device A starts as Host  
2. Device B connects as Client  
3. Both devices detect the same image marker  
4. Press the "Change Color" button on either device  
5. The color changes simultaneously on both devices  

## [KR] 테스트 시나리오
1. 디바이스 A에서 Host 실행  
2. 디바이스 B에서 Client 접속  
3. 두 디바이스 모두 동일한 이미지 마커 인식  
4. 어느 한 쪽에서든 "Change Color" 버튼 클릭  
5. 두 디바이스에서 동시에 색상 변경 확인  

---

## [EN] Design Notes
- Object position is handled locally based on AR marker tracking  
- Only object state (color) is synchronized over the network  
- NetworkTransform is intentionally not used to avoid AR coordinate conflicts  

## [KR] 설계 포인트
- 오브젝트 위치는 각 디바이스의 AR 마커 트래킹을 기준으로 로컬 처리  
- 색상과 같은 상태 정보만 네트워크를 통해 동기화  
- AR 좌표 충돌을 방지하기 위해 NetworkTransform을 의도적으로 미사용  

---

## [EN] Project Purpose
This project was created as part of an XR course assignment to explore  
the integration of AR interaction and multiplayer state synchronization.

## [KR] 프로젝트 목적
본 프로젝트는 개인연구로 제작되었으며,  
AR 상호작용과 멀티플레이 상태 동기화 구조를 실험하는 것을 목표로 한다.

---

## [EN] Notes
- Unity-generated folders such as Library and Temp are excluded via .gitignore  
- This repository contains only essential project files  

## [KR] 참고 사항
- Library, Temp 등 Unity 자동 생성 폴더는 .gitignore로 제외됨  
- 필수 프로젝트 파일만 저장소에 포함됨  
