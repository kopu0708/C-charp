# 챕터 12 — 예외처리
 
> 책을 다시 펼친 지 한 달. 진도를 못 나간 날도, 며칠 쉰 날도 있었지만 생각보다 많은 걸 익혔다.
> 7~11장은 객체지향을 이해하기 위한 내용이었고, 이제부터는 미뤄뒀던 고급 문법이다.
 
---
 
## 📚 목차
 
- [예외란?](#예외란)
- [try~catch문](#trycatch문)
- [System.Exception 클래스](#systemexception-클래스)
- [예외 던지기 (throw)](#예외-던지기)
- [finally](#trycatch와-finally)
- [사용자 정의 예외 클래스](#사용자-정의-예외-클래스-만들기)
- [예외 필터하기 (when)](#예외-필터하기)
- [정리](#예외-처리-다시-생각해보기)
---
 
## 예외란?
 
정수만 입력받는 프로그램을 짰다고 하자. 정수만 넣으라고 분명 설명했는데, 열에 하나는 소수나 문자열을 입력할 것이다. 그 외에도 갑자기 컴퓨터가 꺼진다거나, 감당 안 되는 용량의 파일이 날아온다거나.
 
이렇게 **내가 가정한 상황을 벗어나는 사건**을 예외라 하고, 이것이 프로그램의 오류나 다운으로 이어지지 않게 처리하는 것을 **예외처리**라고 한다.
 
---
 
## try~catch문
 
시도하고(try), 예외가 던져지면 잡는다(catch).
 
```csharp
try
{
    // 예외가 일어나지 않을 경우 실행될 코드
}
catch (예외_객체_1)
{
    // 예외가 발생했을 때의 처리
}
catch (예외_객체_2)
{
    // 예외가 발생했을 때의 처리
}
```
 
**if/switch와의 차이:**
 
| | 검사 방식 |
|--|----------|
| `if`, `switch` | 조건을 **직접** 검사 |
| `try-catch` | 예외가 터지면 **자동으로** catch로 점프 |
 
> ⚠️ catch는 위에서부터 차례로 검사해서 **처음 일치하는 하나만** 실행된다. 그래서 구체적인 예외를 위에, 포괄적인 예외(`Exception`)를 아래에 둬야 한다.
 
> 예제: [예제01](01_TryCatch.cs)
 
---
 
## System.Exception 클래스
 
**모든 예외의 조상.** C#의 모든 예외 클래스는 반드시 이 클래스로부터 상속받는다. `IndexOutOfRangeException`도 여기서 파생됐다.
 
상속 관계이므로 모든 예외는 `System.Exception` 형식으로 간주할 수 있다. 즉 이 형식을 받는 catch문 하나면 모든 예외를 처리할 수 있다.
 
**그럼 catch 하나면 다 되겠네요? 그럴 리가 없다.**
 
```csharp
try
{
    // 파일을 읽는 코드
}
catch (Exception e)  // 모든 예외를 다 잡아버림
{
    Console.WriteLine("에러 발생");
    // 사실은 심각한 시스템 오류였는데
    // "에러 발생"만 찍고 넘어가서 진짜 문제를 놓침
}
```
 
> 무분별한 `catch (Exception)`은 상위 코드에서 처리해야 할 예외까지 삼켜버려서, 문제를 조용히 숨기는 결과를 낳는다. 예외를 처리하는 대신 버그를 만드는 셈이다. 예상되는 구체적 예외를 명시적으로 잡는 게 좋다.
 
---
 
## 예외 던지기
 
catch로 잡는다는 건 어디선가 던진다는 이야기다. `throw` 문을 이용해서 던진다.
 
**아까는 그런 거 없었잖아요?** → 앞선 예제에서는 **CLR(런타임)** 이 자동으로 던진 것이다.
 
```csharp
// 시스템이 자동으로 던지는 경우 (throw 안 보임)
int x = arr[5];  // 범위 초과 → CLR이 자동으로 예외 던짐
 
// 내가 직접 던지는 경우 (throw 사용)
if (age < 0)
{
    throw new ArgumentException("나이는 음수가 될 수 없습니다.");
}
```
 
**왜 직접 던지나?** 문법적으로는 문제없지만 프로그램 규칙상 막아야 하는 경우가 있다. 게임에서 HP는 음수가 되면 안 되는데, 코드상으로는 음수 대입이 가능하다.
 
```csharp
public int Hp
{
    set
    {
        if (value < 0)
            throw new ArgumentException("HP는 음수가 될 수 없습니다.");
        _hp = value;
    }
}
```
 
> 이렇게 던져진 예외는 이 프로퍼티/메서드를 **호출하는 쪽의 try~catch**에서 받아낸다.
 
> 예제: [예제02](02_Throw.cs)
 
### throw 문 vs throw 식
 
C# 7.0부터는 `throw`를 **식(expression)** 으로도 쓸 수 있다.
 
| | 사용 위치 |
|--|----------|
| `throw` 문 | `if` 블록 안에서 독립적으로 사용 |
| `throw` 식 | `??` 나 조건 연산자(`?:`) 안에서 값 대신 사용 (C# 7.0+) |
 
```csharp
int? a = null; // nullable로 선언하고 null 넣은 것
int b = a ?? throw new ArgumentNullException();
// ?? : 왼쪽(a)이 null이 아니면 그 값을, null이면 오른쪽을 실행
// a가 null이므로 throw 식이 실행됨
```
 
```csharp
int[] array = new[] {1, 2, 3};
int index = 4;
int value = array[index >= 0 && index < 3 ? index : throw new IndexOutOfRangeException()];
// index가 0~2 범위면 array[index], 아니면 throw
// index가 4라서 범위를 벗어나므로 예외 발생
```
 
---
 
## try~catch와 finally
 
예외가 던져지면 catch로 점프하면서 중간 코드들은 실행되지 않는다. 그런데 그 코드들이 중요한 코드라면? 버그의 원인이 된다.
 
예를 들어 **파일을 열었으면 반드시 닫아야 하고, DB 연결을 열었으면 반드시 끊어야 한다.** 이런 뒷정리가 필요할 때 `finally`를 쓴다.
 
```csharp
try
{
    // 실행할 코드
}
catch (Exception e)
{
    // 예외 처리
}
finally
{
    // 예외가 나든 안 나든 무조건 실행되는 코드
}
```
 
**finally는 무조건 실행된다:**
- 예외가 발생해서 catch로 갔을 때 ✅
- 예외가 아예 안 났을 때 ✅
- try 안에서 `return`으로 빠져나갈 때 ✅
```csharp
static string LoadGame()
{
    try
    {
        Console.WriteLine("파일 여는 중");
        return "게임 데이터";  // 여기서 반환하는데도
    }
    finally
    {
        Console.WriteLine("파일 닫기");  // 이게 먼저 실행됨
    }
}
```
 
> catch문에 뒷정리 코드를 써도 되지만, catch가 여러 개면 모든 catch에 반복해서 써야 한다. finally를 쓰면 한 번만 쓰면 된다.
 
⚠️ **finally 안에서 예외가 또 일어날 수도 있다.** 이 경우 받아줄 코드가 없으므로 '처리되지 않은 예외'가 된다. 코드를 면밀히 살피거나, 가능성을 배제할 수 없다면 finally 안에 다시 try~catch를 쓰는 것도 방법이다.
 
> 예제: [예제03](03_Finally.cs)
 
---
 
## 사용자 정의 예외 클래스 만들기
 
모든 예외는 `System.Exception`에서 파생되므로, 이 클래스를 상속하면 새로운 예외를 만들 수 있다.
 
```csharp
class MyException : Exception
{
    // ...
}
```
 
**사실 자주 필요하지는 않다.** 이미 다 만들어져 있기 때문이다. 하지만 다음 경우엔 필요하다.
- 특별한 데이터를 담아 예외 처리 루틴에 **추가 정보**를 제공하고 싶을 때
- 예외 상황을 **더 잘 설명**하고 싶을 때
**규칙:**
- 클래스 이름 끝에 `Exception`을 붙이는 게 관례
- 예외 메시지를 넘기려면 `base(message)`로 부모 생성자를 호출해야 함
```csharp
class InventoryFullException : Exception
{
    public int MaxSize { get; }      // 기본 예외엔 없는 추가 정보
    public string ItemName { get; }
 
    public InventoryFullException(string message, int maxSize, string itemName)
        : base(message)  // Exception 생성자에 메시지 전달 → e.Message가 동작
    {
        MaxSize = maxSize;
        ItemName = itemName;
    }
}
 
// 던지기
if (Count >= _items.Length)
    throw new InventoryFullException("인벤토리가 가득 찼습니다.", _items.Length, item._Name);
 
// 잡기 — 추가 정보를 활용할 수 있다
catch (InventoryFullException e)
{
    Console.WriteLine($"{e.Message} (최대: {e.MaxSize}칸, 실패한 아이템: {e.ItemName})");
}
```
 
> 예제: [예제04](04_MyException.cs)
 
---
 
## 예외 필터하기
 
C# 6.0부터 `when` 키워드로 catch 절이 받을 예외에 **조건**을 걸 수 있다. (`when`을 `if`라고 생각하면 이해하기 쉽다.)
 
```csharp
class FilterableException : Exception
{
    public int ErrorNo { get; set; }
}
 
try
{
    int num = GetNumber();
 
    if (num < 0 || num > 10)
        throw new FilterableException() { ErrorNo = num };
    else
        Console.WriteLine($"Output : {num}");
}
catch (FilterableException e) when (e.ErrorNo < 0)  // ErrorNo가 음수일 때만 처리
{
    Console.WriteLine("Negative input is not allowed.");
}
```
 
`num`이 0보다 작거나 10보다 크면 예외를 던지지만, catch는 `when`으로 `ErrorNo < 0`인 경우만 걸러낸다. **그 외의 경우(10 초과)는 처리되지 않은 채 호출자에게 던져진다.**
 
> 예제: [예제05](05_ExceptionFiltering.cs)
 
---
 
## 예외 처리 다시 생각해보기
 
만약 예외 처리를 지원하지 않았다면? 길게 생각할 것 없이 `if-else`문 범벅이었을 것이다. 디버깅할 때도 어디가 문제인지 찾는 데 한참 걸렸을 거고.
 
**정리하면 예외 처리는 세 가지를 해준다.**
 
1. 프로그램이 죽지 않게 막아준다.
2. 정상 로직과 오류 처리를 분리해 읽기 쉽게 만든다.
3. 예외 객체에 정보를 담아 어디서 왜 문제가 생겼는지 알려준다.
