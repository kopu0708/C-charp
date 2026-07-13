# 챕터 11 — 일반화 프로그래밍

> 특수한 개념으로부터 공통된 개념을 찾아 묶는 것을 일반화(Generalization)라고 한다. 그 대상은 바로 데이터 형식이다.

---

## 📚 목차

- [일반화 메서드](#일반화-메서드)
- [일반화 클래스](#일반화-클래스)
- [형식 매개변수 제약](#형식-매개변수-제약시키기)
- [일반화 컬렉션](#일반화-컬렉션)
- [foreach와 IEnumerable\<T\>](#foreach는-그냥-쓸-수-없다)

---

## 일반화 메서드

정수형 배열 복사 메서드를 만들었다고 하자.

```csharp
void CopyArray(int[] source, int[] target)
{
    for (int i = 0; i < source.Length; i++)
        target[i] = source[i];
}
```

문자열 배열도 복사해야 해서 오버로딩, 클래스 배열도 해서 또 오버로딩... 이렇게 31번 오버로딩했다. 근데 **복사 로직 자체는 공통**이지 않는가?

이럴 때 형식을 일반화한다.

```csharp
한정자 반환_형식 메소드_이름<형식_매개변수>(매개변수_목록)
{
    // ...
}
```

```csharp
void CopyArray<T>(T[] source, T[] target)  // int[] → T[]
{
    for (int i = 0; i < source.Length; i++)
        target[i] = source[i];
}
```

데이터 형식이 쓰인 부분을 `T`로 치환했다. `T`는 C# 컴파일러가 지원하는 형식이 아니라서, 어떤 형식인지 알려줘야 한다.

```csharp
int[] src = {1, 2, 3};
int[] dst = new int[3];

CopyArray<int>(src, dst);  // 명시적으로 형식 지정
CopyArray(src, dst);       // 컴파일러가 int[]를 보고 T를 자동 추론
```

> `T`는 관례일 뿐 어떤 이름이든 쓸 수 있다. 여러 개면 `<T, U>` 또는 `<TKey, TValue>`처럼 의미 있는 이름을 쓴다.

> 예제: [예제01](01_GenericMethod.cs)

---

## 일반화 클래스

일반화 메서드는 메서드 하나만 일반화하지만, 일반화 클래스는 그 내부의 메서드·필드 모두를 일반화한다. 같은 성질의 클래스를 자료형마다 여러 개 만들 필요가 없어진다.

```csharp
class 클래스_이름<형식_매개변수>
{
    private T[] array;
    public T 메소드_이름() { /* ... */ }
}

Array_Generic<int> intArr = new Array_Generic<int>();
```

> `<T>`로 일반화해도 `foreach`는 자동으로 되지 않는다. `foreach`를 쓰려면 별도로 `IEnumerable<T>`를 구현해야 한다. `List<T>`가 편한 이유는 둘 다 구현해놨기 때문이다.

> 예제: [예제02](02_GenericClass.cs)

---

## 형식 매개변수 제약시키기

`T`는 모든 형식을 대신할 수 있지만, 특정 조건을 갖춘 형식에만 대응하게 하고 싶을 때가 있다.

```csharp
class MyList<T>
{
    public T Max(T a, T b)
    {
        return a > b ? a : b;  // 오류! T가 > 비교를 지원하는지 모름
    }
}
```

`int`, `float`는 괜찮지만 그 외 형식은 `>`를 지원하지 않을 수 있다. 이때 `where` 키워드로 제약을 건다.

```csharp
class 클래스_이름<T> where T : 제약조건
```

**제약조건 목록:**

| 제약 | 의미 |
|------|------|
| `where T : struct` | T는 값 형식만 |
| `where T : class` | T는 참조 형식만 |
| `where T : new()` | T는 매개변수 없는 생성자를 가져야 함 |
| `where T : 클래스이름` | T는 해당 클래스를 상속받아야 함 |
| `where T : 인터페이스이름` | T는 해당 인터페이스를 구현해야 함 |
| `where T : U` | T는 또 다른 형식 매개변수 U로부터 상속받은 클래스여야 함 |

**인터페이스 제약 예시:**

```csharp
class MyList<T> where T : IComparable  // IComparable 구현 형식만
{
    public T Max(T a, T b)
    {
        if (a.CompareTo(b) > 0)  // IComparable이 보장되니 사용 가능
            return a;
        return b;
    }
}

MyList<int>    list1 = new MyList<int>();    // OK
MyList<string> list2 = new MyList<string>(); // OK
```

**new() 제약 — 기본 생성자 보장:**

```csharp
public static T CreateInstance<T>() where T : new()
{
    return new T();  // 기본 생성자가 있는 형식만 생성 가능
}
```

**U 제약 — 다른 형식 매개변수 상속:**

```csharp
class BaseArray<U> where U : Base
{
    public U[] Array { get; set; }
    public BaseArray(int size) { Array = new U[size]; }

    public void CopyArray<T>(T[] Source) where T : U  // T는 U의 자식만
    {
        Source.CopyTo(Array, 0);
    }
}
```

> `BaseArray<U>`는 `Base`를 상속받은 형식만 담을 수 있고, `CopyArray<T>`는 T가 U를 상속받은 형식이어야만 복사할 수 있다.

**여러 제약 동시에:**

```csharp
class MyClass<T> where T : class, IComparable, new()
// T는 참조 형식이면서, IComparable을 구현하고, 기본 생성자도 있어야 함
```

### 실전 예제 — RPG 인벤토리

다형성은 공통 동작을 묶는 것이고, 제약은 특정 형식일 때만 로직을 허용하는 것이다. (제약은 **컴파일 타임**에 검사되어 버그를 미리 잡는다.)

```
예제03 — Inventory<T> 에서 사용한 제약조건 정리
- where T : Item        → 아이템만 인벤토리에 담을 수 있음
- where W : Weapon      → 무기만 강화 가능
- where U : Item, new() → 기본 생성자가 있는 아이템만 생성 가능
```

> 예제: [예제03](03_Constraint_type.cs)

---

## 일반화 컬렉션

10장에서 배운 컬렉션들은 `object` 기반이라 어떤 형식이든 담을 수 있었지만, 그로 인해 박싱/언박싱이 발생해 성능이 저하됐다.

제네릭이 도입되면서 각자 일반화 버전이 생겼다.

| 구버전 (object 기반) | 일반화 버전 |
|---------------------|-------------|
| `ArrayList` | `List<T>` |
| `Queue` | `Queue<T>` |
| `Stack` | `Stack<T>` |
| `Hashtable` | `Dictionary<TKey, TValue>` |

기능과 동작 방식은 10장과 동일하다.

```csharp
// Hashtable의 일반화 버전 — 키-값 쌍
Dictionary<string, int> hp = new Dictionary<string, int>();
hp["전사"] = 100;
hp["마법사"] = 80;
Console.WriteLine(hp["전사"]); // 100

// foreach 순회 시 KeyValuePair<K,V> 형식으로 나옴
foreach (var kvp in hp)
{
    Console.WriteLine($"{kvp.Key} = {kvp.Value}");
}
```

> 앞서 만든 `Inventory<T>`가 사실상 `List<T>`와 같은 원리로 작동한다. 실무였다면 직접 만들지 않고 그냥 `List<T>`를 쓰면 된다.

> 예제: [ListEx](04_ListEx.cs) | [QueueEx](05_QueueEx.cs) | [StackEx](06_StackEx.cs) | [DictionaryEx](07_DictionaryEx.cs)

---

## foreach는 그냥 쓸 수 없다

일반화 클래스도 `IEnumerable`을 상속하면 `foreach`가 가능하지만, 순회할 때마다 형식 변환 오버헤드가 발생한다.

해결책은 일반화 버전인 **`IEnumerable<T>`** 다. 이걸 상속하면 형식 변환 없이 `foreach` 순회가 가능하다.

**구현해야 하는 메서드:**

| 메서드 | 설명 |
|--------|------|
| `IEnumerator GetEnumerator()` | `IEnumerable`로부터 상속받은 버전 |
| `IEnumerator<T> GetEnumerator()` | 새로 선언된 일반화 버전 |

> `IEnumerable<T>`가 `IEnumerable`을 상속하고 있기 때문에, 부모 인터페이스의 메서드까지 모두 구현해야 한다. 이름은 같지만 반환 형식이 다른 두 메서드를 다 구현하는 이유가 이것이다.

**IEnumerator<T>의 멤버:**

| 메서드/프로퍼티 | 설명 |
|----------------|------|
| `bool MoveNext()` | 다음 요소로 이동. 끝이면 false, 성공하면 true |
| `void Reset()` | 첫 번째 위치의 '앞'(-1)으로 이동 |
| `object Current { get; }` | 현재 요소 반환 (IEnumerator에서 상속) |
| `T Current { get; }` | 현재 요소 반환 (일반화 버전) |

**구현 — yield로 간결하게:**

```csharp
class Inventory<T> : IEnumerable<T> where T : Item
{
    private T[] _items;

    public IEnumerator<T> GetEnumerator()  // 일반화 버전
    {
        for (int i = 0; i < _items.Length; i++)
        {
            yield return _items[i];  // yield 덕분에 MoveNext, Current 직접 구현 불필요
        }
    }

    IEnumerator IEnumerable.GetEnumerator()  // 재활용
    {
        return GetEnumerator();
    }
}
```

이제 직접 만든 일반화 클래스도 `foreach`가 가능하다.

```csharp
foreach (Item item in inventory)
{
    Console.WriteLine(item._Name);
}
```

> 예제: [예제08](08_EnumerableGeneric.cs)
