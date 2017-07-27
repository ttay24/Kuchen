# Kuchen

Pub/Sub Library for Unity

## How to Use

### Simple

```C#
using UnityEngine;
using Kuchen;

public class SimpleSample : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", () => { Debug.Log("baum") });
    }
}
```
```C#
using UnityEngine;
using Kuchen;

public class SimpleButton : MonoBehaviour
{
    void OnClick()
    {
        this.Publish("SampleTopic");
    }
}
```

### With Arguments

```C#
using UnityEngine;
using Kuchen;

public class WithArgs : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", (string message, int number) => {
            Debug.LogFormat("{0}: {1}", message, number);
        });

        this.Publish("SampleTopic", "test message", 611);
    }
}
```

### With Coroutines

```C#
using System.Collections;
using UnityEngine;
using Kuchen;

public class KuchenCoroutineSample : MonoBehaviour
{
    public IEnumerator Start()
    {
        Debug.Log("SampleTopic - wait to see it");
        yield return this.WaitForMessage("SampleTopic");
        Debug.Log("SampleTopic - it was called!");
    }
}
```

### With Wildcard

```C#
using UnityEngine;
using Kuchen;

public class Wildcard : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithTopic("Topic.*", (topic) => { Debug.Log(topic); });

        this.Publish("Topic.Hoge");
        this.Publish("Topic.Fuga");
    }
}
```

### With Multiple Topics

```C#
using UnityEngine;
using Kuchen;

public class Multiple : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithTopic(new string[]{"Topic.Hoge", "Topic.Fuga"}, (topic) => { Debug.Log(topic); });

        this.Publish("Topic.Hoge");
        this.Publish("Topic.Fuga");
    }
}
```

### Subscribe.Once()

```C#
using UnityEngine;
using Kuchen;

public class SubscribeOnce : MonoBehaviour
{
    void Start()
    {
        this.Subscribe("SampleTopic", () => { Debug.Log("!"); }).Once();

        this.Publish("SampleTopic");
        this.Publish("SampleTopic"); // Will not be called second time
    }
}
```

### SubscribeAndStartCoroutine

```C#
using System.Collections;
using UnityEngine;
using Kuchen;

public class SubscribeAndStartCoroutine : MonoBehaviour
{
    void Start()
    {
        this.SubscribeAndStartCoroutine("SampleTopic", Coroutine);
        this.Publish("SampleTopic");
    }

    IEnumerator Coroutine()
    {
        yield return null;
    }
}
```

### Mute

```C#
using System.Collections;
using UnityEngine;
using Kuchen;

public class SubscribeWithCoroutine : MonoBehaviour
{
    void Start()
    {
        this.SubscribeWithCoroutine("SampleTopic", () => { Debug.Log("!"); });

        this.Publish("SampleTopic");

        this.Mute("SampleTopic");
        this.Publish("SampleTopic"); // Mute; this will not be called
        this.Unmute("SampleTopic");

        this.Publish("SampleTopic"); // No longer muted; should be called
    }
}
```

### Non-GameObject

```C#
using Kuchen;

public class NonGameObject
{
	void SubscribeTest()
	{
		using(var subscriber = new Subscriber())
		{
			subscriber.Subscribe("SampleTopic", () => { /* hoge */ });
			Publisher.Publish("SampleTopic");
		}
	}
}
```


## Special Thanks

* Y.O.
* MiniRegex(@kimika127)
