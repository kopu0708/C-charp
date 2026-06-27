# 챕터 7 — 클래스
 
> 클래스 기초부터 구조체, 튜플까지. 객체지향 프로그래밍의 핵심 챕터.
 
---
 
## 📚 목차
 
- [객체지향 프로그래밍이란?](#객체지향-프로그래밍이란)
- [클래스란?](#클래스란)
- [생성자와 종료자](#생성자와-종료자)
- [정적 필드와 메서드](#정적-필드와-메서드)
- [얕은 복사와 깊은 복사](#객체-복사하기-얕은-복사와-깊은-복사)
- [this 키워드](#this-키워드)
- [this() 생성자](#this-생성자)
- [접근 한정자](#접근-한정자로-공개-수준-결정하기)
- [상속](#상속으로-코드-재활용하기)
- [형식 변환 (업캐스팅)](#기반-클래스와-파생-클래스-사이의-형식-변환)
- [오버라이딩과 다형성](#오버라이딩과-다형성)
- [메서드 숨기기](#메서드-숨기기)
- [오버라이딩 봉인](#오버라이딩-봉인하기)
- [읽기 전용 필드](#읽기-전용-필드)
- [중첩 클래스](#중첩-클래스)
- [분할 클래스](#분할-클래스)
- [확장 메서드](#확장-메서드)
- [구조체](#구조체)
- [튜플](#튜플)
---
 
## 객체지향 프로그래밍이란?
 
코드 내 모든 것을 **객체**로 표현하려는 프로그래밍 패러다임. OOP(Object-Oriented Programming)라고도 한다.
 
객체의 가장 주요한 특징은 **속성**과 **기능**이다.
 
| 현실 | 코드 |
|------|------|
| 속성 (키, 몸무게 등) | 필드 (데이터) |
| 기능 (걷기, 먹기 등) | 메서드 |
 
3대 특성: **은닉성**, **상속성**, **다형성**
 
---
 
## 클래스란?
 
**객체를 만들기 위한 청사진.** 클래스가 자동차 설계도라면 객체는 생산된 실제 자동차다.
 
- `string`도 이미 정의된 클래스이고, 그 뒤에 붙는 변수명이 객체(인스턴스)다
- 클래스는 **복합 데이터 형식**이자 **참조 형식**
- `new` 연산자로 **힙(Heap)** 에 객체를 생성하고, 스택의 참조 변수가 그 주소를 가리킴
```csharp
class Cat
{
    public string Name;   // 필드
    public int Age;
 
    public void Meow()    // 메서드
    {
        Console.WriteLine($"{Name}: 야옹~");
    }
}
 
Cat cat = new Cat();      // 인스턴스 생성
cat.Name = "나비";
cat.Meow();
```
 
> 예제: [예제1](01_EXclass.cs)
 
---
 
## 생성자와 종료자
 
### 생성자
 
클래스 이름과 같고 반환 형식이 없다. 명시적으로 구현하지 않으면 컴파일러가 **기본 생성자**를 자동으로 만들어준다.
 
**왜 직접 구현하나?** 객체 생성 시점에 필드를 원하는 값으로 초기화하기 위해.
 
```csharp
class Cat
{
    public string Name;
    public int Age;
 
    public Cat(string name, int age)  // 생성자
    {
        Name = name;
        Age = age;
    }
}
 
Cat cat = new Cat("나비", 3);  // 매개변수 수와 형식이 일치해야 함
```
 
### 종료자
 
- 클래스 이름 앞에 `~`를 붙여 선언
- 매개변수, 한정자 없음 / 오버로딩 불가 / 직접 호출 불가
- CLR의 가비지 컬렉터가 알아서 호출
- 유니티 포함 일반적인 C# 환경에서는 직접 구현할 일이 거의 없음
- 잘못 쓰면 GC 동작을 방해해 프레임 드랍 유발 가능
> 예제: [예제2](02_Cat.cs)
 
---
 
## 정적 필드와 메서드
 
`static` — 메서드나 필드가 **클래스 자체에 소속**되도록 지정하는 한정자.
 
한 프로그램 안에서 인스턴스는 여러 개 존재할 수 있지만, 클래스는 단 하나뿐이다.
→ `static` 필드는 **프로그램 전체에서 유일하게 하나만 존재**한다.
 
```csharp
class Counter
{
    public static int Count = 0;  // 모든 인스턴스가 공유
 
    public Counter() { Count++; }
}
 
Counter a = new Counter();
Counter b = new Counter();
Console.WriteLine(Counter.Count);  // 2 — 클래스명.변수명으로 접근
```
 
- `static` 멤버는 프로그램 실행부터 종료까지 **Static 영역**에 유지됨 (전역 접근 가능)
- 객체 내부 데이터를 이용해야 하면 → **인스턴스 메서드**
- 내부 데이터가 필요 없으면 → **정적 메서드**
- `static class`로 선언하면 하위 모든 멤버에도 `static`을 붙여야 함
> 예제: [예제3](03_Global.cs)
 
---
 
## 객체 복사하기: 얕은 복사와 깊은 복사
 
클래스는 참조 형식 → 단순 `=` 할당은 **힙의 실제 객체가 아닌 참조(주소)를 복사**한다.
 
```csharp
Cat a = new Cat("나비", 3);
Cat b = a;           // 얕은 복사 — b는 a와 같은 객체를 가리킴
 
b.Name = "모모";
Console.WriteLine(a.Name);  // "모모" — 원본도 바뀜!
```
 
**깊은 복사** — `new`로 새 객체를 만들고 필드를 하나하나 복사한다.
 
```csharp
Cat b = new Cat(a.Name, a.Age);  // 깊은 복사 — 독립적인 새 객체
```
 
> 예제: [예제4](04_DeepAndShallow.cs)
 
---
 
## this 키워드
 
객체가 **자기 자신**을 지칭하는 키워드.
 
필드명과 매개변수명이 같을 때 유용하다.
 
```csharp
class Employee
{
    private string Name;
 
    public void SetName(string Name)
    {
        this.Name = Name;  // this.Name = 필드, Name = 매개변수
    }
}
```
 
> 예제: [예제5](05_This.cs)
 
---
 
## this() 생성자
 
자기 자신의 생성자를 가리킨다. 생성자의 코드 블록 내부가 아닌 **앞쪽**에서만 사용 가능.
 
생성자를 여러 개 만들 때 중복 초기화 코드를 제거하는 용도.
 
```csharp
class Player
{
    public string Name;
    public int Hp;
    public int Mp;
 
    public Player() : this("기본이름", 100, 50) { }  // 아래 생성자에 위임
 
    public Player(string name, int hp, int mp)
    {
        Name = name;
        Hp = hp;
        Mp = mp;
    }
}
```
 
> 예제: [예제6](06_createPlayer.cs)
 
---
 
## 접근 한정자로 공개 수준 결정하기
 
은닉성을 구현하는 도구. 필드는 상수를 제외하고 **무조건 감추는 것**이 좋다.
 
| 한정자 | 접근 범위 |
|--------|-----------|
| `public` | 클래스 내부/외부 모두 |
| `protected` | 클래스 내부 + 파생 클래스 |
| `private` | 클래스 내부만 (파생 클래스 불가) |
| `internal` | 같은 어셈블리 내에서 `public` |
| `protected internal` | 같은 어셈블리 내에서 `protected` |
| `private protected` | 같은 어셈블리의 파생 클래스 내부만 |
 
> 한정자 생략 시 기본값은 `private`. "같은 어셈블리" = 같은 프로젝트에서 하나의 `.exe`나 `.dll`로 묶인 범위.
 
> 예제: [예제7](07_ProtectedLv.cs)
 
---
 
## 상속으로 코드 재활용하기
 
```csharp
class Animal
{
    public string Name;
    public Animal(string name) { Name = name; }
}
 
class Dog : Animal   // Animal을 상속
{
    public Dog(string name) : base(name) { }  // 기반 클래스 생성자 호출
 
    public void Bark() { Console.WriteLine("왈왈!"); }
}
```
 
- `private` 멤버는 상속되지 않음
- 파생 클래스 객체 생성 시: **기반 클래스 생성자 먼저** 호출 → 파생 클래스 생성자
- 소멸 순서는 반대: 파생 → 기반
- `base` : 기반 클래스를 가리키는 키워드 (`this`의 기반 클래스 버전)
- `sealed class` : 상속 불가능한 봉인 클래스
> 예제: [예제8](08_Base.cs)
 
---
 
## 기반 클래스와 파생 클래스 사이의 형식 변환
 
파생 클래스의 인스턴스를 기반 클래스 형식으로 변환하는 것을 **업캐스팅(Upcasting)** 이라 한다.
 
```csharp
Dog dog = new Dog("뽀삐");
Animal animal = dog;  // 업캐스팅 — Dog는 Animal이므로 가능
```
 
| 연산자 | 설명 |
|--------|------|
| `is` | 해당 형식인지 검사 → `bool` 반환 |
| `as` | 형식 변환 시도 → 실패하면 예외 대신 `null` 반환 (값 형식 불가) |
 
```csharp
if (animal is Dog d)
    d.Bark();
 
Dog d2 = animal as Dog;  // 실패 시 null
```
 
> 예제: [예제9](09_TypeCasting.cs)
 
---
 
## 오버라이딩과 다형성
 
다형성 — 객체가 여러 형태를 가질 수 있음. 파생 클래스를 통해 실현.
 
메서드를 오버라이딩하려면:
1. 기반 클래스 메서드에 `virtual` 키워드
2. 파생 클래스 메서드에 `override` 키워드
```csharp
class Bird
{
    public virtual void Shoot()
    {
        Console.WriteLine("일반 새: 직선으로 날아감");
    }
}
 
class BoomerangBird : Bird
{
    public override void Shoot()
    {
        Console.WriteLine("부메랑 새: 휘어서 날아감");
    }
}
 
class ExplosiveBird : Bird
{
    public override void Shoot()
    {
        base.Shoot();  // 부모 메서드 활용 후 추가 동작
        Console.WriteLine("폭발!");
    }
}
```
 
> `private` 메서드는 오버라이딩 불가 (파생 클래스에서 보이지도 않음)
 
> 예제: [예제10](10_overrideMethod.cs)
 
---
 
## 메서드 숨기기
 
기반 클래스 버전의 메서드를 감추고 파생 클래스 버전만 보여주는 것.
`new` 한정자로 수식한다. (객체 생성할 때 쓰는 `new` 연산자와 다름)
 
```csharp
class Base
{
    public void Hello() { Console.WriteLine("Base"); }
}
 
class Derived : Base
{
    public new void Hello() { Console.WriteLine("Derived"); }  // 숨기기
}
 
Base obj = new Derived();
obj.Hello();  // "Base" 출력 — 부모 버전이 실행됨 (다형성 미적용)
```
 
**오버라이딩과 차이:**
- 오버라이딩(`override`): 부모 타입 변수로 참조해도 자식 버전 실행 → **다형성 적용**
- 메서드 숨기기(`new`): 부모 타입 변수로 참조하면 부모 버전 실행 → **다형성 미적용**
**언제 쓰나?** 내가 수정할 수 없는 외부 라이브러리 클래스를 상속받아 메서드 동작을 바꿔야 할 때.
 
> 예제: [예제11](11_hideMethod.cs)
 
---
 
## 오버라이딩 봉인하기
 
`virtual` 메서드를 오버라이딩한 버전에 `sealed`를 붙이면 더 이상 오버라이딩 불가.
 
```csharp
class A
{
    public virtual void Method() { }
}
 
class B : A
{
    public sealed override void Method() { }  // 여기서 봉인
}
 
class C : B
{
    // public override void Method() { }  // 컴파일 에러
}
```
 
**왜 쓰나?** 특정 메서드를 오버라이딩하면 클래스 다른 부분에 오작동이 생길 수 있는 경우, 기반 클래스 작성자가 사전에 방지하기 위해.
 
> 예제: [예제12](12_sealed.cs)
 
---
 
## 읽기 전용 필드
 
| | `const` | `readonly` | 일반 변수 |
|--|---------|------------|-----------|
| 값 변경 | 불가 | 생성자에서 1회만 | 자유 |
| 결정 시점 | 컴파일 타임 | 런타임 (생성자) | 런타임 |
 
```csharp
class Circle
{
    readonly double Pi;
 
    public Circle(double pi)
    {
        Pi = pi;  // 생성자에서만 초기화 가능
    }
}
```
 
> 예제: [예제13](13_ReadOnly.cs)
 
---
 
## 중첩 클래스
 
클래스 내부에 선언된 클래스. 바깥 클래스의 `private` 멤버에도 접근 가능.
 
```csharp
class OuterClass
{
    private int OuterMember;
 
    class NestedClass
    {
        public void DoSomething()
        {
            OuterClass outer = new OuterClass();
            outer.OuterMember = 10;  // private 접근 가능
        }
    }
}
```
 
**왜 쓰나?**
- 클래스 외부에 공개하고 싶지 않은 형식을 만들 때
- 현재 클래스의 일부분처럼 표현할 수 있는 클래스를 만들 때
> 예제: [예제14](14_NestedClass.cs)
 
---
 
## 분할 클래스
 
클래스가 너무 길어질 때 여러 파일로 나눠서 구현. `partial` 키워드 사용, 클래스 이름은 동일하게.
 
```csharp
// 파일1.cs
partial class MyClass
{
    public void MethodA() { }
}
 
// 파일2.cs
partial class MyClass
{
    public void MethodB() { }
}
```
 
> 예제: [예제15](15_partial.cs)
 
---
 
## 확장 메서드
 
기존 클래스를 수정하지 않고 기능을 추가하는 기법. 상속과 다르다.
 
- 정적 클래스 안에, 정적 메서드로 선언
- 첫 번째 매개변수에 `this 확장할형식 변수명`
```csharp
static class StringExtensions
{
    public static string Reverse(this string str)  // string 확장
    {
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}
 
string result = "hello".Reverse();  // "olleh"
```
 
> 예제: [예제16](16_ExtendMethod.cs)
 
---
 
## 구조체
 
클래스와 유사하지만 `struct` 키워드 사용. 데이터를 담기 위한 자료구조.
 
**클래스 vs 구조체:**
 
| | 클래스 | 구조체 |
|--|--------|--------|
| 키워드 | `class` | `struct` |
| 메모리 | 힙 (참조 형식) | 스택 (값 형식) |
| 복사 | 얕은 복사 (주소 복사) | 깊은 복사 (값 전체 복사) |
| 상속 | 가능 | 불가 |
| 기본 생성자 | 자동 생성 | C# 10 이전 선언 불가 |
 
```csharp
struct Point
{
    public int X;
    public int Y;
}
 
Point a = new Point { X = 1, Y = 2 };
Point b = a;      // 값 전체가 복사됨 — 독립적인 복사본
b.X = 99;
Console.WriteLine(a.X);  // 1 — 원본 영향 없음
```
 
**읽기 전용 구조체:** 모든 필드/프로퍼티가 변경 불가능한 Immutable 객체.
 
```csharp
readonly struct ImmutablePoint
{
    public readonly int X;
    public readonly int Y;
 
    public ImmutablePoint(int x, int y) { X = x; Y = y; }
}
```
 
Immutable 객체의 장점: 데이터 오염 방지, 멀티 스레드 동기화 불필요 → 성능 향상
 
> 예제: [예제17](17_structEx.cs) | [예제18](18_ReadOnlyStruct.cs)
 
---
 
## 튜플
 
여러 필드를 담을 수 있는 구조체. 형식 이름 없이 즉석에서 사용하는 복합 데이터 형식.
 
```csharp
// 명명되지 않은 튜플
var t1 = (123, 789);
Console.WriteLine(t1.Item1);  // 123
Console.WriteLine(t1.Item2);  // 789
 
// 명명된 튜플
var t2 = (Name: "이정철", Age: 24);
Console.WriteLine(t2.Name);   // 이정철
 
// 튜플 분해
var (name, age) = ("이정철", 24);
 
// 특정 필드 무시
var (_, age2) = ("이정철", 24);
```
 
**왜 쓰나?** 매번 구조체나 클래스를 선언하기 번거로울 때 즉석에서 복합 데이터 묶음이 필요한 경우.
 
> C# 7.0부터 `System.ValueTuple` (구조체 기반) 도입 — 힙 할당 없이 스택에서 처리
 
> 예제: [예제19](19_TupleEx.cs)
