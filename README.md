### 변경점

* TestScene폴더 내의 씬
  * SettingsMenu
    * 삭제 예정 씬
    * 설정 창 구현 테스트 씬
  * CampusPrototype
    * 캠퍼스 맵을 어떻게 구성할 것인지 미리보기 위한 프로토타입 씬
  * DissolveShaderTestScene 추가
    * Dissolve셰이더 효과를 미리보기 위한 씬
    * ![ezgif.com-gif-maker](C:\Users\dltjd\Desktop\ezgif.com-gif-maker.gif)



* 기존 씬

  * Start

    * 메인화면 이미지의 크기 조절 스크립트를 제거,

      Canvas Scaler컴포넌트의 UI Scale Mode를

      Scale With Screen Size로 변경,

      RectTransform의 Anchor로 채우는 방식으로 변경

    * DontDestroyObjects 추가

      * MenuCanvas

        메뉴 버튼들을 담는 Canvas

      * SettingsCanvas

        설정 창을 담는 Canvas

      * UIManager

        UIManager스크립트를 가지는 빈 오브젝트

      * SoundManager

        SoundManager스크립트를 가지는 오브젝트

        게임 실행 시 BGMPlayer와 SFXPlayer를 가짐

      * BrightnessImageCanvas

        밝기 조절용 이미지를 가지는 Canvas

  * Subway

    * 아직 3D리소스가 없어 진행X

  * classroom

    * 마우스 휠을 이용해 화면 확대/축소 기능 존재
    * 기존에 보이던 긴 강의실 대신 아직 개선되지 않은 강의실로 카메라 이동

  * End

    * 종료 문구 이미지의 크기 조절 스크립트를 제거,

      Canvas Scaler컴포넌트의 UI Scale Mode를

      Scale With Screen Size로 변경,

      RectTransform의 Anchor로 채우는 방식으로 변경

  

* laxer tree pkg폴더

  * CampusPrototype에 사용한 나무 에셋

