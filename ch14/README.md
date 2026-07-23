# 챕터 14 — 람다식

> 항상 느끼는 건데 뭔가 이름이 동글동글한 느낌이라 귀여움. 영어로는 Lambda Expression이라는데, 영어로 보니 그렇게 귀엽지 않다.

---

## 📚 목차

- [람다식의 유래](#람다식의-유래)
- [그래서 어떻게 하는건데?](#그래서-어떻게-하는건데)
- [문 형식의 람다식](#문-형식의-람다식)
- [Func와 Action](#func와-action으로-더-간편하게-무명-함수-만들기)
- [식 트리](#식-트리)
- [식으로 이루어지는 멤버](#찐-막으로-식으로-이루어지는-멤버)

---

## 람다식의 유래

알론조 처치라는 수학자가 **1936년**에 발표한 람다 계산법에서 사용하는 식이다. 간결하게 함수를 묘사하기 위해 고안됐다.

람다 계산법은 크게 함수의 정의와 변수, 함수의 적용으로 이루어진다. 이 계산법에서는 **모든 것이 함수**로 이루어져 있다. 심지어 0, 1, 2 같은 정수도 그렇다. 어떤 값을 변수에 대입하고 싶으면 함수를 변수에 대입하며, 이걸 **함수의 적용**이라 부른다.

이걸 존 매카시라는 사람이 프로그래밍 언어에 도입할 수 있겠다는 아이디어를 냈고, 1950년대 말에 **LISP**라는 언어를 만들었다. 이게 C#, C++, 자바, 파이썬 등 주류 언어에 도입된 것이다.

---

## 그래서 어떻게 하는건데?

앞서 배운 **익명 메소드**를 만들기 위해 람다식을 사용한다. 다만 람다식으로 만드는 익명 메소드는 **무명 함수**라는 이름으로 부른다.

메소드는 입력(매개변수)과 출력(반환값)을 갖는다. 람다식도 마찬가지다.

```csharp
매개변수_목록 => 식
```

`=>`는 **람다 연산자**라고 부른다. 왼쪽엔 입력(매개변수), 오른쪽엔 식이나 실행할 코드가 온다.

```csharp
delegate int Calculate(int a, int b);  // 익명 메소드를 만들려면 대리자가 필요하다.

static void Main()
{
    Calculate calc = (int a, int b) => a + b;
    // 두 정수를 a, b로 받아 더해 반환하는 익명 메소드를 람다식으로 만듦
}
```

매우 간결하게 메소드가 표현된다. "이거 두 개를 옆의 식으로 계산해주세요" 정도의 느낌이다.

**형식 유추** 기능 덕분에 매개변수 형식을 생략할 수도 있다.

```csharp
Calculate calc = (a, b) => a + b;
```

> 예제: [예제01](01_Lambda.cs)

---

## 문 형식의 람다식

door의 문이 아니라 문장의 문이다. 앞선 람다식은 **식(expression)** 형식이었는데, 식 대신 **문장(statement)** 으로 쓰면 더 읽기 쉬울 때도 있다.

`=>` 오른편에 식 대신 **중괄호로 둘러싸인 코드 블록**이 위치한다.

```csharp
(매개변수_목록) => {
    문장1;
    문장2;
    문장3;
}
```

식 형식으로는 반환 형식이 없는(void) 무명 함수를 만들 수 없지만, 문 형식으로는 가능하다.

```csharp
delegate void DoSomething();

static void Main()
{
    DoSomething DoIt = () =>  // 매개변수 없으면 그냥 ()
    {
        Console.WriteLine("뭔가");
        Console.WriteLine("출력");
        Console.WriteLine("하기");
    };  // 문장형 람다식은 {}로 둘러싸야 함
    DoIt();
}
```

> 예제: [예제02](02_StatementLambda.cs)

---

## Func와 Action으로 더 간편하게 무명 함수 만들기

익명 메소드/무명 함수는 간결하지만, 하나 만들 때마다 매번 별개의 **대리자를 선언**해야 하는 번거로움이 있다. .NET은 이걸 위해 `Func`와 `Action` 대리자를 미리 만들어뒀다.

| | 반환값 | 목적 |
|--|--------|------|
| `Func` | 있음 | 결과를 반환하는 메소드를 참조 |
| `Action` | 없음(void) | 결과 없이 실행만 하는 메소드를 참조 |

### Func 대리자

.NET에 17가지 버전이 준비되어 있다.

```csharp
public delegate TResult Func<out TResult>()
public delegate TResult Func<in T, out TResult>(T arg)
public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2)
// ...
public delegate TResult Func<in T1, ..., in T16, out TResult>(T1 arg1, ..., T16 arg16)
```

> `in`, `out` 키워드는 제네릭의 공변성/반공변성과 관련된 것으로, 지금은 몰라도 Func/Action 사용에 지장 없다. 나중에 궁금하면 찾아볼 것.

**형식 매개변수 중 가장 마지막이 반환 형식이다.** 형식 매개변수가 하나뿐이면 그 하나가 반환 형식.

```csharp
Func<int> func1 = () => 10;         // 입력 없음, 무조건 10 반환
Console.WriteLine(func1());         // 10

Func<int, int> func2 = (x) => x * 2;  // int 입력, int 반환
Console.WriteLine(func2(3));        // 6
```

마지막을 제외한 앞의 자료형은 입력받고, 마지막 자료형으로 반환한다. **`Func`를 쓰면 대리자 선언이 아예 사라진다**는 게 핵심이다. 간단한 한 번만 쓸 수식에 좋다.

> 예제: [예제03](03_FuncTest.cs)

### Action 대리자

Func와 거의 같지만 **반환 형식이 없다.** 마찬가지로 17개 버전이 있다.

```csharp
public delegate void Action()
public delegate void Action<in T>(T arg)
public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2)
// ...
public delegate void Action<in T1, ..., in T16>(T1 arg1, ..., T16 arg16)
```

모두 입력 매개변수만 있다. 어떤 결과를 반환하는 게 목적이 아니라 **일련의 작업을 수행하는 것**이 목적이기 때문이다.

```csharp
Action act1 = () => Console.WriteLine("Action()");
act1();

// 매개변수 하나뿐인 버전, Action<T>
int result = 0;
Action<int> act2 = (x) => result = x * x;  // 람다식 밖의 result에 x*x 저장
act2(3);
Console.WriteLine("result : {0}", result);  // 9
```

Func와 별 차이 없어 보이지만, **결과는 필요 없고 뭔가를 실행만 시키고 싶을 때** 사용한다. 출력, 로그 남기기, 상태 변경 같은 것들.

> 예제: [예제04](04_ActionTest.cs)

---

## 식 트리

LINQ를 이해하고 사용하는 데 도움이 되는 내용이라고 한다. 지금 굳이 필요한 내용이 아닌 것 같아 일단 넘어간다. (어차피 알고리즘 공부하면 나올 듯)

---

## 찐 막으로 식으로 이루어지는 멤버

메소드를 비롯해 속성(인덱서), 생성자, 종료자는 공통점이 있다 — 모두 클래스의 멤버로서 본문이 `{}`로 만들어진다는 것.

이 본문을 **식만으로 구현**할 수 있는데, 이렇게 만든 걸 **식 본문 멤버**라고 한다.

```csharp
멤버 => 식;
```

하나씩 어떻게 변하는지 보자.

**메서드**
```csharp
// 기존
public int Add(int a, int b) { return a + b; }

// 식 본문 멤버
public int Add(int a, int b) => a + b;
```

**프로퍼티**
```csharp
// 기존
public string Name { get { return _name; } }

// 식 본문 멤버
public string Name => _name;
```

**인덱서**
```csharp
// 기존
public T this[int index] { get { return _items[index]; } }

// 식 본문 멤버
public T this[int index] => _items[index];
```

**생성자**
```csharp
// 기존
public Monster(string name) { Name = name; }

// 식 본문 멤버
public Monster(string name) => Name = name;
```

get/set이 둘 다 있는 프로퍼티나 인덱서도 각각 식 본문으로 쓸 수 있다.

```csharp
class FriendList
{
    private List<string> list = new List<string>();

    public void Add(string name) => list.Add(name);
    public void Remove(string name) => list.Remove(name);

    public void PrintAll()  // 여러 줄 필요하면 기존 방식 그대로
    {
        foreach (var s in list)
            Console.WriteLine(s);
    }

    public FriendList() => Console.WriteLine("FriendList()");   // 생성자
    ~FriendList() => Console.WriteLine("~FriendList()");        // 종료자

    public int Capacity                    // get/set 프로퍼티
    {
        get => list.Capacity;
        set => list.Capacity = value;
    }

    public string this[int index]          // 인덱서
    {
        get => list[index];
        set => list[index] = value;
    }
}
```

> 예제: [예제05](05_ExpressionBodiedMember.cs)

---

## 클래스부터 람다식까지 마치며

한 달 걸렸는데, 중간에 안 놀고 했으면 훨씬 더 빨리 끝났을 듯. 당연한 말이지만.

22일 기준으로 어제(21일) 유니티 코리아 2026을 다녀왔다. 정말 배운 게 많았다. 핸즈온 강연에서 유니티 AI, MCP 사용법을 들었는데, 스크립트는 물론 인스펙터 창을 만질 필요도 없이 2시간 만에 게임을 뽑아내는 걸 보고 깜짝 놀랐다. AI다 보니 항상 같은 결과물이 나오는 건 아니지만, 게임의 70%까지는 딸깍으로 뽑아낼 수 있겠구나 싶었다.

주니어보다 못한 신생아 프로그래머로서 위기감이 드는 동시에, 앞으로 훨씬 더 쉽게 게임을 만들 수 있다는 기대감도 들었다.

위기감이 드는 것도 사실이라, 질의응답 시간에 어떻게 해야 취업까지 할 수 있는지 물었다. 클라이언트 개발자를 희망한다고 하니, 3명 정도가 공통적으로 이제 그런 직군 분류는 희미해지고 다방면으로 아는 사람이 되는 게 중요하다고 답했다.

가장 크게 깨달은 건, 게임의 콘텐츠적인 부분은 어쨌든 사람이 만들게 되어 있어서 현업에서도 AI는 **게임을 만드는 툴을 만드는 데** 가장 많이 사용한다는 말이었다.

### 정리하면

1. 단순 작업(스크립트 작성, 타일맵 작성 등)은 모두 AI의 영역으로 넘어갔다.
2. 직업군의 분류는 점점 흐려지고 있으므로, 이제는 전문가보다 다방면으로 구조를 아는 척척박사가 필요할 것이다.
3. 그래도 사람 손은 타기에, 이제 병목현상은 **코드를 해석하는 능력**에 따라 해결될 것이다.
4. 콘텐츠적인 부분은 사람이 만들기에, AI로 툴을 만드는 데 현업은 사용 중이다.
5. 열심히 하자. 진짜로.
