# 또다시 마침내 프로퍼티 
솔직히 다른 사람 코드 참고하거나 ai가 짜주는 코드볼떄마다. 한번식 나오는 get; set; 이게 뭔지 정말 궁금했다.

일단 클래스의 필드를 private를 선언하는게 좋다고 했다. 데이터가 오염될 수 있기 때문이다. 하지만 그렇게 하면 솔직히 조금 귀찮다.

private는 그냥 접근할 수 없고 메소드를 추가해서 필드에 접근할 수 있게 하고 인스턴스를 할당하고 메소드를 호출해서 값을 쓰거나 가져올거다.

~~~
private int age;

public void SetAge(int value) {
    if (value < 0) age = 0; // 데이터 보호 가능
    else age = value;
}

public int GetAge() {
    return age;
}

// 사용 시: obj.SetAge(20); / Console.WriteLine(obj.GetAge());
~~~

이렇게 말이다. 전혀 문제 있는 코드가 아니다. 은닉성을 지키며 필드를 읽고 쓰고 있다. 만약 Java를 쓴다면 이게 정석이다.

하지만 c#에는 프로퍼티라는게 존재한다.

### 메소드보다 프로퍼티 
일단 선언하는 법부터 보자 
``` c#
class 클래스_이름
{
