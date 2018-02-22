# basta18-webhook-demo
WebHook-Demo der BASTA! Spring 2018 Frankfurt

## Ziel
Dies ist der Demo-Quellcode aus der Demo-Anwendung welcher auf der BASTA! Spring 2018 in Frankfurt vorgestellt wurde. 

Vortrag: https://basta.net/web-development/webhooks-fuer-eine-offene-system-kommunikation-implementieren/
Slides: 

## Solution

Die Solution besteht aus zwei .NET Core (ASP.NET Core Web API) Projekten.

### WebHookPublisher
Der WebHook Publisher bietet eine REST-API Schnittstelle an, welche vom WebHook-Subscriber genutzt werden kann. 

### WebHookSubscriber
Abonniert über eine REST-API, die vom WebHookPublisher zur Verfügung gestellte Events. 
