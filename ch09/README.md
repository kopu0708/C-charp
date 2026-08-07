
# 챕터 9 — 프로퍼티

> `get; set;` 이게 뭔지 정말 궁금했다. 드디어 알게 됐다.

---

## 📚 목차

- [메서드보다 프로퍼티](#메서드보다-프로퍼티)
- [자동 구현 프로퍼티](#자동-구현-프로퍼티)
- [자동 구현 프로퍼티 — 내부 동작](#자동-구현-프로퍼티--내부-동작)
- [프로퍼티와 생성자](#프로퍼티와-생성자)
- [초기화 전용 자동 구현 프로퍼티 (init)](#초기화-전용-자동-구현-프로퍼티-init)
- [초기화를 강제하는 required](#프로퍼티-초기화를-강제하는-required-키워드)
- [레코드 — 불변 객체](#레코드-형식으로-만드는-불변-객체)
  - [값 형식 vs 참조 형식 복사 비용](#값-형식-vs-참조-형식-복사-비용)
  - [레코드 선언하기](#레코드-선언하기)
  - [with를 이용한 레코드 복사](#with를-이용한-레코드-복사)
  - [레코드 객체 비교하기](#레코드-객체-비교하기)
- [무명 형식](#무명-형식)
- [인터페이스의 프로퍼티](#인터페이스의-프로퍼티)
- [추상 클래스의 프로퍼티](#추상-클래스의-프로퍼티)

---

## 메서드보다 프로퍼티

클래스 필드를 `private`으로 선언하는 게 좋다고 했다. 데이터가 오염될 수 있기 때문이다. 근데 그렇게 하면 솔직히 조금 귀찮다. 메서드를 추가해서 필드에 접근해야 하기 때문이다.

```csharp
private int age;

public void SetAge(int value)
{
    if (value < 0) age = 0;  // 데이터 보호 가능
    else age = value;
}

public int GetAge()
{
    return age;
}

// 사용 시: obj.SetAge(20); / Console.WriteLine(obj.GetAge());
```

전혀 문제 있는 코드가 아니다. 은닉성을 지키며 필드를 읽고 쓰고 있다. Java라면 이게 정석이다.

하지만 C#에는 **프로퍼티**라는 게 존재한다.

```csharp
class 클래스_이름
{
    데이터_형식 _필드_이름;

    접근_한정자 데이터_형식 프로퍼티_이름
    {
        get { return _필드_이름; }
        set { _필드_이름 = value; }
    }
}
```

- `get`, `set`을 **접근자**라고 한다
- `get` : 필드에서 값을 읽어옴
- `set` : 필드에 값을 할당. `value`는 선언하지 않아도 컴파일러가 암묵적 매개변수로 처리
- `get`만 쓰면 **읽기 전용**, `set`만 쓰면 쓰기 전용 (쓰기 전용은 결과 확인이 어려워 비추천)

```csharp
MyClass obj = new MyClass();
obj.MyField = 3;                    // set 호출
Console.WriteLine(obj.MyField);    // get 호출
```

메서드를 따로 호출할 필요 없이 `=` 연산자만으로 읽고 쓸 수 있다.

> 관례: 필드명은 앞에 `_`를 붙이고, 프로퍼티명은 대문자로 시작

> 예제: [예제01](01_Property.cs)

---

## 자동 구현 프로퍼티

읽고 쓰는 데 아무런 로직이 없다면 더 간단하게 쓸 수 있다. 드디어 내가 알고 싶던 형태다.

```csharp
public class NameCard
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}
```

필드를 선언할 필요도 없고 코드도 획기적으로 줄었다.

C# 7.0부터는 선언과 동시에 초기화도 가능하다.

```csharp
public string Name { get; set; } = "Unknown";
```

생성자에 초기화 코드를 따로 작성할 필요가 없어졌다.

> 예제: [예제02](02_AutoProperty.cs)

---

## 자동 구현 프로퍼티 — 내부 동작

사실 진짜 필드가 없는 건 아니다. 컴파일러가 자동으로 만들어준다.

```csharp
// 우리가 쓴 코드
public string Name { get; set; }

// 컴파일러가 실제로 만들어내는 코드
private string <Name>k__BackingField;  // 컴파일러만 아는 이름의 숨겨진 필드

public string Name
{
    get { return <Name>k__BackingField; }
    set { <Name>k__BackingField = value; }
}
```

저 숨겨진 필드는 우리가 직접 접근할 수 없다. 프로퍼티를 호출할 때마다 컴파일러가 알아서 값을 넣고 빼준다. (컴파일러야 고마워!)

---

## 프로퍼티와 생성자

프로퍼티를 이용한 객체 초기화도 가능하다.

```csharp
클래스_이름 인스턴스 = new 클래스_이름()
{
    프로퍼티1 = 값,
    프로퍼티2 = 값,
    프로퍼티3 = 값
};
```

모든 프로퍼티를 다 넣을 필요 없이, 초기화하고 싶은 것만 선택해서 넣으면 된다. 매개변수가 있는 생성자처럼 어떤 필드를 초기화할지 미리 고민할 필요가 없다.

> 예제: [예제03](03_PropertyConstructor.cs)

---

## 초기화 전용 자동 구현 프로퍼티 (init)

`get`만 있는 읽기 전용 프로퍼티는 **생성자 내부에서만** 값을 할당할 수 있다. 일반 메서드 내부에서는 불가능하고, 클래스 외부에서 `new Character { Name = "..." }` 처럼 초기화하는 것도 불가능하다.

```csharp
class Transaction
{
    public Transaction(string from, string to, int amount)
    {
        _from = from; _to = to; _amount = amount;
    }

    string _from;
    string _to;
    int _amount;

    public string From   { get { return _from; } }
    public string To     { get { return _to; } }
    public int    Amount { get { return _amount; } }
}
```

`init`을 쓰면 이 문제가 해결된다. `set`처럼 외부에서 값을 넣을 수 있되, **객체 초기화 시에만** 가능하다.

```csharp
public string Name { get; init; }
```

생성자 없이도 외부에서 값을 받아 초기화하고, 이후에는 변경이 불가능한 읽기 전용 프로퍼티가 된다.

> 예제: [예제04](04_Init.cs)

---

## 프로퍼티 초기화를 강제하는 required 키워드

`init`은 초기화 시에만 값을 넣을 수 있게 해주지, 초기화 자체를 강제하지는 않는다. 깜빡하고 빠뜨릴 수 있다.

`required` 한정자를 쓰면 컴파일 수준에서 반드시 초기화하도록 강제한다.

```csharp
class BirthdayInfo
{
    public required string Name { get; set; }
    public required DateTime Birthday { get; init; }
}

// 객체 생성 시 반드시 값을 할당해야 함
BirthdayInfo birth = new BirthdayInfo
{
    Name = "서현",
    Birthday = new DateTime(1991, 6, 28)
};
```

[C# - 프로퍼티]
- private 필드 + get만 있는 프로퍼티 = "생성 시 고정, 외부는 읽기만"
- 기준은 "값이 바뀌냐"가 아니라 "누가 바꾸냐(외부 vs 내부)"
- 내부 로직에서는 프로퍼티 안 거치고 필드 직접 수정해도 됨

---

## 레코드 형식으로 만드는 불변 객체

불변 객체는 내부 상태(데이터)를 변경할 수 없는 객체다.

- 클래스로 만들려면: 모든 필드를 `readonly`로 선언
- 구조체로 만들려면: `readonly struct`로 선언

### 값 형식 vs 참조 형식 복사 비용

| | 값 형식 | 참조 형식 |
|--|---------|-----------|
| 복사 방식 | 깊은 복사 (자동) | 얕은 복사 (주소만) |
| 비교 | 모든 필드 1:1 자동 비교 | 직접 구현 필요 (`Equals()` 오버라이드) |
| 복사 비용 | 필드 수만큼 증가 | 주소만 복사라 저렴 |

**왜 참조 형식의 깊은 복사가 더 비싼가?**

- **값 형식 복사 (스택)**: 스택 포인터만 이동시켜 공간 확보 후 데이터를 통째로 복사. CPU 레벨에서 처리되어 오버헤드가 거의 없음
- **참조 형식 깊은 복사 (힙)**: `new`로 힙에 빈 공간을 탐색하고 메모리를 동적 할당해야 함. 나중에 가비지 컬렉터가 별도로 치워줘야 함

> 책에서 "참조 형식이 복사 비용이 적다"고 한 건, **얕은 복사(주소만 복사)**와 **값 형식의 깊은 복사**를 비교한 것이다. 둘 다 깊은 복사를 한다면 참조 형식이 더 비싸다.

**결론:** 값 형식은 비교하기 편하지만 복사 비용이 크다. 참조 형식은 복사 비용이 적지만 비교 구현이 불편하다. → 이 둘의 장점을 합친 게 **레코드**다.

> 레코드: **참조 형식**이지만 **값처럼 비교**하고 **불변성을 보장**한다.

### 레코드 선언하기

`record` 키워드와 `init` 프로퍼티를 함께 사용한다.

```csharp
record RTransaction
{
    public string From   { get; init; }
    public string To     { get; init; }
    public int    Amount { get; init; }
}
```

> 레코드에는 `init` 프로퍼티 외에도 쓰기 가능한 프로퍼티와 일반 필드도 자유롭게 넣을 수 있다.

> 예제: [예제05](05_Record.cs)

### with를 이용한 레코드 복사

레코드의 복사 생성자는 컴파일러가 자동으로 만들어주지만 `protected`로 선언되어 직접 호출이 안 된다.

`with` 키워드로만 접근 가능하다.

```csharp
var t1 = new RTransaction { From = "Alice", To = "Bob", Amount = 1000 };
var t2 = t1 with { Amount = 500 };  // t1을 복사하되 Amount만 바꿈
// t1은 그대로, t2만 Amount가 500
```

**왜 `protected`인가?**
- 레코드도 상속이 가능하다. 부모 레코드를 상속받은 자식 레코드가 있을 때, 복사 로직이 외부에 노출되면 잘못된 방식으로 복사될 위험이 있다
- `protected`로 자식까지만 접근을 허용하고, 외부에서는 `with`로만 쓰도록 강제해 복사 과정의 무결성을 보장하기 위해서다

> 예제: [예제06](06_WithRecord.cs)

### 레코드 객체 비교하기

레코드는 `Equals()` 메서드를 자동으로 구현해준다. 반환값은 `bool`.

클래스로 직접 구현하려면 필드마다 `if` 조건문이 늘어나지만, 레코드는 컴파일러가 알아서 처리한다.

```csharp
var item1 = new Item { Name = "검", Attack = 100 };
var item2 = item1 with { Attack = 150 };

Console.WriteLine(item1.Equals(item2));  // False
Console.WriteLine(item1 == item2);       // False (레코드는 == 도 값 비교)
```

게임에서 아이템 강화 전후를 비교하거나, 강화 결과가 랜덤일 때 얼마나 올랐는지 확인하는 용도로 활용 가능하다.

> 예제: [예제07](07_EqualsRecord.cs)

---

## 무명 형식

이름이 없는 형식. 형식 선언과 동시에 인스턴스를 할당한다.

```csharp
var myInstance = new { Name = "박상현", Age = 17 };
```

**튜플 vs 무명 형식:**

| | 튜플 | 무명 형식 |
|--|------|-----------|
| 기반 | 구조체 (값 형식) | 클래스 (참조 형식) |
| 값 수정 | 가능 | 불가능 (읽기 전용) |
| 주 용도 | 메서드에서 여러 값 반환 | 일회성 데이터 묶음 |
| 인스턴스 | 여러 개 가능 | 한 번만 사용 |

나중에 배울 LINQ와 함께 사용하면 특히 유용하다고 한다.

> 예제: [예제08](08_AnonymousType.cs)

---

## 인터페이스의 프로퍼티

인터페이스는 메서드뿐만 아니라 프로퍼티와 인덱서도 가질 수 있다. 상속받는 클래스는 반드시 구현해야 한다.

```csharp
interface 인터페이스_이름
{
    public 형식 프로퍼티_이름1 { get; set; }
    public 형식 프로퍼티_이름2 { get; set; }
}
```

클래스의 자동 구현 프로퍼티와 생김새가 비슷하지만, 인터페이스의 프로퍼티는 파생 클래스에서 반드시 구현해야 한다는 점이 다르다.

> 예제: [예제09](09_PropertiesInterface.cs)

---

## 추상 클래스의 프로퍼티

추상 클래스는 추상 프로퍼티도 가질 수 있다.

- `abstract` 한정자로 선언
- 파생 클래스에서 반드시 구현해야 함
- 파생 클래스 구현부에는 `override` 키워드 필요

```csharp
abstract class Shape
{
    public abstract double Area { get; }  // 추상 프로퍼티

    public abstract void Draw();          // 추상 메서드
}

class Circle : Shape
{
    public double Radius { get; init; }

    public override double Area => Math.PI * Radius * Radius;  // 반드시 구현

    public override void Draw() { Console.WriteLine("원을 그립니다."); }
}
```

> 예제: [예제10](10_PropertiesAbstract.cs)
