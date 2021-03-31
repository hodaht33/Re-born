# Re;born



## Coding Convention

- 똑같은 코드가 반복되는 것을 지양 - Method를 사용
- 사용하지 않는 변수 / 함수는 즉시 삭제
- 사용하지 않는 변수 / 함수 주석처리 금지
  - 향후 다른 개발자가 참여할 때 코드를 지워도 되는지 알 수 없으므로 쓸데없는 코드가 늘어남.
  - 꼭 주석처리를 해 둬야만 하는 이유가 있었다면 그 이유를 주석으로 함께 기재
- 클래스간 상호 종속성  최소화 / 캡슐화
  - 클래스간 관계가 최대한 Tree구조로 되도록 참조 관계를 최소화할 것.
- 클래스에서 아래와 같은 상황을 지양
  - 참조 관계가 A->B, B->A 와 같이 상호 참조 발생
  - A->B, B->C, C->A 등과 같은 루프 구조 발생
  - 이런 상황이 발생하면 한 클래스에서 에러 발생 시 여러 개를 동시에 건드려야 함. 디버깅이 힘들어짐.
- 전역변수는 최대한 지양
- 새로 변수 작성 시 Hungarian notation(변수 명에 접두사 붙이는 것) 지양할 것
  - Unity 자체의 convention에서 사용하지 않음,
  - 또한 Inspector에서 변수 이름을 자동으로 capitalize하므로 어색함.
  - Notation이 필요할 만큼 코드가 길어지면 안 됨
  - 차차 바꿀 예정이므로 굳이 기존의 변수 명을 바꿀 필요는 없음
- Don't Destroy Object (Global object) 사용 지양
  - 디버깅이 매우 힘들어짐 (e.g. 처음 Scene부터 시작하지 않으면 `SoundManager`가 없어 오류 발생)
  - 따라서 지금 구현되어있지 않은 Save / Load 기능 구현이 매우 힘들어질것임.
  - 기존에 구현된 것 역시 전역 오브젝트 대신 prefab을 사용하도록 수정할 예정
- 함수는 최대한 Stateless하게 구현
- 단일 오브젝트의 비동기 구현을 위한 `Coroutine` 지양
  - 가능하면 `Update` 함수를 사용한 State-machine형태로 구현할 것.
  - Unity는 single thread로 동작하므로 `Coroutine`을 사용해도 속도 향상을 얻을 수 없음
  - `Coroutine`을 사용하면 전역 변수 사용이 증가하게 됨.
  - 두 개 이상의 `Coroutine`이 동시에 작동할 때 예측이 힘들어짐.

## Git Convention

- 커밋은 적절한 구현 / 수정 단위로 할 것. 한 커밋 내에서 너무 많은 소스를 건드리는 것 지양.
  - 한 커밋에 너무 많은 내용이 들어가면 나중에 문제 발견 시 언제 발생했는지 알기 어려움.
  - 디렉토리 이름을 바꾼다거나 하면 어쩔 수 없이 대량의 변경사항이 발생함. 그러므로 디렉토리 이름 변경 등은 단일 커밋으로 처리할 것. 디렉토리 이름을 변경함과 동시에 소스코드를 수정하거나 하는 일을 지양할 것.
- 커밋 메시지는 이 커밋에서 어떤 변경을 수행했는지 분명하게 작성할 것.
  - 한글로 작성하거나,
  - 영어로 작성할 시 다음 `~~ `에 들어갈 내용을 첫 글자는 대문자로 하여 작성할 것.
    - This commit will `~~`
    - 올바른 예시
      - Add character moving script
      - Extract asdf function to file
    - 잘못된 예시
      - <u>a</u>dd character moving script
      - Extract<u>s</u> asdf function to file
- 마스터에 바로 푸시/풀 하는 대신 브랜치를 적극 사용할 것.
  - 브랜치 이름은 `유저 이름/브랜치 목적`으로 할 것.
  - 예시
    - `unknownpgr/refactoring`
    - `someuser/level-add`
    - `ohteruser/hotfix`

