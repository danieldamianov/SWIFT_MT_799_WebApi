# SWIFT MT799 Web API Documentation

## Overview

The SWIFT MT799 Web API provides endpoints for retrieving and saving SWIFT MT799 messages. The API follows the OpenAPI 3.0.1 specification and is designed to interact with SWIFT MT799 messages using a RESTful approach.

## API Specification

This API uses the OpenAPI 3.0.1 specification.

## Endpoints

### 1. **Get Messages from Sender Bank**

**Endpoint:** `/api/get-messages-from-sender/{bankCode}`  
**Method:** `GET`  
**Tags:** `GetMessagesFromSpecificSenderBank`

#### Parameters

- **`bankCode`** (path parameter, required): The code of the bank from which messages are to be retrieved.
  - **Type:** `string`

#### Responses

- **200 OK**
  - **Content Types:**
    - `application/json`
    - `text/plain`
    - `text/json`
  - **Response Schema:**
    - **Type:** `array`
    - **Items:** [SWIFT_MT799_Message_Model](#swift_mt799_message_model)

#### Example Request

```http
GET /api/get-messages-from-sender/1234
```
### 2. **Save Message**

**Endpoint:** `/api/save`  
**Method:** `POST`  
**Tags:** `SaveMessage`

#### Request Body

- **Content Types:**
  - `application/json`
  - `text/plain`
  - `text/json`
- **Schema:**
  - **Type:** `string`

#### Example Request

```http
POST /api/save
Content-Type: text/plain

{
  "applicationID": "ABC",
  "serviceID": "XYZ",
  "senderBankCode": "1234",
  "senderCountryCode": "US",
  "senderLocationCode": "NY",
  "senderLogicalTerminal": "ABCD",
  "senderSessionNumber": "001",
  "senderSequenceNumber": "0001",
  "inputTime": "2024-08-03T12:00:00Z",
  "receiverBankCode": "5678",
  "receiverCountryCode": "GB",
  "receiverLocationCode": "LD",
  "receiverLogicalTerminal": "EFGH",
  "receiverBranchCode": "0001",
  "messageInputReference": "REF123",
  "messagePriority": "High",
  "transactionReferenceNumber": "TXN123456",
  "relatedReference": "REL123456",
  "narrativeText": "Sample message text",
  "messageAuthenticationCode": "MAC123",
  "checksum": "CHK123"
}
```
