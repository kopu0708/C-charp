# 챕터 10 — 배열과 컬렉션 그리고 인덱서

> 게임 개발과 알고리즘 문제 풀이를 조금 해본 경험상 상당히 중요한 챕터다.

---

## 📚 목차

- [배열](#배열)
  - [배열 초기화](#배열을-초기화-하는-방법)
  - [System.Array](#systemarray)
  - [배열 분할하기](#배열-분할하기)
- [2차원 배열](#2차원-배열)
- [다차원 배열](#다차원-배열)
- [가변 배열](#가변-배열)
- [컬렉션](#컬렉션)
  - [ArrayList](#arraylist)
  - [ArrayList 대신 List](#arraylist-대신-list)
  - [Queue](#queue)
  - [Stack](#stack)
  - [Hashtable](#hashtable)
  - [컬렉션 초기화](#컬렉션을-초기화하는-방법)
- [인덱서](#인덱서)
- [foreach가 가능한 객체 만들기](#foreach가-가능한-객체-만들기)
  - [IEnumerable](#ienumerable)
  - [IEnumerator](#ienumerator)
  - [직접 구현 vs yield](#직접-구현-vs-yield)

---

## 배열

프로그램을 작성하다 보면 같은 성격을 띤 다수의 데이터를 한 번에 다뤄야 하는 경우가 자주 생긴다. 학원 원생 100명의 점수를 저장한다고 하면 변수를 100개 만들 수는 없지 않는가. 그래서 배열이다.

```csharp
데이터_형식[] 배열_이름 = new 데이터_형식[용량];
```

용량을 5라고 쓰면 0부터 4까지의 숫자가 붙은 5개의 상자가 생긴다고 이해하면 직관적이다.

- 마지막 요소: `배열[배열.Length - 1]` 또는 `배열[^1]`
- `^3` → 뒤에서 3번째 / `^0` → 배열 길이와 같아서 범위 초과

> 예제: [예제01](Array.cs)

### 배열을 초기화 하는 방법

```csharp
string[] array1 = new string[3] {"안녕", "반가워", "요"}; // 크기와 초기자 명시
string[] array2 = new string[] {"안녕", "반가워", "요"};  // 크기 생략
string[] array3 = {"안녕", "반가워", "요"};               // 전부 생략
```

### System.Array

C#은 모든 것이 객체이다. 배열도 객체이고 `System.Array` 클래스에 대응된다.

```
Sort(), BinarySearch<T>(), IndexOf(), TrueForAll<T>() ...
```

`<T>`는 형식 매개변수. 자세한 내용은 일반화 프로그래밍에서 다룬다.

> 예제: [예제02](02_MoreOnArray.cs)

### 배열 분할하기

`..` 연산자를 이용한다. 왼쪽은 시작 인덱스, 오른쪽은 마지막 인덱스.

```csharp
System.Range r1 = 0..3;
int[] sliced  = scores[r1];    // 0, 1, 2 인덱스만 — 마지막 인덱스(3)는 제외
int[] sliced2 = scores[0..3]; // 더 간결한 방식
```

> ⚠️ 마지막 인덱스는 항상 제외된다. 반개구간 표기법 `[0, 3)`과 같은 설계이다.

> 예제: [예제03](03_slice.cs)

---

## 2차원 배열

타일맵, 그리드 맵 등 행과 열이 필요한 곳에 자주 쓰인다.

```csharp
데이터_형식[,] 배열이름 = new 데이터_형식[행_수, 열_수];
```

```csharp
int[,] arr  = new int[2,3] {{1,2,3},{4,5,6}}; // 형식과 길이 명시
int[,] arr2 = new int[,] {{1,2,3},{4,5,6}};   // 길이 생략
int[,] arr3 = {{1,2,3},{4,5,6}};              // 전부 생략
```

접근은 `arr[행, 열]` 형식이다.

> 예제: [예제04](04_2DArray.cs)

---

## 다차원 배열

차원이 둘 이상인 배열. 차원이 늘어날수록 인덱스 수가 늘어난다.

```csharp
int[,,] array = new int[4,3,2]; // 3차원 배열, 명시적으로 크기 지정 권장
```

> 3중 for문을 넘어가기 시작하면 잘못된 코드라는 신호다. 유지보수가 어려워지고 대부분의 경우 데이터 구조 설계를 다시 해야 한다.

> 예제: [예제05](05_3DArray.cs)

---

## 가변 배열

다차원 배열과 달리 **행마다 열 수를 다르게** 줄 수 있다.

| | 선언 | 특징 |
|--|------|------|
| 다차원 배열 | `int[,]` | 모든 행의 열 수가 같음 |
| 가변 배열 | `int[][]` | 행마다 열 수가 달라도 됨 |

```csharp
int[][] jagged = new int[3][];
jagged[0] = new int[5] {1,2,3,4,5};
jagged[1] = new int[] {10,20,30};
jagged[2] = new int[] {100,200};

// 접근 방식도 다르다
jagged[0][1]  // 가변 배열 — [][] 두 번
arr[0, 1]     // 다차원 배열 — [,] 콤마
```

> 예제: [예제06](06_JaggedArray.cs)

---

## 컬렉션

같은 성격을 띤 데이터의 모음을 담는 자료구조. 배열도 컬렉션의 일부다. .NET이 제공하는 컬렉션들은 `ICollection` 인터페이스를 상속하고 있기 때문이다.

### ArrayList

배열과 가장 닮은 컬렉션. 크기를 미리 정하지 않아도 자동으로 늘고 줄어든다.

```csharp
ArrayList list = new ArrayList();
list.Add(10);       // 마지막에 추가
list.Add(20);
list.Add(30);
list.RemoveAt(1);   // 20 삭제
list.Insert(1, 25); // 1번 인덱스에 25 삽입 → 10, 25, 30
```

> 사실 이건 좀 낡은 방식이다.

### ArrayList 대신 List<>

`ArrayList`는 선언할 때 타입을 지정하지 않아서 꺼낼 때마다 형변환이 필요하다. 그러면 박싱/언박싱이 일어나 속도가 저하된다.

> 박싱: 값 형식을 `object`로 포장해 힙에 올리는 것. 언박싱: 다시 값 형식으로 꺼내는 것. 이 과정에서 힙 할당이 발생해 느려진다.

```csharp
// ArrayList — 꺼낼 때 형변환 필요
int n = (int)list[0];

// List<int> — 그냥 꺼내면 됨
int n = list[0];
```

제네릭 자세한 내용은 다음 챕터(일반화 프로그래밍)에서.

> 예제: [예제07](07_List.cs)

### Queue

대기열. 선입선출 — **FIFO (First In First Out)**. 입력은 뒤에서, 출력은 앞에서.

- OS의 CPU 작업 정리, 프린터 출력 순서
- 게임: 몬스터 스폰 순서, 테트리스 블록 순서

```csharp
Queue que = new Queue(); // object형. 제네릭 버전 따로 있음
que.Enqueue(1); // 뒤에 추가
que.Dequeue();  // 앞에서 꺼내고 제거
que.Peek();     // 앞에 있는 요소 확인만 (제거 안 함)

Queue<int> que = new Queue<int>(); // 제네릭 버전 권장
```

> 예제: [예제08](08_UsingQue.cs)

### Stack

큐와 반대. 후입선출 — **LIFO (Last In First Out)**. 나중에 넣은 게 먼저 나온다.

- 게임: 실행 취소(Undo) 기능, 던전 입장/퇴장 기록, 메뉴 화면 이동 히스토리

```csharp
Stack stack = new Stack();
stack.Push(1); // 최상위: 1
stack.Push(2); // 최상위: 2
stack.Push(3); // 최상위: 3

int a = (int)stack.Pop();  // 3 꺼내고 제거. 최상위: 2
int b = (int)stack.Peek(); // 2 확인만. 제거 안 함
```

> 제네릭 버전은 일반화 프로그래밍 때 한번에.

### Hashtable

키-값 쌍으로 데이터를 다룬다. Dictionary의 전신. 사전 같은 친구.

처음 봤을 때 2D 타일맵에서 타일 정보를 저장하기 위해 사용했었다. (좌표가 키, 타일 정보가 값)

```csharp
Hashtable ht = new Hashtable();
ht["book"]  = "책";
ht["cook"]  = "요리사";
ht["tweet"] = "지저귀다";

Console.WriteLine(ht["book"]); // "책"
```

배열과 비슷하지만 인덱스 대신 키 데이터를 그대로 사용한다는 점이 다르다. 키로 자료를 찾기 때문에 탐색 속도가 빠르다. (이를 해싱이라고 하는데 알고리즘이므로 따로 공부 필요)

> 제네릭 버전(`Dictionary<K,V>`)은 일반화 프로그래밍 때.

### 컬렉션을 초기화하는 방법

```csharp
int[] arr = {123, 456, 789};

ArrayList list  = new ArrayList(arr);          // 123, 456, 789
ArrayList list2 = new ArrayList() {11, 22, 33}; // 컬렉션 초기자 직접 사용
Stack stack     = new Stack(arr);              // 789, 456, 123 — LIFO라서 마지막 요소가 최상위
Queue queue     = new Queue(arr);              // 123, 456, 789
```

> Stack과 Queue는 컬렉션 초기자를 이용할 수 없다. `IEnumerable`은 상속하지만 `Add()` 메서드를 구현하지 않았기 때문이다.

**Hashtable 초기화:**

```csharp
Hashtable ht = new Hashtable()
{
    ["하나"] = 1, // ; 가 아니라 , 로 구분
    ["둘"]   = 2,
    ["셋"]   = 3
};

// 또는
Hashtable ht2 = new Hashtable()
{
    {"하나", 1},
    {"둘",   2},
    {"셋",   3}
};
// 똑같으니 편한 걸로
```

> 예제: [예제09](09_InitialLizing.cs)

---

## 인덱서

인덱스를 이용해서 객체 내의 데이터에 접근하게 해주는 프로퍼티라고 생각하면 된다. 객체를 배열처럼 사용할 수 있게 해준다.

| | 프로퍼티 | 인덱서 |
|--|---------|--------|
| 접근 방식 | `obj.Name` | `obj[0]` |
| 선언 키워드 | 프로퍼티 이름 | `this[index]` |
| 접근자 | `get` / `set` | `get` / `set` |

내부 동작은 똑같다. 프로퍼티는 이름으로, 인덱서는 인덱스로 접근한다는 점만 다르다.

```csharp
class 이름
{
    한정자 인덱서_형식 this[형식 index]
    {
        get { /* index를 이용하여 내부 데이터 반환 */ }
        set { /* index를 이용하여 내부 데이터 저장 */ }
    }
}
```

인덱스가 범위를 벗어나면 `Array.Resize`로 자동으로 크기를 조정할 수도 있다.

```csharp
set
{
    if (index >= array.Length)  // 범위 초과 시 자동 리사이즈
    {
        Array.Resize<int>(ref array, index + 1);
    }
    array[index] = value;
}
```

> 예제: [예제10](10_Inventory.cs)

---

## foreach가 가능한 객체 만들기

`foreach`는 아무 객체에서나 쓸 수 있는 게 아니다. 기존 컬렉션(배열, List 등)에서 되는 이유는 `IEnumerable`을 이미 구현해놨기 때문이다.

직접 만든 클래스에서 `foreach`를 쓰려면 `IEnumerable`을 상속하고 `GetEnumerator()`를 구현하면 된다.

> 덕 타이핑: `IEnumerable`을 명시적으로 상속하지 않아도 `GetEnumerator()`만 있으면 `foreach`가 동작한다. 단 실무에서는 명시적으로 상속받는 게 좋다.

### IEnumerable

```csharp
public interface IEnumerable
{
    IEnumerator GetEnumerator(); // 딱 하나
}
```

`IEnumerable` 상속 → `GetEnumerator()` 구현 의무 → `IEnumerator` 반환

### IEnumerator

```csharp
public interface IEnumerator
{
    bool MoveNext();        // 다음 요소로 이동. 끝이면 false, 성공하면 true
    void Reset();           // 첫 번째 위치의 '앞'으로 이동 (-1로 리셋)
    object Current { get; } // 현재 요소 반환
}
```

`foreach`가 내부적으로 동작하는 방식:

```csharp
// foreach (int i in obj) 의 실제 동작
IEnumerator enumerator = obj.GetEnumerator();
while (enumerator.MoveNext())
{
    int i = (int)enumerator.Current;
    Console.WriteLine(i);
}
```

> `position = -1`로 시작하는 이유: `foreach`는 요소를 꺼내기 전에 항상 `MoveNext()`를 먼저 호출한다. 첫 호출 시 `-1`에서 `0`으로 이동해 첫 번째 요소에 접근하게 된다.

### 직접 구현 vs yield

| | yield 방식 | 직접 구현 |
|--|-----------|-----------|
| `IEnumerator` 구현 | 컴파일러가 자동으로 | 직접 작성 |
| 코드 길이 | 짧음 | 길음 |
| `MoveNext()`, `Current` | 자동 생성 | 직접 작성 |

**yield 방식 — 짧고 간결:**

```csharp
class MyCollection : IEnumerable
{
    int[] numbers = { 1, 2, 3, 4 };

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            yield return numbers[i]; // 하나씩 반환, 일시 정지 후 복구
        }
        // yield break; — GetEnumerator() 즉시 종료. 이후 yield return은 실행 안 됨
    }
}
```

**직접 구현 — 원리 파악용:**

```csharp
class MyList : IEnumerable, IEnumerator
{
    int position = -1; // -1: 아직 시작 전

    public object Current  { get { return array[position]; } }
    public bool MoveNext() { position++; return position < array.Length; }
    public void Reset()    { position = -1; }
    public IEnumerator GetEnumerator() { return this; }
}
```

> 실무에서는 거의 항상 `yield`를 쓴다. 직접 구현은 `foreach`의 내부 동작 원리를 이해하기 위한 것.

> 예제: [예제11](11_Yield.cs) | [예제12](12_Enumerable.cs) | [예제13](13_Enumerable.cs)
