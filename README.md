![dynamoDB generic mapper](https://github.com/andreasPartenhauser/dynamoDB-generic-mapper/blob/develop/dynamoDB_generic_mapper.png)

[![Twitter](https://img.shields.io/badge/twitter-@loretoGames-blue.svg?style=flat)](https://twitter.com/loretoGames)

DynamoDB-Generic-Mapper is a Basic architectural aproach to do CRUD operations on DynamoDB and map returned data to models written in c#.

- [Features](#features)
- [Requirements](#requirements)
- [Communication](#communication)
- [Installation](#installation)
- [Usage](#usage)
- [FAQ](#faq)
- [Licence](#Licence)

## Features

- [x] Mapping responses to objects with use of generics and reflection APIs
- [x] Mapping nested responses to complex objects
- [x] Example Architecture for DynamoDB Clients
- [x] Example for creating a Table, inserting (put) items and reading items from DynamoDB
- [ ] Mapping objects to Dictionary<string, AttributeValue>
- [ ] Full CRUD example for DynamoDB Access
- [ ] Handle Edge cases in MapModel (List<PrimitveType>)
- [ ] Error Handling

## Requirements

- .Net 4.5 (recommended)
- Unity 4.6 or 5 and higher
- AWS Account with [Cognito Identity Pool](https://aws.amazon.com/de/cognito/getting-started/) setup (for unregistered Access)


## Communication
- If you need help with the setup of your AWS Stack, check [my post on medium](https://medium.com/@andreas.partenhauser/serverless-mobile-game-backend-with-aws-in-unity-f61f2cb508d4) or [Amazon AWS Documentation](https://docs.aws.amazon.com/de_de/amazondynamodb/latest/developerguide/Introduction.html)
- If you need **help with an DynameDB Generic Mapper**, contact me on Twitter [Twitter](https://twitter.com/loretoGames).
- If you **found a bug**, open an issue here on GitHub. The more detail the better!
- If you **want to contribute**, submit a pull request.

## Installation

This is a runable Unity Project with AWS DynamoDB Mobile SDK already included. Checkout and open the root Folder with your Unity Editor.
Before you can test the example, you need to setup your AWS Cognito Identity Pool or App Sync(Adjustment might be needed) to allow unregistered Access. (Make sure the needed Policy for DynamoDB is attached).
In `AWSBaseClient` you need to insert your own `IdentityPoolId`. If this is configured you can run the `DynamoDBMapperDemo` Scene and you will see in the console if a table was created successfully. If so, you can Add simple and complex Players and Load them again. You will see the results in the console.
Use Debugger for Unity with Visual Studio Code to get a deeper look into what happens.

### Import the Unity Package

There are actually two unitypackage files.
- dynamoDB-generic-mapper-Base
- dynamoDB-generic-mapper-Full
Use the Base package if you just want to have the plain code to get started with DynamoDB. There no AWS code, loading or examples are included. So you also need to import the AWS unitypackages by yourself. Also the in the following as optional described Service layer is not part of this, since this layer would need to know your domain objects already.
Use the Full package if you want to have an (almost) running example. The only thing that is needed - as described above - is to setup the `IdentityPoolId` of your pool. And mybe change the region, if you are not on eu1 (Frankfurt)

## Usage

To extend the AWSBaseClient and insert your own entities you need to do the following:
- Create a new c# script. (Should be called something like *Client). Let it inherit from AWSBaseClient.
- In `YourName*Client` override the abstract method `TableName` and return the name of Your Table
- Create a new c# script. (Should be called something like *Model). And let it inherit from `Model``
- Add all properties you need to your model. Follow the same pattern as in `PlayerModel`. (The Mapper currently only reflects public properties with getter and setter specified)
- In `Loader` class add the following line: `gameObject.AddComponent<*YourName*Client>()`
You can call any methods implemented in the Unity Lifecycle or add Buttons to the UI as in the example or just replace the methods in the existing example.
**Optional**
- Add an interface `I*YourNameClient` and add all methods you want to implement
- Add a new c# script (Should be called something like *Service). Let it conform to `I*YourNameClient`.
- Add a property client of type `I*YourNameClient` and just forward all methods to the client.
- Add a default constructor and add the following line `Camera.main.GetComponent<YourName*Client>()`
- You can construct the service anywhere in your project and make calls to the methods implemented.

[see the Medium article to understand the Service Architecture](**Insert Medium Link**)

## FAQ

### Are there more examples how to use DynamoDB?

Download the [AWS Mobile SDK](http://sdk-for-net.amazonwebservices.com/latest/aws-sdk-unity.zip) and include the dynamoDB.unitypackage to an empty Unity Project. There are several examples included as well as a Readme.
I will add more FAQ here, soon.

## Licence

DynamoDB Generic Mapper is provided under [MIT Licence](https://github.com/andreasPartenhauser/dynamoDB-generic-mapper/blob/develop/LICENSE)
