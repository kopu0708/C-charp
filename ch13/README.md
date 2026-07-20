# 챕터 13 — 대리자와 이벤트

> 이제껏 뭔가를 지시하면 컴퓨터가 그 명령을 수행하는 프로그램만 작성해왔다. 지금부터는 **사건에 반응하는** 프로그램을 작성하는 방법을 배운다.

---

## 📚 목차

- [왜 필요한가 — 몬스터 사망 예시](#왜-필요한가--몬스터-사망-예시)
- [대리자란?](#대리자란)
- [대리자는 왜, 언제 사용할까?](#대리자는-왜-언제-사용할까)
- [일반화 대리자](#일반화-대리자)
- [대리자 체인](#대리자-체인)
- [익명 메소드](#익명-메소드)
- [이벤트 — 객체에 일어난 사건 알리기](#이벤트--객체에-일어난-사건-알리기)
- [대리자와 이벤트의 차이](#대리자와-이벤트의-차이)

---

## 왜 필요한가 — 몬스터 사망 예시

RPG에서 몬스터가 죽으면 내부적으로 많은 사건이 처리돼야 한다. 경험치 지급, 퀘스트 진행도 갱신, 아이템 드랍, UI 업데이트... 이걸 대리자 없이 짜면 `Monster` 클래스가 이 모든 걸 알아야 한다.

```csharp
class Monster
{
    Player player;
    QuestManager quest;
    SoundManager sound;
    UIManager ui;
    // ... 계속 늘어남

    void Die()
    {
        player.AddExp(100);
        quest.UpdateProgress(this);
        sound.Play("death");
        ui.Refresh();
        // 기능 추가할 때마다 Monster를 뜯어고쳐야 함
    }
}
```

`Monster` 클래스 하나에 게임의 절반이 의지하게 된다. 몬스터를 길들이는 기능이 추가되면 이 복잡한 클래스를 또 손봐야 한다.

**대리자/이벤트를 배우면 이렇게 바뀐다.** `Monster`는 "죽었다"고 알리기만 하고, 경험치·퀘스트·사운드는 각자 알아서 반응한다. 기능이 추가돼도 `Monster` 코드는 손댈 필요가 없다.

```csharp
class Monster
{
    public event Action<Monster> OnDeath;  // "나 죽었어" 알림만

    void Die()
    {
        OnDeath?.Invoke(this);  // 누가 듣는지 몰라도 됨
    }
}
```

---

## 대리자란?

실장님이 자리를 비우셨을 때 "오시면 전화 부탁드린다고 전해주세요"라고 부탁한다. 이 부탁을 **콜백**이라 부른다. 콜백의 핵심은 "무엇을 할지"는 정해졌지만 **"언제 할지"는 나중**이라는 점이다.

> 대리자는 이런 "나중에 실행할 코드"를 담아두는 그릇이다.

원리를 간단히 말하면, 대리자는 **메소드에 대한 참조**다. 대리자에 메소드의 주소를 할당한 후 호출하면, 그 주소를 통해 메소드가 호출된다.

```csharp
한정자 delegate 반환_형식 대리자_이름(매개변수_목록);
```

`delegate` 키워드만 빼면 메소드와 형태가 똑같다. 대리자는 자신이 참조할 메소드의 **반환 형식과 매개변수**를 명시해야 한다.

```csharp
// 1. 대리자 선언 — "이런 모양의 메서드를 담을 그릇"
delegate int MyDelegate(int a, int b);

// 2. 담을 메서드 — 반환 형식과 매개변수가 대리자와 일치해야 함
static int Add(int a, int b) { return a + b; }

// 3. 대리자의 인스턴스를 만들 때도 new 연산자가 필요하다.
MyDelegate Callback = new MyDelegate(Add);
// MyDelegate Callback = Add; // 이렇게 축약도 가능. 컴파일러가 자동 변환해줌

// 4. 대리자를 호출하면 담아둔 메서드가 실행됨
Console.WriteLine(Callback(3, 4));  // 7
```

> 호출자가 대리자를 호출하고, 대리자는 또 메소드를 호출한다. 그 실행 결과를 호출자에게 전달한다.

대리자는 인스턴스 메서드와 정적 메서드 둘 다 참조할 수 있다.

```csharp
class Calculator
{
    public int Plus(int a, int b) { return a + b; }        // 인스턴스 메서드
    public static int Minus(int a, int b) { return a - b; } // 정적 메서드
}

Calculator calc = new Calculator();
MyDelegate Callback = new MyDelegate(calc.Plus);
Console.WriteLine(Callback(3, 4)); // 7

Callback = Calculator.Minus;  // 축약해서 갈아끼우기
Console.WriteLine(Callback(7, 5)); // 2
```

> 예제: [예제01](01_Delegate.cs)

---

## 대리자는 왜, 언제 사용할까?

**"근데 메소드 직접 호출하는 게 더 빠른데요?"** — 맞다. 하지만 대리자가 빛나는 건 **'값'이 아닌 '코드' 자체를 매개변수로 넘기고 싶을 때**다.

배열을 정렬하는 메소드를 만든다고 하자. 오름차순? 내림차순? 어떤 기준으로? 비교 루틴 자체를 매개변수로 받을 수 있다면, 이 고민은 메소드를 사용하는 쪽의 몫이 된다.

```csharp
delegate int Compare(int a, int b);  // 먼저 비교 대리자를 선언한다.

static int AscendCompare(int a, int b)  // 대리자가 참조할 비교 메소드
{
    if (a > b) return 1;
    else if (a == b) return 0;
    else return -1;
}

// 정렬할 배열과 비교 메소드를 참조한 대리자를 매개변수로 받는다.
static void BubbleSort(int[] DataSet, Compare Comparer)
{
    int temp = 0;
    for (int i = 0; i < DataSet.Length - 1; i++)
    {
        for (int j = 0; j < DataSet.Length - (i + 1); j++)
        {
            // Comparer가 어떤 메소드를 참조하고 있는가에 따라 결과가 달라진다.
            if (Comparer(DataSet[j], DataSet[j + 1]) > 0)
            {
                temp = DataSet[j + 1];
                DataSet[j + 1] = DataSet[j];
                DataSet[j] = temp;
            }
        }
    }
}
```

> 왜 2중 for문? 안쪽 루프는 한 바퀴 돌 때마다 가장 큰(혹은 작은) 값 하나를 뒤로 밀어낸다. 바깥 루프는 그 과정을 요소 개수만큼 반복한다. 이미 확정된 뒤쪽 값은 다시 볼 필요 없어서 `DataSet.Length - (i+1)`로 검사 범위를 줄인다.

대리자가 없었다면 정렬 메소드를 정렬 기준마다 하나씩 새로 짜야 했겠지만, 대리자 덕분에 `BubbleSort` 코드는 그대로 두고 넘겨주는 메소드만 바꾸면 된다.

```csharp
BubbleSort(array, AscendCompare);   // 오름차순
BubbleSort(array, DescendCompare);  // 내림차순 — BubbleSort 코드는 그대로
```

> 예제: [예제02](02_callBack.cs)

---

## 일반화 대리자

대리자도 일반화 메소드를 참조할 수 있도록 형식 매개변수를 받아 선언할 수 있다.

```csharp
delegate int Compare<T>(T a, T b);
```

대리자를 매개변수로 받는 메소드도 형식 매개변수를 받도록 바꿔야 한다.

```csharp
static void BubbleSort<T>(T[] DataSet, Compare<T> comparer)
{
    T temp;  // int가 아니라 T — 어떤 형식이 와도 대응
    for (int i = 0; i < DataSet.Length - 1; i++)
    {
        for (int j = 0; j < DataSet.Length - (i + 1); j++)
        {
            if (comparer(DataSet[j], DataSet[j + 1]) > 0)
            {
                temp = DataSet[j + 1];
                DataSet[j + 1] = DataSet[j];
                DataSet[j] = temp;
            }
        }
    }
}
```

대리자에 담을 메소드도 형식 매개변수를 쓰도록 바꿔야 한다.

```csharp
static int AscendCompare<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b);
}
```

**왜 `T`가 `IComparable<T>`를 상속받아야 하나?** `int`, `double` 같은 수치 형식과 `string`은 모두 `IComparable`을 구현해서 `CompareTo()`를 갖고 있다. `CompareTo`는 자신이 매개변수보다 크면 1, 같으면 0, 작으면 -1을 반환한다. T가 어떤 형식일지 모르니, `CompareTo()`를 확실히 가진 형식만 오도록 (11장에서 배운) 제약을 건 것이다.

```csharp
int[] nums = { 3, 7, 4, 2, 10 };
BubbleSort(nums, AscendCompare);        // T = int

string[] words = { "Cherry", "Apple" };
BubbleSort(words, AscendCompare);       // T = string — 같은 메서드로!
```

> 예제: [예제03](03_GenericDelegate.cs)

---

## 대리자 체인

대리자 하나가 **여러 개의 메소드를 동시에 참조**할 수 있다. `+=` 연산자로 결합한다.

```csharp
delegate void ThereIsAFire(string message);

void Call119(string message) { Console.WriteLine("여기 불남 주소는 {0}", message); }
void ShotOut(string message) { Console.WriteLine("불났어요! {0}에서요.", message); }
void Escape(string message)  { Console.WriteLine("불났어요! {0}에서요. 빨리 피하세요.", message); }
```

```csharp
ThereIsAFire Fire = new ThereIsAFire(Call119);
Fire += new ThereIsAFire(ShotOut);
Fire += new ThereIsAFire(Escape);

Fire("우리 집");
// 여기 불남 주소는 우리 집 / 불났어요! 우리 집에서요. / 불났어요! 우리집에서요. 빨리 피하세요.
```

한 번 호출하면 체인에 걸린 메소드가 **차례대로**(동시가 아니다) 다 실행된다.

**다른 결합 방법들:**

```csharp
// + 연산자와 = 연산자 사용하기
ThereIsAFire Fire = new ThereIsAFire(Call119)
                  + new ThereIsAFire(ShotOut)
                  + new ThereIsAFire(Escape);

// Delegate.Combine() 메소드 사용하기
ThereIsAFire Fire = (ThereIsAFire)Delegate.Combine(
                            new ThereIsAFire(Call119),
                            new ThereIsAFire(ShotOut),
                            new ThereIsAFire(Escape));

// -= 로 체인에서 제거도 가능
Fire -= new ThereIsAFire(Escape);  // 대피 안내만 제거
Fire("우리 집");  // 이제 신고, 소리치기만 실행
```

> `+=` 가 제일 편하니 실무에서는 이걸 쓰면 된다. 위 두 방법은 내부적으로 뭘 하는지 알아두는 정도로 충분하다.

> 예제: [예제04](04_DelegateChains.cs)

---

## 익명 메소드

메소드는 이름이 없어도 되는 게 없다 — 이름만은 있어야 한다. 그런데 **익명 메소드는 이름이 없다.**

```csharp
delegate int Calculate(int a, int b);

public static void Main()
{
    Calculate Calc;
    Calc = delegate (int a, int b)  // 이름을 제외한 구현부 — 이게 익명 메소드
    {
        return a + b;
    };
    Console.WriteLine("3+4 : {0}", Calc(3, 4));
}
```

`delegate` 키워드로 선언해서 대리자 인스턴스가 그 구현부를 바로 실행하게 한다. 자신을 참조할 대리자와 **동일한 형식·매개변수 개수**여야 한다.

**왜 쓰나?** 딱 한 번만 쓸 메소드는 이름을 짓기도 애매하다. 재사용 안 할 코드라면 익명 메소드로 그 자리에서 바로 구현해버리는 게 더 간결하다.

```csharp
// 기존 방식 — 한 번 쓸 건데도 따로 선언
static int Add(int a, int b) { return a + b; }
Calculate calc = Add;

// 익명 메소드 — 그 자리에서 바로
Calculate calc = delegate (int a, int b) { return a + b; };
```

> 예제: [예제05](05_AnonymousMethod.cs)

---

## 이벤트 — 객체에 일어난 사건 알리기

약속을 지키려고, 혹은 운동 루틴을 시작/종료하려고 알람을 맞춘다. 프로그래밍에도 어떤 일이 생겼을 때 알려주는 객체가 필요하다 — 특정 시간이 됐다거나, 사용자가 버튼을 클릭했다거나. 이럴 때 쓰는 게 **이벤트**다.

동작 원리는 대리자와 거의 같다. 이벤트는 대리자를 `event` 한정자로 수식해서 만든다.

```csharp
delegate void EventHandler(string message); // 1. 대리자 선언

class MyNotifier
{
    public event EventHandler SomethingHappened; // 2. event로 수식해서 선언

    public void DoSomething(int number)
    {
        int temp = number % 10;
        if (temp != 0 && temp % 3 == 0)
        {
            SomethingHappened(String.Format("{0} : 3의 배수", number));
        }
    }
}

class Program
{
    static public void MyHandler(string message)  // 3. 이벤트 핸들러 작성
    {
        Console.WriteLine(message);
    }

    static void Main()
    {
        MyNotifier notifier = new MyNotifier();
        notifier.SomethingHappened += new EventHandler(MyHandler);  // 4. 등록

        for (int i = 1; i < 30; i++)
            notifier.DoSomething(i);  // 5. 이벤트 발생 → 핸들러 호출
    }
}
```

**"근데 그냥 대리자 쓰면 되잖아요? 왜 굳이 event를?"**

> 예제: [예제06](06_EventTest.cs)

---

## 대리자와 이벤트의 차이

이벤트는 대리자에 `event` 키워드로 수식해서 선언한 것에 불과하다. 그런데 왜 있을까?

**가장 큰 차이 — 이벤트는 외부에서 직접 사용할 수 없다.** `public`으로 선언되어 있어도 자신이 선언된 클래스 **외부에서는 호출 불가능**하다. 반면 대리자는 `public`/`internal`이면 얼마든지 외부에서 호출 가능하다.

```csharp
delegate void EventHandler(string message);

class MyNotifier
{
    public event EventHandler SomethingHappened;

    public void OnEvent(string message)  // 외부에서 호출 가능한 관문
    {
        SomethingHappened?.Invoke(message);  // 내부에서만 발생 가능
    }
}

class MainApp
{
    static void Main()
    {
        MyNotifier notifier = new MyNotifier();
        notifier.SomethingHappened += (msg) => Console.WriteLine(msg);  // 람다식(다음 챕터)

        notifier.OnEvent("테스트");                    // OK — 내부 메서드를 통한 간접 호출
        // notifier.SomethingHappened("테스트");         // 컴파일 에러 — 직접 호출 불가
        // notifier.SomethingHappened = null;             // 컴파일 에러 — 통째로 교체 불가
    }
}
```

> 프로퍼티가 `private` 필드를 `get`/`set`으로 감싸 보호하듯, `event`는 대리자를 `+=`/`-=`로만 감싸 보호한다.

| | 보호 대상 | 보호 수단 | 외부에 허용하는 것 |
|--|----------|----------|-------------------|
| 프로퍼티 | `private` 필드 | `get`/`set` | 정해진 방식으로만 읽기/쓰기 |
| 이벤트 | 대리자 | `event` | `+=`, `-=` 로만 구독 |

**왜 필요한가:** `event`가 없으면 외부 코드가 "몬스터가 죽었다"는 사건을 조작해서, 실제로 일어나지 않은 일을 일어난 것처럼 꾸밀 수 있다. `event`는 발생 권한을 선언된 클래스 내부로만 제한해서, 진짜 그 사건이 일어났을 때만 알림이 나가도록 보장한다.

```csharp
// event 없이 그냥 대리자였다면
goblin.onDeath("가짜사망");
// 실제로는 안 죽었는데 경험치·아이템 드랍이 다 실행되어 버림

goblin.onDeath = null;
// 다른 시스템들이 등록해둔 구독을 통째로 날려버림
```

### 완성 예제 — 몬스터 사망 이벤트

도입부에서 예고했던 코드를 여기서 완성한다.

```csharp
delegate void OnDeath(string monsterName);

class Monster
{
    public event OnDeath onDeath;
    public string Name { get; set; }

    public void Die()
    {
        Console.WriteLine($"{Name} 사망!");
        onDeath?.Invoke(Name);  // 구독자 있으면 실행, 없으면 안전하게 넘어감
    }
}

class Program
{
    static void GiveExp(string name) => Console.WriteLine($"{name} 처치! 경험치 획득");
    static void UpdateQuest(string name) => Console.WriteLine($"퀘스트 갱신: {name}");
    static void DropItem(string name) => Console.WriteLine($"{name} 아이템 드랍!");

    static void Main()
    {
        Monster goblin = new Monster { Name = "고블린" };
        goblin.onDeath += GiveExp;
        goblin.onDeath += UpdateQuest;
        goblin.onDeath += DropItem;

        goblin.Die();  // 한 번의 호출로 세 가지가 모두 반응

        goblin.onDeath -= DropItem;  // 이번엔 아이템 안 드랍
        goblin.Die();
    }
}
```

> `?.Invoke()`는 이벤트를 발생시킬 때의 정석 패턴이다. 구독자가 없어 `null`이어도 예외 없이 안전하게 넘어간다.

---

3번쯤 직접 만들어보니 손에 익는다. 대리자/이벤트로 기본기는 일단락. 다음은 람다식. (LINQ, 스레드, 어트리뷰트는 궁금하지만 게임 개발이 목적이니 천천히.)
