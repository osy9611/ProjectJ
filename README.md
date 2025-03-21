# **ProjectJ - Unity Project**

**Project J**는 KoyocoGames의 **Project R.O.A.D** 개발 참여 후, 사용했던 주요 기술을 정리하고,  
데이터 관리, 리소스 최적화, UI 시스템 등을 구현한 프로젝트입니다. 

> **참고**  
> Project J 작업 이후, 개발 과정에서의 문제 해결과 유지보수성을 향상시키기 위해 
> **Project T**라는 개선 프로젝트를 진행하고 있습니다. <br/>
> Project T는 Project J의 부족했던 부분을 보완하여, 더 효율적인 시스템을 구축하기 위한 프로젝트입니다.

---

## **🔗 관련 문서**
- [**GitHub Wiki**](https://github.com/osy9611/ProjectJ/wiki)
- [**Project R.O.A.D 데모 영상**](https://www.youtube.com/watch?v=eXGDh9YU4eA&t=3s)
- [**Project T (개선 프로젝트)**](https://github.com/osy9611/ProjectT)

---

## **주요 시스템 개요**  

### **Design Automation Generator (데이터 자동화 시스템)**
- **XML 기반 데이터 관리 및 자동 코드 생성**  
- **Protobuf-net을 이용한 데이터 직렬화 및 .byte 파일 저장**  
- **클래스 자동 생성으로 유지보수성 향상**  

🔗 **[ 자세히 보기](https://github.com/osy9611/ProjectJ/wiki/TableGenerator)**  

---

### **Resource Manager**
- **Addressable을 활용한 리소스 로딩**  
- **풀링 시스템을 통한 리소스 재사용 기능 추가**  
- **비동기 처리 개선 (코루틴 → UniTask 전환)**  

🔗 **[자세히 보기](https://github.com/osy9611/ProjectJ/wiki/ResourceManager)**  

---

### **UI System**
- **UI 계층 구조화 (Scene UI, Popup UI, Element UI)**  
- **Stack 기반 UI 추적 시스템 도입**  
- **UIManagerSystem을 통한 UI 최적화**  

🔗 **[ 자세히 보기](https://github.com/osy9611/ProjectJ/wiki/UI-System)**  

---

### **Skill & Status 시스템**
- **ActionManager → 스킬 등록 및 실행 관리**  
- **BuffManager → 버프 시스템 관리 (공격력 증가/감소 등)**  
- **StatusAgent → 능력치 계산 및 HP 관리**  

🔗 **[ 자세히 보기](https://github.com/osy9611/ProjectJ/wiki/Skill&Status-System)**  

---

## **Project T로의 확장**
Proejct T는 **Project J에서 사용한 기술을 더욱 개선하여, 확장성과 유지보수성을 고려한 시스템을 구축한 프로젝트입니다.**  
자세한 내용은 아래 링크에서 확인할 수 있습니다.  

🔗 **[ Project T 보러가기](https://github.com/osy9611/ProjectT)**
