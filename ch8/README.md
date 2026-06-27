# 챕터 8 — 인터페이스와 추상 클래스
 
> 공익 생활하면서 게임 만들어보다 인터페이스가 뭔지 몰라서 반년만에 책 다시 폈다.
> 클래스 복습부터 시작해서 드디어 여기까지 왔다.
 
---
 
## 📚 목차
 
- [인터페이스란?](#인터페이스란)
- [인터페이스 상속 — 인터페이스의 아들 인터페이스](#인터페이스의-아들-인터페이스)
- [다중 상속](#여러-인터페이스-한꺼번에-상속하기)
- [기본 구현 메서드](#인터페이스의-기본-구현-메서드)
- [추상 클래스](#추상-클래스)
- [인터페이스 vs 추상 클래스 요약](#인터페이스-vs-추상-클래스)
---
 
## 인터페이스란?
 
생긴 건 클래스랑 비슷하다. `interface` 키워드로 선언한다.
 
```csharp
interface IMyInterface
{
    void MethodA();       // 구현부 없음
    int MethodB(int x);
}
```
 
**특징:**
- 메서드, 이벤트, 인덱서, 프로퍼티만 가질 수 있음
- 접근 제한 한정자 사용 불가 — 모든 멤버가 자동으로 `public`
- 구현부(`{}`)가 없음 — 틀만 잡아줌
- 인스턴스 생성 불가. 단, **참조는 만들 수 있음**
**파생 클래스 규칙:**
- 인터페이스에 선언된 **모든 메서드를 반드시 구현**해야 함
- 구현 메서드는 `public`으로 선언해야 함
```csharp
class MyClass : IMyInterface
{
    public void MethodA() { Console.WriteLine("구현!"); }
    public int MethodB(int x) { return x * 2; }
}
 
// 인터페이스 참조로 업캐스팅
IMyInterface obj = new MyClass();
obj.MethodA();
```
 
**왜 쓰나?** 클래스가 따라야 하는 약속(계약). 어떤 클래스든 인터페이스를 구현하면 동일한 역할을 할 수 있다.
 
> 예제: [예제1](01_InterfaceEx.cs) | [예제2 — 게임 캐릭터](02_InterfaceEx.cs)
 
---
 
## 인터페이스의 아들 인터페이스
 
인터페이스를 상속할 수 있는 건 클래스만이 아니다. **인터페이스도 인터페이스를 상속**할 수 있다.
 
```csharp
interface IBase
{
    void Move();
}
 
interface IAdvanced : IBase  // 인터페이스 상속
{
    void Fly();
}
```
 
**왜 기존 인터페이스를 직접 수정하지 않나?**
- 소스 코드 없이 어셈블리로만 제공되는 경우 수정 불가
- 이미 해당 인터페이스를 구현한 클래스가 100개라면? 하나하나 수정하는 건 위험
그냥 상속받는 인터페이스를 하나 더 만들면 해결된다.
 
> 예제: [예제3](03_InhanceInterface.cs)
 
---
 
## 여러 인터페이스, 한꺼번에 상속하기
 
클래스는 **다중 상속 불가** — "죽음의 다이아몬드" 문제 때문이다.
(같은 메서드를 가진 두 부모를 동시에 상속하면 어느 걸 호출할지 모호해짐)
 
인터페이스는 **다중 상속 가능** — 구현이 없으니 어느 걸 호출할지 모호할 일이 없다.
 
```csharp
interface IFlyable { void Fly(); }
interface IRunnable { void Run(); }
 
class Dragon : IFlyable, IRunnable   // 둘 다 상속 가능
{
    public void Fly() { Console.WriteLine("날기"); }
    public void Run() { Console.WriteLine("달리기"); }
}
```
 
**여러 클래스의 구현을 물려받고 싶다면? → 포함(Containment)**
 
클래스를 상속하는 대신 필드로 갖고, 메서드에서 호출한다.
 
```csharp
class MyVehicle
{
    Car car = new Car();
    Plane plane = new Plane();
 
    public void Run() { car.Ride(); }
    public void Fly() { plane.Ride(); }
}
```
 
> 예제: [예제4](04_MultInterface.cs) | [예제4-2](04_04_MultInterface2.cs)
 
---
 
## 인터페이스의 기본 구현 메서드
 
### 문제 상황
 
10년짜리 서비스, 파생 클래스 수백 개. 기본 인터페이스에 메서드 하나 추가하면 → 모든 파생 클래스에서 컴파일 에러.
 
### 해결책 — 기본 구현 메서드
 
```csharp
interface ICharacter
{
    void Move();   // 모든 클래스가 반드시 구현
 
    void Fly()     // 기본 구현 — 선택적 오버라이드
    {
        Console.WriteLine("이 캐릭터는 날 수 없습니다.");
    }
}
 
class Warrior : ICharacter
{
    public void Move() { Console.WriteLine("전사 이동"); }
    // Fly() 구현 안 해도 컴파일 에러 없음
}
 
class Dragon : ICharacter
{
    public void Move() { Console.WriteLine("드래곤 이동"); }
    public void Fly()  { Console.WriteLine("드래곤 비행!"); }  // 오버라이드
}
```
 
### ⚠️ 핵심 규칙
 
기본 구현 메서드는 **인터페이스 참조(업캐스팅)로만 호출 가능**하다.
 
```csharp
ICharacter w = new Warrior();
w.Fly();          // ✅ OK — 인터페이스 참조
 
Warrior w2 = new Warrior();
// w2.Fly();      // ❌ 컴파일 에러 — 클래스 참조로는 접근 불가
```
 
> 예제: [예제5](DefaultIInterface.cs)
 
---
 
## 추상 클래스
 
인터페이스와 클래스 그 사이 어딘가.
 
```csharp
abstract class Animal
{
    public string Name;                          // 필드 가능
    public Animal(string name) { Name = name; } // 생성자 가능
 
    public abstract void Sound();                // 추상 메서드 — 구현 강제
 
    public void Sleep()                          // 일반 메서드 — 기본 구현 제공
    {
        Console.WriteLine($"{Name} 잠들었습니다.");
    }
}
 
class Dog : Animal
{
    public Dog(string name) : base(name) { }
 
    public override void Sound()   // 반드시 override
    {
        Console.WriteLine($"{Name}: 왈왈!");
    }
}
```
 
### 추상 메서드 접근 제한자 규칙
 
한정자 생략 시 기본값은 `private` — 근데 추상 메서드가 `private`이면 파생 클래스에서 보이지 않으니 오버라이드 자체가 불가능하다. 구조적 모순이라 컴파일러가 막는다.
 
반드시 아래 중 하나로 선언해야 한다:
 
| 한정자 | 접근 범위 |
|--------|-----------|
| `public` | 어디서든 |
| `protected` | 파생 클래스 내부 |
| `internal` | 같은 어셈블리 |
| `protected internal` | 파생 클래스 + 같은 어셈블리 |
 
### 왜 쓰나?
 
일반 클래스로도 비슷하게 할 수 있다 — "이 클래스는 직접 쓰지 말고 파생 클래스 만들어서 써라, `MethodA()`는 꼭 오버라이딩 해라" 같은 매뉴얼을 작성해 배포하면 된다.
 
하지만 **매뉴얼은 강제가 아니다.** 따를지 안 따를지는 모르는 일이다.
 
추상 클래스를 쓰면 **컴파일러가 강제**한다. 설명 필요 없다.
 
추가로 공통 상태(필드)를 파생 클래스에 물려주고, `override`로 클래스마다 동작을 다르게 할 수 있어 **다형성** 구현에도 활용된다.
 
> 예제: [예제6](06_AbstractClass.cs)
 
---
 
## 인터페이스 vs 추상 클래스
 
| | 인터페이스 | 추상 클래스 |
|--|:---:|:---:|
| 필드 | ❌ | ✅ |
| 생성자 | ❌ | ✅ |
| 일반 메서드 구현 | ✅ (C# 8.0+) | ✅ |
| 추상 메서드 | ✅ (전부) | ✅ (일부 가능) |
| 다중 상속 | ✅ | ❌ |
| 기본 접근 제한자 | `public` | `private` |
| 인스턴스화 | ❌ | ❌ |
 
**선택 기준:**
- "Dog는 Animal이다" → **추상 클래스** (공통 상태·동작 공유)
- "Dragon은 날 수 있다" → **인터페이스** (능력/계약, 다중 구현 필요)
> 결국 어느 게 나을지는 경험을 통해 배우는 수밖에 없다.
