# Project-J
> 2020 ~ 2022 년 까지 프로젝트를 진행하며 배웠던 것을 만들어보는 프로젝트입니다.
 + 사용기술
    + Unity3D(Addressable,SpriteAtlas,URP)
    + C#
    + protobuf-net
    + Docker
       +Jenkins
 + 작업 내용
    + Editor/Module
    	+ Path Agent를 구현해 에디터 상에서 이동 경로를 설정할 수 있도록 구현
    	+ Pivot Agent를 구현해 에디터 상에서 UI, 파티클 피봇을 설정할 수 있도록 구현
	+ 기획 Table, Enum Generator 구현
	   + Table,Enum 데이터를 xml로 저장 후 에디터 상에서 코드 자동 Generator를 구현
               + Protobuf-net을 이용해 Table 데이터를 직렬화 후 .byte 파일로 저장
            + SpriteAtlas에 들어있는 Sprite를 관리하는 SpriteAtlasManager 구현 
            + Addressable을 이용한 ResourceManager 구현
               + Adderssable로 로딩한 데이터를 PoolManager를 이용한 풀링기능 구현 
               + Addressable을 이용한 씬로딩 구현
    + 콘텐츠
       + DayNight 모듈 구현
       	+ 시간에 따른 UnityEvent 호출 구현
       	+ URP Shader를 이용한 SkyBox 및 태양,달 구현
       + FMS을 이용한 Player,Monster 구현
       + InputSystem을 이용한 PC/Mobile 크로스 플랫폼 Controller 구현
       + UIManager, HUDManager를 구현해 UIPop, UIScene, UI Sprite 데이터 로딩
       + Monster, Player Skill,Buff System 구현
       + Monster, Player Status System 구현
    + CD/CI
        + Jenkins Unity3d 플러그인을 위한 빌드 배포 구축

     
 