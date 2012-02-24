module SampleDbConnect

#if INTERACTIVE
#I @"C:\Tools\MongoDB-CSharp\MongoDB.Linq\bin\Debug"
#r "MongoDB.Driver.dll"
#r "MongoDB.Linq.dll"
#r "FSharp.PowerPack.Linq.dll"
#r "System.Core.dll"
#endif

open System
open FluentMongo
open MongoDB.Driver
open MongoDB.Bson

//open System.Linq

let server = MongoServer.Create("mongodb://bibliopedia.org")
let databaseNames = server.GetDatabaseNames();  
let database (dbName:string) = server.GetDatabase(dbName)
let isCollectionCreated (name:string) (db:MongoDatabase) = db.CollectionExists(name)
let createCollection (name:string) (db:MongoDatabase) = db.CreateCollection(name)

let rec safeCreateCollection (name:string) (db:MongoDatabase) =
    if (db |> isCollectionCreated name) then Some (db.GetCollection name)
    else 
        let result = db |> createCollection name
        db |> safeCreateCollection name 

let addItem (json:string) (collection:MongoCollection) =
    let item = BsonDocument.Parse json
    collection.Insert(item);

//
//let mongo = new FluentMongo.Linq.Util. Mongo()
//let connected = mongo.Connect()
//
//let db = mongo.["movieDB"]
//db.["movies"].Insert((new Document()).Append("title", "Star Wars").Append("releaseDate", DateTime.Now))
//
//let movies = db.["movies"].AsQueryable() :> IQueryable<Document>
//
//let titles = 
//  query <@ seq { for movie in movies do
//                   yield movie } @>
//                   
//let disconnected = mongo.Disconnect()