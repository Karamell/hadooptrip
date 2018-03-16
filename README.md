# hadooptrip
Demonstrate a bug with decimal values in `Microsoft.Hadoop.Avro`

Decimal serializig in different cultures yields different results.

### Program output:
```
This program demonstrates a bug in Microsoft.Hadoop.Avro:
First serialize an object with decimal with Norwegian culture set.
Then switch to English culture and deserialize.

The result: 10.0 became 100 after a roundtrip in the serializer.
:-(
Hit any key...
```

