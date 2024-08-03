# SWIFT MT799 Web API Documentation

## Overview

The SWIFT MT799 Web API provides endpoints for retrieving and saving SWIFT MT799 messages.

## Endpoints

### 1. **Get Messages from Sender Bank**

**Endpoint:** `/api/get-messages-from-sender/{bankCode}`  
**Method:** `GET`

#### Parameters

- **`bankCode`** (path parameter, required): The code of the bank from which messages are to be retrieved.
  - **Type:** `string`

#### Responses

- **200 OK**
  - **Content Type:**
    - `application/json`
  - **Response Schema:**
    - **Type:** `array`
    - **Items:** [SwiftMT799 Message]

#### Example Request

```http
GET /api/get-messages-from-sender/XXXX
```
### 2. **Save Message**

**Endpoint:** `/api/save`  
**Method:** `POST`

#### Request Body

- **Content Types:**
  - `text/plain`
    
- **Schema:**
  - **Type:** `string`

#### Example Request

```
POST /api/save
Content-Type: text/plain

{1:F01PRCBBGSFAXXX1111111111}{2:O7991111111111ABGRSWACAXXX11111111111111111111N}{4:
:20:67-C111111-KNTRL 
:21:30-111-1111111
:79:NA VNIMANIETO NA: OTDEL BANKOVI GARANTSII
.
OTNOSNO: POTVARJDENIE NA AVTENTICHNOST NA
         PRIDRUJITELNO PISMO KAM ISKANE ZA
         PLASHTANE PO BANKOVA GARANCIA
.
UVAJAEMI KOLEGI,
.
UVEDOMJAVAME VI, CHE IZPRASHTAME ISKANE ZA 
PLASHTANE NA STOYNOST BGN 3.100,00, PREDSTAVENO 
OT NASHIA KLIENT.
.
S NASTOYASHTOTO POTVARZHDAVAME AVTENTICHNOSTTA NA 
PODPISITE VARHU PISMOTO NI, I CHE TEZI LICA SA 
UPALNOMOSHTENI DA PODPISVAT TAKAV DOKUMENT OT 
IMETO NA BANKATA AD.
.
POZDRAVI,
TARGOVSKO FINANSIRANE
-}{5:{MAC:00000000}{CHK:111111111111}}
```
## Database Model

The following section describes the database model for storing SWIFT MT799 messages. All fields in this model are of type `string`.

### Fields

- **`applicationID`**: The identifier for the application.
- **`serviceID`**: The identifier for the service.
- **`senderBankCode`**: The code of the sender bank.
- **`senderCountryCode`**: The country code of the sender.
- **`senderLocationCode`**: The location code of the sender.
- **`senderLogicalTerminal`**: The logical terminal of the sender.
- **`senderSessionNumber`**: The session number of the sender.
- **`senderSequenceNumber`**: The sequence number of the sender.
- **`inputTime`**: The timestamp when the message was input.
- **`receiverBankCode`**: The code of the receiver bank.
- **`receiverCountryCode`**: The country code of the receiver.
- **`receiverLocationCode`**: The location code of the receiver.
- **`receiverLogicalTerminal`**: The logical terminal of the receiver.
- **`receiverBranchCode`**: The branch code of the receiver.
- **`messageInputReference`**: The input reference for the message.
- **`messagePriority`**: The priority level of the message.
- **`transactionReferenceNumber`**: The reference number for the transaction.
- **`relatedReference`**: The reference number related to the transaction.
- **`narrativeText`**: The narrative text included in the message.
- **`messageAuthenticationCode`**: The authentication code for the message.
- **`checksum`**: The checksum for validating the message integrity.
