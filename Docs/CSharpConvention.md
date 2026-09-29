# C# 코드 규칙

다섯 사람이 서로의 코드를 읽어야 하므로, 읽는 사람이 **이름만 보고 역할을 알 수 있는 것**이 목표입니다.

규칙이 많지 않습니다. 아래만 지킵니다.

---

## 1. 이름

| 대상 | 표기 | 예시 |
|---|---|---|
| 클래스 | PascalCase | `MonsterVision` |
| 메서드 | PascalCase | `DetectPlayer()` |
| public 필드·프로퍼티 | PascalCase | `public int Hp` |
| private 필드 | camelCase | `private float moveSpeed` |
| 지역 변수·매개변수 | camelCase | `float distance` |
| 상수 | PascalCase | `private const float MaxHp = 100f` |

- 영어로 씁니다.
- **줄임말을 만들지 않습니다.** `mv`, `plyCtrl` 같은 이름은 본인만 압니다.
- bool은 `is`, `has`, `can` 으로 시작하면 읽기 좋습니다. `isDead`, `hasKey`, `canMove`

## 2. 필드는 private, 인스펙터는 SerializeField

```csharp
[SerializeField] private float moveSpeed = 5f;
[SerializeField] private Transform target;

private int currentHp;
```

<mark>인스펙터에서 조정하려고 `public` 으로 열지 않습니다.</mark> `public` 은 다른 스크립트가 마음대로 바꿀 수 있다는 뜻입니다. 인스펙터에만 보이면 되는 것은 `[SerializeField] private` 입니다.

밖에서 값을 읽기만 해야 하면 프로퍼티를 씁니다.

```csharp
[SerializeField] private int maxHp = 100;
private int currentHp;

public int CurrentHp => currentHp;
```

## 3. 클래스 안의 순서

위에서 아래로 이 순서를 지킵니다. 남의 코드를 열었을 때 찾는 것이 늘 같은 자리에 있게 됩니다.

1. 상수
2. `[SerializeField]` 필드
3. private 필드
4. 프로퍼티
5. 이벤트
6. 유니티 라이프사이클 메서드 (`Awake` → `OnEnable` → `Start` → `Update` → `OnDisable` → `OnDestroy`)
7. public 메서드
8. private 메서드

## 4. 값을 코드에 직접 박지 않습니다

```csharp
// 이렇게 하지 않습니다
if (distance < 7.5f) { ... }

// 이렇게 합니다
[SerializeField] private float detectRange = 7.5f;
if (distance < detectRange) { ... }
```

숫자를 코드 안에 직접 쓰면 **조정할 때마다 스크립트를 고치고 다시 컴파일해야 합니다.** 인스펙터에 꺼내 두면 플레이 중에도 값을 바꿔 볼 수 있습니다. 몬스터 시야각, 이동 속도, 체력 같은 값이 전부 여기 해당합니다.

## 5. 유니티에서 자주 틀리는 것

### GetComponent는 Awake에서 한 번만

```csharp
private Rigidbody rb;

private void Awake()
{
	rb = GetComponent<Rigidbody>();
}

private void Update()
{
	rb.velocity = ...;   // 매번 GetComponent 하지 않습니다
}
```

`Update` 안에서 `GetComponent` 를 부르면 매 프레임 찾습니다. `Find` 계열도 마찬가지입니다.

### 태그 비교는 CompareTag

```csharp
if (other.CompareTag("Player")) { ... }      // O
if (other.tag == "Player") { ... }           // X
```

### Update에 무거운 것을 넣지 않습니다

`Update` 는 1초에 수십 번 실행됩니다. 여기서 오브젝트를 찾거나 리스트를 매번 새로 만들면 눈에 띄게 느려집니다.

### 이벤트는 구독했으면 해제합니다

```csharp
private void OnEnable()
{
	PuzzleManager.OnPuzzleCleared += UpdateProgress;
}

private void OnDisable()
{
	PuzzleManager.OnPuzzleCleared -= UpdateProgress;
}
```

<mark>해제하지 않으면 오브젝트가 사라진 뒤에도 호출되어 에러가 납니다.</mark> 씬을 전환할 때 주로 터집니다.

## 6. 메서드

- **한 메서드는 한 가지 일만 합니다.** 이름에 "그리고" 가 들어가야 설명되면 나눌 때입니다.
- 화면 한 눈에 안 들어오면(대략 40줄) 나눕니다.
- 라이프사이클 메서드는 짧게 두고, 실제 작업은 이름 있는 메서드로 빼면 읽기 쉽습니다.

```csharp
private void Update()
{
	HandleInput();
	Move();
	UpdateAnimation();
}
```

## 7. 주석

- **무엇을 하는지가 아니라 왜 그렇게 했는지**를 적습니다. 무엇을 하는지는 코드가 이미 말하고 있습니다.

```csharp
// 체력을 1 줄인다          (필요 없는 주석)
currentHp--;

// 피격 직후 0.5초는 무적. 연속 피격으로 즉사하는 것을 막는다
StartCoroutine(InvincibleRoutine());
```

- 주석 처리한 코드를 남겨 두지 않습니다. 지운 코드는 Git에 남아 있습니다.
- 나중에 손볼 곳은 태그를 답니다.

```csharp
// TODO: 사운드 연결 필요
// FIXME: 경사면에서 이동 속도가 느려짐
```

## 8. 로그

디버그 로그에는 누가 남긴 것인지 붙입니다. 다섯 사람의 로그가 섞이면 구분이 안 됩니다.

```csharp
Debug.Log($"[Monster] 플레이어 감지, 거리 {distance:F1}");
Debug.Log($"[Puzzle] 남은 퍼즐 {remainCount}");
```

<mark>작업이 끝나면 임시 로그는 지웁니다.</mark> 콘솔이 로그로 가득 차면 정작 봐야 할 에러가 묻힙니다.

## 9. 포맷

- 들여쓰기는 탭을 씁니다. 편집기 설정을 팀과 맞춥니다.
- 중괄호는 줄을 바꿔서 엽니다.
- `using` 은 쓰지 않는 것을 지웁니다.

```csharp
private void Move()
{
	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}
```
