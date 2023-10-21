# Stock Market Analysis API

- [Stock Market Analysis API](#stock-market-analysis-api)
  - [API Gateway](#api-gateway)
  - [Authentication](#authentication)
    - [Authenticate](#authenticate)
      - [Authenticate Request](#authenticate-request)
      - [Authenticate Response](#authenticate-response)
    - [User](#user)
      - [User Request](#user-request)
      - [User Response](#user-response)
- [Stock Market Data](#stock-market-data)
  - [Get Stock Market Data](#get-stock-market-data)
    - [Get Stock Market Request](#get-stock-market-data-request)
    - [Get Stock Market Response](#get-stock-market-data-response)
  - [Accounts](#accounts)
    - [Create Account](#create-account)
      - [Create Account Request](#create-account-request)
      - [Create Account Response](#create-account-response)
    - [Get Account](#get-account)
      - [Get Account Request](#get-account-request)
      - [Get Account Response](#get-account-response)
    - [Update Account](#update-account)
      - [Update Account Request](#update-account-request)
      - [Update Account Response](#update-account-response)
    - [Delete Account](#delete-account)
      - [Delete Account Request](#delete-account-request)
      - [Delete Account Response](#delete-account-response)
  - [Settlement System](#settlement-system)
    - [Verify Account](#verify-account)
      - [Verify Account Request](#verify-account-request)
      - [Verify Account Response](#verify-account-response)
    - [Transaction Log](#transaction-log)
      - [Transaction Log Request](#transaction-log-request)
      - [Transaction Log Response](#transaction-log-response)
    - [Capital Loss Alert](#capital-loss-alert)
      - [Capital Loss Alert Request](#capital-loss-alert-request)
      - [Capital Loss Alert Response](#capital-loss-alert-response)
  - [Analyzer](#analyzer)
    - [Portfolio Summary](#portfolio-summary)
      - [Portfolio Summary Request](#portfolio-summary-request)
      - [Portfolio Summary Response](#portfolio-summary-response)

## API Gateway

The API Gateway serves as the entry point for client requests, routing them to the appropriate microservices. It is responsible for several important functions:

## Authentication

### Authenticate

#### Authenticate Request

```js
POST / api / authenticate;
```

```json
{
  "username": "user123",
  "password": "password123"
}
```

#### Authenticate Response

```js
200 OK
```

```json
{
  "token": "jwt_token"
}
```

### User

#### User Request

```js
GET /api/user/{{id}}

HEADERS `Authorization: Bearer jwt_token`
```

#### User Response

```js
200 OK
```

```json
{
  "username": "user123",
  "password": "password123"
}
```

## Stock Market Data

### Get Stock Market Data

#### Get Stock Market Data Request

```js
GET / api / stock;

HEADERS`Authorization: Bearer jwt_token`;
```

#### Get Stock Market Data Response

```json
{
  "currentValue": 100.50,
  "historicalData": [...]
}
```

Logging
Every operation is logged and archived daily at 00:01 in a file directory named with the current date.

## Accounts

### Create Account

#### Create Account Request

```js
POST / api / accounts;

HEADERS`Authorization: Bearer jwt_token`;
```

```json
{
  "username": "user123",
  "email": "user@example.com",
  "initialBalance": 1000.0,
  "currency": "USD"
}
```

#### Create Account Response

```js
201 Created
```

```json
{
  "id": 1,
  "username": "user123",
  "balance": 1000.0,
  "currency": "USD"
}
```

### Get Account

#### Get Account Request

```js
GET /api/accounts/{{id}}

HEADERS `Authorization: Bearer jwt_token`
```

#### Get Account Response

```js
200 OK
```

```json
{
  "id": 1,
  "username": "user123",
  "balance": 1000.0,
  "currency": "USD"
}
```

### Update Account

#### Update Account Request

```js
PUT  /api/accounts/{{id}}

HEADERS `Authorization: Bearer jwt_token`
```

```json
{
  "balance": 1500.0
}
```

#### Update Account Response

```js
204 No Content
```

##### Or

```js
201 Created
```

```json
{
  "id": 1,
  "username": "user123",
  "balance": 1500.0,
  "currency": "USD"
}
```

### Delete Account

#### Delete Account Request

```js
DELETE /api/accounts/{{id}}

HEADERS `Authorization: Bearer jwt_token`
```

#### Delete Account Response

```json
204 No Content
```

### Settlement System

The Settlement System is responsible for managing accounts and transactions, ensuring the integrity of financial operations. It consists of three primary responsibilities:

### Verify Account

#### Verify Account Request

```js
POST /api/settlement/verify;

or

GET /api/settlement/account/{{id}}

HEADERS `Authorization: Bearer jwt_token`
```

```json
{
  "accountId": "1"
}
```

#### Verify Account Response

```js
200 OK
```

```json
{
  "accountID": 1,
  "balance": 1500.0,
  "availableBalance": 1400.0
}
```

### Transaction Log

#### Transaction Log Request

```js
GET /api/settlement/transactions/{{accountId}}

HEADERS `Authorization: Bearer jwt_token`
```

### Transaction Log Response

```js
200 OK
```

```json
{
  "transactions": [
    {
      "id": "00000000-0000-0000-0000-000000000001",
      "type": "Purchase",
      "amount": 1000.0,
      "timestamp": "2023-10-19T12:00:00"
    }
  ]
}
```

### Capital Loss Alert

#### Capital Loss Alert Request

```js
GET /api/settlement/alert/{{accountId}}

HEADERS `Authorization: Bearer jwt_token`
```

#### Capital Loss Alert Response

```js
200 OK
```

```json
{
  "message": "Capital loss alert: Your portfolio has lost more than 15% of its value."
}
```

### Analyzer

The Analyzer provides a summarized view of specific portfolios, offering insights such as:

### Portfolio Summary

#### Portfolio Summary Request

```js
GET /api/analyzer/portfolio/{{portfolioId}}

HEADERS `Authorization: Bearer jwt_token`
```

#### Portfolio Summary Response

```js
200 OK
```

```json
{
  "currentYield": 5.2,
  "percentageChange": -2.1,
  "portfolioRisk": "Medium",
  "dailyChanges": [
    {
      "date": "2023-10-19",
      "change": 1.2
    }
  ]
}
```
