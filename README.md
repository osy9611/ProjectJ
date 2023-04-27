# Project-J
> KoyocoGames에서 작업했던 코드 및 모듈을 개량하여 만든 프로젝트 입니다. 
 + 사용기술
    + Unity3D(Addressable,SpriteAtlas,URP)
    + C#
    + protobuf-net
    + Docker
       +Jenkins
 + 작업 내용
    + Editor/Module
      + 에디터 상에서 이동 경로를 설정할 수 있는 PathAgent 구현
      + 에디터 상에서 UI, 파티클, 발사 피봇을 설정할 수 있는 Pivot Agent 구현
      + 기획 Table, Enum Generator 구현
         + Table, Enum 데이터를 xml로 저장 후 에디터 상에서 자동으로 코드를 만드는 Generator를 구현
         + Protobuf-net을 이용해 Table 데이터를 직렬화 후 .byte 파일로 저장
      + SpriteAtlas에 들어있는 Sprite를 관리하는 SpriteAtlasManager 구현 
      + Addressable을 이용한 ResourceManager 구현
         + Adderssable로 로딩한 데이터를 PoolManager를 이용한 풀링기능 구현 
         + Addressable을 이용한 씬로딩 구현
         + Addressable의 AsyncOperationHandle 관리를 통해 ReferenceCount 증가를 관리
    + 콘텐츠
      + DayNight 모듈 구현
         + 시간에 따른 UnityEvent 호출 구현
         + URP Shader를 이용한 SkyBox 및 태양, 달 구현
      + FMS을 이용한 Player,Monster 구현
      + InputSystem을 이용한 PC/Mobile 크로스 플랫폼 Controller 구현
      + Monster, Player Skill,Buff System 구현
      + Monster, Player Status System 구현
    + CD/CI
       + Jenkins용 docker yml 파일 제작
       + Jenkins Unity3D 플러그인을 설치해 Unity용 빌드 구축
       + PreBuild.bat 파일을 작성해 GitHub에서 미리 프로젝트를 pull 받은 후 빌드를 진행할 수 있도록 구현

     
 
