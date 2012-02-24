module MongoConnect.Tests

open NUnit.Framework
open FsUnit

[<TestFixture>] 
type ``MongoDB: Connecting `` ()= 
    let dbName = "local"
    let dbNames = SampleDbConnect.databaseNames 

    [<Test>] member test.    
     ``Can inspect database`` ()=
        SampleDbConnect.safeCreateCollection "ParsCit" |> should equal true

    [<Test>] member test.    
     ``Can connect`` ()=
        dbNames |> should contain "local"
