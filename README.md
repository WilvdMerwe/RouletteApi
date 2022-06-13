# RouletteApi

## Specification

### Functions
⦁	PlaceBet
⦁	Spin
⦁	Payout
⦁	ShowPreviousSpins

### Rules

Below rules describes the multiplier that should be applied to the bet amount to calculate the resulting payout of that bet.
The user can however make a collection of bets before the the spin result establishes the payout.

#### Outside
##### 1 to 1
⦁	AnyBlack
⦁	AnyRed 
⦁	Even
⦁	Odd
⦁	OneToEighteen
⦁	NineteenToThirtySix

##### 2 to 1
⦁	FirstTwelve
⦁	SecondTwelve
⦁	ThirdTwelve
⦁	ColumnOne
⦁	ColumnTwo
⦁	ColumnThree

#### Inside
##### 5 to 1
⦁	DoubleStreet (2 Rows of 3 numbers each)

##### 6 to 1
⦁	TopLine (00,0,1,2,3)

##### 8 to 1
⦁	Corner (Between 4 numbers)

##### 11 to 1
⦁	Street (Rows of 3 numbers)
⦁	Trio (Between 3 numbers at the zeros)

##### 17 to 1
⦁	Split (Between 2 numbers)
⦁	Zero

##### 35 to 1
⦁	StraightUp (Exact number)

## Design and Implementation
A good place to start with any WebApi is user CRUD. I started with a User entity to scaffold out the layers of a simple monolith architecture, namely - Controllers->Services->EF DbContext->sqliteDb. Furthermore, FluentValidation was implemented on the requests at the beginning of the scope of the ServiceLayer methods.
Naturally, a Models namespace was added including:
⦁	Entities - POCO classes that will map via code-first approach through EntityFramework to the sqlite database file. 
⦁	DTOs - Data Transfer Objects
⦁	Requests
⦁	Responses

### Models

#### Entities

#### DTOs

##### GenericResponse
It is a good idea, from my experience, to have a generic response that can be used as a template for all responses in the system and serves as a standard contract between server-side and client-side. This also helps with unit tests as you can Assert.True(response.Success). 
In some cases you would want to include a response DTO, within the generic response, if the client needs an object as a response. In other cases you would just want to know if the request was successful with a resulting message for the Api consumer. 
If the response is successful and a response DTO is expected, it will return with the expected response DTO as the Result. 
If the response is not successful it will return the resulting error in the Message property, in most cases specifying failed validation on a bad request or in worst case scenario return the stack trace of the caught exception. If a response DTO is expected, the Result property would naturally be null in the case of an error.

##### Requests
Data transder objects coming from the api consumer. These requests are validated at the top scope of the ServiceLayer methods to validate if the request suits the business rule expectation.

##### Responses
Data transfer objects returned, in the GenericResponse Result property, to the api consumer. 

### Testing

#### Database Setup
Run the following command in your CLI:
	`dotnet tool install --global dotnet-ef`
This will install the tools to enable you to create and apply migrations. This is needed to set up the database.
To create a database instance, run the following command in your CLI:
	`dotnet ef database update`
