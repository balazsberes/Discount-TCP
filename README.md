# Discount-TCP

## System description
The system is designed to generate and use DISCOUNT codes. The system consists of a server and client side.

## Features
• DISCOUNT code generation.

• DISCOUNT code usage.

• Communication between server and client.

• DISCOUNT codes remain between service restarts, stored in a file.

• Generation could be repeated as many times as desired.

• A maximum of 2 thousand DISCOUNT codes can be generated with a single request.

• The system is capable of processing multiple requests in parallel.

## Rules for the DISCOUNT code
• The length of the DISCOUNT code is 7-8 characters during generation.

• DISCOUNT code must be generated randomly and cannot be repeated.

• Characters for the code: "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

## Protocol
• TCP sockets
