# 대리자와 이벤트 
이제껏 뭔가를 지시하면 컴퓨터가 그 명령을 수행하는 프로그램만 작성해왔다. 지금 부터는 사건에 반응하는 프로그램을 작성하는 방법을 배운다.

게임을 만들때에도 크게 중요하게 작용하는데 예시를 통해 왜 필요한지 알고 넘어가자 

RPG 게임에서 몬스터가 죽었을 때에도 내부적으로 많은 사건을 처리할 것이다. 경험치를 지급해야고 퀘스트 진행도를 갱신해야고 아이템도 드랍해야하고 UI도 업데이트하고...

이 모든 일들을 대리자 없이 짜면 Monster 클래스가 이 모든 일을 알아야한다.
```C#
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
이 클래스 하나에 게임의 절반이 의지하게 되었다. 만약 몬스터를 길들일 수 있는 기능이 추가되면 저 복잡한 클래스를 또 손봐야한다.

대리자를 몰랐을 때의 난 저 모든 고역을 온전히 감당했어야 할 것이다. (물론 AI가 대신 코드를 짜줬기 때문에 뭔지도 모르고 썻지만) 

이제 왜 필요한지 알았으니 본격적으로 학습해보자. 

이걸 배우고 나면 이렇게 바뀐다. Monster는 "죽었다"고 알리기만 하고, 경험치·퀘스트·사운드는 각자 알아서 반응한다. 

기능이 추가돼도 Monster 코드는 손댈 필요가 없다.
```C#
class Monster
{
    public event Action<Monster> OnDeath;  // "나 죽었어" 알림만

    void Die()
    {
        OnDeath?.Invoke(this);  // 누가 듣는지 몰라도 됨
    }
}
```
### 대리자란? 
실장님이 행정실에서 자리를 비우셨다. 그런데 누가 나에게 전화를 걸어 실장님을 찾으셨다. 이럴 때 보통은 "실장님 오시면 전화 부탁드린다고 전해주세요." 라고 하신다.

잠시 후, 실장님이 돌아오셨을 때 나는 그 말을 전달하고 실장님은 다시 전화를 걸어 통화하신다. 

이 일련의 과정에서 나에게 했던 부탁을 콜백이라고 부른다. 이 부탁(콜백)에서 중요한 건 "무엇을 할지"는 정해졌지만 "언제 할지"는 나중이라는 점이다. 대리자는 이런 "나중에 실행할 코드"를 담아두는 그릇이다.

원리를 간단히 이야기하자면 대리자는 메소드에 대한 참조이다. 즉 대리자에 메소드의 주소를 할당한 후 대리자를 호출하면 해당 주소를 통해 메소드를 호출한다는 것이다. 

```C#
한정자 delegate 반환_형식 대리자_이름(매개변수_목록);
```
delegate 라는 새로운 키워드만 빼면 메소드와 형태가 똑같다. 대리자는 메소드에 대한 참조이기 때문에 자신이 참조할 메소드의 반환 형식과 매개변수를 명시해줘야 한다. 

```C#
// 1. 대리자 선언 — "이런 모양의 메서드를 담을 그릇"
delegate int MyDelegate(int a, int b);

// 2. 담을 메서드 — 반환 형식과 매개변수가 대리자와 일치해야 함
static int Add(int a, int b) { return a + b; }

// 3. 대리자의 인스턴스를 만들 때도 new 연산자가 필요하다.
MyDelegate Callback = new MyDelegate(Add); 

//MyDelegate Callback = Add; // 이렇게도 가능

// 4. 대리자를 호출하면 담아둔 메서드가 실행됨
Console.WriteLine(Callback(3, 4));  // 7
```
호출자가 대리자를 호출하고 대리자는 또 메소드를 호출한다. 그리고 그 실행 및 실행 결과를 호출자에게 전달한다. [예제01](01_Delegate.cs)

근데 메소드 직접 호출하는게 더 빠른데요?

### 대리자는 왜, 언제 사용할까?
프로그래밍을 하다 보면 '값'이 아닌 '코드' 자체를 매개변수에 넘기고 싶을 때가 많다. 

배열을 정렬하는 메소드를 만든다고 생각해보자 오름차순으로 정렬할 것인가 아니면 내림차순? 그것도 아니면 뭔가 계산을 통해 나오는 결과순으로 정렬할 것인지 정해야한다.

이 메소드 정렬을 수행할 때 사용하는 비교 루틴을 매개변수에 넣을 수 있다면 이런 고민은 메소드를 사용하는 사용자의 몫이다. 

이럴 때 대리자가 사용된다. 대리자는 메소드에 대한 참조이므로, 비교 메소드를 참조할 대리자를 매개변수에 받을 수 있도록 정렬 메소드를 작성해두면 된다.

한번 코드를 만들어보자 
```C#
delegate int Compare(int a, int b); //먼저 Compare 대리자를 선언한다.

static int AscendCompare(int a, int b) //대리자가 참조할 비교 메소드를 작성한다.
{
    if(a>b)
        return 1;
    else if(a == b)
        return 0;
    else
        return -1;
}

//이제 정렬을 수행하는 메소드를 작성한다. 이때 매개변수로 정렬할 배열과 비교할 메소드를 참조한 대리자를 받는다.
static void BubbleSort(int[] DataSet, Compare Comparer)
{
    int i = 0;
    int j = 0;
    int temp = 0;
    for(i = 0; i<DataSet.Length-1; i++)
    {
        for(j = 0; j < DataSet.Length - (i + 1); j++)
        {
            if(Comparer(DataSet[j], DataSet[J+1]) > 0) //이게 중요하다 Comparer가 어떤 메소드를 참조하고 있는가에 따라 결과가 달라지기 때문
            {
                temp = DataSet[j+1];
                DataSet[j+1] = DataSet[j];
                DataSet[j] = temp;
            }
        }
    }
}

```
대리자가 없었다면 정렬을 수행하는 메소드를 하나 더 짯어야 했지만 대리자가 참조하는 메소드에 따라 결과 달라지는 코드를 만들 수 있어 큰 수정이 필요없다.

완성된 예제를 만들어보고 넘어가자 [예제02](02_callBack.cs)
### 일반화 대리자 
대리자는 보통의 메소드뿐 아니라 일반화 메소드도 참조할 수 있다. 대리자도 일반화 메소드를 참조할 수 있도록 형식 매개변수를 이용하여 선언되어야 한다.

```C#
delegate int Compare<T>(T a, T b);
```
마찬가지로 delegate 키워드를 제외하면 일반화 메소드를 선언하는 방법과 같다. 대리자를 매개변수로 사용하는 메소드도 형식 매개변수를 받아들이도록 변경해야 한다.
```C#
static void BubbleSort<T>(T[] DataSet, Compare<T> comparer) //형식매개 변수 추가 
{
    int i = 0;
    int j = 0;
    T temp;
    for (i = 0; i < DataSet.Length - 1; i++)
    {
        for (j = 0; j < DataSet.Length - (i + 1); j++)
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
대리자도 참조할 메소드 구현이 없으면 쓸모가 없다. 이 대리자에 담을 메소드도 형식 매개변수를 사용하도록 변경해야 한다.
```C#
static int AscendCompare<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b);
}
```
코드는 짧은데 왜 갑자기 매개변수 형식인 T가 IComparable<T>를 상속받아야만 하고 CompareTo() 메소드는 뭔가?

int, double 같은 수치 형식과 string은 모두 IComparable을 상속해서 CompareTo()를 구현하고 있기 때문이다. 

CompareTo는 매개변수가 자신보다 크면 -1을 반환하고 같으면 0을 작으면 1을 반환하는 메소드이다. 

11장에서 배웠듯이 T가 어떤 형식일지 모르기 때문에 무조건 CompareTo() 메소드를 가졌을 IComparable<T>를 상속받은 클래스만 오게 제약을 건 것이다.

이번 예제는 위 예제의 일반화를 가져오겠다. [예제03](03_GenericDelegate.cs)

### 대리자 체인
대리자에는 한가지 특징이 있는데 대리자 하나가 여러 개의 메소드를 동시에 참조할 수 있다는 것이다.  

+= 연산자를 이용하여 결합할 수 있다. 
```C#
    delegate void ThereIsAFire(string message);

    void Call119(string message)
    {
        Console.WriteLine("여기 불남 주소는 {0}",message);
    }

    void ShotOut(string message)
    {
        Console.WriteLine("불났어요! {0}에서요.", message);
    }

    void Escape(string message)
    {
        Console.WriteLine("불났어요! {0}에서요. 빨리 피하세요.", message);
    }
```
이렇게 선언한 메소드들을 ThereIsAFire 대리자의 인스턴스가 자신들을 동시에 참조할 수 있도록 += 연산자들을 이용해 결합한다.
```C#
ThereIsAFire Fire = new ThereIsAFire(Call119);
Fire += new ThereIsAFire (ShotOut);
Fire += new ThereIsAFire (Escape);
```
이렇게 
