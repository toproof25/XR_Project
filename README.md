# XR UI/UX Programing Final Project

### Meta SDK Team (Student Ddevelopment Know-nothing)

Demo video : [youtube](https://www.youtube.com/watch?v=roQkJPKeeOI)

<br/>


## XR UI/UX 프로그래밍 프로젝트

프로젝트 주제
> 프로젝트 주제인 "공간 디자인"은 미래의 XR시대가 온다면 그리고 XR SNS가 대중화된다면 지금의 SNS에서 자신만의 공간을 디자인 하듯 XR공간에서 자신만의 공간 디자인을 할 것으로 예상합니다. 그런 의미에서 XR환경에서 공간을 디자인하고 다양한 인터페이스를 구성하보고자 시작하였습니다.


프로젝트 개요
> 이 프로젝트는 2024년 1학기 XR UI/UX 프로그래밍 기말 과제로서, VR 및 XR 환경에서 사용자 인터페이스와 상호작용을 구현하는 것을 목표로 합니다. 다양한 기능을 포함한 로비 및 XR 공간을 구현하고, 사용자가 음성 명령과 핸드트래킹을 통해 상호작용할 수 있도록 했습니다. 



<br/>


----


## 로비

- 컨트롤러 텔레포트 이동
- XR, VR씬으로 이동

<img src="https://github.com/toproof25/XR_Project/assets/41888060/9e05783f-f7b7-4d40-916a-d4f36f044233"  width="426" height="240"/>

<br/>
<br/>

## XR씬

- Passthrough
- Controller
- Hand tracking
- 오브젝트 배치 및 편집
- 음성 명령(wit.ai)
- 공간 저장 및 로드

![XR](https://github.com/toproof25/XR_Project/assets/41888060/9da02f0e-c5a7-4395-ae65-55f708efea39)

<br/>
<br/>

## 오브젝트 배치 및 편집

#### UI에서 오브젝트 선택
<img src="https://github.com/toproof25/XR_Project/assets/41888060/79843ccc-6ef0-485c-9681-a2c738ffc536"  width="426" height="240"/>

#### 선택 후 배치

- 왼쪽 조이스틱으로 회전 및 크기 조절
- Controller A key(배치) / B key(취소)
- Hand Tracking 왼손을 쫙 피면 배치 모드 취소(모션 인식)

![배치 - Clipchamp로 제작](https://github.com/toproof25/XR_Project/assets/41888060/6eed0844-4258-479d-a342-8b477b025c1b)

#### 오브젝트 편집

- Grab, Distance Grab Interactable, Two Hand Grab 등 Meta SDK 컴포넌트를 이용하여 이동, 회전, 크기를 조절

![편집](https://github.com/toproof25/XR_Project/assets/41888060/f013c684-ca0d-42c8-a60c-1b2ef2ff4fc2)


<br/>
<br/>

## 음성 명령

- [Wit.ai](https://wit.ai/) 음성 인식
- 마이크 활성화 후 음성 명령 내리기

- 음성 명령 모음
- - Open the menu / option
  - Passthrough on / off
  - Create box / toycar / lamp / camera / microwave oven / flower / table / dresser / chair

![음성인식 - Clipchamp로 제작](https://github.com/toproof25/XR_Project/assets/41888060/8fe67a5f-ed31-45d2-8c01-d9f849011208)


<br/>
<br/>


### 프로젝트 참고 자료
- [Meta SDK 공식 문서](https://developer.oculus.com/documentation/unity/bb-overview/)
- [Black Whale Studio](https://www.youtube.com/@blackwhalestudio)

### 사용한 애셋
- [Sparrow - Quirky Series: Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/animals/sparrow-quirky-series-247228)
- [Furniture FREE: Unity Asset Store](https://assetstore.unity.com/packages/3d/props/furniture/furniture-free-260522)
- [Meta XR All-in-One SDK: Unity Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)


