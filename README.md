
# Masiv Roulette

This is a Web API implemented with .NET Core, RedisLab DB and Docker Containers.

## There are 5 simple endpoints: 

### 1) API to create a new Roulette.
    * Endpoint: /CreateRoulette
    * Method: POST
    * Response: 
        {createdRouletteId}

### 2) API to verify if the Roulette is Open.
    * Endpoint: /IsRouletteOpenById/{id}
    * Method: GET
    * Response: 
        StatusCode: 200 or 204

### 3) API to bet on a random open Roulette.
    * Endpoint: /RouletteBetting
    * Method: POST
    * Header: 
        * Authorization: Basic am9obm5hdGFuREVWOnBhc3N3b3Jk
        * userID: 45317153-6fc7-431a-91a9-11f6da3e6a96
        * Content-Type: application/json
    * Body: 
        *  {
              "bettingAmount": 1000,
              "betIsOnNumber": true,
              "bettingNumber": 4,
              "bettingColor": "red"
           }
    * Response:
        StatusCode: 200 or 204

### 4) API that gets All bets of a Closing Roulette.
    * Endpoint: /ClosingRoulette/{id}
    * Method: GET
    * Response: 
        {ListOfBets}

### 5) API that gets All Open and Close Roulettes.
    * Endpoint: /GetAllRoulettes
    * Method: GET
    * Response: 
        {ListOfRoulettes}


## Web API Postman Link Collection: 
 
[a link](https://www.getpostman.com/collections/c5d09868f948a69cfc70)

## Web API Postman .json Export

[a link](https://github.com/JohnnyCoRuyzo/Masiv-Roulette/blob/master/PostmanCollection/Masiv-Roulette%20WEB%20API.postman_collection.json)



