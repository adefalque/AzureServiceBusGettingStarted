# Azure Service Bus Getting Started

A basic project to play with Azure Service Bus.

Composed of two .Net Core console applications:
- Publisher: push messages to one queue and one topic
- Subscriber: receive message from the queue and two topic subscriptions

The InitAzureResources.ps1 Powershell script takes care of creating the corresponding Azure resources if they don't exist yet.
It's necessary to login to Azure before executing the script by using the Connect-AzAccount command.
