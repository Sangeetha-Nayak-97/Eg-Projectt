# Invoice System API

## Project Overview

The Invoice System API is a RESTful service that allows managing invoices, processing payments, and handling overdue invoices.

## Features

- **Create invoices**: POST `/invoices`
- **Get invoices**: GET `/invoices`
- **Process payments**: POST `/invoices/{id}/payments`
- **Process overdue invoices**: POST `/invoices/process-overdue`

## Assumptions

- **Late Fee Calculation**: Assumes late fees are calculated based on a daily rate provided.
- **Payment Handling**: Allows partial payments; invoices are marked as paid when the full amount is received.
- **Status Definitions**: Invoices can have statuses "pending", "paid", or "void" based on payment and overdue processing rules.

