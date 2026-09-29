## Rider 코드 규칙 자동화

### 사용하는 이유
팀내 그라운드룰로 코딩 컨벤션을 만들고 그것을 준수하는 것은 매우 중요합니다.
그러나 개발 일정 등 피치못할 사정으로 컨벤션을 지키지 못하거나 혹은 아직 컨벤션에 익숙하지
않아서 실수가 발생할 수 있습니다.

이런 것을 막기위해 EsLint, .editorconfig 같은 설정을 통해 단축키 실행 혹은 저장 시
자동으로 코드가 정리되도록 하는 것이 좋습니다.

이 문서에서는 팀 그라운드에 맞춘 .editorconfig 파일을 공유하고 그것을 바탕으로
Cleanup 프로파일 설정 및 자동 적용에 대해 공유합니다.

### `.editorconfig`
Docs 디렉토리에 있는 [`.editorconfig`](https://github.com/swchoi-dev/PracticeTeamProject/blob/prod/Docs/.editorconfig) 파일이 팀의 코딩컨벤션에 맞춘 코드 정리 규칙이 명시되어 있습니다.
이 파일은 원본 파일로 실제로는 프로젝트 경로 `Asset`이 있는 위치에 존재하는 `.editorconfig` 파일이 적용됩니다.
따라서 `.editorconfig` 파일 내 규칙 수정이 필요한 경우 Docs의 원본파일과 프로젝트 경로에 있는 파일을
모두 교체합니다.

### Cleanup 프로파일 생성
Rider에서 코드 정리를 할 때 참고하는 Cleanup 프로파일을 생성합니다. 이 생성한 프로파일이 동작할 때
`.editorconfig`의 규칙을 참고하여 코드 정리를 해줍니다. Cleanup 프로파일은 이 코드정리 규칙이
어떤 파일을 대상으로 어느 범위까지 적용할지를 설정해주는 것이라고 생각하면 됩니다.

#### Cleanup 프로파일 신규 생성
설정 -> 에디터 -> 코드정리
<img width="739" height="454" alt="스크린샷 2026-09-27 오후 5 48 00" src="https://github.com/user-attachments/assets/065c6063-e93c-4e69-b33b-77916c341f26" />
<img width="710" height="276" alt="스크린샷 2026-09-27 오후 6 21 33" src="https://github.com/user-attachments/assets/7121c909-420d-4da8-ae4f-9da8ba3ffaa1" />


#### 대상 및 범위 설정
<img width="520" height="154" alt="스크린샷 2026-09-27 오후 5 49 38" src="https://github.com/user-attachments/assets/0ceec67a-0f21-40d2-8211-88f3b1abbe03" />

---

<img width="521" height="508" alt="스크린샷 2026-09-27 오후 5 49 52" src="https://github.com/user-attachments/assets/837844b6-0c6c-4914-be41-a7bdd2073743" />

---

<img width="516" height="126" alt="스크린샷 2026-09-27 오후 5 50 01" src="https://github.com/user-attachments/assets/a1333e4c-e229-46d0-a3ed-b22f0e553983" />

---

<img width="507" height="71" alt="스크린샷 2026-09-27 오후 5 50 10" src="https://github.com/user-attachments/assets/c111a9d5-2ae6-45a7-9144-a1b6a42c94d5" />

---

### Actions on Save
저장버튼을 누를 때 자동으로 코드정리가 되도록 하는 설정입니다.

설정 -> 도구 -> 저장 시 액션
<img width="852" height="237" alt="스크린샷 2026-09-27 오후 5 52 21" src="https://github.com/user-attachments/assets/2d9703f1-7ad0-47ab-a4db-ef9077917681" />


### 단축키 설정
단축키를 눌러서 자동으로 코드정리가 되도록 하는 설정입니다.

설정 -> 키맵
<img width="999" height="581" alt="스크린샷 2026-09-27 오후 6 19 47" src="https://github.com/user-attachments/assets/6b6af485-2c1d-4e1c-91f9-717356a269c1" />
